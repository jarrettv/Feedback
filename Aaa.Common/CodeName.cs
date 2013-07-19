// <copyright file="CodeName.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2010 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>9/11/2010 6:49:04 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    /// <summary>
    /// An object that is identified by a code and has a name. Test.
    /// </summary>
    public class CodeName : IComparable<CodeName>
    {
        [Key, Required, StringLength(Lengths.Code), Column(Order = 0)]
        [RegularExpression("^[\\w\\d_-]*$", ErrorMessage = "The code must contain letters, numbers, dashes, and underscores only, with no spaces.")]
        public string Code { get; set; }

        [Required, StringLength(Lengths.LongName), Column(Order = 1)]
        public string Name { get; set; }

        [Column(Order = 99)]
        public bool Inactive { get; set; }

        /// <summary>
        /// Overrides ToString()
        /// </summary>
        /// <returns>String representing object</returns>
        public override string ToString()
        {
            return this.Name;
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"/> is equal to this instance. Override object.Equals
        /// </summary>
        /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
        /// <returns>
        /// <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="T:System.NullReferenceException">
        /// The <paramref name="obj"/> parameter is null.
        /// </exception>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return this.Code == ((CodeName)obj).Code;
        }

        /// <summary>
        /// Returns a hash code for this instance. Override object.GetHashCode
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return this.Code.GetHashCode();
        }

        /// <summary>
        /// Compares identities of companies 
        /// </summary>
        /// <param name="other">Company to compare with this company</param>
        /// <returns>Boolean indicating whether the companies are equal</returns>
        public int CompareTo(CodeName other)
        {
            return this.Code.CompareTo(other.Code);
        }

        //public virtual void Update(bool isNew, string code, string name)
        //{
        //    if (isNew)
        //        this.Code = code.TrimOrEmpty().ToUpper();
        //    this.Name = name.TrimOrEmpty();
        //}

        //public virtual void ToggleInactive()
        //{
        //    this.Inactive = !this.Inactive;
        //}
    }
}
