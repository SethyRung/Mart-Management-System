using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Mart_Management_System
{
    public partial class FrmSupplier : Form
    {
        public FrmSupplier()
        {
            InitializeComponent();
        }

        Boolean isNewClicked;
        SqlDataAdapter da;
        DataTable dt;
        string supID = "0";
        SqlCommand com;

        private void LoadData()
        {
            da = new SqlDataAdapter("SELECT * FROM  dbo.GetSupplier()", MyOperation.con);
            dt = new DataTable();
            da.Fill(dt);
            dgvSup.DataSource = dt;
            dgvSup.ColumnHeadersDefaultCellStyle.Font = new Font("Time New Roman", 12);
            dgvSup.DefaultCellStyle.Font = new Font("!Khmer OS Siemreap", 12);
            dgvSup.Columns["Supplier ID"].Width = 150;
            dgvSup.Columns["Supplier"].Width = 277;
            dgvSup.Columns["Supplier Address"].Width = 350;
            dgvSup.Columns["Supplier Contect"].Width = 329;

            //Off sort
            foreach (DataGridViewColumn col in dgvSup.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dt.Dispose();
            da.Dispose();

        }

        private void Modify(string type)
        {
            com = new SqlCommand("ProSupplier", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar)).Value = type;
            if (type.Equals("insert"))
                com.Parameters.Add(new SqlParameter("@SupID", SqlDbType.Int)).Value = txtSupID.Text.Replace("លេខសម្គាល់ដោយស្វ័យប្រវត្តិ", "0");
            else
                com.Parameters.Add(new SqlParameter("@SupID", SqlDbType.Char)).Value = txtSupID.Text;
            com.Parameters.Add(new SqlParameter("@Supplier", SqlDbType.NVarChar)).Value = txtSupplier.Text;
            com.Parameters.Add(new SqlParameter("@SupAddress", SqlDbType.NVarChar)).Value = txtSupAddress.Text;
            com.Parameters.Add(new SqlParameter("@SupContect", SqlDbType.NVarChar)).Value = txtSupContact.Text;
            com.ExecuteNonQuery();
            MyOperation.ClearData(this);
        }

        private void FrmSupplier_Load(object sender, EventArgs e)
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
                txtSupID.Enabled = false;
                txtSupID.Text = "លេខសម្គាល់ដោយស្វ័យប្រវត្តិ";
                txtSupplier.Focus();
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
            txtSupID.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (isNewClicked == false && txtSupID.Text == null)
            {
                MessageBox.Show("Please enter supplier id.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSupID.Focus();
            }
            else if (txtSupplier.Text == null)
            {
                MessageBox.Show("Please enter supplier name.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSupplier.Focus();
            }

            else if (txtSupContact.Text == null)
            {
                MessageBox.Show("Please enter supplier contect.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSupContact.Focus();
            }

            else if (txtSupAddress.Text == null)
            {
                MessageBox.Show("Please enter supplier address.", "Missing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSupplier.Focus();
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

        private void dgvSup_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvSup.RowCount > 0)
            {
                int i = e.RowIndex;
                if (i < 0) return;
                if (btnNew.Text == "Cancel" && btnSave.Enabled == true) return;
                txtSupID.Text = dgvSup.Rows[i].Cells[0].Value.ToString();
                txtSupplier.Text = dgvSup.Rows[i].Cells[1].Value.ToString();
                txtSupAddress.Text = dgvSup.Rows[i].Cells[2].Value.ToString();
                txtSupContact.Text = dgvSup.Rows[i].Cells[3].Value.ToString();
                btnEdit.Enabled = true;
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            com = new SqlCommand("ProSearchSupplier", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@s", txtSearch.Text);
            da = new SqlDataAdapter();
            dt = new DataTable();
            da.SelectCommand = com;
            da.Fill(dt);
            dgvSup.DataSource = dt;

            da.Dispose();
            dt.Dispose();
        }
    }
}
