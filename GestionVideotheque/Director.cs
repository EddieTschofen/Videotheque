using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVideotheque
{
    class Director
    {
        public int directorId { get; set; }
        public string name { get; set; }

        public Director(int id, string name)
        {
            this.directorId = id;
            this.name = name;
        }

        override
        public string ToString()
        {
            return "Director : " + directorId + " - " + name;
        }
    }
}
