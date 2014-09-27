//  --------------------------------
//  <copyright file="IMapFrom.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>09/26/2014</date>
//  ---------------------------------
namespace AutoMapperFramework
{
    using System.Diagnostics.CodeAnalysis;

    [SuppressMessage("Microsoft.Design", "CA1040:AvoidEmptyInterfaces", Justification = "Marker Interface")]
    public interface IMapFrom<T>
    {
    }
}