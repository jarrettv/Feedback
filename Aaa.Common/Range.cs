// <copyright file="Range.cs" company="Computer Technology Solutions, Inc.">
// Copyright (c) 2011 Computer Technology Solutions, Inc.  ALL RIGHTS RESERVED
// </copyright>
// <author>jvance</author>
// <date>2/19/2011 1:07:40 PM</date>
// <productName></productName>
namespace Aaa.Common
{
    using System;

    /// <summary>
    /// A generic range of two values of the same type.
    /// </summary>
    public class Range<T> where T : IComparable<T>
    {
        public readonly T Start;
        public readonly T End;

        public Range(T start, T end)
        {
            this.Start = start;
            this.End = end;
        }

        public bool Overlaps(Range<T> range)
        {
            return Range.Overlap(this, range);
        }
    }

    static class Range
    {
        /// <summary>
        /// Determines if the range overlaps assuming that each range start value is before the end value.
        /// The comparision is not inclusive.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static bool Overlap<T>(Range<T> left, Range<T> right)
            where T : IComparable<T>
        {
            // (StartA <= EndB) And (EndA >= StartB)
            if (left.Start.CompareTo(right.End) < 0 && left.End.CompareTo(right.Start) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
