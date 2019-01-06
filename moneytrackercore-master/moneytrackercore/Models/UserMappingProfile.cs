using AutoMapper;
using moneytrackercore.data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace moneytrackercore.Models
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<Users, UsersModel>();
        }
    }
}
