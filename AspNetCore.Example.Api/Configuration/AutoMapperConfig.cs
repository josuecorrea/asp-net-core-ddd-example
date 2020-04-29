using AutoMapper;
using AspNetCore.Example.Api.Mapper;

namespace AspNetCore.Example.Api.Configuration
{
    public static class AutoMapperConfig
    {
        public static MapperConfiguration GetMapperConfiguration()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            return mappingConfig;
        }
       
    }
}
