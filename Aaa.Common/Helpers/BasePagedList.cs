﻿// <copyright file="BasePagedList.cs" company="Computer Technology Solutions, Inc.">
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
	/// <seealso cref="List{T}"/>
	public abstract class BasePagedList<T> : List<T>, IPagedList<T>
	{
		/// <summary>
		/// Initializes a new instance of a type deriving from <see cref="BasePagedList{T}"/> and sets properties needed to calculate position and size data on the subset and superset.
		/// </summary>
		/// <param name="index">The index of the subset of objects contained by this instance.</param>
		/// <param name="pageSize">The maximum size of any individual subset.</param>
		/// <param name="totalItemCount">The size of the superset.</param>
		internal protected BasePagedList(int index, int pageSize, int totalItemCount)
		{
			// set source to blank list if superset is null to prevent exceptions
			TotalItemCount = totalItemCount;
			PageSize = pageSize;
			PageIndex = index;
			if (TotalItemCount > 0)
				PageCount = (int) Math.Ceiling(TotalItemCount/(double) PageSize);
			else
				PageCount = 0;

			if (index < 0)
				throw new ArgumentOutOfRangeException("index", index, "PageIndex cannot be below 0.");
			if (pageSize < 1)
				throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");
		}

		#region IPagedList<T> Members

		/// <summary>
		/// Total number of subsets within the superset.
		/// </summary>
		/// <value>
		/// Total number of subsets within the superset.
		/// </value>
		public int PageCount { get; protected set; }

		/// <summary>
		/// Total number of objects contained within the superset.
		/// </summary>
		/// <value>
		/// Total number of objects contained within the superset.
		/// </value>
		public int TotalItemCount { get; protected set; }

		/// <summary>
		/// Zero-based index of this subset within the superset.
		/// </summary>
		/// <value>
		/// Zero-based index of this subset within the superset.
		/// </value>
		public int PageIndex { get; protected set; }

		/// <summary>
		/// One-based index of this subset within the superset.
		/// </summary>
		/// <value>
		/// One-based index of this subset within the superset.
		/// </value>
		public int PageNumber
		{
			get { return PageIndex + 1; }
		}

		/// <summary>
		/// Maximum size any individual subset.
		/// </summary>
		/// <value>
		/// Maximum size any individual subset.
		/// </value>
		public int PageSize { get; protected set; }

		/// <summary>
		/// Returns true if this is NOT the first subset within the superset.
		/// </summary>
		/// <value>
		/// Returns true if this is NOT the first subset within the superset.
		/// </value>
		public bool HasPreviousPage
		{
			get { return PageIndex > 0; }
		}

		/// <summary>
		/// Returns true if this is NOT the last subset within the superset.
		/// </summary>
		/// <value>
		/// Returns true if this is NOT the last subset within the superset.
		/// </value>
		public bool HasNextPage
		{
			get { return PageIndex < (PageCount - 1); }
		}

		/// <summary>
		/// Returns true if this is the first subset within the superset.
		/// </summary>
		/// <value>
		/// Returns true if this is the first subset within the superset.
		/// </value>
		public bool IsFirstPage
		{
			get { return PageIndex <= 0; }
		}

		/// <summary>
		/// Returns true if this is the last subset within the superset.
		/// </summary>
		/// <value>
		/// Returns true if this is the last subset within the superset.
		/// </value>
		public bool IsLastPage
		{
			get { return PageIndex >= (PageCount - 1); }
		}

		#endregion
	}
}