using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVideotheque
{
    class Client
    {
        public int clientId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string zip { get; set; }

        public Client(int id, string name, string address, string city, string zip)
        {
            this.clientId = id;
            this.name = name;
            this.address = address;
            this.city = city;
            this.zip = zip;
        }

        override
        public string ToString()
        {
            return "Client : " + clientId + " - " + name + " - " + address + " - " + city + " - " + zip;
        }
    }
}
