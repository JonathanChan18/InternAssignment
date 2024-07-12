using System.Data;
using System.Data.SqlClient;

namespace InternAssignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Assignment 1 - Get
            GetAllEmployees();

            // Assignment 2 - Add
            AddEmployee();

            // Assignment 2 - Update
            UpdateEmployee();

            // Assignment 2 - Delete
            DeleteEmployee();
        }

        public static void GetAllEmployees()
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            //Create SQL Command
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "SELECT * FROM Employee";
            cmd.Connection = dbConnection.connection;

            //Create a DataTable to store all of the values of the DB table
            DataTable dtb = new DataTable();

            //Fill the DataTable
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(dtb);

            //Create list of EmployeeModel objects
            List<EmployeeModel> list = new List<EmployeeModel>();

            //Iterate through each rows of the DataTable and assign each values of the employee properties to an employee object, then add to the list.
            foreach (DataRow row in dtb.Rows)
            {
                int employeeId = Convert.ToInt32(row["EmployeeId"]);
                string firstName = row["FirstName"].ToString();
                string lastName = row["LastName"].ToString();
                int age = Convert.ToInt32(row["Age"]);

                EmployeeModel employee = new EmployeeModel()
                {
                    EmployeeId = employeeId,
                    FirstName = firstName,
                    LastName = lastName,
                    Age = age
                };
                list.Add(employee);
            }

            //Print the List
            foreach (EmployeeModel employee in list)
            {
                Console.WriteLine($"Employee ID: {employee.EmployeeId}");
                Console.WriteLine($"First Name: {employee.FirstName}");
                Console.WriteLine($"Last Name: {employee.LastName}");
                Console.WriteLine($"Age: {employee.Age}\n");

            }
            dbConnection.CloseConnection();
        }

        public static void AddEmployee()
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            //Add New Employee
            EmployeeModel Addemployee = new EmployeeModel()
            {
                FirstName = "John",
                LastName = "Doe",
                Age = 40

            };

            //Create SQL Command
            string insertQuery = "INSERT INTO Employee (FirstName,LastName,Age) VALUES (@FirstName,@LastName,@Age)";

            using (SqlCommand cmd = new SqlCommand(insertQuery, dbConnection.connection))
            {
                // Add parameters and their values
                cmd.Parameters.AddWithValue("@FirstName", Addemployee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", Addemployee.LastName);
                cmd.Parameters.AddWithValue("@Age", Addemployee.Age);

                // Execute the command
                cmd.ExecuteNonQuery();

                // Display the result
                Console.WriteLine($"New Employee added.");
            }
            dbConnection.CloseConnection();
        }

        public static void UpdateEmployee()
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            EmployeeModel UpdateEmployee = new EmployeeModel()
            {
                EmployeeId = 6,
                FirstName = "Jonathan",
                LastName = "Chan",
                Age = 20
            };

            string updateQuery = "UPDATE Employee SET FirstName=@FirstName,LastName=@LastName,Age=@Age WHERE EmployeeId=@EmployeeId";

            using (SqlCommand cmd = new SqlCommand(updateQuery, dbConnection.connection))
            {
                // Add parameters and their values
                cmd.Parameters.AddWithValue("@EmployeeId", UpdateEmployee.EmployeeId);
                cmd.Parameters.AddWithValue("@FirstName", UpdateEmployee.FirstName);
                cmd.Parameters.AddWithValue("@LastName", UpdateEmployee.LastName);
                cmd.Parameters.AddWithValue("@Age", UpdateEmployee.Age);

                // Execute the command
                cmd.ExecuteNonQuery();

                // Display the result
                Console.WriteLine($"Employee records updated.");
            }
            dbConnection.CloseConnection();
        }

        public static void DeleteEmployee()
        {
            //Open Connection to the DB
            DbConnection dbConnection = new DbConnection();
            dbConnection.OpenConnection();

            EmployeeModel DeleteEmployee = new EmployeeModel()
            {
                EmployeeId = 6
            };

            string deleteQuery = "DELETE FROM Employee WHERE EmployeeId=@EmployeeId";

            using (SqlCommand cmd = new SqlCommand(deleteQuery, dbConnection.connection))
            {
                cmd.Parameters.AddWithValue("@EmployeeId", DeleteEmployee.EmployeeId);

                // Execute the command
                cmd.ExecuteNonQuery();

                // Display the result
                Console.WriteLine($"One Employee deleted.");
            }
            dbConnection.CloseConnection();
        }
    }
}