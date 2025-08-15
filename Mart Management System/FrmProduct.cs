using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mart_Management_System
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            MyOperation.MyConnection();
            MyOperation.OnOFFControl(this, false);
            MyOperation.FillCbo(cboCategory, "CatID", "Category", "tbCategory");
            cboCategory.Text = null;
            LoadData();
        }

        SqlDataAdapter da;
        DataTable dt;
        SqlCommand com;

        private void LoadData()
        {
            da = new SqlDataAdapter("SELECT * FROM  dbo.GetProduct()", MyOperation.con);
            dt = new DataTable();
            da.Fill(dt);
            dgvPro.DataSource = dt;
            dgvPro.ColumnHeadersDefaultCellStyle.Font = new Font("Time New Roman", 12);
            dgvPro.DefaultCellStyle.Font = new Font("!Khmer OS Siemreap", 12);
            dgvPro.Columns["Product ID"].Width = 102;
            dgvPro.Columns["Product Name"].Width = 230;
            dgvPro.Columns["QTY"].Width = 100;
            dgvPro.Columns["Unit Price In Stock"].Width = 230;
            dgvPro.Columns["Sale Unit Price"].Width = 230;
            dgvPro.Columns["Category"].Width = 230;

            //Off sort
            foreach (DataGridViewColumn col in dgvPro.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dt.Dispose();
            da.Dispose();

        }

        private void Modify(string type)
        {
            com = new SqlCommand("ProProduct", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar)).Value = type;
            com.Parameters.Add(new SqlParameter("@ProID", SqlDbType.VarChar)).Value = txtProID.Text;
            com.Parameters.Add(new SqlParameter("@ProName", SqlDbType.NVarChar)).Value = txtProName.Text;
            com.Parameters.Add(new SqlParameter("@ProQty", SqlDbType.Int)).Value = txtQty.Text;
            com.Parameters.Add(new SqlParameter("@UPIS", SqlDbType.Money)).Value = txtUPIS.Text;
            com.Parameters.Add(new SqlParameter("@SUP", SqlDbType.Money)).Value = txtSUP.Text;
            com.Parameters.Add(new SqlParameter("@CatID", SqlDbType.VarChar)).Value = cboCategory.SelectedValue;
            com.ExecuteNonQuery();
            MyOperation.ClearData(this);
        }

        private void dgvPro_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvPro.RowCount > 0)
            {
                int i = e.RowIndex;
                if (i < 0) return;
                if (btnEdit.Text == "Cancel" && btnSave.Enabled == true) return;
                txtProID.Text = dgvPro.Rows[i].Cells[0].Value.ToString();
                txtProName.Text = dgvPro.Rows[i].Cells[1].Value.ToString();
                txtQty.Text = dgvPro.Rows[i].Cells[2].Value.ToString();
                txtUPIS.Text = string.Format("{0:c}", dgvPro.Rows[i].Cells[3].Value);
                txtSUP.Text = string.Format("{0:c}", dgvPro.Rows[i].Cells[4].Value);
                cboCategory.Text = dgvPro.Rows[i].Cells[5].Value.ToString();
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (btnEdit.Text == "Edit")
            {
                MyOperation.OnOFFControl(this, true);
                btnEdit.Image = Mart_Management_System.Properties.Resources.exit_32px;
                btnEdit.Text = "Cancel";
                txtProName.Focus();
                btnDelete.Enabled = false;
            }
            else
            {
                DialogResult rs;
                rs = MessageBox.Show("Do you want to cancel?", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    MyOperation.OnOFFControl(this, false);
                    btnEdit.Image = Mart_Management_System.Properties.Resources.edit_property_32px;
                    btnEdit.Text = "Edit";
                    MyOperation.ClearData(this);
                }
            }
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProName.Text.Trim()))
            {
                MessageBox.Show("Please enter a product name.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtProName.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                MessageBox.Show("Please enter a QTY.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtQty.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(cboCategory.Text.Trim()))
            {
                MessageBox.Show("Please select a category.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCategory.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtUPIS.Text.Trim()))
            {
                MessageBox.Show("Please enter unit price in stock.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUPIS.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtSUP.Text.Trim()))
            {
                MessageBox.Show("Please enter an sale unit price.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSUP.Focus();
                return;
            }
            else
            {
                try
                {
                    Modify("update");
                    MessageBox.Show("You have successfully updated.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyOperation.ClearData(this);
                    MyOperation.OnOFFControl(this, false);
                    btnEdit.Image = Mart_Management_System.Properties.Resources.edit_property_32px;
                    btnEdit.Text = "Edit";
                    LoadData();
                }
                catch (Exception ex)
                {
                        MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult re;
            re = MessageBox.Show("Are you sure to Delete it?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                Modify("delete");
                MessageBox.Show("You have successfully deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            MyOperation.ClearData(this);
            LoadData();
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            com = new SqlCommand("ProSearchProduct", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@s", txtSearch.Text);
            da = new SqlDataAdapter();
            dt = new DataTable();
            da.SelectCommand = com;
            da.Fill(dt);
            dgvPro.DataSource = dt;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            decimal x;
            char ch = e.KeyChar;
            if (ch == (char)8)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch))
            {
                e.Handled = true;
            }
        }

        private void txtUPIS_KeyPress(object sender, KeyPressEventArgs e)
        {
            decimal x;
            char ch = e.KeyChar;
            if (ch == (char)8)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && (ch != '.' || !Decimal.TryParse(txtUPIS.Text + ch, out x)))
            {
                e.Handled = true;
            }
        }

        private void txtUPIS_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUPIS.Text.Trim()))
            {
                txtUPIS.Text = string.Format("{0:c}", decimal.Parse(txtUPIS.Text));
            }
        }

        private void txtUPIS_Enter(object sender, EventArgs e)
        {
            decimal s;
            if (!string.IsNullOrEmpty(txtUPIS.Text.Trim()))
            {
                s = decimal.Parse(txtUPIS.Text, NumberStyles.Currency);
                txtUPIS.Text = s.ToString();
            }
        }

        private void txtSUP_KeyPress(object sender, KeyPressEventArgs e)
        {
            decimal x;
            char ch = e.KeyChar;
            if (ch == (char)8)
            {
                e.Handled = false;
            }
            else if (!char.IsDigit(ch) && (ch != '.' || !Decimal.TryParse(txtSUP.Text + ch, out x)))
            {
                e.Handled = true;
            }
        }

        private void txtSUP_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtSUP.Text.Trim()))
            {
                txtSUP.Text = string.Format("{0:c}", decimal.Parse(txtSUP.Text));
            }
        }

        private void txtSUP_Enter(object sender, EventArgs e)
        {
            decimal s;
            if (!string.IsNullOrEmpty(txtSUP.Text.Trim()))
            {
                s = decimal.Parse(txtSUP.Text, NumberStyles.Currency);
                txtSUP.Text = s.ToString();
            }
        }
    }
}
