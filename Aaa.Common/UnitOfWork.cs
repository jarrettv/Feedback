// <copyright file="IUnitOfWorkWeb.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2011 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/3/2011 4:21:45 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Data.Entity;
    using Autofac;

    public abstract class UnitOfWork<T> : IDisposable where T : DbContext
    {
        protected readonly T db;

        public UnitOfWork()
        {
            // get from container so it is part of lifetime scope shared by all other
            // repositories
            db = Container.Get().Resolve<T>();
        }

        public abstract void Dispose();
    }

}
