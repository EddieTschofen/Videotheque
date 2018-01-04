using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.Sql;
using System.Configuration;

namespace GestionVideotheque
{
    /**
     * Contient toutes les interactions avec la base de donnée
     * Contient aussi les listes d'objet
     */
    class DbUtility
    {

        private static DbUtility instance;

        public List<Director> directorList { get; }
        public List<Genre> genreList { get; }
        public List<Film> filmList { get; }
        public List<Client> clientList { get; }
        public List<Rental> rentalList { get; }

        SqlConnection ddb;

        private DbUtility()
        {
            //Configuration de la connection à la base de donnée
            ConnectionStringSettings parametreConnexion = ConfigurationManager.ConnectionStrings["GestionVideotheque.Properties.Settings.videothequeConnectionString"];
            string laChaineDeConnexion = parametreConnexion.ConnectionString;
            ddb = new SqlConnection(laChaineDeConnexion);
            //initialisation des listes
            directorList = new List<Director>();
            initDirectors();

            genreList = new List<Genre>();
            initGenres();

            filmList = new List<Film>();
            initFilms();

            clientList = new List<Client>();
            initClient();

            rentalList = new List<Rental>();
            initRental();
        }
        /**
         * Singleton instance 
         */
        public static DbUtility Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbUtility();
                }
                return instance;
            }
        }

        /*Director*/
        /**
         * Get the Directors list in the db 
         */
        private void initDirectors()
        {
            try
            {
                ddb.Open();
                string requete = "SELECT * from Director";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        string name = res.GetString(1);
                        Director d = new Director(id, name);
                        directorList.Add(d);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception initDirectors : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }
        /**
         * Get a Director with the name
         * Create a new one if missing
         */
        private Director getDirector(String name)
        {
            foreach (Director d in directorList)
            {
                if (d.name == name)
                {
                    return d;
                }
            }
            return newDirector(name);
        }
        /**
         * Get a Director with the id
         */
        private Director getDirector(int id)
        {
            foreach (Director d in directorList)
            {
                if (d.directorId == id)
                {
                    return d;
                }
            }
            return null;
        }
        /**
         * Create a new Director
         * Add it in the database and in the list
         */
        private Director newDirector(String name)
        {
            try
            {
                ddb.Open();
                string requete = "INSERT INTO Director (name) VALUES ('" + name + "'); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(requete, ddb);
                object resO = command.ExecuteScalar();

                Director d = new Director(int.Parse(resO + ""), name);
                directorList.Add(d);
                return d;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception newDirector : " + ex.Message);

            }
            finally
            {
                ddb.Close();
            }
            return null;
        }

        /*Genre*/
        /**
         * Get the Genre list in the db 
         */
        private void initGenres()
        {
            try
            {
                ddb.Open();
                string requete = "SELECT * from FilmGenre";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        string name = res.GetString(1);

                        Genre g = new Genre(id, name);
                        genreList.Add(g);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception initGenre : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }
        /**
         * Get a Genre with the name
         * Create a new one if missing
         */
        private Genre getGenre(String name)
        {
            foreach (Genre g in genreList)
            {
                if (g.name == name)
                {
                    return g;
                }
            }
            return newGenre(name);
        }
        /**
         * Get a Genre with the id
         */
        private Genre getGenre(int id)
        {
            foreach (Genre g in genreList)
            {
                if (g.genreId == id)
                {
                    return g;
                }
            }
            return null;
        }
        /**
         * Create a new Genre
         * Add it in the database and in the list
         */
        private Genre newGenre(String name)
        {
            try
            {
                ddb.Open();
                string requete = "INSERT INTO FilmGenre (name) VALUES ('" + name + "'); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(requete, ddb);
                object resO = command.ExecuteScalar();

                Genre g = new Genre(int.Parse(resO + ""), name);
                genreList.Add(g);
                return g;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception newDirector : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
            return null;
        }

        /*Film*/
        /**
         * Get the Film list in the db 
         */
        private void initFilms()
        {
            try
            {
                ddb.Open();
                string requete = "SELECT * from Film Order By title";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        string title = res.GetString(1);
                        Director d = getDirector((int)res.GetValue(2));
                        DateTime release = res.GetDateTime(3);
                        Genre g = getGenre((int)res.GetValue(4));
                        double prix = res.GetDouble(5);

                        Film f = new Film(id, title, release, prix, d, g);
                        filmList.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception initFilms : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }
        /**
         * filter the film list from its name
         */
        public List<Film> filterName(string text, int mode)
        {
            string t = text.ToLower();
            List<Film> fList = new List<Film>();
            for (int i = 0; i < filmList.Count; i++)
            {
                switch (mode)
                {
                    case 0:
                        if (filmList[i].title.ToLower().Contains(t))
                        {
                            fList.Add(filmList[i]);
                        }
                        break;
                    case 1:
                        if (filmList[i].director.name.ToLower().Contains(t))
                        {
                            fList.Add(filmList[i]);
                        }
                        break;
                    case 2:
                        if (filmList[i].genre.name.ToLower().Contains(t))
                        {
                            fList.Add(filmList[i]);
                        }
                        break;
                    case 3:
                        if (filmList[i].releaseDate.ToShortDateString().ToLower().Contains(t))
                        {
                            fList.Add(filmList[i]);
                        }
                        break;
                }
                
            }
            return fList;
        }

        /**
         * Create a new Film
         * Add it in the database and in the list
         */
        public void addFilm(string titre, DateTime release, string director, string genre, string price)
        {
            try
            {
                Director d = getDirector(director);
                Genre g = getGenre(genre);

                ddb.Open();

                string requete = "INSERT INTO Film(title, director, releaseDate, genre, dailyPrice) VALUES " +
                    "('" + titre + "', " + d.directorId + ", '" + release + "', " + g.genreId + ", " + price + "); SELECT SCOPE_IDENTITY();";
                SqlCommand command = new SqlCommand(requete, ddb);
                object resO = command.ExecuteScalar();

                Film f = new Film(int.Parse(resO + ""), titre, release, double.Parse(price), d, g);
                filmList.Add(f);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception addFilms : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }
        /*Edit a film*/
        public void editFilm(int fId, string titre, DateTime release, string director, string genre, string price)
        {
            try
            {
                Director d = getDirector(director);
                Genre g = getGenre(genre);

                ddb.Open();

                string requete = "UPDATE Film " +
                    "SET title = '" + titre + "' " +
                    ", director = " + d.directorId + " " +
                    ", releaseDate = '" + release + "' " +
                    ", genre = " + g.genreId + " " +
                    ", dailyPrice = " + price + " " +
                    "WHERE filmId = " + fId + "; SELECT SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(requete, ddb);
                object resO = command.ExecuteScalar();

                for (int i = 0; i < filmList.Count; i++)
                {
                    if (fId == filmList[i].filmId)
                    {
                        filmList[i].title = titre;
                        filmList[i].releaseDate = release;
                        filmList[i].dailyRate = double.Parse(price);
                        filmList[i].director = getDirector(director);
                        filmList[i].genre = getGenre(genre);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception editFilm : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }
        /*Get a film by id*/
        public Film getFilm(int id)
        {
            foreach (Film f in filmList)
            {
                if (f.filmId == id)
                {
                    return f;
                }
            }
            return null;
        }
        /*Get the n top rented films*/
        public List<Film> getTopFilm(int n)
        {
            if (n == 0)
                return filmList;
            List<Film> fList = new List<Film>();
            try
            {
                ddb.Open();
                string requete = "SELECT * from Film WHERE filmId in (SELECT TOP(" + n + ") film from Rental GROUP BY film ORDER BY count(film) DESC) ";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        string title = res.GetString(1);
                        Director d = getDirector((int)res.GetValue(2));
                        DateTime release = res.GetDateTime(3);
                        Genre g = getGenre((int)res.GetValue(4));
                        double prix = res.GetDouble(5);

                        Film f = new Film(id, title, release, prix, d, g);
                        fList.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception initFilms : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }

            return fList;
        }
        /*Delete a film*/
        public void deleteFilm(int id)
        {
            try
            {
                ddb.Open();
                string requete = "DELETE FROM Film WHERE filmId = " + id;
                SqlCommand command = new SqlCommand(requete, ddb);
                command.ExecuteNonQuery();

                for (int i = 0; i < filmList.Count; i++)
                {
                    if (filmList[i].filmId == id)
                    {
                        filmList.RemoveAt(i);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception deleteFilm : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }
        /*Get the list of the unrested films*/
        public List<Film> getUnrentedFilms()
        {
            List<Film> fList = new List<Film>();
            try
            {
                ddb.Open();
                string requete = "select * from film where filmId not in (select film from Rental where dateRendu is NULL) Order By title";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        string title = res.GetString(1);
                        Director d = getDirector((int)res.GetValue(2));
                        DateTime release = res.GetDateTime(3);
                        Genre g = getGenre((int)res.GetValue(4));
                        double prix = res.GetDouble(5);

                        Film f = new Film(id, title, release, prix, d, g);
                        fList.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception getUnrentedFilm : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }

            return fList;
        }

        /*Client*/
        /*ON THE OTHER PART YOU'LL FIND THE SAME FUNCTIONS AS PREVIOUS ONE*/
        private void initClient()
        {
            try
            {
                ddb.Open();
                string requete = "SELECT * from Client order by name";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        string name = res.GetString(1);
                        string address = res.GetString(2);
                        string city = res.GetString(3);
                        string zip = res.GetString(4);

                        Client c = new Client(id, name, address, city, zip);
                        clientList.Add(c);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception initClients : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }

        public Client getClient(int id)
        {
            foreach (Client c in clientList)
            {
                if (c.clientId == id)
                {
                    return c;
                }
            }
            return null;
        }

        public void deleteClient(int id)
        {
            try
            {
                ddb.Open();
                string requete = "DELETE FROM Client WHERE clientId = " + id;
                SqlCommand command = new SqlCommand(requete, ddb);
                command.ExecuteNonQuery();

                for(int i = 0; i < clientList.Count; i++)
                {
                    if(clientList[i].clientId == id)
                    {
                        clientList.RemoveAt(i);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception deleteClient : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }

        public void editClient(int clientId, string name, string address, string city, string zip)
        {
            try
            {
                ddb.Open();

                string requete = "UPDATE Client " +
                    "SET name = '" + name + "' " +
                    ", address = '" + address + "' " +
                    ", city = '" + city + "' " +
                    ", zip = '" + zip + "' " +
                    "WHERE clientId = " + clientId;

                SqlCommand command = new SqlCommand(requete, ddb);
                object resO = command.ExecuteScalar();

                for (int i = 0; i < clientList.Count; i++)
                {
                    if (clientId == clientList[i].clientId)
                    {
                        clientList[i].name = name;
                        clientList[i].address = address;
                        clientList[i].city = city;
                        clientList[i].zip = zip;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception editClient : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }

        /**
         * Create a new Client
         * Add it in the database and in the list
         */
        public void addClient(string nom, string adresse, string city, string zip)
        {
            try
            {
                ddb.Open();

                string requete = "INSERT INTO Client(name, address, city, zip) VALUES " +
                    "('" + nom + "', '" + adresse + "', '" + city + "', '" + zip + "');";
                //Console.WriteLine(requete);
                SqlCommand command = new SqlCommand(requete, ddb);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception addClient : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
            clientList.Clear();
            initClient();
        }

        public List<List<string>> clientRentHistory(int id)
        {
            List<List<string>> infos = new List<List<string>>();

            try
            {
                ddb.Open();
                string requete = "select f.title, r.dateLocation, r.dateLimiteRendu, r.dateRendu FROM Rental r, Film f WHERE r.film = f.filmId AND r.client = " + id + " order by dateLocation desc";
                Console.WriteLine(requete);
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        List<string> info = new List<string>();
                        info.Add(res.GetString(0));
                        info.Add(res.GetDateTime(1).ToShortDateString());
                        info.Add(res.GetDateTime(2).ToShortDateString());
                        if (res.IsDBNull(3))
                            info.Add("");
                        else info.Add(res.GetDateTime(3).ToShortDateString());

                        infos.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception initFilms2 : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
            return infos;
        }

        /*Rental*/
        private void initRental()
        {
            rentalList.Add(new Rental(1, new DateTime(2017, 1, 18), new DateTime(2017, 1, 18).AddMonths(2), clientList[0], filmList[0]));
        }

        /*Get rented film and rent information*/
        public Dictionary<List<Film>, List<Rental>> getRentedFilms()
        {
            Dictionary<List<Film>, List<Rental>> rentalListe = new Dictionary<List<Film>, List<Rental>>();
            List<Rental> rList = new List<Rental>();
            List<Film> fList = new List<Film>();

            try
            {
                ddb.Open();
                string requete = "SELECT * from Film WHERE filmId in (SELECT film from Rental WHERE dateRendu IS NULL) order by filmId";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        string title = res.GetString(1);
                        Director d = getDirector((int)res.GetValue(2));
                        DateTime release = res.GetDateTime(3);
                        Genre g = getGenre((int)res.GetValue(4));
                        double prix = res.GetDouble(5);

                        Film f = new Film(id, title, release, prix, d, g);
                        fList.Add(f);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception getRentedFilms : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
            try
            {
                ddb.Open();
                string requete = "SELECT * from Rental WHERE dateRendu IS NULL order by film";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        int id = (int)res.GetValue(0);
                        Client c = getClient((int)res.GetValue(1));
                        Film f = getFilm((int)res.GetValue(2));
                        DateTime DLocation = res.GetDateTime(3);
                        DateTime DLimiteLocation = res.GetDateTime(4);

                        Rental r = new Rental(id, DLocation, DLimiteLocation, c, f);
                        rList.Add(r);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception getRentedFilms2 : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }

            rentalListe.Add(fList, rList);
            return rentalListe;
        }

        /**
        * filter the rented film list from its name
        */
        public Dictionary<List<Film>, List<Rental>> filterRentedName(string text)
        {
            Dictionary<List<Film>, List<Rental>> rentalListe = new Dictionary<List<Film>, List<Rental>>();
            List<Rental> rList = getRentedFilms().ElementAt(0).Value;
            List<Film> fList = getRentedFilms().ElementAt(0).Key;
            string t = text.ToLower();
            Dictionary<List<Film>, List<Rental>> rentedFList = new Dictionary<List<Film>, List<Rental>>();
            List<Rental> rrList = new List<Rental>();
            List<Film> frList = new List<Film>();
            for (int i = 0; i < fList.Count; i++)
            {
                if (fList[i].title.ToLower().Contains(t))
                {
                    rrList.Add(rList[i]);
                    frList.Add(fList[i]);
                }
            }
            rentedFList.Add(frList, rrList);
            return rentedFList;
        }

        public Boolean isAvailable(int id)
        {
            try
            {
                ddb.Open();
                string requete = "select count(*) from Rental Where dateRendu IS NULL and film = "+id;
                SqlCommand command = new SqlCommand(requete, ddb);
                object resO = command.ExecuteScalar();
                return ((int)resO == 0);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception filterRentedName : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
            return false;
        }

        public string getRentInfo(int filmId)
        {
            try
            {
                ddb.Open();
                string requete = " select c.name, r.dateLocation, r.dateLimiteRendu FROM Rental r, Client c WHERE r.film = " + filmId + " AND dateRendu IS NULL AND r.client = c.clientId";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    res.Read();
                    return res.GetString(0) + " - Loué le " + res.GetDateTime(1).ToShortDateString() + " - doit etre rendu avant le " + res.GetDateTime(2).ToShortDateString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception getRentInfo : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }          
            return "";
        }

        public List<List<string>> rentHistory(int id)
        {
            List<List<string>> infos = new List<List<string>>();

            try
            {
                ddb.Open();
                string requete = "select c.name, r.dateLocation, r.dateLimiteRendu, r.dateRendu " +
                                 "FROM Rental r, Client c " +
                                 "WHERE r.film = " + id +
                                 " AND r.client = c.clientId" +
                                 " order by dateLocation desc";
                SqlCommand command = new SqlCommand(requete, ddb);
                SqlDataReader res = command.ExecuteReader();
                if (res.HasRows)
                {
                    while (res.Read())
                    {
                        List<string> info = new List<string>();
                        info.Add(res.GetString(0));
                        info.Add(res.GetDateTime(1).ToShortDateString());
                        info.Add(res.GetDateTime(2).ToShortDateString());
                        if(res.IsDBNull(3))
                            info.Add("");
                        else info.Add(res.GetDateTime(3).ToShortDateString());

                        infos.Add(info);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception rentHistory : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
            return infos;
        }

        public void addRental(Film film, int selectedClient, DateTime renduLImite)
        {
            Client c = clientList[selectedClient];
            Console.WriteLine(film);
            Console.WriteLine(c);
            Console.WriteLine(renduLImite);
            try
            {
                ddb.Open();
                string requete = "INSERT INTO Rental (client,film,dateLocation,dateLimiteRendu) VALUES ("+c.clientId+", "+film.filmId+", '"+DateTime.Now+"', '"+ renduLImite + "')";
                SqlCommand command = new SqlCommand(requete, ddb);
                object resO = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception addRental : " + ex.Message);

            }
            finally
            {
                ddb.Close();
            }
        }

        public void endRental(Rental r)
        {
            DateTime date = DateTime.Now;
            r.dateRendu = date;

            try
            {
                ddb.Open();

                string requete = "UPDATE Rental " +
                    "SET dateRendu = '" + date + "' " +
                    "WHERE rentalId = " + r.rentalId + "; SELECT SCOPE_IDENTITY()";
                SqlCommand command = new SqlCommand(requete, ddb);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception endRental : " + ex.Message);
            }
            finally
            {
                ddb.Close();
            }
        }

    }
}