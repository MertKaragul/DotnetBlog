using AutoMapper;
using Core.Dtos;
using Core.Dtos.BlogDto;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mapper
{
    public class MapperService : Profile {
        public MapperService()
        {
            CreateMap<User, UserDto>()
                .ReverseMap();

            CreateMap<LoginDto, User>()
                .ReverseMap();

            CreateMap<Tag, TagDto>()
                .ReverseMap();

            CreateMap<Comment, CommentDto>()
                .ReverseMap();

            CreateMap<Blog, BlogDto>()
                .ReverseMap();

            CreateMap<CreateBlogDto, Blog>().ReverseMap();
        }
    }
}
