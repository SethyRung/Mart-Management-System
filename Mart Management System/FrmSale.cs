using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mart_Management_System
{
    public partial class FrmSale : Form
    {
        public FrmSale()
        {
            InitializeComponent();
        }

        SqlCommand com;
        string cusID;
        decimal Total = 0;

        private void FrmSale_Load(object sender, EventArgs e)
        {
            MyOperation.MyConnection();
            MyOperation.OnOFFControl(this, false);

            dgvItem.ColumnHeadersDefaultCellStyle.Font = new Font("Time New Roman", 12);
            dgvItem.DefaultCellStyle.Font = new Font("!Khmer OS Siemreap", 12);
            dgvItem.Columns.Add("Product ID", "Product ID");
            dgvItem.Columns.Add("Product Name", "Product Name");
            dgvItem.Columns.Add("Category", "Category");
            dgvItem.Columns.Add("QTY", "QTY");
            dgvItem.Columns.Add("Price", "Price");
            dgvItem.Columns.Add("Amount", "Amount");
            dgvItem.Columns.Add("CatID", "CatID");

            dgvItem.Columns["Product ID"].Width = 188;
            dgvItem.Columns["Product Name"].Width = 190;
            dgvItem.Columns["Category"].Width = 190;
            dgvItem.Columns["QTY"].Width = 190;
            dgvItem.Columns["Price"].Width = 190;
            dgvItem.Columns["Amount"].Width = 220;
            dgvItem.Columns["CatID"].Visible = false;
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (btnNew.Text == "New")
            {
                Total = 0;
                MyOperation.OnOFFControl(this, true);
                btnNew.Image = Mart_Management_System.Properties.Resources.exit_32px;
                btnNew.Text = "Cancel";
                MyOperation.ClearData(this);

                com = new SqlCommand("SELECT MAX(SaleID) FROM tbSale", MyOperation.con);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == "")
                            txtSaleID.Text = "Auto Number";
                        else
                        {
                            int impID = int.Parse(dr[0].ToString()) + 1;
                            txtSaleID.Text = impID.ToString();
                        }
                    }
                }
                dr.Dispose();
                com.Dispose();
                dtpSale.Focus();
                SendKeys.Send("%{DOWN}");
                MyOperation.FillCbo(cboCus, "CusID", "CusName", "tbCustomer");
                cboCus.Text = null;
                MyOperation.FillCbo(cboCate, "CatID", "Category", "tbCategory");
                cboCate.Text = null;
                MyOperation.FillCbo(cboPro, "ProID", "ProName", "tbProduct");
                cboPro.Text = null;

                txtCusID.Enabled = false;
                txtCusCon.Enabled = false;
                txtCusAddress.Enabled = false;

                txtQty.Text = "5";
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
                    txtCusID.Text = null;
                    cboCus.Text = null;
                    txtCusAddress.Text = null;
                    txtCusCon.Text = null;
                    btnNewCus.Text = "New Supplier";
                    cboCus.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }

        private void dtpSale_ValueChanged(object sender, EventArgs e)
        {
            dtpSale.CustomFormat = "dd/MM/yyyy";
            SendKeys.Send("%{UP}");
            cboCus.DroppedDown = true;
        }

        private void cboCus_SelectionChangeCommitted(object sender, EventArgs e)
        {
            cusID = cboCus.SelectedValue.ToString();
            com = new SqlCommand("SELECT CusID, CusAdd, CusContact FROM tbCustomer WHERE CusID='" + cusID + "'", MyOperation.con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtCusID.Text = dr[0].ToString();
                txtCusAddress.Text = dr[1].ToString();
                txtCusCon.Text = dr[2].ToString();
            }
            com.Dispose();
            dr.Dispose();
        }

        private void cboPro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtProID.Text = cboPro.SelectedValue.ToString();
            com = new SqlCommand("SELECT CatID,SUP FROM tbProduct WHERE ProID='" + txtProID.Text + "'", MyOperation.con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                cboCate.SelectedValue = dr[0].ToString();
                txtPrice.Text = dr[1].ToString();
            }
            dr.Dispose();
            com.Dispose();
        }

        private void txtProID_TextChanged(object sender, EventArgs e)
        {
            com = new SqlCommand("SELECT CatID, ProID, SUP FROM tbProduct WHERE ProID='" + txtProID.Text + "'", MyOperation.con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboCate.SelectedValue = dr[0].ToString();
                    cboPro.SelectedValue = dr[1].ToString();
                    txtPrice.Text = dr[2].ToString();
                }
            }
            else
            {
                cboCate.Text = null;
                cboPro.Text = null;
            }
            dr.Dispose();
            com.Dispose();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProID.Text.Trim()))
            {
                MessageBox.Show("Please enter product id.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(cboPro.Text.Trim()))
            {
                MessageBox.Show("Please enter product name.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(txtQty.Text.Trim()))
            {
                MessageBox.Show("Please enter quantity.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(txtPrice.Text.Trim()))
            {
                MessageBox.Show("Please enter price.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else if (string.IsNullOrEmpty(cboCate.Text.Trim()))
            {
                MessageBox.Show("Please enter category.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                decimal price = decimal.Parse(txtPrice.Text);
                decimal amount = decimal.Parse(txtQty.Text) * decimal.Parse(txtPrice.Text);
                dgvItem.Rows.Add(txtProID.Text, cboPro.Text, cboCate.Text, txtQty.Text, string.Format("{0:c}", decimal.Parse(txtPrice.Text)), string.Format("{0:c}", decimal.Parse(txtQty.Text) * decimal.Parse(txtPrice.Text)), cboCate.SelectedValue);
                Total += amount;
                txtTotal.Text = string.Format("{0:c}", Total);

                txtProID.Text = null;
                txtQty.Text = null;
                txtPrice.Text = null;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            DialogResult re;
            if (dgvItem.SelectedRows[0].Index != -1)
            {
                re = MessageBox.Show("Do you want to remove this item?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (re == DialogResult.Yes)
                {
                    var a = decimal.Parse(dgvItem.SelectedRows[0].Cells[5].Value.ToString(), NumberStyles.Currency);
                    dgvItem.Rows.Remove(dgvItem.SelectedRows[0]);
                    Total = Total - a;
                    txtTotal.Text = string.Format("{0:c}", Total);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (dtpSale.CustomFormat == " ")
            {
                MessageBox.Show("Please enter sale date.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpSale.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtCusID.Text.Trim()))
            {
                MessageBox.Show("Please enter a customer id.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCusID.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(cboCus.Text.Trim()))
            {
                MessageBox.Show("Please enter a customer.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCus.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtCusCon.Text.Trim()))
            {
                MessageBox.Show("Please enter customer contect.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCusCon.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtCusAddress.Text.Trim()))
            {
                MessageBox.Show("Please enter customer address.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCusAddress.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtMaster = new DataTable();
                    dtMaster.Columns.Add("saleDate", typeof(DateTime));
                    dtMaster.Columns.Add("cusID", typeof(int));
                    dtMaster.Columns.Add("cusName", typeof(string));
                    dtMaster.Columns.Add("cusAdd", typeof(string));
                    dtMaster.Columns.Add("cusCon", typeof(string));
                    dtMaster.Columns.Add("empID", typeof(string));
                    dtMaster.Columns.Add("empKhName", typeof(string));
                    dtMaster.Columns.Add("empEnName", typeof(string));
                    dtMaster.Columns.Add("amount", typeof(float));
                    dtMaster.Rows.Add(dtpSale.Value, int.Parse(txtCusID.Text), cboCus.Text, txtCusAddress.Text, txtCusCon.Text, MyOperation.EmpID, MyOperation.EmpKhName, MyOperation.EmpEnName, Total);

                    DataTable dtDetail = new DataTable();
                    dtDetail.Columns.Add("proID", typeof(string));
                    dtDetail.Columns.Add("proName", typeof(string));
                    dtDetail.Columns.Add("qty", typeof(int));
                    dtDetail.Columns.Add("sup", typeof(float));
                    dtDetail.Columns.Add("catID", typeof(string));
                    foreach (DataGridViewRow row in dgvItem.Rows)
                        dtDetail.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), int.Parse(row.Cells[3].Value.ToString()), decimal.Parse(row.Cells[4].Value.ToString(), NumberStyles.Currency), row.Cells[6].Value.ToString());

                    com = new SqlCommand("ProSale", MyOperation.con);
                    com.CommandType = CommandType.StoredProcedure;
                    if (btnNewCus.Text == "Old Customer")
                        com.Parameters.Add(new SqlParameter("@isNewCustomer", SqlDbType.Char)).Value = '1';
                    else
                        com.Parameters.Add(new SqlParameter("@isNewCustomer", SqlDbType.Char)).Value = '0';

                    com.Parameters.Add(new SqlParameter("@SM", SqlDbType.Structured)).Value = dtMaster;
                    com.Parameters.Add(new SqlParameter("@SD", SqlDbType.Structured)).Value = dtDetail;

                    com.ExecuteNonQuery();
                    MessageBox.Show("You have successfully saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyOperation.ClearData(this);
                    txtCusID.Text = null;
                    cboCus.Text = null;
                    txtCusAddress.Text = null;
                    txtCusCon.Text = null;
                    dgvItem.Rows.Clear();
                    MyOperation.OnOFFControl(this, false);
                    btnNew.Image = Mart_Management_System.Properties.Resources.new_copy_32px;
                    btnNew.Text = "New";
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNewCus_Click(object sender, EventArgs e)
        {
            if (btnNewCus.Text == "New Customer")
            {
                cboCus.DropDownStyle = ComboBoxStyle.Simple;
                txtCusID.Text = null;
                cboCus.Text = null;
                txtCusAddress.Text = null;
                txtCusCon.Text = null;
                txtCusID.Focus();
                txtCusID.Enabled = true;
                txtCusCon.Enabled = true;
                txtCusAddress.Enabled = true;
                btnNewCus.Text = "Old Customer";
            }
            else
            {
                btnNewCus.Text = "New Customer";
                cboCus.DropDownStyle = ComboBoxStyle.DropDownList;
                txtCusID.Enabled = false;
                txtCusCon.Enabled = false;
                txtCusAddress.Enabled = false;
                txtCusID.Text = null;
                cboCus.Text = null;
                txtCusAddress.Text = null;
                txtCusCon.Text = null;
            }
        }
    }
}
