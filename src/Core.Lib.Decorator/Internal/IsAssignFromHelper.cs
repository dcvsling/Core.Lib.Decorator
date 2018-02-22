using System.Diagnostics;
using System.Linq;
using System;
using System.Collections.Generic;
namespace Core.Lib.Decorator
{

    public static class IsAssignFromHelper
    {
        public static bool IsAssignFrom(this Type @base, Type input)
            => (@base, input).IsAllOpenGenericType();

        private static bool IsAllOpenGenericType(this (Type @base, Type input) types)
            => types.@base.IsGenericTypeDefinition && types.input.IsGenericTypeDefinition
                ? types.IsImplement()
                : types.IsOpenGenericTypeExist();

        private static bool IsOpenGenericTypeExist(this (Type @base, Type input) types)
            => types.@base.IsGenericTypeDefinition || types.input.IsGenericTypeDefinition
                ? types.IsImplement()
                : types.@base.IsAssignableFrom(types.input);
     
        private static bool IsImplement(this (Type @base, Type input) types)
            => types.input.GetAll(x => x.GetInterfaces()).Any(x => x.IsSameOf(types.@base))
                || (types.@base.IsSameOf(types.input));
                
        private static bool IsSameOf(this Type @base, Type input)
            => @base.MetadataToken == input.MetadataToken;
    }
}
