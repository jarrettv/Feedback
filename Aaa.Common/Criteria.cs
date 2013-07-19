// <copyright file="Criteria.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2010 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/22/2010 9:30:05 AM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Criteria class
    /// </summary>
    public abstract class Criteria
    {
        public Criteria()
        {
            this.ShowOptions = new List<string>();
        }

        public Criteria(string orderColumn)
            : this()
        {
            this.OrderColumn = orderColumn;
        }

        public string SiteCodes { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public bool PageSizeFixed { get; set; }
        public bool HidePageSizeSelections { get; set; }
        public bool HideFilter { get; set; }
        public bool HideActions { get; set; }

        public string OrderColumn { get; set; }
        public bool? OrderDescending { get; set; }
        public virtual bool DefaultOrderDescending { get; set; }

        public IList<string> ShowOptions { get; set; }
        public string Show { get; set; }

        public bool ShowPin { get; set; }
        public string PinUrl { get; set; }

        public bool ShowExport { get; set; }
        public string ExcelExportUrl { get; set; }

        public bool Desc { get { return OrderDescending ?? DefaultOrderDescending; } }

        public IDictionary<string, bool> GetShowOptionsSelectList()
        {
            var dict = new Dictionary<string, bool>();
            foreach (var o in this.ShowOptions) dict.Add(o, o == this.Show);
            return dict;
        }

        /// <summary>
        /// Gets a querystring representing the criteria
        /// </summary>
        public override string ToString()
        {
            return this.ToString(null);
        }

        public string ToStringOrderedBy(string orderColumn)
        {
            return this.ToString((v) =>
            {
                v["orderColumn"] = orderColumn;

                if (v.Keys.Contains("orderDescending")) v.Remove("orderDescending");

                // Toggle OrderDescending if re-sorting on the same column, otherwise default to Ascending
                if (string.Equals(this.OrderColumn, orderColumn, StringComparison.InvariantCultureIgnoreCase))
                {
                    if (this.Desc == DefaultOrderDescending)
                        v.Add("orderDescending", (!this.Desc).ToString());
                }
            });
        }

        public string ToStringAtPage(int pageIndex)
        {
            return this.ToString(v => v["pageIndex"] = pageIndex.ToString());
        }

        public string ToStringForWidget()
        {
            return this.ToString(v =>
            {
                v["pageIndex"] = "0";
                v["showPin"] = "false";
                v["showExport"] = "false";
                v["pageSize"] = "5";
                v["hideFilter"] = "true";
                v["hidePageSizeSelections"] = "true";
                v["hideActions"] = "true";
            });
        }

        /// <summary>
        /// Updates the provided dictionary with values from this Criteria's properties
        /// </summary>
        /// <param name="values">Dictionary updated with values from this Criteria's properties</param>
        protected virtual void SetValues(IDictionary<string, string> values)
        {
            if (this.SiteCodes != null) values.Add("siteCodes", this.SiteCodes);
            if (this.PageIndex > 0) values.Add("pageIndex", this.PageIndex.ToString());
            //if (this.PageSize != Criteria.DefaultPageSize)
            values.Add("pageSize", this.PageSize.ToString());
            if (!string.IsNullOrWhiteSpace(this.OrderColumn)) values.Add("orderColumn", this.OrderColumn);
            if (this.OrderDescending.HasValue) values.Add("orderDescending", this.OrderDescending.Value.ToString());
            if (!string.IsNullOrWhiteSpace(this.Show)) values.Add("show", this.Show);
            if (this.HidePageSizeSelections) values.Add("hidePageSizeSelections", "true");
            if (this.HideActions) values.Add("hideActions", "true");
            if (this.ShowPin) values.Add("showPin", "true");
            if (this.ShowExport) values.Add("showExport", "true");
        }

        /// <summary>
        /// Allows caller to fill a dictionary containing key-value pairs; Converts KVPs to query-string format.
        /// </summary>
        /// <param name="alterValues">Action to update the dictionary of values contributing to the query-string</param>
        /// <returns>Query-string representation of this Criteria object</returns>
        protected string ToString(Action<IDictionary<string, string>> alterValues)
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            this.SetValues(values);

            // allow caller to update values
            if (alterValues != null) alterValues(values);

            return string.Join("&", values.Select(kvp => kvp.Key + "=" + HttpUtility.UrlEncode(kvp.Value)));
        }
    }
}
