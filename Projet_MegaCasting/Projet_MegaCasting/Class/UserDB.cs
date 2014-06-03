using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_MegaCasting.Class
{
    class UserDB
    {
        public static List<User> List()
        {
            //conection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"SELECT 
                                        Id, 
                                        LastName, 
                                        FirstName, 
                                        PhoneNumber, 
                                        Email, 
                                        Job,
                                        Addres,
                                        CP,
                                        City,
                                        DateofBirth, 
                                        IdCivility, 
                                    FROM User";

            //Execution

            connection.Open();
            SqlDataReader dataReader = commande.ExecuteReader();

            List<User> listUsers = new List<User>();
            while (dataReader.Read())
            {
                String lastName = dataReader.GetString(1);
                Int64 idCivility = dataReader.GetInt64(10);

                User user = new User(lastName, CivilityDB.Get(idCivility));
                user.Id = dataReader.GetInt64(0);
                user.FirstName = dataReader.GetString(2);
                user.PhoneNumber = dataReader.GetString(3);
                user.Email = dataReader.GetString(4);
                user.Job = dataReader.GetString(5);
                user.Address = dataReader.GetString(6);
                user.CP = dataReader.GetString(7);
                user.City = dataReader.GetString(8);
                user.DateOfBirth = dataReader.GetDateTime(9);



                listUsers.Add(user);

            }
            dataReader.Close();
            connection.Close();
            return listUsers;
        }

        public static User Get(Int64 id)
        {
            //Récupération de la chaine de connexion
            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"SELECT Id, LastName, FirstName, PhoneNumber, Email, Job, Address, CP, City, DateOfBirth, IdCivility
                                    FROM User
                                    WHERE Id = @Id";

            //Parametre
            commande.Parameters.AddWithValue("Id", id);

            //Execution
            connection.Open();

            SqlDataReader dataReader = commande.ExecuteReader();
            dataReader.Read();
            User user = new User(dataReader.GetString(1), CivilityDB.Get(dataReader.GetInt64(10)));
            user.Id = dataReader.GetInt64(0);
            user.FirstName = dataReader.GetString(2);
            user.PhoneNumber = dataReader.GetString(3);
            user.Email = dataReader.GetString(4);
            user.Job = dataReader.GetString(5);
            user.Address = dataReader.GetString(6);
            user.CP = dataReader.GetString(7);
            user.City = dataReader.GetString(8);
            user.DateOfBirth = dataReader.GetDateTime(9);
            dataReader.Close();
            connection.Close();
            return user;
        }

        public static User Insert(User user)
        {
            //connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            commande.CommandText = @"INSERT INTO user(LastName, FirstName, PhoneNumber, Email, Job, Address, CP, City, DateOfBirth, IdCivility) 
                                    VALUES(@LastName, @FirstName, @PhoneNumber, @Email, @Job, @Address, @CP, @City, @DateOfBirth, @IdCivility); 
                                    SELECT SCOPE_IDENTITY();";
            //paramètre
            commande.Parameters.AddWithValue("LastName", user.LastName);
            commande.Parameters.AddWithValue("FirstName", user.FirstName);
            commande.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
            commande.Parameters.AddWithValue("Email", user.Email);
            commande.Parameters.AddWithValue("Job", user.Job);
            commande.Parameters.AddWithValue("Address", user.Address);
            commande.Parameters.AddWithValue("CP", user.CP);
            commande.Parameters.AddWithValue("City", user.City);
            commande.Parameters.AddWithValue("DateOfBirth", user.DateOfBirth);
            commande.Parameters.AddWithValue("IdCivility", user.IdCivility);
            try
            {
                //execution
                connection.Open();
                Decimal IdLastAdd = (Decimal)commande.ExecuteScalar();
                return UserDB.Get((Int64)IdLastAdd);
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

        public static Boolean Update(User user)
        {
            Boolean isUpdateOK = false;

            //Connection
            ConnectionStringSettings connectionStringSettings = ConfigurationManager.ConnectionStrings["megacasting"];
            SqlConnection connection = new SqlConnection(connectionStringSettings.ToString());
            //Commande
            SqlCommand commande = new SqlCommand();
            commande.Connection = connection;
            String requete = @"UPDATE user SET LastName = @LastName,
                                FirstName = @FirstName,
                                PhoneNumber = @PhoneNumber,
                                Email = @Email,
                                Job = @Job,
                                Address = @Address,
                                CP = @CP,
                                City = @City,
                                DateOfBirth = @DateOfBirth,
                                IdCivility = @IdCivility
                                WHERE Id = @Id;";
            commande.CommandText = requete;
            //Paramètres
            commande.Parameters.AddWithValue("LastName", user.LastName);
            commande.Parameters.AddWithValue("FirstName", user.FirstName);
            commande.Parameters.AddWithValue("PhoneNumber", user.PhoneNumber);
            commande.Parameters.AddWithValue("Email", user.Email);
            commande.Parameters.AddWithValue("Job", user.Job);
            commande.Parameters.AddWithValue("Address", user.Address);
            commande.Parameters.AddWithValue("CP", user.CP);
            commande.Parameters.AddWithValue("City", user.City);
            commande.Parameters.AddWithValue("DateOfBirth", user.DateOfBirth);
            commande.Parameters.AddWithValue("IdCivility", user.IdCivility);

            commande.Parameters.AddWithValue("Id", user.Id);

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
            commande.CommandText = @"DELETE FROM User WHERE User = @User";
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
                throw new Exception("You can't delete an user\n" + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
