using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Diagnostics;

namespace wpf_adatbazis
{
    public class EmployeeService
    {
        MySqlConnection connection;
        public EmployeeService()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.Port = 3306;
            builder.Database = "employeeDB";
            builder.UserID = "root";
            builder.Password = "mypassword";
            connection = new MySqlConnection(builder.ToString());
        }
        //CRUD
        public bool Create(Employee employee)
        {
            OpenConn();
            string sql = "INSERT INTO dolgozok(nev,nem,kor,fizetes) VALUES (@name,@gender,@age,@salary)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", employee.Name);
            command.Parameters.AddWithValue("@gender", employee.Gender);
            command.Parameters.AddWithValue("@age", employee.Age);
            command.Parameters.AddWithValue("@salary", employee.Salary);
            int affectedRows = command.ExecuteNonQuery();    //Ezzel hajtom végre a parancsot az sql-be, ha 1 akkor sikeres(mert egy sort adtam hozzá)
            CloseConn();
            return affectedRows == 1;
        }
        public List<Employee> GetAll()
        {
            List<Employee> employees = new List<Employee>();
            OpenConn();
            string sql = "SELECT * FROM dolgozok";
            MySqlCommand command = new MySqlCommand(sql, connection);      //Ezzel írok a DB-be (command)
            using (MySqlDataReader reader = command.ExecuteReader())        //Ez azért kell, mert amíg ez meg van nyitva addig nem lehet lezárni a connectiont
            {
                while (reader.Read())        //amíg van olvasni való addig olvassa
                {
                    Employee employee = new Employee();
                    employee.Id = reader.GetInt32("id");
                    employee.Name = reader.GetString("nev");
                    employee.Age = reader.GetInt32("kor");
                    employee.Salary = reader.GetInt32("fizetes");
                    employee.Gender = reader.GetString("nem");
                    employees.Add(employee);
                }
            }

            CloseConn();
            return employees;
        }

        public bool CreateEmp(Employee employee)
        {
            OpenConn();
            string sql = "INSERT INTO dolgozok (nev, nem, kor, fizetes) VALUES (@name, @gender, @age, @salary)";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", employee.Name);
            command.Parameters.AddWithValue("@gender", employee.Gender);
            command.Parameters.AddWithValue("@age", employee.Age);
            command.Parameters.AddWithValue("@salary", employee.Salary);
            int affectedRows = command.ExecuteNonQuery();
            CloseConn();
            return affectedRows == 1;
        }

        public bool Update(Employee employee)
        {
            OpenConn();
            string sql = "UPDATE dolgozok SET nev=@name, nem=@gender, kor=@age, fizetes=@salary WHERE id=@id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@name", employee.Name);
            command.Parameters.AddWithValue("@gender", employee.Gender);
            command.Parameters.AddWithValue("@age", employee.Age);
            command.Parameters.AddWithValue("@salary", employee.Salary);
            command.Parameters.AddWithValue("@id", employee.Id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConn();
            return affectedRows == 1;

        }

        public bool Delete(Employee employee)
        {
            OpenConn();
            string sql = "DELETE FROM dolgozok WHERE id = @id";
            MySqlCommand command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@id", employee.Id);
            int affectedRows = command.ExecuteNonQuery();
            CloseConn();
            return affectedRows == 1;
        }


        private void CloseConn()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }

        private void OpenConn()
        {
            if (connection.State != System.Data.ConnectionState.Open)
            {
                Debug.WriteLine("Connection estabilesd");
                connection.Open();
            }
        }
    }
}
