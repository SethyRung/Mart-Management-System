using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mart_Management_System
{
    public partial class FrmCustomer : Form
    {
        public FrmCustomer()
        {
            InitializeComponent();
        }

        Boolean isNewClicked;
        SqlDataAdapter da;
        DataTable dt;
        string cusID = "0";
        SqlCommand com;

        private void LoadData()
        {
            da = new SqlDataAdapter("SELECT * FROM  dbo.GetCustomer()", MyOperation.con);
            dt = new DataTable();
            da.Fill(dt);
            dgvCus.DataSource = dt;
            dgvCus.ColumnHeadersDefaultCellStyle.Font = new Font("Time New Roman", 12);
            dgvCus.DefaultCellStyle.Font = new Font("!Khmer OS Siemreap", 12);
            dgvCus.Columns["Customer ID"].Width = 201;
            dgvCus.Columns["Name in Khmer"].Width = 301;
            dgvCus.Columns["Name in English"].Width = 301;
            dgvCus.Columns["Customer Address"].Width = 301;
            dgvCus.Columns["Customer Contect"].Width = 302;

            //Off sort
            foreach (DataGridViewColumn col in dgvCus.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dt.Dispose();
            da.Dispose();

        }

        private void Modify(string type)
        {
            com = new SqlCommand("ProCustomer", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar)).Value = type;
            if (type.Equals("insert"))
                com.Parameters.Add(new SqlParameter("@CusID", SqlDbType.Int)).Value = txtCusID.Text.Replace("លេខសម្គាល់ដោយស្វ័យប្រវត្តិ", "0");
            else
                com.Parameters.Add(new SqlParameter("@CusID", SqlDbType.Int)).Value = txtCusID.Text;
            com.Parameters.Add(new SqlParameter("@CusEnName", SqlDbType.NVarChar)).Value = txtCusName.Text;
            com.Parameters.Add(new SqlParameter("@CusAddress", SqlDbType.NVarChar)).Value = txtAddress.Text;
            com.Parameters.Add(new SqlParameter("@CusContect", SqlDbType.NVarChar)).Value = txtContect.Text;
            com.ExecuteNonQuery();
            MyOperation.ClearData(this);
        }

        private void FrmCustomer_Load(object sender, EventArgs e)
        {
            MyOperation.MyConnection();
            MyOperation.OnOFFControl(this, false);
            LoadData();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isNewClicked = true;
            if (btnNew.Text == "New")
            {
                MyOperation.OnOFFControl(this, true);
                btnEdit.Enabled = false;
                btnNew.Image = Mart_Management_System.Properties.Resources.exit_32px;
                btnNew.Text = "Cancel";
                MyOperation.ClearData(this);
                txtCusName.Focus();
                txtCusID.Enabled = false;
                txtCusID.Text = "លេខសម្គាល់ដោយស្វ័យប្រវត្តិ";
            }
            else
            {
                DialogResult rs;
                rs = MessageBox.Show("Do you want to cancel?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    MyOperation.OnOFFControl(this, false);
                    btnNew.Image = Mart_Management_System.Properties.Resources.new_copy_32px;
                    btnNew.Text = "New";
                    MyOperation.ClearData(this);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isNewClicked = false;
            MyOperation.OnOFFControl(this, true);
            btnEdit.Enabled = false;
            btnNew.Image = Mart_Management_System.Properties.Resources.exit_32px;
            btnNew.Text = "Cancel";
            txtCusID.Enabled = false;
            txtCusName.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (txtCusName.Text == null)
            {
                MessageBox.Show("Please enter customer name.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCusName.Focus();
            }
            else if (txtAddress.Text == null)
            {
                MessageBox.Show("Please enter customer address.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAddress.Focus();
            }
            else if (txtContect.Text == null)
            {
                MessageBox.Show("Please enter customer contect.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtContect.Focus();
            }
            else
            {
                try
                {
                    if (isNewClicked == true)
                    {
                        Modify("insert");
                        MessageBox.Show("You have successfully created.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Modify("update");
                        MessageBox.Show("You have successfully updated.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnNew.Image = Mart_Management_System.Properties.Resources.new_copy_32px;
                    btnNew.Text = "New";
                    LoadData();
                    MyOperation.OnOFFControl(this, false);
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                        MessageBox.Show("ID is already existed");
                    else
                        MessageBox.Show(ex.Message);
                }
            }
        }

        private void dgvCus_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvCus.RowCount > 0)
            {
                int i = e.RowIndex;
                if (i < 0) return;
                if (btnNew.Text == "Cancel" && btnSave.Enabled == true) return;
                txtCusID.Text = dgvCus.Rows[i].Cells[0].Value.ToString();
                txtCusName.Text = dgvCus.Rows[i].Cells[1].Value.ToString();
                txtAddress.Text = dgvCus.Rows[i].Cells[2].Value.ToString();
                txtContect.Text = dgvCus.Rows[i].Cells[3].Value.ToString();
                btnEdit.Enabled = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            com = new SqlCommand("ProSearchCustomer", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@s", txtSearch.Text);
            da = new SqlDataAdapter();
            dt = new DataTable();
            da.SelectCommand = com;
            da.Fill(dt);
            dgvCus.DataSource = dt;

            da.Dispose();
            dt.Dispose();
        }
    }
}
