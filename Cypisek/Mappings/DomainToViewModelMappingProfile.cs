using AutoMapper;
using Cypisek.Models;
using Cypisek.ViewModels.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<EndPlayerClient, EndPlayerClientViewModel>();
            CreateMap<ClientGroup, ClientManagerViewModel>()
                .ForMember(dest => dest.EndPlayerClientsVM, opt => opt.MapFrom(src => src.EndPlayerClients));
        }

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
    }
}