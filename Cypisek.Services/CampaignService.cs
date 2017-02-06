using Cypisek.Models;
using Cypisek.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cypisek.Data.Infrastructure;

namespace Cypisek.Services
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetAllCampaigns();
        IEnumerable<Campaign> GetAllCampaignsIncludeSchedules();
        IEnumerable<Campaign> GetAllIncludeSchedulesWithPlaylists();
        Campaign GetCampaign(int id);
        //Campaign GetCampaignIncludeSchedules(int id);
        void CreateCampaign(Campaign Campaign);
        void EditCampaign(Campaign Campaign);
        void DeleteCampaign(int id);
        void CommitChanges();

        //IEnumerable<ClientSchedule> GetAllSchedules
        //int? GetCurrentScheduleID(int campaignId);

    }

    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository CampaignsRepository;
        private readonly IClientScheduleRepository clientScheduleRepository;
        private readonly IUnitOfWork unitOfWork;

        public CampaignService(ICampaignRepository cR, IClientScheduleRepository csR, IUnitOfWork unitOfWork)
        {
            this.CampaignsRepository = cR;
            this.clientScheduleRepository = csR;
            this.unitOfWork = unitOfWork;
        }

        #region ICampaignService Members

        public IEnumerable<Campaign> GetAllCampaigns()
        {
            var Campaigns = CampaignsRepository.GetAll();
            return Campaigns;
        }

        public Campaign GetCampaign(int id)
        {
            var Campaign = CampaignsRepository.GetById(id);
            return Campaign;
        }

        public void CreateCampaign(Campaign Campaign)
        {
            CampaignsRepository.Add(Campaign);
        }

        public void CommitChanges()
        {
            unitOfWork.Commit();
        }

        public IEnumerable<Campaign> GetAllCampaignsIncludeSchedules()
        {
            return CampaignsRepository.GetAllIncludeSchedules();
        }

        public void DeleteCampaign(int id)
        {
            throw new NotImplementedException();
            //CampaignsRepository.Delete(c => c.ID == id); // cascade schedules
        }

        public void EditCampaign(Campaign Campaign)
        {
            CampaignsRepository.Edit(Campaign);
            foreach (var schedule in Campaign.Schedules)
            {
                clientScheduleRepository.Edit(schedule);
            }
        }

        public IEnumerable<Campaign> GetAllIncludeSchedulesWithPlaylists()
        {
            return CampaignsRepository.GetAllIncludeSchedulesWithPlaylists();
        }

        //public int? GetCurrentScheduleID(int campaignId)
        //{
        //    var campaign = CampaignsRepository.GetById(campaignId);

        //    if (campaign != null)
        //    {
        //        return campaign.Schedules.Where(s => s.StartDate >= DateTime.Now).First().ID;
        //    }
        //    else
        //        return null;
        //}
        #endregion

    }
}
