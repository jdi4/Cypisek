using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cypisek.Models
{
    public class Client
    {
        public string Name { get; }

        public Client(string na)
        {
            Name = na;
        }
    }
}