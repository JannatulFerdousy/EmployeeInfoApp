using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeInfoApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string connectionString = ConfigurationManager.ConnectionStrings["EmployeeConString"].ConnectionString;
        private void label2_Click(object sender, EventArgs e)
        {

        }

        
        private void saveButton_Click(object sender, EventArgs e)
        {
            Employee aEmployee = new Employee();

            aEmployee.name = nameTextBox.Text;
            aEmployee.email = emailTextBox.Text;
            aEmployee.address = addressTextBox.Text;
            aEmployee.salary = Convert.ToDouble(salaryTextBox.Text);

           

            //is email unique? if unique insert else not insert

            if (isEmailUnique(aEmployee.email))
            {
                MessageBox.Show("already exists");

            }
            else
            {

                // connect database

                
                SqlConnection connection = new SqlConnection(connectionString);

                //write query

                string query = "INSERT INTO EmployeeTable VALUES('" + aEmployee.name + "','" + aEmployee.address + "','" +
                               aEmployee.email + "','" + aEmployee.salary + "')";

                //execute query
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                int rowInsertion = command.ExecuteNonQuery();
                connection.Close();

                //see result
                if (rowInsertion > 0)
                {
                    MessageBox.Show("Inserrted successfully");
                }
                else
                {
                    MessageBox.Show("Insertion failed");
                }


                nameTextBox.Clear();
                addressTextBox.Clear();
                salaryTextBox.Clear();
                emailTextBox.Clear();
            }

            
            
            //checking email uniqness

        }

        public bool isEmailUnique(string email)
        {
            //connect database

           
            SqlConnection connection = new SqlConnection(connectionString);


            //write query

            string query = "SELECT * FROM EmployeeTable WHERE Email = '"+email+"'";

            //execute query


            SqlCommand command = new SqlCommand(query, connection);
            bool isEmail = false;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
              isEmail = true;
                break;
            }
             reader.Close();
            connection.Close();
            return isEmail;

            //see result

        }

        private void showAllButton_Click(object sender, EventArgs e)
        {
           
            //connect database
         SqlConnection connection = new SqlConnection(connectionString);


            //write query
        string query = "SELECT * FROM EmployeeTable";

           
            //execute query
        SqlCommand command = new SqlCommand(query, connection);
            
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            List<Employee> employeeList = new List<Employee>();

            while (reader.Read())
            {
                Employee employee = new Employee();

                employee.id = int.Parse(reader["ID"].ToString());
                employee.name = reader["Name"].ToString();
                employee.address = reader["Address"].ToString();
                employee.email = reader["Email"].ToString();

                employeeList.Add(employee);

            }
            reader.Close();
            connection.Close();
        }


    }
}
