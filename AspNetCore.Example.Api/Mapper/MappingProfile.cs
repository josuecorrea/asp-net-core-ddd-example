using AutoMapper;
using AspNetCore.Example.Application.Mapping.Param;
using AspNetCore.Example.Application.Mapping.Request;
using AspNetCore.Example.Domain.Entities;
using AspNetCore.Example.Domain.Entities.UserAgg;
using AspNetCore.Example.Infra.Models.CompanyInformation;

namespace AspNetCore.Example.Api.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CompanyInformationModel, CompanyInformation>();           


            CreateMap<CompanyInformation, CompanyInformationDto>();            

            CreateMap<NewUserRequest, User>();
            CreateMap<UpdateUserRequest, User>().ConstructUsing(c => new User(c.Id));

        }
    }
}
