﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeSpent.Core.Extensions
{
    public static class DataExtensions
    {
        public static IEnumerable<T> ToFullyLoaded<T>(this IQueryable<T> query)
        {
            return query.ToArray().ToList();
        }
    }
}
