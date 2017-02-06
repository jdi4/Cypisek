using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data
{
    public class CypisekSeedData : DropCreateDatabaseIfModelChanges<CypisekEntities>
    {
        protected override void Seed(CypisekEntities context)
        {
            //seed if needed
            PopulateClientGroups().ForEach(cg => context.ClientGroups.Add(cg));
            PopulateEndPlayerClients().ForEach(c => context.EndPlayerClients.Add(c));
            PopulateCampaignsandPlaylists().ForEach(c => context.ClientScheduleMediaFilesLists.Add(c));

            context.Commit();
        }

        private static List<ClientGroup> PopulateClientGroups()
        {
            return new List<ClientGroup>()
            {
                new ClientGroup()
                {
                    Name = "Grupa A",
                },
                new ClientGroup()
                {
                    Name = "Grupa B"
                }
            };
        }

        private static List<EndPlayerClient> PopulateEndPlayerClients()
        {
            return new List<EndPlayerClient>()
            {
                new EndPlayerClient()
                {
                    Name = "Końcówka 1",
                    ClientGroupID = 2,
                    LastConnectionDate = DateTime.Now,
                    IsSynchronized = false,
                    IsConnected = false
                },
                new EndPlayerClient()
                {
                    Name = "Końcówka 2",
                    ClientGroupID = 1,
                    LastConnectionDate = DateTime.Now,
                    IsSynchronized = true,
                    IsConnected = false
                },
                new EndPlayerClient()
                {
                    Name = "Końcówka 3",
                    ClientGroupID = 2,
                    LastConnectionDate = DateTime.Now,
                    IsSynchronized = false,
                    IsConnected = false
                },
                new EndPlayerClient()
                {
                    Name = "Końcówka 4",
                    LastConnectionDate = DateTime.Now,
                    IsSynchronized = false,
                    IsConnected = false
                },
                new EndPlayerClient()
                {
                    Name = "Końcówka 5",
                    LastConnectionDate = DateTime.Now,
                    IsSynchronized = false,
                    IsConnected = false
                }
            };
        }

        private static List<ClientScheduleMediaFilesList> PopulateCampaignsandPlaylists()
        {
            var campaign1 = new Campaign()
            {
                Name = "Kampania testowa 1",
                EndPlayerClients = new List<EndPlayerClient>
                {
                            new EndPlayerClient()
                            {
                                Name = "Końcówka H1",
                                LastConnectionDate = DateTime.Now,
                                IsSynchronized = false,
                                IsConnected = false
                            },
                            new EndPlayerClient()
                            {
                                Name = "Końcówka H2",
                                LastConnectionDate = DateTime.Now,
                                IsSynchronized = false,
                                IsConnected = false
                            }
                }
            };


            var schedule1 = new ClientSchedule()
            {
                Name = "Harmonogram test1",
                StartDate = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(3).AddHours(5),
                Campaign = campaign1
             };

            var schedule2 = new ClientSchedule()
            {
                Name = "Harmonogram test2",
                StartDate = DateTime.Now.AddDays(5).AddHours(2),
                ExpirationDate = DateTime.Now.AddDays(15).AddHours(1),
                Campaign = campaign1
            };

            return new List<ClientScheduleMediaFilesList>
            {
                new ClientScheduleMediaFilesList
                {
                    ClientSchedule = schedule1,
                    PlayTime = 30,
                    MediaFile = new MediaFile { Name = "Plik testowy 1", Path = "Wirtualny", Size = 20 }
                },
                new ClientScheduleMediaFilesList
                {
                    ClientSchedule = schedule1,
                    PlayTime = 30,
                    MediaFile = new MediaFile { Name = "Plik testowy 2", Path = "Wirtualny", Size = 25 }
                },
                new ClientScheduleMediaFilesList
                {
                    ClientSchedule = schedule2,
                    PlayTime = 10,
                    MediaFile = new MediaFile { Name = "Plik testowy 3", Path = "Wirtualny", Size = 35 }
                }
            };
        }
    }
}
