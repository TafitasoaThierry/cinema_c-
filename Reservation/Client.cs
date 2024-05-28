using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Reservation
{
    class Client
    {
        private int numBillet, nbReserver;
        private string titre;

        public void reserver(int nbReserver, string titre)
        {
            this.nbReserver = nbReserver;
            this.titre = titre;
            int nbPlaceTotal;
            int nbPlaceDispo;

            string connStr = "server=localhost;user=root;database=cinema";
            string query = "select numSalle from diffuser where titre = '" + this.titre + "'"; // get numSalle
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);

                conn.Open();

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    int numSalle = Convert.ToInt32(result);
                    MessageBox.Show("Diffuser par la salle " + result.ToString());
                    string query2 = "select nbPlace from salle where numSalle = '" + numSalle + "'"; // get nbPlace from numSalle
                    MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                    object result2 = cmd2.ExecuteScalar();
                    if (result2 != null)
                    {
                        nbPlaceTotal = Convert.ToInt32(result2);
                        MessageBox.Show("Qui contient " + result2.ToString() + " places");
                        string query3 = "select count(numPlace) from place where occup = 'Libre' and numSalle = '" + numSalle + "'"; // get nbPlaceDispo = count(numPlace) where occup = "Libre";
                        MySqlCommand cmd3 = new MySqlCommand(query3, conn);
                        object result3 = cmd3.ExecuteScalar();
                        if (result3 != null)
                        {
                            nbPlaceDispo = Convert.ToInt32(result3);
                            MessageBox.Show("Avec " + result3.ToString() + " places libres");
                            if (nbPlaceDispo >= nbReserver)
                            {
                                int reste = nbPlaceTotal - nbPlaceDispo + 1;

                                string billet = "select numBillet from place where numPlace = '" + reste + "' and numSalle = '" + numSalle + "'"; // numero de billet et update table place
                                MySqlCommand cmd4 = new MySqlCommand(billet, conn);
                                object result4 = cmd4.ExecuteScalar();
                                if (result4 != null)
                                {
                                    this.numBillet = Convert.ToInt32(result4);
                                    MessageBox.Show("Numero de billet " + this.numBillet.ToString());
                                }

                                for (int i = 0; i < nbReserver; i++)
                                {
                                    int j = this.numBillet + i;
                                    string update = "update place set occup = 'Reservée' where numSalle = '" + numSalle + "' and numBillet = '" + j + "'";
                                    MySqlCommand cmd5 = new MySqlCommand(update, conn);
                                    cmd5.ExecuteNonQuery();
                                }
                                MessageBox.Show("Okay e!");

                            }
                            else
                            {
                                MessageBox.Show("Place insuffisant");
                            }
                        }
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        
        /*
        public void completer()
        {
            try{
                string connStr = "server=localhost;user=root;database=cinema";
                MySqlConnection conn = new MySqlConnection(connStr);
                conn.Open();

                for(int i = 1; i <= 50; i++)
                {
                    string query = "insert into place values( 0,'Libre','3','2024-04-15','14:00:00','" + i + "')";
                    MySqlCommand cmd = new MySqlCommand(query, conn);
                    cmd.ExecuteNonQuery();
                    
                }
                MessageBox.Show("C'est fini");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        */
    }
}
