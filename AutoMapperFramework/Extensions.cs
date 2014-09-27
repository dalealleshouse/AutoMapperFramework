//  --------------------------------
//  <copyright file="Extensions.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>05/22/2014</date>
//  ---------------------------------
namespace AutoMapperFramework
{
    using System;
    using System.Reflection;

    using AutoMapper;

    public static class Extensions
    {
        // Extension to ignore any properties that do not exist in the output type
        public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            const BindingFlags Flags = BindingFlags.Public | BindingFlags.Instance;
            var sourceType = typeof(TSource);
            var destinationProperties = typeof(TDestination).GetProperties(Flags);

            foreach (var property in destinationProperties)
            {
                if (sourceType.GetProperty(property.Name, Flags) == null)
                {
                    expression.ForMember(property.Name, opt => opt.Ignore());
                }
            }

            return expression;
        }
    }
}