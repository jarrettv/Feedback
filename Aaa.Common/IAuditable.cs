// <copyright file="IAuditable.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2010 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/11/2010 6:49:04 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;

    /// <summary>
    /// Defines the standard audit properties for a record
    /// </summary>
    public interface IAuditable
    {
        string CreatedBy { get; set; }
        DateTimeOffset CreatedDate { get; set; }
        string UpdatedBy { get; set; }
        DateTimeOffset UpdatedDate { get; set; }
    }

    /// <summary>
    /// Defines the standard audit properties should be automatically handled by persistence
    /// </summary>
    public interface IAutoAuditable : IAuditable { }

    public static class Auditable
    {
        public const int ByLength = Lengths.Name;
    }
}
