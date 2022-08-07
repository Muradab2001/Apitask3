using AutoMapper;
using BookApi.DTOs;
using BookApi.DTOs.Book;
using BookApi.DTOs.Category;
using BookApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookApi.Mapping.Profiles
{
    public class MapProfile:Profile
    {
        public MapProfile()
        {
            //book start
            CreateMap<Book,BookGetDto>();
            CreateMap<Category, CategoryInGetDto>();
            CreateMap<Book, BookListItemDto>();
            //category start
            CreateMap<Category, CategoryGetDto>();
            CreateMap<Category, CategoryListItemDto>();




        }
    }
}
