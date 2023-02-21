using AutoMapper;
using BLL.Dtos;
using DAL.Models;

namespace BLL.Mapper
{
    internal class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dto => dto.ReviewsNumber,
                opt => opt.MapFrom(book => (decimal)book.Reviews.Count))
                .ForMember(dto => dto.Rating,
                opt => opt.MapFrom(book => book.Ratings.Count != 0 ? Math.Round(book.Ratings.Average(r => r.Score), 2) : 0))
                .ReverseMap();

            CreateMap<Book, BookDetails>()
                .ForMember(dto => dto.Reviews,
                opt => opt.MapFrom(book => book.Reviews))
                .ForMember(dto => dto.Rating,
                opt => opt.MapFrom(book => book.Ratings.Count != 0 ? Math.Round(book.Ratings.Average(r => r.Score), 2) : 0))
                .ReverseMap();

            CreateMap<Book, AddBook>().ReverseMap();
        }
    }
}
