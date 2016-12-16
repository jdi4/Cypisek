using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class ConnectedClients
    {
        private static readonly Lazy<ConnectedClients> _instance =
            new Lazy<ConnectedClients>(() => new ConnectedClients());

        public List<Client> ClientList;

        private ConnectedClients()
        {
            ClientList = new List<Client>();
        }

        public static ConnectedClients Instance
        {
            get { return _instance.Value; }
        }
    }
}