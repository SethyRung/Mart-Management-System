using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace Mart_Management_System
{
    public partial class FrmAccountSetting : Form
    {
        public FrmAccountSetting()
        {
            InitializeComponent();
        }

        SqlDataAdapter da;
        DataTable dt;
        Boolean isNewClicked;
        string eid = "0";
        SqlCommand com;

        private void LoadData()
        {
            dt = new DataTable();
            da = new SqlDataAdapter($"SELECT * FROM dbo.GetUser('{MyOperation.EmpID}','{MyOperation.EmpPositon}')", MyOperation.con);
            if (!(MyOperation.EmpID.Equals("0") || MyOperation.EmpPositon.Equals("Admin")))
                btnNew.Enabled = false;
            da.Fill(dt);
            dgvUser.DataSource = dt;
            dgvUser.ColumnHeadersDefaultCellStyle.Font = new Font("Time New Roman", 12);
            dgvUser.DefaultCellStyle.Font = new Font("!Khmer OS Siemreap", 12);
            dgvUser.Columns["EmpID"].Width = 105;
            dgvUser.Columns["EmpKhName"].Width = 175;
            dgvUser.Columns["EmpEnName"].Width = 175;
            dgvUser.Columns["EmpPos"].Width = 175;
            dgvUser.Columns["EmpUserName"].Width = 175;
            dgvUser.Columns["EmpPassword"].Width = 175;
            
            foreach (DataGridViewColumn col in dgvUser.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dt.Dispose();
            da.Dispose();
        }

        private void FrmAccountSetting_Load(object sender, EventArgs e)
        {
            MyOperation.MyConnection();
            MyOperation.OnOFFControl(this, false);
            LoadData();
            if (MyOperation.EmpID.Equals("0") || MyOperation.EmpPositon.Equals("Admin"))
                btnNew.Enabled = true;
            else
                btnNew.Enabled = false;
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;

        }

        private void FillCboEmp()
        {
            da = new SqlDataAdapter("SELECT * FROM dbo.GetNonUser()", MyOperation.con);
            dt = new DataTable();
            da.Fill(dt);
            cboEmp.DataSource = null;
            cboEmp.Items.Clear();
            cboEmp.DataSource = dt;
            cboEmp.DisplayMember = "EmpEnName";
            cboEmp.ValueMember = "EmpID";
            cboEmp.Text = null;
        }

        private void cboEmp_SelectionChangeCommitted(object sender, EventArgs e)
        {
            eid = cboEmp.SelectedValue.ToString();
            txtUserName.Focus();
        }

        private void dgvUser_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvUser.RowCount > 0)
            {
                int i = e.RowIndex;
                if (i < 0) return;
                if (btnNew.Text == "Cancel" && btnSave.Enabled == true) return;

                eid = dgvUser.Rows[i].Cells[0].Value.ToString();
                da = new SqlDataAdapter($"SELECT tbEmployee.EmpID, EmpEnName FROM tbEmployee INNER JOIN tbUser ON tbEmployee.EmpID = tbUser.EmpID WHERE tbEmployee.EmpID = '{eid}'", MyOperation.con);
                dt = new DataTable();
                da.Fill(dt);

                cboEmp.DataSource = dt;
                cboEmp.DisplayMember = "EmpEnName";
                cboEmp.ValueMember = "EmpID";

                txtUserName.Text = dgvUser.Rows[i].Cells[4].Value.ToString();

                btnEdit.Enabled = true;
                if (MyOperation.EmpID.Equals(eid))
                    btnDelete.Enabled = false;
                else
                    btnDelete.Enabled = true;
            }
        }

        private void Modify(string type)
        {
            com = new SqlCommand("ProUser", MyOperation.con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.Add(new SqlParameter("@Type", SqlDbType.VarChar)).Value = type;
            com.Parameters.Add(new SqlParameter("@ID", SqlDbType.Char)).Value = eid;
            com.Parameters.Add(new SqlParameter("@UserName", SqlDbType.VarChar)).Value = txtUserName.Text;
            com.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar)).Value = txtPwd.Text;
            com.ExecuteNonQuery();
            MyOperation.ClearData(this);
            com.Dispose();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            isNewClicked = true;
            if (btnNew.Text == "New")
            {
                MyOperation.OnOFFControl(this, true);
                MyOperation.ClearData(this);
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                btnSave.Enabled = true;
                btnNew.Image = Mart_Management_System.Properties.Resources.exit_32px;
                btnNew.Text = "Cancel";
                FillCboEmp();
            }
            else
            {
                DialogResult rs;
                rs = MessageBox.Show("Do you want to cancel?", "Cancel...", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (rs == DialogResult.Yes)
                {
                    isNewClicked = false;
                    MyOperation.OnOFFControl(this, false);
                    btnNew.Image = Mart_Management_System.Properties.Resources.new_copy_32px;
                    btnNew.Text = "New";
                    MyOperation.ClearData(this);
                    if (MyOperation.EmpID.Equals("0") || MyOperation.EmpPositon.Equals("Admin"))
                        btnNew.Enabled = true;
                    else
                        btnNew.Enabled = false;
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                    btnSave.Enabled = false;
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            isNewClicked = false;
            MyOperation.OnOFFControl(this, true);
            btnEdit.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = true;
            btnNew.Image = Mart_Management_System.Properties.Resources.exit_32px;
            btnNew.Text = "Cancel";
            btnNew.Enabled = true;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult re;
            re = MessageBox.Show("Are you sure to Delete it?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                try
                {
                    Modify("delete");
                    MessageBox.Show("You have successfully deleted.", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MyOperation.ClearData(this);
                    LoadData();
                    FillCboEmp();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cboEmp.Text.Trim()))
            {
                MessageBox.Show("Please select an employee name.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboEmp.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                MessageBox.Show("Please enter a username.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtUserName.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtPwd.Text.Trim()))
            {
                MessageBox.Show("Please enter a password.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPwd.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(txtRePwd.Text.Trim()))
            {
                MessageBox.Show("Please enter a re-password.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRePwd.Focus();
                return;
            }
            else if (txtPwd.Text != txtRePwd.Text)
            {
                MessageBox.Show("Your password does not match with re-password.", "Messing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPwd.Text = null;
                txtRePwd.Text = null;
                txtPwd.Focus();
                return;
            }
            else
            {
                try
                {
                    if (isNewClicked == true)
                    {
                        Modify("insert");
                        MessageBox.Show("Your account has been created.", "Create", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        Modify("update");
                        MessageBox.Show("Your account has been updated.", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    btnNew.Image = Mart_Management_System.Properties.Resources.new_copy_32px;
                    btnNew.Text = "New";
                    MyOperation.ClearData(this);
                    MyOperation.OnOFFControl(this, false);
                    btnSave.Enabled = false;
                    LoadData();
                    FillCboEmp();
                    isNewClicked = false;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
