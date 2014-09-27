//  --------------------------------
//  <copyright file="MapLoaderShould.cs">
//      Copyright (c) 2014 All rights reserved.
//  </copyright>
//  <author>Alleshouse, Dale</author>
//  <date>06/08/2014</date>
//  ---------------------------------
namespace AutoMapperFramework.Tests
{
    using System;
    using System.Collections.Generic;

    using AutoMapper;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Moq;

    [TestClass]
    public class MapLoaderShould
    {
        private Mock<IProfileExpression> _mapMock;

        private IEnumerable<Type> _types;

        [TestInitialize]
        public void TestInit()
        {
            this._mapMock = new Mock<IProfileExpression>();
            this._types = new[]
                              {
                                  typeof(MapLoaderTestSource), typeof(MapLoaderTestDestination), typeof(MapLoaderTestCustom), typeof(IMapTo<>), 
                                  typeof(IConfiguration), typeof(MapLoaderShould)
                              };
        }

        [TestMethod]
        public void RunAutoMapperForEveryTypeWithTheInterfaceOfIMapTo()
        {
            var sut = new MapLoader(this._mapMock.Object);

            sut.LoadMappings(this._types);

            this._mapMock.Verify(m => m.CreateMap(typeof(MapLoaderTestSource), typeof(MapLoaderTestDestination)), Times.Once);
        }

        [TestMethod]
        public void RunAutoMapperForEveryTypeWithTheInterfaceOfIMapFrom()
        {
            var sut = new MapLoader(this._mapMock.Object);

            sut.LoadMappings(this._types);

            this._mapMock.Verify(m => m.CreateMap(typeof(MapLoaderTestDestination), typeof(MapLoaderTestSource)), Times.Once);
        }

        [TestMethod]
        public void RunCustomMappingOnEveryTypeWithIHaveCustomMapping()
        {
            var sut = new MapLoader(this._mapMock.Object);

            sut.LoadMappings(this._types);

            this._mapMock.Verify(m => m.CreateMap(typeof(MapLoaderTestCustom), typeof(MapLoaderTestDestination)), Times.Once);
        }

        internal class MapLoaderTestCustom : IHaveCustomMappings
        {
            public void CreateMappings(IProfileExpression configuration)
            {
                configuration.CreateMap(typeof(MapLoaderTestCustom), typeof(MapLoaderTestDestination));
            }
        }

        internal class MapLoaderTestDestination
        {
        }

        internal class MapLoaderTestSource : IMapTo<MapLoaderTestDestination>, IMapFrom<MapLoaderTestDestination>
        {
        }
    }
}