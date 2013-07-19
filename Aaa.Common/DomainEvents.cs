// <copyright file="DomainEvents.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2011 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>8/27/2011 10:41:30 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;
    using Autofac;
    using Aaa.Common;
    using System.Data.Entity;

    public interface IDomainEvent { }

    public interface IEventHandler<T> where T : IDomainEvent
    {
        void Handle(T args);
    }

    /// <summary>
    /// http://www.udidahan.com/2009/06/14/domain-events-salvation/
    /// </summary>
    //[DebuggerStepThrough]
    public static class DomainEvents
    {
        [ThreadStatic] //so that each thread has its own callbacks
        private static List<Delegate> actions;

        /// <summary>
        /// FOR TEST ONLY: Registers a callback for the given domain event
        /// </summary> 
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actions == null)
                actions = new List<Delegate>();

            actions.Add(callback);
        }

        /// <summary>
        /// Clears callbacks passed to Register on the current thread
        /// </summary>
        public static void ClearCallbacks()
        {
            actions = null;
        }

        /// <summary>
        /// Raises the given domain event
        /// </summary>
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            var container = Container.Get();
            if (container != null)
            {
                var handlers = (IEnumerable<IEventHandler<T>>)container.Resolve(typeof(IEnumerable<IEventHandler<T>>));
                foreach (var handler in handlers)
                {
                    try
                    {
                        // force save changes so we can get the identity
                        // needed by event handlers
                        SaveChangesWhenAdded(container);

                        handler.Handle(args);
                    }
                    catch (DbEntityValidationException val)
                    {
                        throw val.ToRulesException();
                    }
                    catch (OptimisticConcurrencyException)
                    {
                        throw new RulesException(string.Empty,
                            @"The record has changed. Please cancel and try again");
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        throw new RulesException(string.Empty,
                            @"The record has changed. Please cancel and try again");
                    }
                }
            }

            // this is only here to support unit testing and the manual registration above
            if (actions != null)
                foreach (var action in actions)
                    if (action is Action<T>)
                        ((Action<T>)action)(args);
        }

        private static void SaveChangesWhenAdded(ILifetimeScope container)
        {
            // get db context (any in scope), check change tracker for added entities
            // call save if anything added
            var dbs = container.Resolve<IEnumerable<DbContext>>();
            foreach (var db in dbs)
            {
                if (db.ChangeTracker.Entries()                    
                    .Any(x => x.State == EntityState.Added))
                {
                    db.SaveChanges();
                }
            }
        }
    }
}
