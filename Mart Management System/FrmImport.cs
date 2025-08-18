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
    public partial class FrmImport : Form
    {
        public FrmImport()
        {
            InitializeComponent();
        }

        SqlCommand com;
        string supID;
        decimal Total = 0;

        private void FrmImport_Load(object sender, EventArgs e)
        {
            MyOperation.MyConnection();
            MyOperation.OnOFFControl(this, false);

            dgvItem.ColumnHeadersDefaultCellStyle.Font = new Font("Time New Roman", 12);
            dgvItem.DefaultCellStyle.Font = new Font("!Khmer OS Siemreap", 12);
            dgvItem.Columns.Add("Product ID","Product ID");
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

                com = new SqlCommand("SELECT MAX(ImpID) FROM tbImport", MyOperation.con);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        if (dr[0].ToString() == "")
                            txtImportID.Text = "Auto Number";
                        else
                        {
                            int impID = int.Parse(dr[0].ToString()) + 1;
                            txtImportID.Text = impID.ToString();
                        }
                    }
                }
                dr.Dispose();
                com.Dispose();
                dtpImp.Focus();
                SendKeys.Send("%{DOWN}");
                MyOperation.FillCbo(cboSup, "SupID", "Supplier", "tbSupplier");
                cboSup.Text = null;
                MyOperation.FillCbo(cboCate, "CatID", "Category", "tbCategory");
                cboCate.Text = null;
                MyOperation.FillCbo(cboPro, "ProID", "ProName", "tbProduct");
                cboPro.Text = null;

                txtSupID.Enabled = false;
                txtSupCon.Enabled = false;
                txtSupAddress.Enabled = false;
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
                    txtSupID.Text = null;
                    cboSup.Text = null;
                    txtSupAddress.Text = null;
                    txtSupCon.Text = null;
                    btnNewSup.Text = "New Supplier";
                    cboSup.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
        }

        private void dtpImp_ValueChanged(object sender, EventArgs e)
        {
            dtpImp.CustomFormat = "dd/MM/yyyy";
            SendKeys.Send("%{UP}");
            cboSup.DroppedDown = true;
        }

        private void cboSup_SelectionChangeCommitted(object sender, EventArgs e)
        {
            supID = cboSup.SelectedValue.ToString();
            com = new SqlCommand($"SELECT SupID, SupAdd, SupContact FROM tbSupplier WHERE SupID='{supID}'", MyOperation.con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                txtSupID.Text = dr[0].ToString();
                txtSupAddress.Text = dr[1].ToString();
                txtSupCon.Text = dr[2].ToString();
            }
            com.Dispose();
            dr.Dispose();
        }

        private void cboPro_SelectionChangeCommitted(object sender, EventArgs e)
        {
            txtProID.Text = cboPro.SelectedValue.ToString();
            com = new SqlCommand($"SELECT CatID FROM tbProduct WHERE ProID='{txtProID.Text}'", MyOperation.con);
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                cboCate.SelectedValue = dr[0].ToString();
            }
            dr.Dispose();
            com.Dispose();
        }

        private void txtProID_TextChanged(object sender, EventArgs e)
        {
            com = new SqlCommand($"SELECT CatID, ProID FROM tbProduct WHERE ProID='{txtProID.Text}'", MyOperation.con);
            SqlDataReader dr = com.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    cboCate.SelectedValue = dr[0].ToString();
                    cboPro.SelectedValue = dr[1].ToString();
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
            if (dtpImp.CustomFormat == " ")
            {
                MessageBox.Show("Please enter import date.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtpImp.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtSupID.Text.Trim()))
            {
                MessageBox.Show("Please enter a supplier id.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupID.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(cboSup.Text.Trim()))
            {
                MessageBox.Show("Please enter a supplier.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSup.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtSupCon.Text.Trim()))
            {
                MessageBox.Show("Please enter supplier contect.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupCon.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtSupAddress.Text.Trim()))
            {
                MessageBox.Show("Please enter supplier address.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSupAddress.Focus();
                return;
            }
            else
            {
                try
                {
                    DataTable dtMaster = new DataTable();
                    dtMaster.Columns.Add("impDate", typeof(DateTime));
                    dtMaster.Columns.Add("supID", typeof(string));
                    dtMaster.Columns.Add("supplier", typeof(string));
                    dtMaster.Columns.Add("supAdd", typeof(string));
                    dtMaster.Columns.Add("supCon", typeof(string));
                    dtMaster.Columns.Add("empID", typeof(string));
                    dtMaster.Columns.Add("empKhName", typeof(string));
                    dtMaster.Columns.Add("empEnName", typeof(string));
                    dtMaster.Columns.Add("amount", typeof(float));
                    dtMaster.Rows.Add(dtpImp.Value, txtSupID.Text, cboSup.Text, txtSupAddress.Text, txtSupCon.Text, MyOperation.EmpID, MyOperation.EmpKhName, MyOperation.EmpEnName, Total);

                    DataTable dtDetail = new DataTable();
                    dtDetail.Columns.Add("proID", typeof(string));
                    dtDetail.Columns.Add("proName", typeof(string));
                    dtDetail.Columns.Add("qty", typeof(int));
                    dtDetail.Columns.Add("upis", typeof(float));
                    dtDetail.Columns.Add("catID", typeof(string));
                    foreach (DataGridViewRow row in dgvItem.Rows)
                        dtDetail.Rows.Add(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), int.Parse(row.Cells[3].Value.ToString()), decimal.Parse(row.Cells[4].Value.ToString(), NumberStyles.Currency), row.Cells[6].Value.ToString());

                    com = new SqlCommand("ProImport", MyOperation.con);
                    com.CommandType = CommandType.StoredProcedure;
                    if (btnNewSup.Text == "Old Supplier")
                        com.Parameters.Add(new SqlParameter("@isNewSupplier", SqlDbType.Char)).Value = '1';
                    else
                        com.Parameters.Add(new SqlParameter("@isNewSupplier", SqlDbType.Char)).Value = '0';

                    com.Parameters.Add(new SqlParameter("@IM", SqlDbType.Structured)).Value = dtMaster;
                    com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Structured)).Value = dtDetail;

                    //SqlParameter par2 = new SqlParameter("@IM", SqlDbType.Structured);
                    //par2.Value = dtMaster;
                    //com.Parameters.Add(par2);

                    //SqlParameter par3 = new SqlParameter("@ID", SqlDbType.Structured);
                    //par3.Value = dtDetail;
                    //com.Parameters.Add(par3);

                    com.ExecuteNonQuery();
                    MessageBox.Show("You have successfully saved.", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyOperation.ClearData(this);
                    txtSupID.Text = null;
                    cboSup.Text = null;
                    txtSupAddress.Text = null;
                    txtSupCon.Text = null;
                    dgvItem.Rows.Clear();
                    MyOperation.OnOFFControl(this, false);
                    btnNew.Image = Mart_Management_System.Properties.Resources.new_copy_32px;
                    btnNew.Text = "New";
                }
                catch (Exception ex)
                {
                    //if (ex.Message.Contains("Violation of PRIMARY KEY constraint"))
                    //{
                    //    if (btnNewSup.Text == "Old Supplier")
                    //        MessageBox.Show("Supplier ID is already existed");
                    //}
                    //else
                        MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnNewSup_Click(object sender, EventArgs e)
        {
            if (btnNewSup.Text == "New Supplier")
            {
                cboSup.DropDownStyle = ComboBoxStyle.Simple;
                txtSupID.Enabled = false;
                txtSupID.Text = "លេខសម្គាល់ដោយស្វ័យប្រវត្តិ";
                cboSup.Text = null;
                txtSupAddress.Text = null;
                txtSupCon.Text = null;
                cboSup.Focus();
                txtSupCon.Enabled = true;
                txtSupAddress.Enabled = true;
                btnNewSup.Text = "Old Supplier";

            }
            else
            {
                btnNewSup.Text = "New Supplier";
                cboSup.DropDownStyle = ComboBoxStyle.DropDownList;
                txtSupID.Enabled = false;
                txtSupCon.Enabled = false;
                txtSupAddress.Enabled = false;
                txtSupID.Text = null;
                cboSup.Text = null;
                txtSupAddress.Text = null;
                txtSupCon.Text = null;
            }
        }
    }
}
