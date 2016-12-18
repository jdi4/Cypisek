using AutoMapper;
using Cypisek.Models;
using Cypisek.ViewModels.Clients;
using Cypisek.ViewModels.MediaLibrary;
using Cypisek.ViewModels.Schedules;
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

            CreateMap<ClientSchedule, ClientScheduleViewModel>();

            CreateMap<MediaFile, MediaFileViewModel>()
                .ForMember(vm => vm.FileName, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Bytes, map => map.MapFrom(m => m.Size));
        }

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
    }
}