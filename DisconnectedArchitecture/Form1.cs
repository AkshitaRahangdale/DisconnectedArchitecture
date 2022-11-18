using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Windows.Forms;


namespace DisconnectedArchitecture
{
    public partial class Form1 : Form
    {
        SqlConnection con;
        SqlDataAdapter da;
        SqlCommandBuilder scb;
        DataSet ds;

        public Form1()
        {
            InitializeComponent();
            InitializeComponent();
            string str = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
            con = new SqlConnection(str);

        }
        public DataSet GetAllStudent()
        {
            da = new SqlDataAdapter("select * from Student", con);
            da.MissingSchemaAction = MissingSchemaAction.AddWithKey;
            scb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "Student");
            return ds;

        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["Student"].NewRow();
                row["name"] = txtName.Text;
                row["Percent"] = txtPercent.Text;
                ds.Tables["Student"].Rows.Add(row);
                int result = da.Update(ds.Tables["Student"]);
                if (result == 1)
                {
                    MessageBox.Show("Record inserted");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["Student"].Rows.Find(txtRoll.Text);
                if (row != null)
                {
                    row["name"] = txtName.Text;
                    row["Percent"] = txtPercent.Text;

                    int result = da.Update(ds.Tables["Student"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record updated");
                    }
                }
                else
                {
                    MessageBox.Show("Id not found to update");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["Student"].Rows.Find(txtRoll.Text);
                if (row != null)
                {
                    row.Delete();
                    int result = da.Update(ds.Tables["Student"]);
                    if (result == 1)
                    {
                        MessageBox.Show("Record deleted");
                    }
                }
                else
                {
                    MessageBox.Show("Id not found to delete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

            try
            {
                ds = GetAllStudent();
                DataRow row = ds.Tables["Student"].Rows.Find(txtRoll.Text);
                if (row != null)
                {
                    txtName.Text = row["name"].ToString();
                    txtPercent.Text = row["Percent"].ToString();
                }
                else
                {
                    MessageBox.Show("Record not found");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowAll_Click_1(object sender, EventArgs e)
        {
            try
            {
                ds = GetAllStudent();
                dataGridView1.DataSource = ds.Tables["Student"]; 

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

       

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
