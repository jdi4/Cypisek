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
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<MediaFileSelectViewModel, MediaFile>();

            CreateMap<ClientScheduleFormViewModel, ClientSchedule>();
            //.ForMember(dest => dest.MediaFilessList;

            CreateMap<CampaignFormViewModel, Campaign>();

            CreateMap<CampaignSchedulesFormViewModel, Campaign>();

            CreateMap<ClientGroupFormModel, ClientGroup>();
        }

        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }
    }
}