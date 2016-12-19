using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Cors;
using Cypisek.Services;

namespace SignalRTest
{
    [EnableCors("AllowSpecificOrigin")]
    public class ContentHub : Hub
    {
        private readonly IEndPlayerClientService endPlayerClientService;

        public ContentHub(IEndPlayerClientService epcS)
        {
            this.endPlayerClientService = epcS;
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        public void PoorAuthenticate(int clientid)
        {
            var client = endPlayerClientService.GetEndPlayerClient(clientid);

            if (client != null)
            {
                Clients.Caller.confirm(true);
            }
        }
    }
}