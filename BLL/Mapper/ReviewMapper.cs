using AutoMapper;
using BLL.Dtos;
using DAL.Models;

namespace BLL.Mapper
{
    public class ReviewMapper : Profile
    {
        public ReviewMapper()
        {
            CreateMap<Review, ReviewDto>().ReverseMap();
        }
    }
}
