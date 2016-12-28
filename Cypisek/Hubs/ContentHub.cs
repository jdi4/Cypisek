using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Cors;
using Cypisek.Services;
using System.Threading.Tasks;
using Autofac;

namespace Cypisek.Hubs
{
    [EnableCors("AllowSpecificOrigin")]
    public class ContentHub : Hub
    {
        private readonly IEndPlayerClientService endPlayerClientService;
        private readonly static ConnectionMapping<int> _connections =
            new ConnectionMapping<int>();

        private readonly ILifetimeScope _hubLifetimeScope;

        //public ContentHub(IEndPlayerClientService epcS) : base()
        //{
        //    this.endPlayerClientService = epcS;
        //}

        public ContentHub(ILifetimeScope lifetimeScope)
        {
            this._hubLifetimeScope = lifetimeScope.BeginLifetimeScope();

            // Resolve dependencies from the hub lifetime scope.
            endPlayerClientService = _hubLifetimeScope.Resolve<IEndPlayerClientService>();
        }

        public void PoorAuthenticate(string clientidstr)
        {
            int clientid = Int32.Parse(clientidstr);
            var client = endPlayerClientService.GetEndPlayerClient(clientid);

            if (client != null)
            {
                _connections.Add(Context.ConnectionId, clientid);
                client.IsConnected = true;
                endPlayerClientService.EditEndPlayerClient(client);
                endPlayerClientService.SaveEndPlayerClient();
                Clients.Caller.confirm(true);
            }
            else
            {
                Clients.Caller.confirm(false);
            }
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string conid = Context.ConnectionId;
            int clientid = _connections.GetUser(conid);

            if (clientid != 0)
            {
                var client = endPlayerClientService.GetEndPlayerClient((int) clientid);
                client.LastConnectionDate = DateTime.Now;
                client.IsConnected = false;
                endPlayerClientService.EditEndPlayerClient(client);
                endPlayerClientService.SaveEndPlayerClient();
            }

            _connections.Remove(Context.ConnectionId);

            return base.OnDisconnected(stopCalled);
        }

        protected override void Dispose(bool disposing)
        {
            // Dipose the hub lifetime scope when the hub is disposed.
            if (disposing && _hubLifetimeScope != null)
            {
                _hubLifetimeScope.Dispose();
            }

            base.Dispose(disposing);
        }

    }
}