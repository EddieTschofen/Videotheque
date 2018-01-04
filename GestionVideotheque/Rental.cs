using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionVideotheque
{
    class Rental
    {
        public int rentalId { get; set; }
        public DateTime dateRental { get; set; }
        public DateTime dateLimiteRendu { get; set; }
        public DateTime dateRendu { get; set; }
        public Client client { get; set; }
        public Film film { get; set; }

        public Rental(int rentalId, DateTime dateRental, DateTime dateLimiteRendu, Client client, Film film)
        {
            this.rentalId = rentalId;
            this.dateRental = dateRental;
            this.dateLimiteRendu = dateLimiteRendu;
            this.client = client;
            this.film = film;
        }

        override
        public string ToString()
        {
            return "Rental : " + rentalId + " - " + dateRental + " - " + dateLimiteRendu + " - " + dateRendu + " - " + client.ToString() + " - " + film.ToString();
        }
    }
}
