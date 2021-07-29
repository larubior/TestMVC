using AutoMapper;
using TestMVC.Business.AutoMapperProfile;

namespace TestMVC.Business
{
    public class AutoMapperConfiguration
    {
        public static MapperConfiguration Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<UserProfile>();
            });

            return configuration;
        }
    }
}
