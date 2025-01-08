using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Student_Progress_Tracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string constring = "Data Source=AUMC-LAB3-21\\SQLEXPRESS;Initial Catalog=DB_Students;Integrated Security=True";

        public MainWindow()
        {
            InitializeComponent();
            Load_Data();
            Load_combo();

        }

       public void Load_Data()
        {
            SqlConnection conn = new SqlConnection(constring);
            string query;
            conn.Open();
            query = "select * from Student_DT";
            SqlCommand sqlcommand = new SqlCommand(query,conn);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgt.ItemsSource = dt.DefaultView;
            conn.Close();
        }

        public void Load_subject_Data()
        {
            SqlConnection conn = new SqlConnection(constring);
            string query;
            conn.Open();
            query = "select * from Student_DT where Subject=@subject";
            SqlCommand sqlcommand = new SqlCommand(query, conn);
            sqlcommand.Parameters.AddWithValue("@subject",Subject.SelectedItem);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgt.ItemsSource = dt.DefaultView;
            conn.Close();
        }

        public void Load_Grade_Data()
        {
            SqlConnection conn = new SqlConnection(constring);
            string query;
            conn.Open();
            query = "select * from Student_DT where Grade=@grade";
            SqlCommand sqlcommand = new SqlCommand(query, conn);
            sqlcommand.Parameters.AddWithValue("@grade", Grade.SelectedItem);
            SqlDataAdapter adapter = new SqlDataAdapter(sqlcommand);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            dgt.ItemsSource = dt.DefaultView;
            conn.Close();
        }


        public void Load_combo()
        {

            Subject.Items.Add("English");
            Subject.Items.Add("Urdu");
            Subject.Items.Add("VP");
            Subject.Items.Add("PF");
            Subject.Items.Add("Statistics");

            Grade.Items.Add("A");
            Grade.Items.Add("B");
            Grade.Items.Add("C");
            Grade.Items.Add("D");
            Grade.Items.Add("F");

        }

        public bool check()
        {
            if(txt_name.Text == "")
            {
                MessageBox.Show("Enter Student Name", "Input Required");
                return false;
            }
            if (txt_grade.Text == "")
            {
                MessageBox.Show("Enter Student Grade", "Input Required");
                return false;
            }
            if (txt_subject.Text == "")
            {
                MessageBox.Show("Enter Student Subject", "Input Required");
                return false;
            }
            if (txt_marks.Text == "")
            {
                MessageBox.Show("Enter Student Marks", "Input Required");
                return false;
            }
            if (txt_percent.Text == "")
            {
                MessageBox.Show("Enter Student Attendance Percentage", "Input Required");
                return false;
            }


            return true;
        }

        
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            if(check())
            {
                SqlConnection conn = new SqlConnection(constring);
                string query = "insert into Student_DT (Name,Grade,Subject,Marks,Attendance_Percentage) values (@name,@grade,@subject,@marks,@percent)";
                conn.Open();
                SqlCommand sqlcommand = new SqlCommand(query,conn);
                sqlcommand.Parameters.AddWithValue("@name", txt_name.Text);
                sqlcommand.Parameters.AddWithValue("@grade", txt_grade.Text);
                sqlcommand.Parameters.AddWithValue("@subject", txt_subject.Text);
                sqlcommand.Parameters.AddWithValue("@marks", Convert.ToInt32(txt_marks.Text));
                sqlcommand.Parameters.AddWithValue("@percent", Convert.ToInt32(txt_percent.Text));
                int value = sqlcommand.ExecuteNonQuery();
                if(value > 0 )
                {
                    MessageBox.Show("Student Record Saved Successfullt", "Successfully");
                }
                else
                {
                    MessageBox.Show("Student Record Not Saved", "Failed");
                }
                Load_Data();
                clear();
            }
        }

        public void clear()
        {
            txt_id.Text = "";
            txt_name.Text = "";
            txt_grade.Text = "";
            txt_subject.Text = "";
            txt_marks.Text = "";
            txt_percent.Text = "";
        }
        
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            if(txt_id.Text != "")
            {
                SqlConnection conn = new SqlConnection(constring);
                string query = "delete from Student_DT where ID = @id";
                conn.Open();
                SqlCommand sqlcommand = new SqlCommand(query, conn);
                sqlcommand.Parameters.AddWithValue("@id", Convert.ToInt32(txt_id.Text));
                int value = sqlcommand.ExecuteNonQuery();
                if (value > 0)
                {
                    MessageBox.Show("Student Record Deleted Successfullt", "Successfully");
                }
                else
                {
                    MessageBox.Show("Student Record Not Found", "Failed");
                }
                Load_Data();
            }
            else
            {
                MessageBox.Show("First Enter Any Record Name", "Failed");
            }
            clear();
        }

        private void update_btn_Click(object sender, RoutedEventArgs e)
        {

            if (check() && txt_id.Text!="")
            {

                SqlConnection conn = new SqlConnection(constring);
                string query = "update Student_DT set Name = @name,Grade = @grade,subject=@subject,Marks=@marks,Attendance_Percentage=@percent where ID = @id";
                conn.Open();
                SqlCommand sqlcommand = new SqlCommand(query, conn);
                sqlcommand.Parameters.AddWithValue("@id", Convert.ToInt32(txt_id.Text));
                sqlcommand.Parameters.AddWithValue("@name", txt_name.Text);
                sqlcommand.Parameters.AddWithValue("@grade", txt_grade.Text);
                sqlcommand.Parameters.AddWithValue("@subject", txt_subject.Text);
                sqlcommand.Parameters.AddWithValue("@marks", Convert.ToInt32(txt_marks.Text));
                sqlcommand.Parameters.AddWithValue("@percent", Convert.ToInt32(txt_percent.Text));
                int value = sqlcommand.ExecuteNonQuery();
                if (value > 0)
                {
                    MessageBox.Show("Student Record Updated Successfullt", "Successfully");
                }
                else
                {
                    MessageBox.Show("Student Record Not Found", "Failed");
                }
                Load_Data();
                clear();
            }
            else
            {
                MessageBox.Show("First Select Any Record", "Failed");
            }
        }

        private void Subject_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            Load_subject_Data();
        }

        private void Grade_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Load_Grade_Data();
        }

        private void refresh_Click(object sender, RoutedEventArgs e)
        {
            Load_Data();
        }
    }
}
