//  --------------------------------
//  <copyright file="IHaveCustomMappings.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>05/22/2014</date>
//  ---------------------------------
namespace AutoMapperFramework
{
    using AutoMapper;

    public interface IHaveCustomMappings
    {
        void CreateMappings(IProfileExpression configuration);
    }
}