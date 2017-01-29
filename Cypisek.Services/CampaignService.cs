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
        Campaign GetCampaign(int id);
        //Campaign GetCampaignIncludeSchedules(int id);
        void CreateCampaign(Campaign Campaign);
        void CommitChanges();

        //IEnumerable<ClientSchedule> GetAllSchedules
        //int? GetCurrentScheduleID(int campaignId);

    }

    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository CampaignsRepository;
        private readonly IUnitOfWork unitOfWork;

        public CampaignService(ICampaignRepository csR, IUnitOfWork unitOfWork)
        {
            this.CampaignsRepository = csR;
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
