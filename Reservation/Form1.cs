using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Reservation.Admin;
using static Reservation.Client;
using MySql.Data.MySqlClient;

namespace Reservation
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            ajout.Visible = false;

            toutRadio.Checked = true;
            codeError.Visible = false;
            mdpError.Visible = false;

            codeError2.Visible = false;
            nomVide.Visible = false;
            confirmError.Visible = false;
            mdp222.Visible = false;

            label24.Visible = false;
            xxx.Visible = false;
        }
        private void accueil_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            searchBox.Text = "";

            sallePanel.Visible = false;
            filmMenu.Visible = false;

            numPlaceB.Text = "";
            nbPlace.Value = 1;
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select diffuser.titre, diffuser.numSalle, diffuser.dateDiff, diffuser.heureDiff, film.poster from diffuser, film where diffuser.titre = film.titre";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void toutRadio_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            searchBox.Text = "";
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=";
                string Query = "select diffuser.titre, diffuser.numSalle, diffuser.dateDiff, diffuser.heureDiff, film.poster from cinema.diffuser, cinema.film where cinema.diffuser.titre = cinema.film.titre";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void todayRadio_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            searchBox.Text = "";
            try
            {
                string str = "datasource=localhost;port=3306;username=root;password=";
                var dateTime = DateTime.Now;//// 2024-04-13
                var date = dateTime.Date.ToString("yyyy-MM-dd");
                string Query = "select diffuser.titre, diffuser.numSalle, diffuser.dateDiff, diffuser.heureDiff, film.poster from cinema.diffuser, cinema.film where cinema.diffuser.titre = cinema.film.titre and diffuser.dateDiff ='" + date + "'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)  //// COMBO-BOX
        {
            string dayName = "";
            searchBox.Text = "";
            switch (comboBox1.Text)
            {
                case "Lundi": dayName = "Monday";break;
                case "Mardi": dayName = "Tuesday"; break;
                case "Mercredi": dayName = "Wednesday"; break;
                case "Jeudi": dayName = "Thursday"; break;
                case "Vendredi": dayName = "Friday"; break;
                case "Samedi": dayName = "Saturday"; break;
                case "Dimanche": dayName = "Sunday"; break;
            }
            //MessageBox.Show(dayName);
            try
            {
                string str = "datasource=localhost;port=3306;username=root;password=";
                var dateTime = DateTime.Now;
                var date = dateTime.Date.ToString("yyyy-MM-dd");
                string Query = "select diffuser.titre, diffuser.numSalle, diffuser.dateDiff, diffuser.heureDiff, film.poster from cinema.diffuser, cinema.film where cinema.diffuser.titre = cinema.film.titre and dayName(diffuser.dateDiff) ='" + dayName + "'"; //// 2024-04-13

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void searchBox_TextChanged(object sender, EventArgs e)
        {
            //todayRadio.Checked = false;
            //toutRadio.Checked = false;
            comboBox1.Text = "";
            string titre = searchBox.Text;
            try
            {
                string str = "datasource=localhost;port=3306;username=root;password=";
                string Query = "select diffuser.titre, diffuser.numSalle, diffuser.dateDiff, diffuser.heureDiff, film.poster from cinema.diffuser, cinema.film where cinema.diffuser.titre = cinema.film.titre and diffuser.titre like '%" + titre + "%'"; //// 2024-04-13

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                dataGridView1.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

                string titre = row.Cells["titre"].Value.ToString();
                string salle = row.Cells["numSalle"].Value.ToString();
                string date = row.Cells["dateDiff"].Value.ToString();
                string heure = row.Cells["heureDiff"].Value.ToString();


                string str3 = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query3 = "select dateDiff, heureDiff from diffuser where numSalle = '" + salle + "' and dateDiff = '" + date + "' and heureDiff = '" + heure + "' and curDate() <= '"+date+"' and curTime() <= '"+heure+"'";

                MySqlConnection conn3 = new MySqlConnection(str3);
                MySqlCommand cmd3 = new MySqlCommand(Query3, conn3);
                conn3.Open();
                MySqlDataReader r;
                r = cmd3.ExecuteReader();
                /*
                if (r.HasRows)
                {
                */
                    sallePanel.Visible = true;
                    occupation(salle, date, heure, titre);
                /*
                }
                else
                {
                    MessageBox.Show("Film déjà diffusé","Information de diffusion",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                */
                conn3.Close();
            }
        }
        public void occupation(string numSalle, string dateO, string heureO, string titre)
        {
            int nb;
            try
            {
                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select nbPlace from salle where numSalle = '" + numSalle + "'";

                MySqlConnection conn = new MySqlConnection(str);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(Query, conn);
                Object res = cmd.ExecuteScalar();

                if (res != null)
                {
                    nb = Convert.ToInt32(res);
                    int col1 = 0;
                    int i;
                    test.Rows.Clear();
                    for (i = 1; i <= nb; i++)
                    {
                        col1 = i;
                        test.Rows.Add(col1, col1 + 1, col1 + 2, col1 + 3, col1 + 4, col1 + 5, col1 + 6, col1 + 7, col1 + 8, col1 + 9);
                        col1 = col1 + 9;
                        i = col1;
                    }

                    string str2 = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                    for (int c = 1; c <= nb; c++)
                    {
                        string sql = "select occup from place where numSalle = '" + numSalle + "' and dateO = '" + dateO + "' and heureO = '" + heureO + "' and numPlace = '" + c + "'";
                        MySqlConnection conn2 = new MySqlConnection(str2);
                        conn2.Open();
                        MySqlCommand cmd2 = new MySqlCommand(sql, conn2);
                        Object a = cmd2.ExecuteScalar();
                        if (a.ToString() == "Reservee")
                        {
                            for (int ro = 0; ro < 5; ro++)
                            {
                                for (int col = 0; col < 10; col++)
                                {
                                    if (Convert.ToInt32(test.Rows[ro].Cells[col].Value) == c)
                                    {
                                        test.Rows[ro].Cells[col].Style.BackColor = Color.Red;
                                    }
                                }
                            }
                        }
                    }
                }
                switch (numSalle)
                {
                    case "1": numSalleBtn.Text = "Salle 1"; break;
                    case "2": numSalleBtn.Text = "Salle 2"; break;
                    case "3": numSalleBtn.Text = "Salle 3"; break;
                    case "4": numSalleBtn.Text = "Salle 4"; break;
                    case "5": numSalleBtn.Text = "Salle 5"; break;
                    case "6": numSalleBtn.Text = "Salle 6"; break;
                    case "7": numSalleBtn.Text = "Salle 7"; break;
                    case "8": numSalleBtn.Text = "Salle 8"; break;
                    case "9": numSalleBtn.Text = "Salle 9"; break;
                    case "10": numSalleBtn.Text = "Salle 10"; break;
                }

                string str3 = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query3 = "select count(numPlace) from place where numSalle = '" + numSalle + "' and dateO = '" + dateO + "' and heureO = '" + heureO + "' and occup = 'Libre'";

                MySqlConnection conn3 = new MySqlConnection(str3);
                MySqlCommand cmd3 = new MySqlCommand(Query3, conn3);
                conn3.Open();
                Object result = cmd3.ExecuteScalar();
                if (result != null)
                {
                    totalBtn.Text = result.ToString() + "\nPlaces Libres";
                    occupBtn.Text = (50 - Convert.ToInt32(result.ToString())).ToString() + "\nPlaces Reservées";
                    dateOL.Text = dateO;
                    heureOL.Text = heureO;
                    titreL.Text = titre;
                }
                conn3.Close();

                string Query4 = "select prix from film where titre = '" + titre + "'";

                MySqlConnection conn4 = new MySqlConnection(str3);
                MySqlCommand cmd4 = new MySqlCommand(Query4, conn4);
                conn4.Open();
                Object result2 = cmd4.ExecuteScalar();
                if (result2 != null)
                {
                    prixL.Text = result2.ToString() + " Ar";
                }
                conn4.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            try
            {
                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select poster as post from film where titre = '" + titre + "'"; //// 2024-04-13

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();

                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                titreBtn.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            titre2.Text = titre;
            dateDiffL.Text = dateO;
            heureOL.Text = heureO;
            numSall.Text = numSalle;
            nbPlace.Value = 1;
            huereDiffL.Text = heureO;
        }
        private void test_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string numSalle = "";
            if (e.RowIndex >= 0)
            {
                int numero = Convert.ToInt32(test.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                

                switch (numSalleBtn.Text)
                {
                    case "Salle 1": numSalle = "1"; break;
                    case "Salle 2": numSalle = "2"; break;
                    case "Salle 3": numSalle = "3"; break;
                    case "Salle 4": numSalle = "4"; break;
                    case "Salle 5": numSalle = "5"; break;
                    case "Salle 6": numSalle = "6"; break;
                    case "Salle 7": numSalle = "7"; break;
                    case "Salle 8": numSalle = "8"; break;
                    case "Salle 9": numSalle = "9"; break;
                    case "Salle 10": numSalle = "10"; break;
                }
                
                try
                {
                    string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                    string Query = "select occup from place where numSalle = '" + numSalle + "' and numPlace = '" + numero + "' and dateO = '" + dateOL.Text + "' and heureO = '" + heureOL.Text + "'";
                    //string Query = "select occup from place where numSalle = '" + numSalle + "' and dateO = '" + dateDiffL.Text + "' and heureO = '" + heureOL.Text + "' and numPlace = '" + numero.ToString() + "'"; //// 2024-04-13
                    //string Query = "select occup from place where numSalle = 1 and numPlace = 29 and dateO = '2024-04-17' and heureO = '" + heureOL + "'"; //// 2024-04-13

                    MySqlConnection conn4 = new MySqlConnection(str);
                    MySqlCommand cmd4 = new MySqlCommand(Query, conn4);
                    conn4.Open();
                    Object result2 = cmd4.ExecuteScalar();
                    if (result2 != null)
                    {
                        //MessageBox.Show(result2.ToString());
                        if((result2.ToString() == "Libre") && (nbPlace.Value > 0))
                        {
                            MessageBox.Show("Encore Libre");
                            test.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Aqua;

                            string Query2 = "update place set occup = 'Reservee' where numSalle = '" + numSalle + "' and dateO = '" + dateOL.Text + "' and heureO = '" + heureOL.Text + "' and numPlace = '" + numero + "'";
                            MySqlConnection conn2 = new MySqlConnection(str);
                            MySqlCommand cmd = new MySqlCommand(Query2, conn2);
                            MySqlDataReader r;
                            conn2.Open();
                            r = cmd.ExecuteReader();
                            while (r.Read())
                            {
                            }
                            conn2.Close();

                            test.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.Aqua;

                            string str22 = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                            string Query3 = "select numBillet from place where numSalle = '" + numSalle + "' and dateO = '" + dateOL.Text + "' and heureO = '" + heureOL.Text + "' and numPlace = '" + numero + "'";
                            //string Query3 = "select numBillet from place where numSalle = '" + numSalle + "' and dateO = '" + dateOL.Text + "' and heureO = '" + heureOL.Text + "' and numPlace = '" + numero + "'";
                            MySqlConnection conn3 = new MySqlConnection(str22);
                            MySqlCommand cmd3 = new MySqlCommand(Query3, conn3);
                            MySqlDataReader r2;
                            conn3.Open();
                            r2 = cmd3.ExecuteReader();
                            //MessageBox.Show("Data Updated");
                            while (r2.Read())
                            {
                                numBilletL.Text = r2.GetInt32(0).ToString();
                            }
                            conn3.Close();

                            numPlaceB.Text = numPlaceB.Text + " " + test.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                            nbPlace.Value--;
                        }
                        else if(result2.ToString() == "Reservee")
                        {
                            MessageBox.Show("La place est déjà occupée");
                        }
                        else
                        {
                            MessageBox.Show("Augmenter le nombre de place à reserver");
                        }
                    }
                    conn4.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
        }
        private void nbPlace_ValueChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            occupation(numSall.Text, dateOL.Text, heureOL.Text, titre2.Text);
            int n = numPlaceB.Text.Length;
            string str = numPlaceB.Text.ToString();
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (str[i] == ' ')
                {
                    count++;
                }
            }
            if (numBilletL.Text != "")
            {
                string path = "F:/Langages/C#/Reservation/Billets/" + numBilletL.Text + ".txt";
                string text = "\n\n\tBILLETS DE RESERVATION\n\n\nTitre : " + titre2.Text + "\nSalle : " + numSall.Text + "\nNombre de place : " + count.ToString() + "\nNumero de place : " + numPlaceB.Text + "\nNumero de billet : " + numBilletL.Text + "\nDate de diffusion : " + dateDiffL.Text + "\nHeure de Diffusion : " + heureOL.Text + "\n";
                File.WriteAllText(path, text);
                MessageBox.Show("La reservetion a été effectuée");
            }
            else
            {
                MessageBox.Show("Aucune place n'a été selectionnée");
            }
            
            numPlaceB.Text = "";
            numBilletL.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            sallePanel.Visible = true;
            filmMenu.Visible = true;

            titrePic.Text = "";
            salleCombo.Text = "";

            stocTitre.Visible = false;
            stocSalle.Visible = false;
            stocDate.Visible = false;
            stocTime.Visible = false;
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre as Titre, genre as Genre, dure as Duree, prix as Prix, poster as Poster from film";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                toutLesFilms.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Monday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string titreA = titrePic.Text;
            string salleA = salleCombo.Text;
            string dateA = datePic.Text;
            string timeA = timePic.Text;
            if (titrePic.Text == "")
            {
                MessageBox.Show("Le titre est vide");
            }
            else
            {
                //MessageBox.Show(stocTitre.Text + " " + stocSalle.Text + " " + stocDate.Text + " " + stocTime.Text);
                //MessageBox.Show(titreA + " " + salleA + " " + dateA + " " + timeA);
                try
                {
                    //MessageBox.Show(datePic.Value.ToString("dddd"));
                    string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                    string Query = "update diffuser set titre = '" + titreA + "', numSalle = '" + salleA + "', dateDiff = '" + datePic.Value.ToString("yyyy-MM-dd") + "', heureDiff = '" + timeA + "' where titre = '" + stocTitre.Text + "' and numSalle = '" + stocSalle.Text + "' and dateDiff = '" + stocDate.Value.ToString("yyyy-MM-dd") + "' and heureDiff = '" + stocTime.Text + "'";
                    MySqlConnection conn = new MySqlConnection(str);
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    MySqlDataReader r;
                    conn.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {

                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    //MessageBox.Show(datePic.Value.ToString("dddd"));
                    string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                    string Query = "update place set occup = 'Libre', numSalle = '" + salleA + "', dateO = '" + datePic.Value.ToString("yyyy-MM-dd") + "', heureO = '" + timeA + "' where numSalle = '" + stocSalle.Text + "' and dateO = '" + stocDate.Value.ToString("yyyy-MM-dd") + "' and heureO = '" + stocTime.Text + "'";
                    MySqlConnection conn = new MySqlConnection(str);
                    MySqlCommand cmd = new MySqlCommand(Query, conn);
                    MySqlDataReader r;
                    conn.Open();
                    r = cmd.ExecuteReader();
                    while (r.Read())
                    {

                    }
                    MessageBox.Show("La mise à jour à été effectée");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


                string dayName = "";
                switch (datePic.Value.ToString("dddd"))
                {
                    case "lundi": dayName = "Monday"; break;
                    case "mardi": dayName = "Tuesday"; break;
                    case "mercredi": dayName = "Wednesday"; break;
                    case "jeudi": dayName = "Thursday"; break;
                    case "vendredi": dayName = "Friday"; break;
                    case "samedi": dayName = "Saturday"; break;
                    case "dimanche": dayName = "Sunday"; break;
                }
                //MessageBox.Show("Nom du jour : " + dayName);
                try
                {

                    string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                    string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = '" + dayName + "'";

                    MySqlConnection conn = new MySqlConnection(str);
                    MySqlCommand cmd = new MySqlCommand(Query, conn);

                    conn.Open();
                    MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                    MyAdapter.SelectCommand = cmd;
                    DataTable dTable = new DataTable();
                    MyAdapter.Fill(dTable);
                    filmData.DataSource = dTable;
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void filmData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.filmData.Rows[e.RowIndex];

                string titreB = row.Cells[0].Value.ToString();
                string salleB = row.Cells[1].Value.ToString();
                string dateB = row.Cells[2].Value.ToString();
                string heureB = row.Cells[3].Value.ToString();

                //La valeur apres
                titrePic.Text = titreB;
                salleCombo.Text = salleB;
                datePic.Text = dateB;
                timePic.Text = heureB;

                datePic.Text = datePic.Value.ToString("yyyy-MM-dd");

                //La valeur avant : invisible
                stocTitre.Text = titreB;
                stocSalle.Text = salleB;
                stocDate.Text = dateB;
                stocTime.Text = heureB;

                stocDate.Text = stocDate.Value.ToString("yyyy-MM-dd");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Monday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Tuesday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Wednesday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Thursday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Friday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Saturday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            try
            {

                string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                string Query = "select titre, numSalle, dateDiff, heureDiff from diffuser where dayName(dateDiff) = 'Sunday'";

                MySqlConnection conn = new MySqlConnection(str);
                MySqlCommand cmd = new MySqlCommand(Query, conn);

                conn.Open();
                MySqlDataAdapter MyAdapter = new MySqlDataAdapter();
                MyAdapter.SelectCommand = cmd;
                DataTable dTable = new DataTable();
                MyAdapter.Fill(dTable);
                filmData.DataSource = dTable;
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void toutLesFilms_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.toutLesFilms.Rows[e.RowIndex];
                titrePic.Text = toutLesFilms.Rows[e.RowIndex].Cells[0].Value.ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            log2.Visible = true;
            login.Visible = true;
            sallePanel.Visible = true;
            filmMenu.Visible = true;

            code.Text = "";
            mdp.Text = "";
            codeError.Visible = false;
            mdpError.Visible = false;

            label24.Visible = false;
            xxx.Visible = false;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            string c = code.Text;
            string m = mdp.Text;
            Admin connect = new Admin();
            if(c == "")
            {
                codeError.Visible = true;
            }else if(m == "")
            {
                mdpError.Visible = true;
                codeError.Visible = false;
            }
            else
            {
                if(connect.isConnected(c,m) == true)
                {
                    log2.Visible = false;
                    login.Visible = false;
                    sallePanel.Visible = false;
                    filmMenu.Visible = false;

                    label24.Visible = true;
                    xxx.Visible = true;
                    try
                    {

                        string str = "datasource=localhost;port=3306;username=root;password=;database=cinema";
                        string Query = "select nom from admin where code = '" + c + "' and mdp = '" + m + "'";

                        MySqlConnection conn = new MySqlConnection(str);
                        MySqlCommand cmd = new MySqlCommand(Query, conn);
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if(result != null)
                        {
                            xxx.Text = "Admin : " + result.ToString();
                        }
                        else
                        {
                            MessageBox.Show("Aucun nom n'a été trouvé");
                        }

                        conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Code ou mots de passe invalide");
                }
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            
        }

        private void button17_Click(object sender, EventArgs e)
        {
            codeError2.Visible = false;
            nomVide.Visible = false;
            mdp222.Visible = false;
            confirmError.Visible = false;

            string c = code2.Text;
            string nom = nom2.Text;
            string mdp = mdp2.Text;
            string mdpConf = mdp22.Text;
            Admin X = new Admin();

            if (c == "")
            {
                codeError2.Visible = true;
            }else if (nom == "")
            {
                codeError2.Visible = false;
                nomVide.Visible = true;
            }else if (mdp == "")
            {
                codeError2.Visible = false;
                nomVide.Visible = false;
                mdp222.Visible = true;
            }else if(mdp != mdpConf)
            {
                codeError2.Visible = false;
                nomVide.Visible = false;
                mdp222.Visible = false;
                confirmError.Visible = true;
            }
            else
            {
                X.setCode(c);
                X.setNom(nom);
                X.setMdp(mdp);
                X.signUp();

                connecter.Visible = true;
                ajout.Visible = false;
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            ajout.Visible = true;
        }

        private void button18_Click(object sender, EventArgs e)
        {
            code.Text = "";
            mdp.Text = "";

            mdpError.Visible = false;
            codeError.Visible = false;
            ajout.Visible = false;
            connecter.Visible = true;
        }

        private void button15_Click_1(object sender, EventArgs e)
        {
            code.Text = "";
            mdp.Text = "";

            ajout.Visible = true;
            connecter.Visible = false;
        }

        private void button15_Click_2(object sender, EventArgs e)
        {
            ajout.Visible = true;
            connecter.Visible = false;
        }

        private void button15_Click_3(object sender, EventArgs e)
        {
            code2.Text = "";
            nom2.Text = "";
            mdp2.Text = "";
            mdp22.Text = "";

            codeError2.Visible = false;
            nomVide.Visible = false;
            mdp222.Visible = false;
            confirmError.Visible = false;

            ajout.Visible = true;
            connecter.Visible = false;
        }
    }
}
