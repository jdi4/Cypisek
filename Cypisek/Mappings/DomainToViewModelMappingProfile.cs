using AutoMapper;
using Cypisek.Models;
using Cypisek.ViewModels;
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

            //CreateMap<ClientGroup, ClientManagerViewModel>()
            //    .ForMember(dest => dest.ClientsWithoutGroup, opt => opt.MapFrom(src => src.EndPlayerClients));

            CreateMap<ClientGroup, ClientGroupViewModel>()
                .ForMember(dest => dest.EndPlayerClients, opt => opt.MapFrom(src => src.EndPlayerClients));

            CreateMap<ClientSchedule, ClientScheduleViewModel>()
                .ForMember(vm => vm.MediaPlaylist, map => map.Ignore());
                //map => map
                //.MapFrom(m => m.MediaPlaylist.Select( p => p.MediaFile)));

            CreateMap<Campaign, CampaignsIndexViewModel>();

            CreateMap<MediaFile, MediaFileViewModel>()
                .ForMember(vm => vm.FileName, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Bytes, map => map.MapFrom(m => m.Size));

            CreateMap<MediaFile, MediaFileSelectViewModel>()
                .ForMember(vm => vm.FileName, map => map.MapFrom(m => m.Name))
                .ForMember(vm => vm.Bytes, map => map.MapFrom(m => m.Size))
                .ForMember(vm => vm.IsSelected, map => map.UseValue<bool>(false));

            CreateMap<ClientScheduleMediaFilesList, MediaFileSelectViewModel>()
            .ForMember(vm => vm.ID, map => map.MapFrom(m => m.MediaFile.ID))
            .ForMember(vm => vm.FileName, map => map.MapFrom(m => m.MediaFile.Name))
            .ForMember(vm => vm.Bytes, map => map.MapFrom(m => m.MediaFile.Size))
            .ForMember(vm => vm.IsSelected, map => map.UseValue<bool>(false));
        }

        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
    }
}