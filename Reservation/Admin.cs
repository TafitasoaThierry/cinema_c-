using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace Reservation
{
    class Admin
    {
        private string code, nom, prenom, mdp;
        private int numGuichet;
        public void setCode(string code)
        {
            this.code = code;
        }
        public string getCode()
        {
            return this.code;
        }
        public void setNom(string nom)
        {
            this.nom = nom;
        }
        public string getNom()
        {
            return this.nom;
        }
        public void setPrenom(string prenom)
        {
            this.prenom = prenom;
        }
        public string getPrenom()
        {
            return this.prenom;
        }
        public void setMdp(string mdp)
        {
            this.mdp = mdp;
        }
        public string getMdp()
        {
            return this.mdp;
        }
        public void setNumGuichet(int numGuichet)
        {
            this.numGuichet = numGuichet;
        }
        public int getNumGuichet()
        {
            return this.numGuichet;
        }
        public bool isConnected(string code, string mdp)
        {
            ///"server=localhost;user=root;database=cinema;port=3306;password="
            MySqlConnection conn = new MySqlConnection("server=localhost;user=root;database=cinema");

            conn.Open();
            string sql = "select mdp from admin where code = '" + code + "'";
            string mdpBase;
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            object result = cmd.ExecuteScalar();
            if (result != null)
            {
                 mdpBase = result.ToString();
                 if (mdpBase == mdp)
                 {
                     return true;
                 }
                 else
                 {
                     return false;
                 }
            }
            else
            {
                 return false;
            }
            conn.Close();
        }

        public void signUp()
        {
            try
            {
                string connStr = "server=localhost;user=root;database=cinema";
                string query = "insert into admin values('" + this.code + "','" + this.nom + "','" + this.mdp + "');";

                MySqlConnection conn = new MySqlConnection(connStr);

                MySqlCommand cmd = new MySqlCommand(query, conn);
                conn.Open();

                cmd.ExecuteNonQuery();
                MessageBox.Show("Inscription reussie");
                conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
