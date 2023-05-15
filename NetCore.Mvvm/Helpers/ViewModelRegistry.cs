using System;
using System.Collections.Generic;

namespace NetCore.Mvvm.Helpers
{
    public class ViewModelRegistry
    {
        public Dictionary<Type, Type> ViewModelTypeToViewType { get; } = new();
    }
}
