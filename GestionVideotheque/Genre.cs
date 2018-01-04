using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVideotheque
{
    class Genre
    {
        public int genreId { get; set; }
        public string name { get; set; }

        public Genre(int id, string name)
        {
            this.genreId = id;
            this.name = name;
        }

        override
        public string ToString()
        {
            return "Genre : " + genreId + " - " + name;
        }
    }
}
