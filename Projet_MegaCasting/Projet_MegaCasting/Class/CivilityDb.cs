using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_MegaCasting.Class
{
    class CivilityDB
    {
        /// <summary>
        /// Récupère une liste de Civilité à partir de la base de données
        /// </summary>
        /// <returns>Une liste de civilités</returns>
        public static List<Civility> List()
        {
            //Récupération de la chaine de connexion
            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["CarnetAdresseE1BConnectionString"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            String requete = "SELECT Id, ShortLibel, LongLibel FROM Civility";
            connection.Open();
            SqlCommand commande = new SqlCommand(requete, connection);
            //execution

            SqlDataReader dataReader = commande.ExecuteReader();

            List<Civility> list = new List<Civility>();
            while (dataReader.Read())
            {
                //1 - Créer un civilité à partir des donner de la ligne du dataReader
                Civility civility = new Civility();
                civility.Id = dataReader.GetInt64(0);

                civility.ShortLibel = dataReader.GetString(1);
                civility.LongLibel = dataReader.GetString(2);


                //2 - Ajouter cette civilité à la list de civilité
                list.Add(civility);
            }
            dataReader.Close();
            connection.Close();
            return list;
        }

        /// <summary>
        /// Récupère une civilité à partir d'un identifiant de civilité
        /// </summary>
        /// <param name="Identifiant">Identifant de civilité</param>
        /// <returns>Une civilité</returns>
        public static Civility Get(Int64 id)
        {
            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["CarnetAdresseE1BConnectionString"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            String requete = @"SELECT Id, ShortLibel, LongLibel FROM Civility
                                WHERE Id = @Id";
            SqlCommand commande = new SqlCommand(requete, connection);

            //Paramètres
            commande.Parameters.AddWithValue("Id", id);


            //Execution
            connection.Open();

            SqlDataReader dataReader =  commande.ExecuteReader();


            dataReader.Read();


            //1 - Création de la civilite
            Civility civility = new Civility();

            civility.Id = dataReader.GetInt64(0);
            civility.LongLibel = dataReader.GetString(2);
            civility.ShortLibel = dataReader.GetString(1);
            dataReader.Close();
            connection.Close();
            return civility;

        }
    }
}
