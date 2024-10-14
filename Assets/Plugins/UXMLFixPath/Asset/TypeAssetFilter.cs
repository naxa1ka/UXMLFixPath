﻿using System;

namespace Nxlk.UXMLFixPath
{
    public class TypeAssetFilter : IAssetFilter
    {
        private readonly Type _type;

        public TypeAssetFilter(Type type)
        {
            _type = type;
        }

        public string AsString() => $"t:{_type}";
    }
}