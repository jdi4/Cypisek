using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Hubs
{
    public class ConnectionMapping<T>
    {
        private readonly Dictionary<string, T> _connections =
            new Dictionary<string, T>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(string connectionIdKey, T userkey)
        {
            lock (_connections)
            {
                T connections;
                if (!_connections.TryGetValue(connectionIdKey, out connections))
                {
                    _connections.Add(connectionIdKey, userkey);
                }

            }
        }

        public T GetUser(string connectionID)
        {
            T userKey;
            lock (_connections)
            {
                _connections.TryGetValue(connectionID, out userKey);
            }
            return userKey;
        }

        //public IEnumerable<string> GetConnections(T key)
        //{
        //    HashSet<string> connections;
        //    if (_connections.TryGetValue(key, out connections))
        //    {
        //        return connections;
        //    }

        //    return Enumerable.Empty<string>();
        //}

        public void Remove(string connectionIdKey)
        {
            lock (_connections)
            {
                T temp;
                if (!_connections.TryGetValue(connectionIdKey, out temp))
                {
                    return;
                }

                _connections.Remove(connectionIdKey);
            }
        }
    }
}