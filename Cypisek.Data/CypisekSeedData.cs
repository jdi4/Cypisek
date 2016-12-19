using Cypisek.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cypisek.Data
{
    public class CypisekSeedData : DropCreateDatabaseAlways<CypisekEntities>
    {
        protected override void Seed(CypisekEntities context)
        {
            //seed if needed
            PopulateClientGroups().ForEach(cg => context.ClientGroups.Add(cg));
            PopulateEndPlayerClients().ForEach(c => context.EndPlayerClients.Add(c));

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
                    ID = 1,
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
                }
            };
        }
    }
}
