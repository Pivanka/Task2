using AutoMapper;
using BLL.Dtos;
using DAL.Models;

namespace BLL.Mapper
{
    public class RatingMapper : Profile
    {
        public RatingMapper()
        {
            CreateMap<Rating, RatingDto>().ReverseMap();
        }
    }
}
