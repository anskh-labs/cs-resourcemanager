using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ResourceManager.Helpers
{
    internal static class FilterManager
    {
        public static Filter<T> Create<T>() where T: class
        {
            return new Filter<T>();
        }
        
    }
}
