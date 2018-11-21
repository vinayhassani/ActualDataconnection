using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace LastTry
{
    class CreateEmployeeViewModel
    {
        Baseclass b = new Baseclass();
        private string _id;
        private string _firstName;
        private string _address;
        public CreateEmployeeViewModel()
        {
            SaveCommand = new DelegateCommand(Save, () => CanSave);

        }

        public string ID
        {
            get { return _id; }
            set
            {
                _id = value;
                b.NotifyPropertyChanged("ID");
            }
        }
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                b.NotifyPropertyChanged("FirstName");
            }
        }
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
                b.NotifyPropertyChanged("Address");
            }
        }
        public ICommand SaveCommand { get; private set; }

        public bool CanSave
        {
            get { return !string.IsNullOrEmpty(ID) && !string.IsNullOrEmpty(FirstName); }
        }
        string connectionString =
          @"Data Source=localhost;Initial Catalog=Student;Integrated Security=True";
        public void Save()
        {

            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "INSERT INTO Users(ID,FirstName,Address)VALUES(@ID,@FirstName,@Address)";
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@FirstName", FirstName);
            cmd.Parameters.AddWithValue("@Address", Address);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                con.Close();
            }
            MessageBox.Show("Data Saved Successfully.");
        }
    }
}
