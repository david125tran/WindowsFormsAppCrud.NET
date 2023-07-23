using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppCRUD
{
    public partial class Form1 : Form
    {
        // Create an object model for our table
        Employee employeeModel = new Employee();
        public Form1()
        {
            InitializeComponent();      // This class loads the GUI when we run
        }

        //------------------------CREATE------------------------
        private void btnAdd_Click(object sender, EventArgs e)
        {
            // Make an object off the database
            EmployeesDBEntities employeesDB = new EmployeesDBEntities();
            // Add the textbox entries to the object
            employeeModel.FirstName = tbFirstName.Text;
            employeeModel.LastName = tbLastName.Text;   
            employeeModel.Salary = tbSalary.Text;
            employeeModel.Position = tbPosition.Text;
            employeesDB.Employees.Add(employeeModel);
            employeesDB.SaveChanges();
            MessageBox.Show("The database has been updated");

            // Refresh the grid
            populateDataGridView();

        }

        //------------------------READ------------------------
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load all of the data from the database by calling populateDataGridView()
            populateDataGridView();
        }

        //------------------------READ------------------------
        private void populateDataGridView()
        {
            // Load all of the data from the SQL database
            EmployeesDBEntities db = new EmployeesDBEntities();
            dataGridView1.DataSource = db.Employees.ToList<Employee>();
        }

        //------------------------UPDATE------------------------
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            {
                employeeModel.EmployeeId = (int)dataGridView1.CurrentRow.Cells["EmployeeId"].Value;
                // Make an object off of the database
                EmployeesDBEntities db = new EmployeesDBEntities();
                // Push the data to the textboxes in the GUI
                employeeModel = db.Employees.Where(x => x.EmployeeId == employeeModel.EmployeeId).FirstOrDefault();
                tbFirstName.Text = employeeModel.FirstName;
                tbLastName.Text = employeeModel.LastName;
                tbSalary.Text = employeeModel.Salary;
                tbPosition.Text = employeeModel.Position;

            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Make an object off the database
            EmployeesDBEntities employeesDB = new EmployeesDBEntities();
            // Add the textbox entries to the object
            employeeModel.FirstName = tbFirstName.Text;
            employeeModel.LastName = tbLastName.Text;
            employeeModel.Salary = tbSalary.Text;
            employeeModel.Position = tbPosition.Text;

            // Add the object to the SQL database
            if (employeeModel.EmployeeId == 0)                   // Hand                                   
                employeesDB.Employees.Add(employeeModel);
            else
                employeesDB.Entry(employeeModel).State = System.Data.Entity.EntityState.Modified;
            employeesDB.SaveChanges();
            MessageBox.Show("The database has been updated");

            // Refresh the grid
            populateDataGridView();
        }

        //------------------------DELETE------------------------
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try     // Try to delete a row
            {
                EmployeesDBEntities db = new EmployeesDBEntities();
                var entry = db.Entry(employeeModel);
                if (entry.State == System.Data.Entity.EntityState.Detached) // Check to see if the entity is detached (the entity is not being tracked by the context)
                    db.Employees.Attach(employeeModel);                     // Attach the object 
                db.Employees.Remove(employeeModel);                         // Remove the entry
                db.SaveChanges();
                // Refresh the grid
                populateDataGridView();
            }
            catch   // Handle cases where the user did not select a row to delete
            {
                MessageBox.Show("You need to select a row to delete");
                throw;
            }
        }
    }
}
