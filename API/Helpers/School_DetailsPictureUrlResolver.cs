using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class School_DetailsPictureUrlResolver : IValueResolver<School_Details, School_DetailsDTO, string>
    {
        private readonly IConfiguration _configuration;

        public School_DetailsPictureUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Resolve(School_Details source, School_DetailsDTO destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.ImageCover))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.ImageCover}";
            }
            if (!string.IsNullOrEmpty(source.Images))
            {
                return $"{_configuration["ApiBaseUrl"]}/{source.Images}";
            }
            return string.Empty;
        }
    }
}
