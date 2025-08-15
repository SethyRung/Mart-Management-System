using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mart_Management_System
{
    public partial class FrmLogin : Form
    {
        public FrmLogin()
        {
            InitializeComponent();
        }

        SqlDataAdapter da;
        DataTable dt;
        SqlCommand com;
        int count = 3;

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            MyOperation.MyConnection();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlDataReader dr;
            string un = txtUsername.Text.Trim();
            string pd = txtPassword.Text.Trim();
            string sql = $"SELECT tbEmployee.EmpID, EmpKhName, EmpEnName, EmpPos FROM tbEmployee INNER JOIN tbUser ON tbEmployee.EmpID = tbUser.EmpID WHERE EmpUserName='{un}' AND EmpPassword='{pd}'";
            com = new SqlCommand(sql, MyOperation.con);
            dr = com.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    MyOperation.EmpID = dr[0].ToString();
                    MyOperation.EmpKhName = dr[1].ToString();
                    MyOperation.EmpEnName = dr[2].ToString();
                    MyOperation.EmpPositon = dr[3].ToString();
                }
                this.Hide();
                FrmMain ma = new FrmMain();
                ma.ShowDialog();
                this.Close();
            }
            else
            {
                count--;
                MessageBox.Show($"Your username or password is incorrect.\nYou have {count} more chances left.", "Login", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                if (count == 0)
                    Application.Exit();
            }    

            
            dr.Close();
        }
    }
}
