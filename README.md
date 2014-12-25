AutoMapperFramework
===================

Ever get tired of the monolithic map definition files? AutoMapperFramework generates maps from types that implement header interfaces.

How does it work?
===================
First, you need to install the package via NuGet:

```
PM> Install-Package AutoMapperFramework
```

Next, you need to decorate the classes you want to create maps for with one or more of three header interfaces: IMapTo<T>, IMapFrom<T>, and IHaveCustomMappings. The first two are fairly self-explanatory: they will create a default map with no additional options. Your code file will look something like below:

```C#
public class SomeClass: IMapFrom<SomeClassDto>, IMapTo<SomeClassDto>
{
    public string SomeProperty { get; set; }

    public string ServerOnlyProperty { get; set; }
}

public class SomeClassDto
{
    public string SomeProperty { get; set; }
}
```
Use IHaveCustomMappings if you need special options for your map.

```C#
public class SomeClass: IHaveCustomMappings
{
    public string SomeProperty { get; set; }

    public string ServerOnlyProperty { get; set; }

    public void CreateMappings(IProfileExpression configuration)
    {
        configuration.CreateMap<SomeClassDto, SomeClass>()
            .ForMember(m => m.SomeProperty, opts => 
                opts.MapFrom(s => s.SomePropertyWithADifferntName));
    }
}

public class SomeClassDto
{
    public string SomePropertyWithADifferntName { get; set; }
}
```
Finally, you just need to tell AutoMapperFramework to load your maps. Create a class like the one below and run it on app start (Global.asax or anyway you like). If you have maps that do not exist in the currently executing assembly, just pass those types into LoadMappings separately. LoadMappings just accepts an IEnumerable<Type>.

```C#
public static class AutoMapperConfig
{
    public static void RegisterMaps()
    {
        var mapLoader = new MapLoader(Mapper.Configuration);

        mapLoader.LoadMappings(Assembly
            .GetExecutingAssembly()
            .GetExportedTypes());
    }
}
```

I'm happy to accept any pull requests (as long as they build).