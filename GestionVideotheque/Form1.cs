using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionVideotheque
{
    public partial class Form1 : Form
    {
        /*
         * Contient tous les panels 
         * 
         * */
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            initFilmPanel();
        }

        //ALL
        private Label titre;
        //List Film
        private Button clients;
        private Label rechercheBoxLabel;
        private TextBox rechercheBox;
        private TableLayoutPanel filmTable;
        private Button addFilm;
        private Button addRent;
        private Button currentRent;
        private Label topBoxLabel;
        private TextBox topBox;
        private ComboBox rechercheCombo;

        private void initFilmPanel()
        {
            this.Controls.Clear();

            titre = new Label();
            titre.Text = "Liste des films";
            titre.Location = new Point(325, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            //titre.Size = new System.Drawing.Size(150, 50);
            this.Controls.Add(titre);

            clients = new Button();
            clients.Text = "Client";
            clients.Location = new Point(25, 50);
            clients.Font = new Font(titre.Font.FontFamily, 10); ;
            clients.Size = new System.Drawing.Size(55, 30);
            clients.Click += Clients_Click;
            this.Controls.Add(clients);

            rechercheBoxLabel = new Label();
            rechercheBoxLabel.Text = "Recherche : ";
            rechercheBoxLabel.Location = new Point(500, 50);
            rechercheBoxLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            rechercheBoxLabel.Size = new System.Drawing.Size(85, 40);
            this.Controls.Add(rechercheBoxLabel);

            rechercheCombo = new ComboBox();
            rechercheCombo.Location = new Point(590, 50);
            rechercheCombo.Font = new Font(titre.Font.FontFamily, 10);
            rechercheCombo.Size = new System.Drawing.Size(85, 40);
            rechercheCombo.Items.Add("Titre");
            rechercheCombo.Items.Add("Realisateur");
            rechercheCombo.Items.Add("Genre");
            rechercheCombo.Items.Add("Date");
            rechercheCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            rechercheCombo.SelectedIndex = 0;
            this.Controls.Add(rechercheCombo);

            rechercheBox = new TextBox();
            rechercheBox.Text = "";
            rechercheBox.Location = new Point(675, 50);
            rechercheBox.Font = new Font(titre.Font.FontFamily, 10);
            rechercheBox.Size = new System.Drawing.Size(100, 40);
            rechercheBox.KeyUp += RechercheBox_KeyUp; ;
            this.Controls.Add(rechercheBox);

            initFilmList(DbUtility.Instance.filmList);

            addFilm = new Button();
            addFilm.Text = "Ajouter film";
            addFilm.Location = new Point(25, 550);
            addFilm.Font = new Font(titre.Font.FontFamily, 10); ;
            addFilm.Size = new System.Drawing.Size(100, 30);
            addFilm.Click += AddFilm_Click;
            this.Controls.Add(addFilm);

            currentRent = new Button();
            currentRent.Text = "Locations actuels";
            currentRent.Location = new Point(470, 550);
            currentRent.Font = new Font(titre.Font.FontFamily, 10); ;
            currentRent.Size = new System.Drawing.Size(150, 30);
            currentRent.Click += CurrentRent_Click;
            this.Controls.Add(currentRent);

            addRent = new Button();
            addRent.Text = "Nouvelle Locations";
            addRent.Location = new Point(625, 550);
            addRent.Font = new Font(titre.Font.FontFamily, 10); ;
            addRent.Size = new System.Drawing.Size(150, 30);
            addRent.Click += AddRent_Click;
            this.Controls.Add(addRent);

            topBoxLabel = new Label();
            topBoxLabel.Text = "Afficher top :";
            topBoxLabel.Location = new Point(300, 550);
            topBoxLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            topBoxLabel.Size = new System.Drawing.Size(95, 40);
            this.Controls.Add(topBoxLabel);

            topBox = new TextBox();
            topBox.Text = "";
            topBox.Location = new Point(400, 550);
            topBox.Font = new Font(titre.Font.FontFamily, 10);
            topBox.Size = new System.Drawing.Size(50, 40);
            topBox.KeyUp += TopBox_KeyUp;
            this.Controls.Add(topBox);
        }

        private void AddRent_Click(object sender, EventArgs e)
        {
            initNewRentPanel();
        }

        private void Clients_Click(object sender, EventArgs e)
        {
            initClientListPanel();
        }

        private void initFilmList(List<Film> fList)
        {
            filmTable = new TableLayoutPanel();
            filmTable.ColumnCount = 6;
            filmTable.RowCount = fList.Count + 1;
            filmTable.Location = new Point(50, 100);
            filmTable.Size = new System.Drawing.Size(700, 400);
            filmTable.AutoScroll = true;
            //filmTable.AutoSize = true;
            filmTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            //th
            filmTable.Controls.Add(new Label() { Text = "Titre", Font = new Font(filmTable.Font.FontFamily, 12) }, 0, 0);
            filmTable.Controls.Add(new Label() { Text = "Realisateur", Font = new Font(filmTable.Font.FontFamily, 12) }, 1, 0);
            filmTable.Controls.Add(new Label() { Text = "Genre", Font = new Font(filmTable.Font.FontFamily, 12) }, 2, 0);
            filmTable.Controls.Add(new Label() { Text = "Sortie le", Font = new Font(filmTable.Font.FontFamily, 12) }, 3, 0);
            filmTable.Controls.Add(new Label() { Text = "Prix par jour", Font = new Font(filmTable.Font.FontFamily, 12) }, 4, 0);
            for (int i = 0; i < fList.Count; i++)
            {
                //filmTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                //filmTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                filmTable.Controls.Add(new Label() { Text = fList[i].title, AutoEllipsis = true }, 0, i + 1);
                //filmTable.Controls.Add(new Label() { Text = "azertyuiopqsdfghjklmwxcvbnazertyuiop", AutoEllipsis = true }, 0, i + 1);
                filmTable.Controls.Add(new Label() { Text = fList[i].director.name, AutoEllipsis = true }, 1, i + 1);
                filmTable.Controls.Add(new Label() { Text = fList[i].genre.name, AutoEllipsis = true }, 2, i + 1);
                filmTable.Controls.Add(new Label() { Text = fList[i].releaseDate.ToShortDateString(), AutoEllipsis = true }, 3, i + 1);
                filmTable.Controls.Add(new Label() { Text = fList[i].dailyRate + "", AutoEllipsis = true }, 4, i + 1);
                Button b = new Button() { Text = "Details" };
                b.Name = fList[i].filmId + "";
                b.Click += B_Click;
                filmTable.Controls.Add(b, 5, i + 1);

            }
            this.Controls.Add(filmTable);
        }

        private void TopBox_KeyUp(object sender, KeyEventArgs e)
        {
            rechercheBox.Text = "";
            string top = topBox.Text;
            try
            {
                int topN = int.Parse(topBox.Text);
                if(topN > 0)
                {
                    List<Film> fList = DbUtility.Instance.getTopFilm(int.Parse(topBox.Text));
                    Console.WriteLine(fList.Count());
                    this.Controls.Remove(filmTable);
                    initFilmList(fList);
                }
                else
                {
                    throw new Exception();
                }
            }
            catch (Exception ex)
            {
                this.Controls.Remove(filmTable);
                initFilmList(DbUtility.Instance.filmList);
            }
            
        }

        private void CurrentRent_Click(object sender, EventArgs e)
        {
            initCurrentLocationPanel();
        }

        private void AddFilm_Click(object sender, EventArgs e)
        {
            initAddFilmPanel();
        }

        private void B_Click(object sender, EventArgs e)
        {
            int id = int.Parse(((Button)sender).Name);
            initEditFilmPanel(id);
        }

        private void RechercheBox_KeyUp(object sender, KeyEventArgs e)
        {
            topBox.Text = "";
            List<Film> fList = DbUtility.Instance.filterName(((TextBox)sender).Text,rechercheCombo.SelectedIndex);
            this.Controls.Remove(filmTable);
            initFilmList(fList);
        }


        //AddFilm
        private Button retourFilms;
        private Label titleLabel;
        private TextBox titleBox;
        private Label releaseLabel;
        private DateTimePicker release;
        private Label directorLabel;
        private ComboBox directorCombo;
        private Label genreLabel;
        private ComboBox genreCombo;
        private Label dailyPriceLabel;
        private TextBox dailyPriceBox;
        private Label dailyPriceLabel2;
        private Button addFilmButton;

        private void initAddFilmPanel()
        {
            this.Controls.Clear();

            titre = new Label();
            titre.Text = "Ajout films";
            titre.Location = new Point(325, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            titre.Size = new System.Drawing.Size(250, 50);
            this.Controls.Add(titre);

            retourFilms = new Button();
            retourFilms.Text = "Retour aux films";
            retourFilms.Location = new Point(25, 50);
            retourFilms.Font = new Font(titre.Font.FontFamily, 10); ;
            retourFilms.Size = new System.Drawing.Size(150, 30);
            retourFilms.Click += RetourFilms_Click;
            this.Controls.Add(retourFilms);

            titleLabel = new Label();
            titleLabel.Text = "Titre : ";
            titleLabel.Location = new Point(50, 100);
            titleLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            titleLabel.Size = new System.Drawing.Size(45, 40);
            this.Controls.Add(titleLabel);

            titleBox = new TextBox();
            titleBox.Text = "";
            titleBox.Location = new Point(95, 100);
            titleBox.Font = new Font(titre.Font.FontFamily, 10);
            titleBox.Size = new System.Drawing.Size(180, 40);
            this.Controls.Add(titleBox);

            releaseLabel = new Label();
            releaseLabel.Text = "Release Date : ";
            releaseLabel.Location = new Point(300, 100);
            releaseLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            releaseLabel.Size = new System.Drawing.Size(115, 40);
            this.Controls.Add(releaseLabel);

            release = new DateTimePicker();
            release.Location = new Point(420, 100);
            release.Font = new Font(titre.Font.FontFamily, 10);
            release.Size = new System.Drawing.Size(250, 40);
            this.Controls.Add(release);

            directorLabel = new Label();
            directorLabel.Text = "Réalisateur : ";
            directorLabel.Location = new Point(50, 200);
            directorLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            directorLabel.Size = new System.Drawing.Size(90, 40);
            this.Controls.Add(directorLabel);

            directorCombo = new ComboBox();
            directorCombo.Text = "";
            directorCombo.Location = new Point(145, 200);
            directorCombo.Font = new Font(titre.Font.FontFamily, 10);
            directorCombo.Size = new System.Drawing.Size(140, 40);
            for (int i = 0; i < DbUtility.Instance.directorList.Count; i++)
                directorCombo.Items.Add(DbUtility.Instance.directorList[i].name);
            this.Controls.Add(directorCombo);

            genreLabel = new Label();
            genreLabel.Text = "Genre : ";
            genreLabel.Location = new Point(300, 200);
            genreLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            genreLabel.Size = new System.Drawing.Size(60, 40);
            this.Controls.Add(genreLabel);

            genreCombo = new ComboBox();
            genreCombo.Text = "";
            genreCombo.Location = new Point(365, 200);
            genreCombo.Font = new Font(titre.Font.FontFamily, 10);
            genreCombo.Size = new System.Drawing.Size(140, 40);
            for (int i = 0; i < DbUtility.Instance.genreList.Count; i++)
                genreCombo.Items.Add(DbUtility.Instance.genreList[i].name);
            this.Controls.Add(genreCombo);

            dailyPriceLabel = new Label();
            dailyPriceLabel.Text = "Daily Price : ";
            dailyPriceLabel.Location = new Point(50, 300);
            dailyPriceLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            dailyPriceLabel.Size = new System.Drawing.Size(100, 40);
            this.Controls.Add(dailyPriceLabel);

            dailyPriceBox = new TextBox();
            dailyPriceBox.Text = "";
            dailyPriceBox.Location = new Point(150, 300);
            dailyPriceBox.Font = new Font(titre.Font.FontFamily, 10);
            dailyPriceBox.Size = new System.Drawing.Size(50, 40);
            this.Controls.Add(dailyPriceBox);

            dailyPriceLabel2 = new Label();
            dailyPriceLabel2.Text = "€";
            dailyPriceLabel2.Location = new Point(210, 300);
            dailyPriceLabel2.Font = new Font(titre.Font.FontFamily, 10); ;
            dailyPriceLabel2.Size = new System.Drawing.Size(10, 40);
            this.Controls.Add(dailyPriceLabel2);

            addFilmButton = new Button();
            addFilmButton.Text = "Ajouter film";
            addFilmButton.Location = new Point(250, 300);
            addFilmButton.Font = new Font(titre.Font.FontFamily, 10); ;
            addFilmButton.Size = new System.Drawing.Size(150, 30);
            addFilmButton.Click += AddFilmButton_Click;
            this.Controls.Add(addFilmButton);
        }
        private void AddFilmButton_Click(object sender, EventArgs e)
        {
            DbUtility.Instance.addFilm(titleBox.Text,release.Value,directorCombo.Text,genreCombo.Text, dailyPriceBox.Text);
            initFilmPanel();
        }

        private void RetourFilms_Click(object sender, EventArgs e)
        {
            initFilmPanel();
        }

        //EditFilm
        private Button editFilmButton;
        private int filmId;
        private Film f;

        private Label rentLabel;
        private Button rentButton;
        private Label rentInfo;

        private Button deleteFilmButton;

        private TableLayoutPanel rentHistory;

        private void initEditFilmPanel(int id)
        {
            f = DbUtility.Instance.getFilm(id);
            filmId = f.filmId;

            this.Controls.Clear();

            titre = new Label();
            titre.Text = "Edit films";
            titre.Location = new Point(325, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            titre.Size = new System.Drawing.Size(250, 50);
            this.Controls.Add(titre);

            retourFilms = new Button();
            retourFilms.Text = "Retour aux films";
            retourFilms.Location = new Point(25, 50);
            retourFilms.Font = new Font(titre.Font.FontFamily, 10); ;
            retourFilms.Size = new System.Drawing.Size(150, 30);
            retourFilms.Click += RetourFilms_Click;
            this.Controls.Add(retourFilms);

            titleLabel = new Label();
            titleLabel.Text = "Titre : ";
            titleLabel.Location = new Point(50, 100);
            titleLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            titleLabel.Size = new System.Drawing.Size(45, 40);
            this.Controls.Add(titleLabel);

            titleBox = new TextBox();
            titleBox.Text = f.title;
            titleBox.Location = new Point(95, 100);
            titleBox.Font = new Font(titre.Font.FontFamily, 10);
            titleBox.Size = new System.Drawing.Size(180, 40);
            this.Controls.Add(titleBox);

            releaseLabel = new Label();
            releaseLabel.Text = "Release Date : ";
            releaseLabel.Location = new Point(300, 100);
            releaseLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            releaseLabel.Size = new System.Drawing.Size(115, 40);
            this.Controls.Add(releaseLabel);

            release = new DateTimePicker();
            release.Value = f.releaseDate;
            release.Location = new Point(420, 100);
            release.Font = new Font(titre.Font.FontFamily, 10);
            release.Size = new System.Drawing.Size(250, 40);
            this.Controls.Add(release);

            directorLabel = new Label();
            directorLabel.Text = "Réalisateur : ";
            directorLabel.Location = new Point(50, 200);
            directorLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            directorLabel.Size = new System.Drawing.Size(90, 40);
            this.Controls.Add(directorLabel);

            directorCombo = new ComboBox();
            directorCombo.Text = f.director.name;
            directorCombo.Location = new Point(145, 200);
            directorCombo.Font = new Font(titre.Font.FontFamily, 10);
            directorCombo.Size = new System.Drawing.Size(140, 40);
            for (int i = 0; i < DbUtility.Instance.directorList.Count; i++)
                directorCombo.Items.Add(DbUtility.Instance.directorList[i].name);
            this.Controls.Add(directorCombo);

            genreLabel = new Label();
            genreLabel.Text = "Genre : ";
            genreLabel.Location = new Point(300, 200);
            genreLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            genreLabel.Size = new System.Drawing.Size(60, 40);
            this.Controls.Add(genreLabel);

            genreCombo = new ComboBox();
            genreCombo.Text = f.genre.name;
            genreCombo.Location = new Point(365, 200);
            genreCombo.Font = new Font(titre.Font.FontFamily, 10);
            genreCombo.Size = new System.Drawing.Size(140, 40);
            for (int i = 0; i < DbUtility.Instance.genreList.Count; i++)
                genreCombo.Items.Add(DbUtility.Instance.genreList[i].name);
            this.Controls.Add(genreCombo);

            dailyPriceLabel = new Label();
            dailyPriceLabel.Text = "Daily Price : ";
            dailyPriceLabel.Location = new Point(50, 300);
            dailyPriceLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            dailyPriceLabel.Size = new System.Drawing.Size(100, 40);
            this.Controls.Add(dailyPriceLabel);

            dailyPriceBox = new TextBox();
            dailyPriceBox.Text = f.dailyRate+"";
            dailyPriceBox.Location = new Point(150, 300);
            dailyPriceBox.Font = new Font(titre.Font.FontFamily, 10);
            dailyPriceBox.Size = new System.Drawing.Size(50, 40);
            this.Controls.Add(dailyPriceBox);

            dailyPriceLabel2 = new Label();
            dailyPriceLabel2.Text = "€";
            dailyPriceLabel2.Location = new Point(210, 300);
            dailyPriceLabel2.Font = new Font(titre.Font.FontFamily, 10); ;
            dailyPriceLabel2.Size = new System.Drawing.Size(10, 40);
            this.Controls.Add(dailyPriceLabel2);

            editFilmButton = new Button();
            editFilmButton.Text = "Edit film";
            editFilmButton.Location = new Point(250, 300);
            editFilmButton.Font = new Font(titre.Font.FontFamily, 10); ;
            editFilmButton.Size = new System.Drawing.Size(150, 30);
            editFilmButton.Click += EditFilmButton_Click;
            this.Controls.Add(editFilmButton);

            deleteFilmButton = new Button();
            deleteFilmButton.Text = "delete film";
            deleteFilmButton.ForeColor = Color.Red;
            deleteFilmButton.Location = new Point(400, 300);
            deleteFilmButton.Font = new Font(titre.Font.FontFamily, 10); ;
            deleteFilmButton.Size = new System.Drawing.Size(150, 30);
            deleteFilmButton.Click += DeleteFilmButton_Click;
            this.Controls.Add(deleteFilmButton);

            rentLabel = new Label();
            rentLabel.Location = new Point(50, 365);
            rentLabel.Font = new Font(titre.Font.FontFamily, 13); ;
            rentLabel.Size = new System.Drawing.Size(150, 30);

            rentButton = new Button();
            rentButton.Font = new Font(titre.Font.FontFamily, 10); ;

            if (DbUtility.Instance.isAvailable(filmId))
            {
                rentLabel.Text = "Film disponible : ";

                rentButton.Text = "Louer ce film";
                rentButton.Location = new Point(250, 365);
                rentButton.Size = new System.Drawing.Size(150, 30);
                rentButton.Click += RentButton_Click;
            }
            else
            {
                rentLabel.Text = "Film loué : ";

                rentInfo = new Label();
                rentInfo.Text = DbUtility.Instance.getRentInfo(filmId);
                rentInfo.Location = new Point(150, 370);
                rentInfo.Font = new Font(titre.Font.FontFamily, 10); ;
                rentInfo.Size = new System.Drawing.Size(500, 20);
                this.Controls.Add(rentInfo);

                rentButton.Text = "Rendre ce film";
                rentButton.Location = new Point(150, 400);
                rentButton.Size = new System.Drawing.Size(150, 30);
                rentButton.Click += RentButton_Click1; ;
            }
            this.Controls.Add(rentLabel);
            this.Controls.Add(rentButton);

            initRentalHistory(filmId);
        }

        private void DeleteFilmButton_Click(object sender, EventArgs e)
        {
            DbUtility.Instance.deleteFilm(filmId);
            initFilmPanel();
        }

        private void initRentalHistory(int id)
        {
            List<List<string>> infos = DbUtility.Instance.rentHistory(id);
            rentHistory = new TableLayoutPanel();
            rentHistory.ColumnCount = 4;
            rentHistory.RowCount = infos.Count + 1;
            rentHistory.Location = new Point(50, 450);
            rentHistory.Size = new System.Drawing.Size(700, 150);
            rentHistory.AutoScroll = true;
            rentHistory.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            //th
            rentHistory.Controls.Add(new Label() { Text = "Client", Font = new Font(rentHistory.Font.FontFamily, 12) }, 0, 0);
            rentHistory.Controls.Add(new Label() { Text = "Loué le", Font = new Font(rentHistory.Font.FontFamily, 12) }, 1, 0);
            rentHistory.Controls.Add(new Label() { Text = "limite rendu", Font = new Font(rentHistory.Font.FontFamily, 12) }, 2, 0);
            rentHistory.Controls.Add(new Label() { Text = "rendu le", Font = new Font(rentHistory.Font.FontFamily, 12) }, 3, 0);
            for (int i = 0; i < infos.Count; i++)
            {
                rentHistory.Controls.Add(new Label() { Text = infos[i][0], AutoEllipsis = true }, 0, i + 1);
                rentHistory.Controls.Add(new Label() { Text = infos[i][1], AutoEllipsis = true }, 1, i + 1);
                rentHistory.Controls.Add(new Label() { Text = infos[i][2], AutoEllipsis = true }, 2, i + 1);
                rentHistory.Controls.Add(new Label() { Text = infos[i][3], AutoEllipsis = true }, 3, i + 1);
            }
            this.Controls.Add(rentHistory);
        }

        private void RentButton_Click1(object sender, EventArgs e)
        {
            initFilmReturnPanel(f);
        }

        private void RentButton_Click(object sender, EventArgs e)
        {
            initNewRentPanel();
            for (int i = 0; i < unrentedFilms.Count; i++)
            {
                if (unrentedFilms[i].filmId == filmId)
                {
                    filmCombo.SelectedIndex = i;
                    break;
                }
            }
        }

        private void EditFilmButton_Click(object sender, EventArgs e)
        {
            DbUtility.Instance.editFilm(filmId, titleBox.Text, release.Value, directorCombo.Text, genreCombo.Text, dailyPriceBox.Text);
        }

        //CurrentRent

        public void initCurrentLocationPanel()
        {
            this.Controls.Clear();

            titre = new Label();
            titre.Text = "Liste des films loué en ce moment";
            titre.Location = new Point(225, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            titre.Size = new System.Drawing.Size(350, 50);
            this.Controls.Add(titre);

            retourFilms = new Button();
            retourFilms.Text = "Retour aux films";
            retourFilms.Location = new Point(25, 50);
            retourFilms.Font = new Font(titre.Font.FontFamily, 10); ;
            retourFilms.Size = new System.Drawing.Size(150, 30);
            retourFilms.Click += RetourFilms_Click;
            this.Controls.Add(retourFilms);

            rechercheBoxLabel = new Label();
            rechercheBoxLabel.Text = "Recherche : ";
            rechercheBoxLabel.Location = new Point(590, 50);
            rechercheBoxLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            rechercheBoxLabel.Size = new System.Drawing.Size(85, 40);
            this.Controls.Add(rechercheBoxLabel);

            rechercheBox = new TextBox();
            rechercheBox.Text = "";
            rechercheBox.Location = new Point(675, 50);
            rechercheBox.Font = new Font(titre.Font.FontFamily, 10);
            rechercheBox.Size = new System.Drawing.Size(100, 40);
            rechercheBox.KeyUp += RechercheBox_KeyUp1;
            this.Controls.Add(rechercheBox);

            initRentalFilmList(DbUtility.Instance.getRentedFilms());
        }

        private void initRentalFilmList(Dictionary<List<Film>, List<Rental>> rentalListe)
        {
            List<Film> fList = rentalListe.Keys.ElementAt(0);
            List<Rental> rList = rentalListe.ElementAt(0).Value;

            filmTable = new TableLayoutPanel();
            filmTable.ColumnCount = 5;
            filmTable.RowCount = fList.Count + 1;
            filmTable.Location = new Point(50, 100);
            filmTable.Size = new System.Drawing.Size(700, 400);
            filmTable.AutoScroll = true;
            //filmTable.AutoSize = true;
            filmTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            //th
            filmTable.Controls.Add(new Label() { Text = "Titre", Font = new Font(filmTable.Font.FontFamily, 12) }, 0, 0);
            filmTable.Controls.Add(new Label() { Text = "Client", Font = new Font(filmTable.Font.FontFamily, 12) }, 1, 0);
            filmTable.Controls.Add(new Label() { Text = "Emprunté le", Font = new Font(filmTable.Font.FontFamily, 12) }, 2, 0);
            filmTable.Controls.Add(new Label() { Text = "Date limite rendu", Font = new Font(filmTable.Font.FontFamily, 12) }, 3, 0);
            for (int i = 0; i < fList.Count; i++)
            {
                //filmTable.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute,100));
                //filmTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                filmTable.Controls.Add(new Label() { Text = fList[i].title, AutoEllipsis = true, Width = 195 }, 0, i + 1);
                filmTable.Controls.Add(new Label() { Text = rList[i].client.name, AutoEllipsis = true }, 1, i + 1);
                filmTable.Controls.Add(new Label() { Text = rList[i].dateRental.ToShortDateString(), AutoEllipsis = true }, 2, i + 1);
                filmTable.Controls.Add(new Label() { Text = rList[i].dateLimiteRendu.ToShortDateString(), AutoEllipsis = true }, 3, i + 1);
                Button b = new Button() { Text = "Details" };
                b.Name = fList[i].filmId + "";
                b.Click += B_Click;
                filmTable.Controls.Add(b, 5, i + 1);

            }
            this.Controls.Add(filmTable);
        }

        private void RechercheBox_KeyUp1(object sender, KeyEventArgs e)
        {
            Dictionary<List<Film>, List<Rental>> fList = DbUtility.Instance.filterRentedName(((TextBox)sender).Text);
            Console.WriteLine(fList.ElementAt(0).Key.Count);
            this.Controls.Remove(filmTable);
            initRentalFilmList(fList);
        }

        /*ClientListPanel*/

        private TableLayoutPanel clientTable;
        private Label newClientLabel;
        private Label newNameLabel;
        private Label newAddressLabel;
        private Label newCityLabel;
        private Label newZipLabel;
        private TextBox newName;
        private TextBox newAddress;
        private TextBox newCity;
        private TextBox newZip;
        private Button newClientButton;


        private void initClientListPanel()
        {
            this.Controls.Clear();

            titre = new Label();
            titre.Text = "Liste des Clients";
            titre.Location = new Point(325, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            titre.Size = new System.Drawing.Size(250, 50);
            this.Controls.Add(titre);

            retourFilms = new Button();
            retourFilms.Text = "Retour aux films";
            retourFilms.Location = new Point(25, 50);
            retourFilms.Font = new Font(titre.Font.FontFamily, 10); ;
            retourFilms.Size = new System.Drawing.Size(150, 30);
            retourFilms.Click += RetourFilms_Click;
            this.Controls.Add(retourFilms);

            initClientList();

            newClientLabel = new Label();
            newClientLabel.Text = "Nouveau Client ";
            newClientLabel.Location = new Point(625, 60);
            newClientLabel.Font = new Font(titre.Font.FontFamily, 12); ;
            newClientLabel.Size = new System.Drawing.Size(250, 20);
            this.Controls.Add(newClientLabel);

            newNameLabel = new Label();
            newNameLabel.Text = "Nom : ";
            newNameLabel.Location = new Point(625, 100);
            newNameLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            newNameLabel.Size = new System.Drawing.Size(50, 50);
            this.Controls.Add(newNameLabel);

            newAddressLabel = new Label();
            newAddressLabel.Text = "Adresse : ";
            newAddressLabel.Location = new Point(603, 150);
            newAddressLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            newAddressLabel.Size = new System.Drawing.Size(70, 50);
            this.Controls.Add(newAddressLabel);

            newCityLabel = new Label();
            newCityLabel.Text = "City : ";
            newCityLabel.Location = new Point(627, 200);
            newCityLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            newCityLabel.Size = new System.Drawing.Size(50, 50);
            this.Controls.Add(newCityLabel);

            newZipLabel = new Label();
            newZipLabel.Text = "ZIP : ";
            newZipLabel.Location = new Point(625, 250);
            newZipLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            newZipLabel.Size = new System.Drawing.Size(50, 50);
            this.Controls.Add(newZipLabel);

            newName = new TextBox();
            newName.Text = "";
            newName.Location = new Point(675, 100);
            newName.Font = new Font(titre.Font.FontFamily, 10);
            newName.Size = new System.Drawing.Size(100, 10);
            this.Controls.Add(newName);

            newAddress = new TextBox();
            newAddress.Text = "";
            newAddress.Location = new Point(675, 150);
            newAddress.Font = new Font(titre.Font.FontFamily, 10);
            newAddress.Size = new System.Drawing.Size(100, 30);
            this.Controls.Add(newAddress);

            newCity = new TextBox();
            newCity.Text = "";
            newCity.Location = new Point(675, 200);
            newCity.Font = new Font(titre.Font.FontFamily, 10);
            newCity.Size = new System.Drawing.Size(100, 30);
            this.Controls.Add(newCity);

            newZip = new TextBox();
            newZip.Text = "";
            newZip.Location = new Point(675, 250);
            newZip.Font = new Font(titre.Font.FontFamily, 10);
            newZip.Size = new System.Drawing.Size(100, 30);
            this.Controls.Add(newZip);

            newClientButton = new Button();
            newClientButton.Text = "Ajouter Client";
            newClientButton.Location = new Point(600, 300);
            newClientButton.Font = new Font(titre.Font.FontFamily, 10);
            newClientButton.Size = new System.Drawing.Size(175, 30);
            newClientButton.Click += NewClientButton_Click;
            this.Controls.Add(newClientButton);


        }

        private void NewClientButton_Click(object sender, EventArgs e)
        {
            DbUtility.Instance.addClient(newName.Text, newAddress.Text, newCity.Text, newZip.Text);
            this.Controls.Remove(clientTable);
            initClientList();
        }

        private void initClientList()
        {
            List<Client> cList = DbUtility.Instance.clientList;
            clientTable = new TableLayoutPanel();
            clientTable.ColumnCount = 5;
            clientTable.RowCount = cList.Count + 1;
            clientTable.Location = new Point(50, 100);
            clientTable.Size = new System.Drawing.Size(525, 400);
            clientTable.AutoScroll = true;
            clientTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            //th
            clientTable.Controls.Add(new Label() { Text = "Nom", Font = new Font(clientTable.Font.FontFamily, 12) }, 0, 0);
            clientTable.Controls.Add(new Label() { Text = "Adresse", Font = new Font(clientTable.Font.FontFamily, 12) }, 1, 0);
            clientTable.Controls.Add(new Label() { Text = "Ville", Font = new Font(clientTable.Font.FontFamily, 12) }, 2, 0);
            clientTable.Controls.Add(new Label() { Text = "ZIP", Font = new Font(clientTable.Font.FontFamily, 12) }, 3, 0);
            for (int i = 0; i < cList.Count; i++)
            {
                clientTable.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
                clientTable.RowStyles.Add(new RowStyle(SizeType.AutoSize));
                clientTable.Controls.Add(new Label() { Text = cList[i].name, AutoEllipsis = true }, 0, i + 1);
                clientTable.Controls.Add(new Label() { Text = cList[i].address, AutoEllipsis = true }, 1, i + 1);
                clientTable.Controls.Add(new Label() { Text = cList[i].city, AutoEllipsis = true }, 2, i + 1);
                clientTable.Controls.Add(new Label() { Text = cList[i].zip, AutoEllipsis = true }, 3, i + 1);
                Button b = new Button() { Text = "Details" };
                b.Name = cList[i].clientId + "";
                b.Click += B_Click1; ;
                clientTable.Controls.Add(b, 4, i + 1);

            }

            this.Controls.Add(clientTable);
        }

        private void B_Click1(object sender, EventArgs e)
        {
            initClientDetailPanel(int.Parse(((Button)sender).Name));
        }

        /*ClientDetailPanel*/

        Button retourClients;
        TextBox nameBox;
        Label addressLabel;
        TextBox addressBox;
        Label cityLabel;
        TextBox cityBox;
        Label zipLabel;
        TextBox zipBox;
        Button editClientButton;
        Button deleteClientButton;

        int clientId;

        private void initClientDetailPanel(int id)
        {
            Client c = DbUtility.Instance.getClient(id);
            clientId = c.clientId;

            this.Controls.Clear();

            titre = new Label();
            titre.Text = "Client Détails";
            titre.Location = new Point(325, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            titre.Size = new System.Drawing.Size(250, 50);
            this.Controls.Add(titre);

            retourClients = new Button();
            retourClients.Text = "Retour aux clients";
            retourClients.Location = new Point(25, 50);
            retourClients.Font = new Font(titre.Font.FontFamily, 10); ;
            retourClients.Size = new System.Drawing.Size(150, 30);
            retourClients.Click += RetourClients_Click;
            this.Controls.Add(retourClients);

            titleLabel = new Label();
            titleLabel.Text = "Nom : ";
            titleLabel.Location = new Point(50, 100);
            titleLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            titleLabel.Size = new System.Drawing.Size(45, 40);
            this.Controls.Add(titleLabel);

            nameBox = new TextBox();
            nameBox.Text = c.name;
            nameBox.Location = new Point(95, 100);
            nameBox.Font = new Font(titre.Font.FontFamily, 10);
            nameBox.Size = new System.Drawing.Size(180, 40);
            this.Controls.Add(nameBox);

            addressLabel = new Label();
            addressLabel.Text = "Adresse : ";
            addressLabel.Location = new Point(300, 100);
            addressLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            addressLabel.Size = new System.Drawing.Size(115, 40);
            this.Controls.Add(addressLabel);

            addressBox = new TextBox();
            addressBox.Text = c.address;
            addressBox.Location = new Point(420, 100);
            addressBox.Font = new Font(titre.Font.FontFamily, 10);
            addressBox.Size = new System.Drawing.Size(250, 40);
            this.Controls.Add(addressBox);

            cityLabel = new Label();
            cityLabel.Text = "City : ";
            cityLabel.Location = new Point(50, 150);
            cityLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            cityLabel.Size = new System.Drawing.Size(90, 40);
            this.Controls.Add(cityLabel);

            cityBox = new TextBox();
            cityBox.Text = c.city;
            cityBox.Location = new Point(145, 150);
            cityBox.Font = new Font(titre.Font.FontFamily, 10);
            cityBox.Size = new System.Drawing.Size(140, 40);
            this.Controls.Add(cityBox);

            zipLabel = new Label();
            zipLabel.Text = "Zip : ";
            zipLabel.Location = new Point(300, 150);
            zipLabel.Font = new Font(titre.Font.FontFamily, 10); ;
            zipLabel.Size = new System.Drawing.Size(60, 40);
            this.Controls.Add(zipLabel);

            zipBox = new TextBox();
            zipBox.Text = c.zip;
            zipBox.Location = new Point(365, 150);
            zipBox.Font = new Font(titre.Font.FontFamily, 10);
            zipBox.Size = new System.Drawing.Size(140, 40);
            this.Controls.Add(zipBox);

            editClientButton = new Button();
            editClientButton.Text = "Edit Client";
            editClientButton.Location = new Point(50, 200);
            editClientButton.Font = new Font(titre.Font.FontFamily, 10); ;
            editClientButton.Size = new System.Drawing.Size(150, 30);
            editClientButton.Click += EditClientButton_Click;
            this.Controls.Add(editClientButton);

            deleteClientButton = new Button();
            deleteClientButton.Text = "delete client";
            deleteClientButton.ForeColor = Color.Red;
            deleteClientButton.Location = new Point(220, 200);
            deleteClientButton.Font = new Font(titre.Font.FontFamily, 10); ;
            deleteClientButton.Size = new System.Drawing.Size(150, 30);
            deleteClientButton.Click += DeleteClientButton_Click;
            this.Controls.Add(deleteClientButton);

            initClientRentedFilm(clientId);
        }

        private void initClientRentedFilm(int cId)
        {
            List<List<string>> infos = DbUtility.Instance.clientRentHistory(cId);
            rentHistory = new TableLayoutPanel();
            rentHistory.ColumnCount = 4;
            rentHistory.RowCount = infos.Count + 1;
            rentHistory.Location = new Point(50, 250);
            rentHistory.Size = new System.Drawing.Size(700, 350);
            rentHistory.AutoScroll = true;
            rentHistory.CellBorderStyle = TableLayoutPanelCellBorderStyle.Inset;

            //th
            rentHistory.Controls.Add(new Label() { Text = "Film", Font = new Font(rentHistory.Font.FontFamily, 12) }, 0, 0);
            rentHistory.Controls.Add(new Label() { Text = "Loué le", Font = new Font(rentHistory.Font.FontFamily, 12) }, 1, 0);
            rentHistory.Controls.Add(new Label() { Text = "limite rendu", Font = new Font(rentHistory.Font.FontFamily, 12) }, 2, 0);
            rentHistory.Controls.Add(new Label() { Text = "rendu le", Font = new Font(rentHistory.Font.FontFamily, 12) }, 3, 0);
            for (int i = 0; i < infos.Count; i++)
            {
                rentHistory.Controls.Add(new Label() { Text = infos[i][0], AutoEllipsis = true }, 0, i + 1);
                rentHistory.Controls.Add(new Label() { Text = infos[i][1], AutoEllipsis = true }, 1, i + 1);
                rentHistory.Controls.Add(new Label() { Text = infos[i][2], AutoEllipsis = true }, 2, i + 1);
                rentHistory.Controls.Add(new Label() { Text = infos[i][3], AutoEllipsis = true }, 3, i + 1);
            }
            this.Controls.Add(rentHistory);
        }

        private void DeleteClientButton_Click(object sender, EventArgs e)
        {
            DbUtility.Instance.deleteClient(clientId);
            initClientListPanel();
        }

        private void EditClientButton_Click(object sender, EventArgs e)
        {
            DbUtility.Instance.editClient(clientId, nameBox.Text, addressBox.Text, cityBox.Text, zipBox.Text);
        }

        private void RetourClients_Click(object sender, EventArgs e)
        {
            initClientListPanel();
        }

        /*Add Rent*/

        private Label filmLabel;
        private Label clientLabel;
        private Label limitDateLabel;
        private Label previewLabel;
        private ComboBox filmCombo;
        private ComboBox clientCombo;
        private DateTimePicker limitDate;
        private Button validateRent;

        private List<Film> unrentedFilms;

        private void initNewRentPanel()
        {
            this.Controls.Clear();

            unrentedFilms = DbUtility.Instance.getUnrentedFilms();

            titre = new Label();
            titre.Text = "Nouvelle Location";
            titre.Location = new Point(315, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            titre.Size = new System.Drawing.Size(250, 50);
            this.Controls.Add(titre);

            retourFilms = new Button();
            retourFilms.Text = "Retour aux films";
            retourFilms.Location = new Point(25, 50);
            retourFilms.Font = new Font(titre.Font.FontFamily, 10); ;
            retourFilms.Size = new System.Drawing.Size(150, 30);
            retourFilms.Click += RetourFilms_Click;
            this.Controls.Add(retourFilms);

            filmLabel = new Label();
            filmLabel.Text = "Film : ";
            filmLabel.Location = new Point(50, 130);
            filmLabel.Font = new Font(titre.Font.FontFamily, 13); ;
            filmLabel.Size = new System.Drawing.Size(54, 50);
            this.Controls.Add(filmLabel);

            filmCombo = new ComboBox();
            filmCombo.Text = "";
            filmCombo.Location = new Point(105, 130);
            filmCombo.Font = new Font(titre.Font.FontFamily, 10);
            filmCombo.Size = new System.Drawing.Size(250, 40);
            for (int i = 0; i < unrentedFilms.Count; i++)
                filmCombo.Items.Add(unrentedFilms[i].title);
            filmCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            filmCombo.SelectedIndexChanged += FilmCombo_SelectedIndexChanged;
            this.Controls.Add(filmCombo);

            clientLabel = new Label();
            clientLabel.Text = "Client : ";
            clientLabel.Location = new Point(380, 130);
            clientLabel.Font = new Font(titre.Font.FontFamily, 13); ;
            clientLabel.Size = new System.Drawing.Size(70, 50);
            this.Controls.Add(clientLabel);

            clientCombo = new ComboBox();
            clientCombo.Text = "";
            clientCombo.Location = new Point(455, 130);
            clientCombo.Font = new Font(titre.Font.FontFamily, 10);
            clientCombo.Size = new System.Drawing.Size(250, 40);
            for (int i = 0; i < DbUtility.Instance.clientList.Count; i++)
                clientCombo.Items.Add(DbUtility.Instance.clientList[i].name);
            clientCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            this.Controls.Add(clientCombo);

            limitDateLabel = new Label();
            limitDateLabel.Text = "Date limite rendu : ";
            limitDateLabel.Location = new Point(50, 180);
            limitDateLabel.Font = new Font(titre.Font.FontFamily, 13);
            limitDateLabel.Size = new System.Drawing.Size(175, 40);
            this.Controls.Add(limitDateLabel);

            limitDate = new DateTimePicker();
            limitDate.Location = new Point(230, 180);
            limitDate.Font = new Font(titre.Font.FontFamily, 10);
            limitDate.ValueChanged += LimitDate_ValueChanged;
            this.Controls.Add(limitDate);

            previewLabel = new Label();
            previewLabel.Text = "";
            previewLabel.Location = new Point(50, 250);
            previewLabel.Font = new Font(titre.Font.FontFamily, 11);
            previewLabel.Size = new System.Drawing.Size(250, 40);
            this.Controls.Add(previewLabel);

            validateRent = new Button();
            validateRent.Text = " Valider Location";
            validateRent.Location = new Point(50, 300);
            validateRent.Font = new Font(titre.Font.FontFamily, 13);
            validateRent.Size = new System.Drawing.Size(175, 40);
            validateRent.Click += ValidateRent_Click;
            this.Controls.Add(validateRent);
        }

        private void ValidateRent_Click(object sender, EventArgs e)
        {
            try
            {
                DbUtility.Instance.addRental(unrentedFilms[filmCombo.SelectedIndex], clientCombo.SelectedIndex, limitDate.Value);
                initCurrentLocationPanel();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception ValidateRent_Click : " + ex.Message);
            }
        }

        DateTime dateLimite = DateTime.Now;
        Film selectedF;
        private void LimitDate_ValueChanged(object sender, EventArgs e)
        {
            dateLimite = ((DateTimePicker)sender).Value;
            setPreviewLabel();
        }

        private void FilmCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = ((ComboBox)sender).SelectedIndex;
            selectedF = unrentedFilms[index];
            setPreviewLabel();
        }

        private void setPreviewLabel()
        {
            try
            {
                TimeSpan duration = new DateTime(dateLimite.Year, dateLimite.Month, dateLimite.Day) - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
                previewLabel.Text = "Prix : " + selectedF.dailyRate + "(€/J) x " + Math.Round(duration.TotalDays) + "(J) = " + (selectedF.dailyRate * Math.Truncate(duration.TotalDays)) + "€";
            }
            catch
            {

            }
        }

        /*Retour film*/

        private Button rentBack;
        private Label filmLabel2;
        private Label clientLabel2;
        private Label limitDateLabel2;
        private Label dateLabel;
        private Label dateLabel2;

        Rental endedRent = null;

        private void initFilmReturnPanel(Film f)
        {
            this.Controls.Clear();
            
            KeyValuePair<List<Film>, List<Rental>> rs = DbUtility.Instance.getRentedFilms().ElementAt(0);
            for (int i = 0; i < rs.Key.Count; i++)
            {
                if (rs.Key.ElementAt(i).filmId == f.filmId)
                {
                    endedRent = rs.Value.ElementAt(i);
                    break;
                }
            }

            titre = new Label();
            titre.Text = "Rendu film";
            titre.Location = new Point(315, 25);
            titre.Font = new Font(titre.Font.FontFamily, 16); ;
            titre.Size = new System.Drawing.Size(250, 50);
            this.Controls.Add(titre);

            retourFilms = new Button();
            retourFilms.Text = "Retour aux films";
            retourFilms.Location = new Point(25, 50);
            retourFilms.Font = new Font(titre.Font.FontFamily, 10); ;
            retourFilms.Size = new System.Drawing.Size(150, 30);
            retourFilms.Click += RetourFilms_Click;
            this.Controls.Add(retourFilms);
            
            filmLabel = new Label();
            filmLabel.Text = "Film : ";
            filmLabel.Location = new Point(50, 130);
            filmLabel.Font = new Font(titre.Font.FontFamily, 13); ;
            filmLabel.Size = new System.Drawing.Size(54, 50);
            this.Controls.Add(filmLabel);

            filmLabel2 = new Label();
            filmLabel2.Text = f.title;
            filmLabel2.Location = new Point(105, 133);
            filmLabel2.Font = new Font(titre.Font.FontFamily, 10);
            filmLabel2.Size = new System.Drawing.Size(250, 40);
            this.Controls.Add(filmLabel2);

            clientLabel = new Label();
            clientLabel.Text = "Client : ";
            clientLabel.Location = new Point(380, 130);
            clientLabel.Font = new Font(titre.Font.FontFamily, 13); ;
            clientLabel.Size = new System.Drawing.Size(70, 50);
            this.Controls.Add(clientLabel);

            clientLabel2 = new Label();
            clientLabel2.Text = endedRent.client.name;
            clientLabel2.Location = new Point(450, 133);
            clientLabel2.Font = new Font(titre.Font.FontFamily, 10); ;
            clientLabel2.Size = new System.Drawing.Size(200, 50);
            this.Controls.Add(clientLabel2);

            dateLabel = new Label();
            dateLabel.Text = "Date emprunt : ";
            dateLabel.Location = new Point(50, 180);
            dateLabel.Font = new Font(titre.Font.FontFamily, 13);
            dateLabel.Size = new System.Drawing.Size(150, 40);
            this.Controls.Add(dateLabel);

            dateLabel2 = new Label();
            dateLabel2.Text = endedRent.dateRental.ToShortDateString();
            dateLabel2.Location = new Point(200, 183);
            dateLabel2.Font = new Font(titre.Font.FontFamily, 10);
            dateLabel2.Size = new System.Drawing.Size(175, 40);
            this.Controls.Add(dateLabel2);

            limitDateLabel = new Label();
            limitDateLabel.Text = "Date limite rendu : ";
            limitDateLabel.Location = new Point(380, 180);
            limitDateLabel.Font = new Font(titre.Font.FontFamily, 13);
            limitDateLabel.Size = new System.Drawing.Size(170, 40);
            this.Controls.Add(limitDateLabel);

            limitDateLabel2 = new Label();
            limitDateLabel2.Text = endedRent.dateLimiteRendu.ToShortDateString();
            limitDateLabel2.Location = new Point(545, 183);
            limitDateLabel2.Font = new Font(titre.Font.FontFamily, 10);
            limitDateLabel2.Size = new System.Drawing.Size(168, 40);
            this.Controls.Add(limitDateLabel2);

            TimeSpan duration = new DateTime(endedRent.dateLimiteRendu.Year, endedRent.dateLimiteRendu.Month, endedRent.dateLimiteRendu.Day) - new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            previewLabel = new Label();
            previewLabel.Text = "Balance : " + f.dailyRate * Math.Truncate(duration.TotalDays) + "€";
            previewLabel.Location = new Point(50, 250);
            previewLabel.Font = new Font(titre.Font.FontFamily, 10);
            previewLabel.Size = new System.Drawing.Size(175, 40);
            this.Controls.Add(previewLabel);

            rentBack = new Button();
            rentBack.Text = "Film rendu";
            rentBack.Location = new Point(50, 300);
            rentBack.Font = new Font(titre.Font.FontFamily, 13);
            rentBack.Size = new System.Drawing.Size(175, 40);
            rentBack.Click += RentBack_Click;
            this.Controls.Add(rentBack);
        }

        private void RentBack_Click(object sender, EventArgs e)
        {
            DbUtility.Instance.endRental(endedRent);
            initFilmPanel();
        }
    }
}
