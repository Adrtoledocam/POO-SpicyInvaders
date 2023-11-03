using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace SpicyInvaders
{
    public class DataBaseConnect
    {
        /// <summary>
        /// Initialiser notre DB connexion avec ses valeurs
        /// </summary>
        private string connectionS = "Server=localhost;Port=6033;Database=db_space_invaders;UserId = root; Password=root;";

        public List<String> playersName = new List<String>();
        public List<Int64> playersScore = new List<Int64>();

        /// <summary>
        /// Méthode de test de la connexion
        /// </summary>
        public void Connection()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionS))
            {
                connection.Open();
                Debug.Print("Connection Works with database:" + connection);
            }
        }
        /// <summary>
        /// Méthode d'enregistrement des points
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="points"></param>
        public void SavePoints(string nickName, int points)
        {
            using (MySqlConnection connect = new MySqlConnection(connectionS))
            {
                try
                {
                    connect.Open();
                    string requete = "INSERT INTO t_joueur (jouPseudo, jouNombrePoints) VALUE (@nickName,@points)";
                    MySqlCommand cmd = new MySqlCommand(requete, connect);
                    cmd.Parameters.AddWithValue("@nickName", nickName);
                    cmd.Parameters.AddWithValue("@points", points);
                    cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur : " +ex.Message);
                }

            }
        }

        /// <summary>
        /// Méthode d'enregistrement des résultats du jeu avec le nom du joueur et le score. 
        /// </summary>
        public void Highscore()
        {
            using (MySqlConnection connect = new MySqlConnection(connectionS))
            {
                try
                {
                    connect.Open();
                    string requete = "SELECT jouPseudo, jouNombrePoints FROM t_joueur ORDER BY jouNombrePoints DESC LIMIT 5";
                    MySqlCommand cmd = new MySqlCommand(requete, connect);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            playersName.Add(reader.GetString(0));
                            playersScore.Add(reader.GetInt64(1));
                        }
                    }
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Erreur : " + ex.Message);
                }
                
            }
        }


    }
}
