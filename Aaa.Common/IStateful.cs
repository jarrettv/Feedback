// <copyright file="IAuditable.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2010 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/11/2010 6:49:04 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System.Linq;

    /// <summary>
    /// Defines the standard stateful properties for a record
    /// </summary>
    public interface IStateful
    {
        /// <summary>
        /// Comma separated list of actions that are available when in the current state
        /// </summary>
        string Triggers { get; }
    }

    public static class StatefulExtensions
    {
        public static bool HasAction(this IStateful stateful, params string[] action)
        {
            return (stateful.Triggers ?? string.Empty)
                .Split(',')
                .Intersect(action)
                .Any();
        }
    }
}
