﻿using Cypisek.Models;
using Cypisek.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cypisek.Data.Infrastructure;

namespace Cypisek.Services
{
    public interface IClientScheduleService
    {
        IEnumerable<ClientSchedule> GetClientSchedules();
        ClientSchedule GetClientSchedule(int id);
        void CreateClientSchedule(ClientSchedule ClientSchedule);
        void CreateClientSchedule(ClientSchedule ClientSchedule, IEnumerable<int> filesIDs);
        void DeleteClientSchedule(int id);
        void SaveClientSchedule();

        IEnumerable<ClientScheduleMediaFilesList> GetSchedulePlaylist(int scheduleId);
        ClientSchedule GetCurrentSchedule(int campaignId);
        //string GetScheduleAsString(int scheduleID);
        string GetScheduleAsString(ClientSchedule schedule);
    }

    public class ClientScheduleService : IClientScheduleService
    {
        private readonly IClientScheduleRepository clientSchedulesRepository;
        private readonly IClientScheduleMediaFilesListRepository clientScheduleMediaFilesListRepository;
        private readonly IUnitOfWork unitOfWork;

        public ClientScheduleService(IClientScheduleRepository csR, IClientScheduleMediaFilesListRepository csmfR, IUnitOfWork unitOfWork)
        {
            this.clientSchedulesRepository = csR;
            this.clientScheduleMediaFilesListRepository = csmfR;
            this.unitOfWork = unitOfWork;
        }

        #region IClientScheduleService Members

        public IEnumerable<ClientSchedule> GetClientSchedules()
        {
            var ClientSchedules = clientSchedulesRepository.GetAll();
            return ClientSchedules;
        }

        public ClientSchedule GetClientSchedule(int id)
        {
            var ClientSchedule = clientSchedulesRepository.GetById(id);
            return ClientSchedule;
        }

        public void CreateClientSchedule(ClientSchedule ClientSchedule)
        {
            clientSchedulesRepository.Add(ClientSchedule);
        }

        public void SaveClientSchedule()
        {
            unitOfWork.Commit();
        }

        public void CreateClientSchedule(ClientSchedule ClientSchedule, IEnumerable<int> filesIDs)
        {
            clientSchedulesRepository.Add(ClientSchedule);

            foreach (int i in filesIDs)
            {
                clientScheduleMediaFilesListRepository.Add( new ClientScheduleMediaFilesList()
                {
                    ClientScheduleID = ClientSchedule.ID,
                    MediaFileID = i,
                    PlayTime = 10
                });
            }
        }

        public string GetScheduleAsString(ClientSchedule schedule)
        {
            //var schedule = clientSchedulesRepository.GetById(scheduleID);
            string dateformat = @"MM\/dd\/yyyy HH:mm";

            string scheduleString = String.Format("{0},{1},{2},{3}",
                schedule.Name,
                schedule.StartDate.ToString(dateformat),
                schedule.ExpirationDate.ToString(dateformat),
                schedule.MediaPlaylist.Count
                );

            var playlist = clientScheduleMediaFilesListRepository
                .GetManyIncludeMediaFiles(p => p.ClientScheduleID == schedule.ID);

            System.Text.StringBuilder sb = new System.Text.StringBuilder(scheduleString);

            foreach (var item in playlist)
            {
                sb.AppendFormat(",{0},{1}", item.MediaFile.Name, item.PlayTime);
            }

            return sb.ToString();
        }

        public ClientSchedule GetCurrentSchedule(int campaignId)
        {
            var time = DateTime.Now;
            //return clientSchedulesRepository.GetAll().OrderBy(s => s.ExpirationDate).FirstOrDefault()
            return clientSchedulesRepository.Get(s => s.CampaignID == campaignId && s.ExpirationDate > time);
        }

        public void DeleteClientSchedule(int id)
        {
            var schedule = clientSchedulesRepository.GetById(id);
            schedule.MediaPlaylist.ToList().ForEach(
                p => clientScheduleMediaFilesListRepository.Delete(p)
                );
            clientSchedulesRepository.Delete(schedule);
        }

        public IEnumerable<ClientScheduleMediaFilesList> GetSchedulePlaylist(int scheduleId)
        {
            return clientScheduleMediaFilesListRepository
                .GetManyIncludeMediaFiles(csmfl => csmfl.ClientScheduleID == scheduleId);
        }

        #endregion

    }
}
