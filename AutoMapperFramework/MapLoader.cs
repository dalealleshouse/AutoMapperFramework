//  --------------------------------
//  <copyright file="MapLoader.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>05/22/2014</date>
//  ---------------------------------
namespace AutoMapperFramework
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    public class MapLoader
    {
        private readonly IProfileExpression _mapperConfiguration;

        public MapLoader(IProfileExpression mapperConfiguration)
        {
            this._mapperConfiguration = mapperConfiguration;
        }

        public void LoadMappings(IEnumerable<Type> types)
        {
            var safeTypes = types as Type[] ?? types.ToArray();

            this.LoadToMappings(safeTypes);
            this.LoadFromMappings(safeTypes);
            this.LoadCustomMappings(safeTypes);
        }

        private static IEnumerable<dynamic> GetMapsForType(IEnumerable<Type> types, Type target)
        {
            return (from t in types
                    from i in t.GetInterfaces()
                    where i.IsGenericType && i.GetGenericTypeDefinition() == target && !t.IsAbstract && !t.IsInterface
                    select new { ClassType = t, TypeFromInterface = i.GetGenericArguments()[0] }).ToArray();
        }

        private void LoadToMappings(IEnumerable<Type> types)
        {
            var maps = GetMapsForType(types, typeof(IMapTo<>));

            foreach (var map in maps)
            {
                this._mapperConfiguration.CreateMap(map.ClassType, map.TypeFromInterface);
            }
        }

        private void LoadFromMappings(IEnumerable<Type> types)
        {
            var maps = GetMapsForType(types, typeof(IMapFrom<>));

            foreach (var map in maps)
            {
                this._mapperConfiguration.CreateMap(map.TypeFromInterface, map.ClassType);
            }
        }

        private void LoadCustomMappings(IEnumerable<Type> types)
        {
            var maps = (from t in types
                        from i in t.GetInterfaces()
                        where typeof(IHaveCustomMappings).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface
                        select (IHaveCustomMappings)Activator.CreateInstance(t)).ToArray();

            foreach (var map in maps)
            {
                map.CreateMappings(this._mapperConfiguration);
            }
        }
    }
}