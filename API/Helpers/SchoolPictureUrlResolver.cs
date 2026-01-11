using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class SchoolPictureUrlResolver : IValueResolver<School, SchoolDTO, string>
    {
        private readonly IConfiguration _configuration;

        public SchoolPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(School source, SchoolDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageCover))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.ImageCover}";
            }
            return string.Empty;
        }
    }
}
