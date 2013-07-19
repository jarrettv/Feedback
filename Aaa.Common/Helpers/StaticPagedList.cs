// <copyright file="BasePagedList.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2010 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/11/2010 6:49:04 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;

	/// <summary>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
	/// </summary>
	/// <remarks>
	/// Represents a subset of a collection of objects that can be individually accessed by index and containing metadata about the superset collection of objects this subset was created from.
	/// </remarks>
	/// <typeparam name="T">The type of object the collection should contain.</typeparam>
	/// <seealso cref="IPagedList{T}"/>
	/// <seealso cref="BasePagedList{T}"/>
	/// <seealso cref="PagedList{T}"/>
	/// <seealso cref="List{T}"/>
	public class StaticPagedList<T> : BasePagedList<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StaticPagedList{T}"/> class that contains the already divided subset and information about the size of the superset and the subset's position within it.
		/// </summary>
		/// <param name="subset">The single subset this collection should represent.</param>
		/// <param name="index">The index of the subset of objects contained by this instance.</param>
		/// <param name="pageSize">The maximum size of any individual subset.</param>
		/// <param name="totalItemCount">The size of the superset.</param>
		/// <exception cref="ArgumentOutOfRangeException">The specified index cannot be less than zero.</exception>
		/// <exception cref="ArgumentOutOfRangeException">The specified page size cannot be less than one.</exception>
		public StaticPagedList(IEnumerable<T> subset, int index, int pageSize, int totalItemCount)
			: base(index, pageSize, totalItemCount)
		{
			AddRange(subset);
		}
	}
}