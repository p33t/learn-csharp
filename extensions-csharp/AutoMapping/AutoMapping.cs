using System;
using System.Diagnostics;
using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace extensions_csharp.AutoMapping
{
    public static class AutoMapping
    {
        class MyProfile : Profile
        {
            public MyProfile()
            {
                CreateMap<DeepAggregate, FlatAggregate>()
                    .ForMember(dest => dest.ExtraField, opt => opt.Ignore())
                    .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.FamilyName))
                    .ForMember(dest => dest.AddressRegion, opt => opt.MapFrom(src => src.Address.Province))
                    .ForMember(dest => dest.AddressCity, opt => opt.MapFrom(src => src.Address.City))
                    // Flagging is NOT done via CRUD.  There is specific 'Flag' op that timestamps.  Is one-way mapping.
                    .ForMember(dest => dest.IsFlagged, opt => opt.MapFrom(src => src.FlaggedAt != null))
                    // Doesn't seem to affect anything
                    // .ForMember(dest => dest.IsFlagged, opt => opt.ExplicitExpansion())
                    .ReverseMap();
            }
        }

        public static void Demo()
        {
            Console.WriteLine("AutoMapping =====================");
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MyProfile>());
            mapperConfiguration.AssertConfigurationIsValid();

            var mapper = mapperConfiguration.CreateMapper();

            Trace.Assert(EnumB.V1 == mapper.Map<EnumB>(EnumA.V1));
            Trace.Assert(EnumA.V1 == mapper.Map<EnumA>(EnumB.V1));

            var deep = new DeepAggregate();
            Trace.Assert(deep.Address != null); // largely guaranteed from nullable reference types
            deep.Address!.City = "La Paz";
            deep.Address.Province = "Bolivia";
            deep.FlaggedAt = DateTime.UtcNow;

            var flatAuto = mapper.Map<FlatAggregate>(deep);
            Trace.Assert(flatAuto.AddressCity == "La Paz");
            Trace.Assert(flatAuto.AddressRegion == "Bolivia");
            Trace.Assert(flatAuto.IsFlagged);

            var deepAuto = mapper.Map<DeepAggregate>(flatAuto);
            Trace.Assert(deepAuto.Address.City == deep.Address.City);
            Trace.Assert(deepAuto.Address.Province == deep.Address.Province);
            Trace.Assert(deepAuto.FlaggedAt == null); //<<<< one way only

            var deeps = new EnumerableQuery<DeepAggregate>(new[] {deep, deepAuto});
            Trace.Assert(1 == deeps.Count(d => d.FlaggedAt != null));
            var projectedFlats = deeps.ProjectTo<FlatAggregate>(mapper.ConfigurationProvider, a => a.IsFlagged);
            Trace.Assert(2 == projectedFlats.Count(f => f.AddressRegion == "Bolivia"));
            Trace.Assert(0 == projectedFlats.Count(f => f.AddressRegion == "Bolognese"));
            
            Trace.Assert(1 == projectedFlats.Count(f => f.IsFlagged));
            Trace.Assert(1 == projectedFlats.Count(f => !f.IsFlagged));
        }
    }
}