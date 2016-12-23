using System;
using System.Data;
using System.Data.SqlClient;

namespace DatabaseTest
{
    class Program
    {
        private static string connectionString =
            "Server=ealdb1.eal.local; Database=ejl87_db; User Id=ejl87_usr; Password=Baz1nga87;";
        SqlConnection con = new SqlConnection
               (@"Data Source = ealdb1.eal.local;
                 Database = EJL87_DB;
                 User ID = ejl87_usr;
                 Password = Baz1nga87"
               );


        static void Main(string[] args)
        {
            Program program = new Program();
            program.Login();

        }
        private void Login()
        {
            Console.WriteLine("Please insert username:");
            string username = Console.ReadLine();
            Console.WriteLine("Please insert password:");
            string password = Console.ReadLine();
            if ((username == "admin") && (password == "admin"))
                Menu();
            else
                LoginPlayer(username, password);
        }
        private void LoginPlayer(string username, string password)
        {

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("UserLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string DataUsername = reader["PlayerUserName"].ToString();
                    string DataPassword = reader["PlayerPassword"].ToString();
                    if ((DataUsername == username) && (DataPassword == password))
                    {
                        conn.Close();
                        PlayerMenu(DataUsername);
                    }
                }



            }
        }
        private void PlayerMenu(string username)
        {
            Console.Clear();
            Console.WriteLine("Commands:");
            Console.WriteLine("1) Show newsfeed \n2) Show personal data");
            Console.WriteLine("3) Exit");
            Console.WriteLine("Please input your command:");
            string input = Console.ReadLine();
            Console.Clear();
            int x = Convert.ToInt32(input);
            switch (x)
            {
                case 1: ShowNews(); break;
                case 2: ShowUserData(username); break;
                case 3: break;
            }

        }
        private void ShowUserData(string username)
        {
            string temp;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdShowPlayerData = new SqlCommand("ShowPlayerData", con);

                cmdShowPlayerData.CommandType = CommandType.StoredProcedure;
                cmdShowPlayerData.Parameters.Add(new SqlParameter("@s", username));
                SqlDataReader reader = cmdShowPlayerData.ExecuteReader();

                while (reader.Read())
                {
                    string PlayerFirstName = reader["PlayerFirstName"].ToString();
                    string PlayerLastName = reader["PlayerLastName"].ToString();
                    string PlayerEmail = reader["PlayerEmail"].ToString();
                    string PlayerPhone = reader["PlayerPhone"].ToString();
                    temp = PlayerFirstName + PlayerLastName + PlayerEmail + PlayerPhone;
                    Console.WriteLine(temp);
                }


                Console.ReadKey();
                //username = writer["@PlayerUserName"].ToString();
                // string PlayerFirstName = reader["PlayerFirstName"].ToString();

                con.Close();
            }
        }
        private void Menu()
        {
            Console.Clear();
            Console.WriteLine("Commands:\n1) Show newsfeed\n2) Edit newsfeed");
            Console.WriteLine("3) View player list\n4) Absence for today \n5) Insert new user");
            Console.WriteLine("6) Edit user info\n7) Delete user");
            Console.WriteLine("8) Exit");
            Console.WriteLine("Please input your command:");
            string input = Console.ReadLine();
            Console.Clear();
            int x = Convert.ToInt32(input);
            switch (x)
            {
                case 1: ShowNews(); break;
                case 2: EditNews(); break;
                case 3: PlayerInfo(); break;
                case 4: AbsenceToday(); break;
                case 5: InsertUser(); break;
                case 6: EditUser(); break;
                case 7: DeleteUser(); break;
                case 8: break;
            }
        }
        private void DeleteUser()
        {
            throw new NotImplementedException();
        }
        private void EditUser()
        {
            throw new NotImplementedException();
        }
        private void EditNews()
        {
            throw new NotImplementedException();
        }
        private void ShowNews()
        {
            throw new NotImplementedException();
        }
        private void AbsenceToday()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                con.Open();
                conn.Open();
                SqlCommand cmdShowPlayerInfo = new SqlCommand("ShowPlayerInfo", con);
                cmdShowPlayerInfo.CommandType = CommandType.StoredProcedure;
                SqlCommand cmd = new SqlCommand("AddAbsence", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                string input;


                SqlDataReader reader = cmdShowPlayerInfo.ExecuteReader();
                while (reader.Read())
                {
                    string PlayerID = reader["PlayerID"].ToString();
                    string PlayerFirstName = reader["PlayerFirstName"].ToString();
                    string PlayerLastName = reader["PlayerLastName"].ToString();
                    string PlayerUserName = reader["PlayerUserName"].ToString();
                    Console.WriteLine(PlayerFirstName + " " + PlayerLastName + " " + PlayerUserName);
                    Console.WriteLine("Update the status of player: (A for absent and N for not absent)");
                    input = Console.ReadLine();
                    cmd.Parameters.Add(new SqlParameter("Absence", input));
                    //cmd.Parameters.Add(new SqlParameter("PlayerID", PlayerID));
                    cmd.Parameters.Add(new SqlParameter("AbsenceDate", DateTime.Now));
                    cmd.ExecuteNonQuery();

                }
                Console.ReadKey();
                con.Close();
                conn.Close();
                Console.Clear();
                Menu();
            }
        }
        private string GetInput(string v)
        {
            return Console.ReadLine();
        }
        private void PlayerInfo()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmdShowPlayerInfo = new SqlCommand("ShowPlayerInfo", con);
                cmdShowPlayerInfo.CommandType = CommandType.StoredProcedure;


                SqlDataReader reader = cmdShowPlayerInfo.ExecuteReader();
                while (reader.Read())
                {
                    string PlayerFirstName = reader["PlayerFirstName"].ToString();
                    string PlayerLastName = reader["PlayerLastName"].ToString();
                    string PlayerEmail = reader["PlayerEmail"].ToString();
                    string PlayerPhone = reader["PlayerPhone"].ToString();
                    string PlayerUserName = reader["PlayerUserName"].ToString();
                    Console.WriteLine(PlayerFirstName + " " + PlayerLastName + " " + PlayerEmail + " " + PlayerPhone + " " + PlayerUserName);
                }
                Console.ReadKey();
                con.Close();
                Console.Clear();
                Menu();
            }
        }
        private void InsertUser()
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {

                SqlCommand cmd = new SqlCommand("Insert_user", con);
                cmd.CommandType = CommandType.StoredProcedure;
                string input;
                con.Open();
                Console.WriteLine("Please insert Firstname:");
                input = GetInput("PlayerFirstName");
                cmd.Parameters.Add(new SqlParameter("PlayerFirstName", input));
                Console.WriteLine("Please insert Lastname:");
                input = GetInput("PlayerLastName");
                cmd.Parameters.Add(new SqlParameter("PlayerLastName", input));
                Console.WriteLine("Please insert Player Email:");
                input = GetInput("PlayerEmail");
                cmd.Parameters.Add(new SqlParameter("PlayerEmail", input));
                Console.WriteLine("Please insert Player Phone:");
                input = GetInput("PlayerPhone");
                cmd.Parameters.Add(new SqlParameter("PlayerPhone", input));
                Console.WriteLine("Please insert Username:");
                input = GetInput("PlayerUserName");
                cmd.Parameters.Add(new SqlParameter("PlayerUserName", input));
                Console.WriteLine("Please insert Password:");
                input = GetInput("PlayerPassword");
                cmd.Parameters.Add(new SqlParameter("PlayerPassword", input));
                cmd.ExecuteNonQuery();
                Console.WriteLine("User added succesfully");
                Console.ReadKey();
                con.Close();
                Console.Clear();
                Menu();
            }
            Console.WriteLine("Person Added :)");

        }
    }
}

