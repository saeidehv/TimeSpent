using System;
using System.Collections.Generic;
using System.Linq;

namespace TimeSpent.Core
{
    [AttributeUsage(AttributeTargets.Property)]
    public class NotNavigableAttribute : Attribute
    {
    }
}
