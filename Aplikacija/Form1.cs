using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Aplikacija
{
    public partial class Form1 : Form
    {
        public int Red = 0;
        DataTable film = new DataTable();
        string cs = "Data source=.; Initial catalog=Forma; Integrated security=true";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SqlConnection veza = new SqlConnection(cs);
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID, naziv, trajanje, zanr, naziv_originala, zemlja FROM Filmovi ORDER BY ID", veza);
            adapter.Fill(film);
            FillBoxes(Red);
        }

        private void FillBoxes(int id)
        {
            if (film.Rows.Count == 0)
            {
                boxId.Text = "";
                boxNaziv.Text = "";
                boxTrajanje.Text = "";
                boxZanr.Text = "";
                boxOrig.Text = "";
                boxZemlja.Text = "";

                btnNext.Enabled = false;
                btnEnd.Enabled = false;
                btnPrev.Enabled = false;
                btnBegin.Enabled = false;
            }
            else
            {
                boxId.Text = film.Rows[Red]["ID"].ToString();
                boxNaziv.Text = film.Rows[Red]["naziv"].ToString();
                boxTrajanje.Text = film.Rows[Red]["trajanje"].ToString();
                boxZanr.Text = film.Rows[Red]["zanr"].ToString();
                boxOrig.Text = film.Rows[Red]["naziv_originala"].ToString();
                boxZemlja.Text = film.Rows[Red]["zemlja"].ToString();

                btnNext.Enabled = (Red != film.Rows.Count - 1);
                btnEnd.Enabled = (Red != film.Rows.Count - 1);

                btnPrev.Enabled = (Red != 0);
                btnBegin.Enabled = (Red != 0);
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            Red++;
            FillBoxes(Red);
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            Red--;
            FillBoxes(Red);
        }

        private void btnBegin_Click(object sender, EventArgs e)
        {
            Red = 0;
            FillBoxes(Red);
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Red = film.Rows.Count - 1;
            FillBoxes(Red);
        }

        private void btnDodaj_Click(object sender, EventArgs e)
        {
            string komanda = "INSERT INTO Filmovi (naziv, trajanje, zanr, naziv_originala, zemlja) VALUES ('"
                + boxNaziv.Text
                + "', '" + boxTrajanje.Text
                + "', '" + boxZanr.Text
                + "', '" + boxOrig.Text
                + "', '" + boxZemlja.Text + "')";

            SqlConnection veza = new SqlConnection(cs);
            SqlCommand dodaj = new SqlCommand(komanda, veza);

            veza.Open();
            dodaj.ExecuteNonQuery();
            veza.Close();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID, naziv, trajanje, zanr, naziv_originala, zemlja FROM Filmovi ORDER BY ID", veza);
            film.Clear();
            adapter.Fill(film);
            Red = film.Rows.Count - 1;
            FillBoxes(Red);
        }

        private void btnObrisi_Click(object sender, EventArgs e)
        {
            string komanda = "DELETE FROM Filmovi WHERE ID = " + boxId.Text;

            SqlConnection veza = new SqlConnection(cs);
            SqlCommand dodaj = new SqlCommand(komanda, veza);

            veza.Open();
            dodaj.ExecuteNonQuery();
            veza.Close();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID, naziv, trajanje, zanr, naziv_originala, zemlja FROM Filmovi ORDER BY ID", veza);
            film.Clear();
            adapter.Fill(film);
            if (Red > film.Rows.Count - 1)
            {
                Red = film.Rows.Count - 1;
            }
            FillBoxes(Red);
        }

        private void btnIzmeni_Click(object sender, EventArgs e)
        {
            string komanda = "UPDATE Filmovi SET naziv = '"
                + boxNaziv.Text
                + "', trajanje = '" + boxTrajanje.Text
                + "', zanr = '" + boxZanr.Text
                + "', naziv_originala = '" + boxOrig.Text
                + "', zemlja = '" + boxZemlja.Text
                + "' WHERE id = " + boxId.Text;


            SqlConnection veza = new SqlConnection(cs);
            SqlCommand dodaj = new SqlCommand(komanda, veza);

            veza.Open();
            dodaj.ExecuteNonQuery();
            veza.Close();

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID, naziv, trajanje, zanr, naziv_originala, zemlja FROM Filmovi ORDER BY ID", veza);
            film.Clear();
            adapter.Fill(film);
            FillBoxes(Red);
        }
    }
}
