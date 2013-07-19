// <copyright file="UnitOfWorkWeb.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2011 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/3/2011 4:21:45 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Runtime.InteropServices;
    using System.Transactions;

    public class UnitOfWorkWeb<T> : UnitOfWork<T> where T : DbContext
    {
        private readonly TransactionScope scope;

        public UnitOfWorkWeb()
            : base()
        {
            scope = new TransactionScope();
        }

        public override void Dispose()
        {
            try
            {
                // if an exception occurred, don't try and save, etc.
                if (Marshal.GetExceptionCode() == 0)
                {
                    // catch all known exceptions that we can handle and convert to rules exception
                    db.SaveChanges();
                    this.scope.Complete();
                }
            }
            catch (DbEntityValidationException val)
            {
                throw val.ToRulesException();
            }
            catch (OptimisticConcurrencyException) // in case our manual check didn't work? why?
            {
                throw new RulesException(string.Empty,
                    @"The record has changed. Please cancel and try again");
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new RulesException(string.Empty,
                    @"The record has changed. Please cancel and try again");
            }
            finally
            {
                this.scope.Dispose();
                DomainEvents.ClearCallbacks();
            }
        }
    }
}
