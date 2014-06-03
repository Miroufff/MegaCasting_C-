using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_MegaCasting.Class
{
    class CompanyDB
    {
        public static List<Company> List()
        {
            //conection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"SELECT 
                                        Id, 
                                        Name, 
                                        PhoneNumber, 
                                        Email, 
                                        Addres,
                                        CP,
                                        City,
                                        Sector
                                    FROM Company";

            //Execution

            connection.Open();
            SqlDataReader dataReader = commande.ExecuteReader();

            List<Company> listCompanies = new List<Company>();
            while (dataReader.Read())
            {
                String Name = dataReader.GetString(1);

                Company company = new Company(Name);
                company.Id = dataReader.GetInt64(0);
                company.PhoneNumber = dataReader.GetString(2);
                company.Email = dataReader.GetDateTime(3);
                company.Address = dataReader.GetString(4);
                company.CP = dataReader.GetString(5);
                company.City = dataReader.GetString(6);
                company.Sector = dataReader.GetString(7);



                listCompanies.Add(company);

            }
            dataReader.Close();
            connection.Close();
            return listCompanies;
        }

        public static Company Get(Int64 id)
        {
            //Récupération de la chaine de connexion
            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"SELECT Id, Name, PhoneNumber, Email, Address, CP, City, Sector
                                    FROM Company
                                    WHERE Id = @Id";

            //Parametre
            commande.Parameters.AddWithValue("Id", id);

            //Execution
            connection.Open();

            SqlDataReader dataReader = commande.ExecuteReader();
            dataReader.Read();
            Company company = new Company(dataReader.GetString(1));
            company.Id = dataReader.GetInt64(0);
            company.PhoneNumber = dataReader.GetString(2);
            company.Email = dataReader.GetDateTime(3);
            company.Address = dataReader.GetString(4);
            company.CP = dataReader.GetString(5);
            company.City = dataReader.GetString(6);
            company.Sector = dataReader.GetString(7);
            dataReader.Close();
            connection.Close();
            return company;
        }

        public static Company Insert(Company company)
        {
            //connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"INSERT INTO C(Name, PhoneNumber, Email, Address, CP, City, Sector) 
                                    VALUES(@Name, @PhoneNumber, @Email, @Adress, @CP, @City, @Sector); 
                                    SELECT SCOPE_IDENTITY();";
            //paramètre
            commande.Parameters.AddWithValue("Name", company.Name);
            commande.Parameters.AddWithValue("PhoneNumber", company.PhoneNumber);
            commande.Parameters.AddWithValue("Email", company.Email);
            commande.Parameters.AddWithValue("Address", company.Address);
            commande.Parameters.AddWithValue("CP", company.CP);
            commande.Parameters.AddWithValue("City", company.City);
            commande.Parameters.AddWithValue("Sector", company.Sector);

            try
            {
                //execution
                connection.Open();
                Decimal IdLastAdd = (Decimal)commande.ExecuteScalar();
                return CompanyDB.Get((Int64)IdLastAdd);
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

        public static Boolean Update(Company company)
        {
            Boolean isUpdateOK = false;

            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            String requete = @"UPDATE company SET Name = @Name,
                                PhoneNumber = @PhoneNumber,
                                Email = @Email,
                                Address = @Adress,
                                CP = @CP,
                                City = @City,
                                Sector = @Sector,
                                WHERE Id = @Id;";
            commande.CommandText = requete;
            //Paramètres
            commande.Parameters.AddWithValue("Name", company.Name);
            commande.Parameters.AddWithValue("PhoneNumber", company.PhoneNumber);
            commande.Parameters.AddWithValue("Email", company.Email);
            commande.Parameters.AddWithValue("Address", company.Address);
            commande.Parameters.AddWithValue("CP", company.CP);
            commande.Parameters.AddWithValue("City", company.City);
            commande.Parameters.AddWithValue("Sector", company.Sector);


            commande.Parameters.AddWithValue("Id", company.Id);

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
            commande.CommandText = @"DELETE FROM Company WHERE Company = @Company";
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
                throw new Exception("You can't delete an company\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
