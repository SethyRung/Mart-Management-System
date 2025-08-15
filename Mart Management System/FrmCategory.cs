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
    public partial class FrmCategory : Form
    {
        public FrmCategory()
        {
            InitializeComponent();
        }

        SqlDataAdapter da;
        DataTable dt;
        SqlCommand com;
        Boolean isNewClicked;
        string catID = "0";

        private void LoadData()
        {
            da = new SqlDataAdapter("SELECT * FROM  dbo.GetCatagory()", MyOperation.con);
            dt = new DataTable();
            da.Fill(dt);
            dgvCat.DataSource = dt;
            dgvCat.ColumnHeadersDefaultCellStyle.Font = new Font("Time New Roman", 12);
            dgvCat.DefaultCellStyle.Font = new Font("!Khmer OS Siemreap", 12);
            dgvCat.Columns["Category ID"].Width = 200;
            dgvCat.Columns["Category"].Width = 498;

            //Off sort
            foreach (DataGridViewColumn col in dgvCat.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dt.Dispose();
            da.Dispose();
        }

        private void Modify(string type)
        {
            com = new SqlCommand("ProCategory", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar)).Value = type;
            com.Parameters.Add(new SqlParameter("@CatID", SqlDbType.VarChar)).Value = txtCatID.Text;
            com.Parameters.Add(new SqlParameter("@Category", SqlDbType.NVarChar)).Value = txtCategory.Text;

            com.ExecuteNonQuery();
            MyOperation.ClearData(this);
        }

        private void FrmCategory_Load(object sender, EventArgs e)
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
                txtCatID.Focus();
            }
            else
            {
                DialogResult re;
                re = MessageBox.Show("Do you want to cancel?", "Cancel...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (re == DialogResult.Yes)
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
            txtCatID.Enabled = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCatID.Text.Trim()))
            {
                MessageBox.Show("Please enter catagory ID", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCatID.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtCategory.Text.Trim()))
            {
                MessageBox.Show("Please enter a category", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCategory.Focus();
                return;
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
                        MessageBox.Show("Category ID is already existed");
                    else
                        MessageBox.Show(ex.Message);
                }
            }
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            com = new SqlCommand("ProSearchCategory", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@S",SqlDbType.NVarChar)).Value = txtSearch.Text;
            da = new SqlDataAdapter();
            da.SelectCommand = com;
            dt = new DataTable();
            da.Fill(dt);
            dgvCat.DataSource = dt;
        }

        private void dgvCat_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //private void lswCate_SelectedIndexChanged(object sender, EventArgs e)
            //{
            //    if (lswCate.SelectedItems.Count > 0)
            //    {
            //        if (btnNew.Text == "Cancel" && btnSave.Enabled == true)
            //            return;
            //        ListViewItem item = lswCate.SelectedItems[0];
            //        catID = item.SubItems[0].Text;
            //        txtCatID.Text = catID;
            //        txtCate.Text = item.SubItems[1].Text;
            //        btnEdit.Enabled = true;
            //    }
            //}
            int i;
            if (dgvCat.RowCount > 0)
            {
                i = e.RowIndex;
                if (i < 0) return;
                if (btnNew.Text == "Cancel" && btnSave.Enabled == true) return;

                txtCatID.Text = dgvCat.Rows[i].Cells[0].Value.ToString();
                txtCategory.Text = dgvCat.Rows[i].Cells[1].Value.ToString();
            }
            btnEdit.Enabled = true;
        }
    }
}
