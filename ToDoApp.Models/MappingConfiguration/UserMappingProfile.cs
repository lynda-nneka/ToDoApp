using System;
using AutoMapper;
using ToDoApp.DAL.Entities;
using ToDoApp.Models.Dtos.Requests;
using ToDoApp.Models.Dtos.Responses;

namespace ToDoApp.Models.MappingConfiguration
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserRequest, User>();
            CreateMap<User, UserResponse>();
            CreateMap<UpdateUserRequest, User>().ReverseMap();
            
                
        }
    }
}

