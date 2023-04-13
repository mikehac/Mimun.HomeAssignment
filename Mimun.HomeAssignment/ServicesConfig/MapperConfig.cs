using AutoMapper;
using Mimun.HomeAssignment.Mapping;

namespace Mimun.HomeAssignment.ServicesConfig
{
    public class MapperConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var MappingConfig = new MapperConfiguration(config =>
            {
                config.AddProfile(new EntityiesProfile());
            });
            return MappingConfig;
        }
    }
}
