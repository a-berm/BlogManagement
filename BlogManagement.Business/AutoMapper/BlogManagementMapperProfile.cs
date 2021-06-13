using AutoMapper;
using BlogManagement.Domain.Dto;
using BlogManagement.Domain.Entities;

namespace BlogManagement.Business.AutoMapper
{
    public class BlogManagementMapperProfile : Profile
    {
        public BlogManagementMapperProfile()
        {
            CreateMap<Post, PostDto>()
                .ForMember(dto => dto.EditionDate, opt => opt.MapFrom(post => post.EditionDate.ToString("yyyy-MM-dd")));
        }
    }
}
