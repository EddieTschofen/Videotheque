using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVideotheque
{
    class Film
    {
        public int filmId { get; set; }
        public string title { get; set; }
        public DateTime releaseDate { get; set; }
        public double dailyRate { get; set; }
        public Director director { get; set; }
        public Genre genre { get; set; }

        public Film(int id, string title, DateTime releaseDate, double dailyRate, Director director, Genre genre)
        {
            this.filmId = id;
            this.title = title;
            this.releaseDate = releaseDate;
            this.dailyRate = dailyRate;
            this.director = director;
            this.genre = genre;
        }

        override
        public string ToString()
        {
            return "Film : " + filmId + " - " + title + " - " + releaseDate + " - " + dailyRate + "e - " + director.ToString() + " - " + genre.ToString();
        }
    }
}
