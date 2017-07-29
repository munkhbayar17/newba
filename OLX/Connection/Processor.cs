using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.IO;

namespace OLX.Connection
{
    class Processor
    {
        public SqlConnection connection;
        public HashSet<int> allUsers;

        public Processor()
        {
            Initialize_DB();
        }

        private void Initialize_DB()
        {
            SqlConnectionStringBuilder connBuilder = new SqlConnectionStringBuilder();
            connBuilder.InitialCatalog = "olx";
            connBuilder.DataSource = "MUUNUU\\OLX_SQLSERVER";
            connBuilder.IntegratedSecurity = true;

            connection = new SqlConnection(connBuilder.ToString());
            
        }

        //open connection to database
        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        Console.ReadKey();
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        Console.ReadKey();
                        break;
                }
                return false;
            }
        }

        //Close connection
        private bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
                return false;
            }
        }

        public void Select_User_Message_Text() {
            string query = "SELECT TOP (1000) * FROM[olx].[dbo].[User_messages_test]";

            if (this.OpenConnection() == true)
            {

                SqlCommand command = new SqlCommand(query, connection);
                string filePath = "C:\\Users\\mb17__000\\Documents\\Visual Studio 2017\\Projects\\OLX\\OLX\\Connection\\ads_recommendation.csv";
                File.WriteAllText(filePath, String.Empty);

                StringBuilder sb = new StringBuilder("user_id,category_id,ads");

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        int user_id = Convert.ToInt32(dataReader["user_id"]);
                        int category_id = Convert.ToInt32(dataReader["category_id"]);
                        
                        int[] recommendations = 
                            (this.allUsers.Contains(user_id)) ? Recommender.Recommender.recommend_for_existing_user(user_id, category_id): Recommender.Recommender.recommend_for_new_user(user_id, category_id);

                        sb.Append(string.Format("\n{0},{1},\"[{2}]\"", user_id, category_id,
                            string.Join(" ", recommendations)));
                    }

                    File.WriteAllText(filePath, sb.ToString());

                    dataReader.Close();

                }

                this.CloseConnection();

            }

        }

        public void Save_Users_in_Memory() {
            string query = "select distinct user_id from User_data;";

            if (this.OpenConnection() == true)
            {
                this.allUsers = new HashSet<int>();

                SqlCommand command = new SqlCommand(query, connection);

                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        this.allUsers.Add(Convert.ToInt32(dataReader["user_id"]));
                    }

                    dataReader.Close();

                }

                this.CloseConnection();

            }
        }
    }
}
