using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projet_MegaCasting.Class
{
    class AdsDB
    {
        public static List<Ads> List()
        {
            //conection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"SELECT 
                                        Id, 
                                        Title, 
                                        PhoneNumber, 
                                        Email, 
                                        Addres,
                                        CP,
                                        City,
                                        ReleaseDate,
                                        StartDate,
                                        DescriptionJob,
                                        DescriptionProfile,
                                        DescriptionAgreement,
                                        NameCompany,
                                        Ref
                                    FROM Ads";

            //Execution

            connection.Open();
            SqlDataReader dataReader = commande.ExecuteReader();

            List<Ads> listCompanies = new List<Ads>();
            while (dataReader.Read())
            {
                String Title = dataReader.GetString(1);

                Ads ads = new Ads(Title);
                ads.Id = dataReader.GetInt64(0);
                ads.PhoneNumber = dataReader.GetString(2);
                ads.Email = dataReader.GetDateTime(3);
                ads.Address = dataReader.GetString(4);
                ads.CP = dataReader.GetString(5);
                ads.City = dataReader.GetString(6);
                ads.ReleaseDate = dataReader.GetInt64(7);
                ads.StartDate = dataReader.GetDateTime(8);
                ads.DescriptionJob = dataReader.GetString(9);
                ads.DescriptionProfile = dataReader.GetString(10);
                ads.DescriptionAgreement = dataReader.GetString(11);
                ads.NameCompany = dataReader.GetString(12);
                ads.Ref = dataReader.GetString(13);

                listCompanies.Add(ads);

            }
            dataReader.Close();
            connection.Close();
            return listCompanies;
        }

        public static Ads Get(Int64 id)
        {
            //Récupération de la chaine de connexion
            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"SELECT 
                                        Id, 
                                        Title, 
                                        PhoneNumber, 
                                        Email, 
                                        Addres,
                                        CP,
                                        City,
                                        ReleaseDate,
                                        StartDate,
                                        DescriptionJob,
                                        DescriptionProfile,
                                        DescriptionAgreement,
                                        NameCompany,
                                        Ref
                                    FROM Ads
                                    WHERE Id = @Id";

            //Parametre
            commande.Parameters.AddWithValue("Id", id);

            //Execution
            connection.Open();

            SqlDataReader dataReader = commande.ExecuteReader();
            dataReader.Read();
            Ads ads = new Ads(dataReader.GetString(1));
            ads.Id = dataReader.GetInt64(0);
            ads.PhoneNumber = dataReader.GetString(2);
            ads.Email = dataReader.GetDateTime(3);
            ads.Address = dataReader.GetString(4);
            ads.CP = dataReader.GetString(5);
            ads.City = dataReader.GetString(6);
            ads.ReleaseDate = dataReader.GetInt64(7);
            ads.StartDate = dataReader.GetDateTime(8);
            ads.DescriptionJob = dataReader.GetString(9);
            ads.DescriptionProfile = dataReader.GetString(10);
            ads.DescriptionAgreement = dataReader.GetString(11);
            ads.NameCompany = dataReader.GetString(12);
            ads.Ref = dataReader.GetString(13);

            dataReader.Close();
            connection.Close();
            return ads;
        }

        public static Ads Insert(Ads ads)
        {
            //connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"INSERT INTO Ads(Title, PhoneNumber, Email, Address, CP, City, ReleaseDate, StartDate, DescriptionJob, DescriptionProfile, DescriptionAgreement, NameCompany, Ref) 
                                    VALUES(@Title, @PhoneNumber, @Email, @Address, @CP, @City, @ReleaseDate, @StartDate, @DescriptionJob, @DescriptionProfile, @DescriptionAgreement, @NameCompany, @Ref); 
                                    SELECT SCOPE_IDENTITY();";
            //paramètre
            commande.Parameters.AddWithValue("Title", ads.Title);
            commande.Parameters.AddWithValue("PhoneNumber", ads.PhoneNumber);
            commande.Parameters.AddWithValue("Email", ads.Email);
            commande.Parameters.AddWithValue("Address", ads.Address);
            commande.Parameters.AddWithValue("CP", ads.CP);
            commande.Parameters.AddWithValue("City", ads.City);
            commande.Parameters.AddWithValue("ReleaseDate", ads.ReleaseDate);
            commande.Parameters.AddWithValue("StartDate", ads.StartDate);
            commande.Parameters.AddWithValue("DescriptionJob", ads.DescriptionJob);
            commande.Parameters.AddWithValue("DescriptionProfile", ads.DescriptionProfile);
            commande.Parameters.AddWithValue("DescriptionAgreement", ads.DescriptionAgreement);
            commande.Parameters.AddWithValue("NameCompany", ads.NameCompany);
            commande.Parameters.AddWithValue("Ref", ads.Ref);



            try
            {
                //execution
                connection.Open();
                Decimal IdLastAdd = (Decimal)commande.ExecuteScalar();
                return AdsDB.Get((Int64)IdLastAdd);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public static Boolean Update(Ads ads)
        {
            Boolean isUpdateOK = false;

            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            String requete = @"UPDATE ads SET Title = @Title,
                                PhoneNumber = @PhoneNumber,
                                Email = @Email,
                                Address = @Adress,
                                CP = @CP,
                                City = @City,
                                ReleaseDate = @ReleaseDate, 
                                StartDate = @StartDate, 
                                DescriptionJob = @DescriptionJob, 
                                DescriptionProfile = @DescriptionProfile,
                                DescriptionAgreement = @DescriptionAgreement, 
                                NameCompany = @NameCompany, 
                                Ref = @Ref                       
                                WHERE Id = @Id;";
            commande.CommandText = requete;
            //Paramètres
            commande.Parameters.AddWithValue("Title", ads.Title);
            commande.Parameters.AddWithValue("PhoneNumber", ads.PhoneNumber);
            commande.Parameters.AddWithValue("Email", ads.Email);
            commande.Parameters.AddWithValue("Address", ads.Address);
            commande.Parameters.AddWithValue("CP", ads.CP);
            commande.Parameters.AddWithValue("City", ads.City);
            commande.Parameters.AddWithValue("ReleaseDate", ads.ReleaseDate);
            commande.Parameters.AddWithValue("StartDate", ads.StartDate);
            commande.Parameters.AddWithValue("DescriptionJob", ads.DescriptionJob);
            commande.Parameters.AddWithValue("DescriptionProfile", ads.DescriptionProfile);
            commande.Parameters.AddWithValue("DescriptionAgreement", ads.DescriptionAgreement);
            commande.Parameters.AddWithValue("NameCompany", ads.NameCompany);
            commande.Parameters.AddWithValue("Ref", ads.Ref);


            commande.Parameters.AddWithValue("Id", ads.Id);

            //Execution
            try
            {
                connection.Open();
                commande.ExecuteNonQuery();
                isUpdateOK = true;
            }
            catch (Exception)
            {
                isUpdateOK = false;
            }
            finally
            {
                connection.Close();
            }
            return isUpdateOK;
        }

        public static void Delete(Int64 id)
        {
            //connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"DELETE FROM Ads WHERE Ads = @Ads";
            //paramètre
            commande.Parameters.AddWithValue("Id", id);
            try
            {
                //execution
                connection.Open();
                commande.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("You can't delete an ads\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
