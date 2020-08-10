using System;
using System.Diagnostics;
using AutoMapper;

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
                    .ReverseMap();
            }
        }

        public static void Demo()
        {
            Console.WriteLine("AutoMapping =====================");
            var mapperConfiguration = new MapperConfiguration(cfg => cfg.AddProfile<MyProfile>());
            mapperConfiguration.AssertConfigurationIsValid<MyProfile>();

            var mapper = mapperConfiguration.CreateMapper();

            Trace.Assert(EnumB.V1 == mapper.Map<EnumB>(EnumA.V1));
            Trace.Assert(EnumA.V1 == mapper.Map<EnumA>(EnumB.V1));

            var deep = new DeepAggregate();
            Trace.Assert(deep.Address != null); // largely guaranteed from nullable reference types
            deep.Address!.City = "La Paz";
            deep.Address.Province = "Bolivia";
            var flatAuto = mapper.Map<FlatAggregate>(deep);
            Trace.Assert(flatAuto.AddressCity == "La Paz");
            Trace.Assert(flatAuto.AddressRegion == "Bolivia");
        }
    }
}