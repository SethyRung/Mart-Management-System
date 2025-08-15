using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mart_Management_System
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }

        private Button currentButton;
        private Form activeForm;

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    if(currentButton != null)
                        currentButton.BackColor = ColorTranslator.FromHtml("#33334C");
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = ColorTranslator.FromHtml("#5252CF");
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();
            ActivateButton(btnSender);
            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDesktopPane.Controls.Add(childForm);
            this.panelDesktopPane.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lblTitle.Text = childForm.Text;
        }

        //private void Reset()
        //{
        //    lblTitle.Text = "Home";
        //    currentButton = null;
        //}

        private void FrmMain_Load(object sender, EventArgs e)
        {
            btnHome.PerformClick();
            if (MyOperation.EmpPositon == "Admin" && MyOperation.EmpID != "0")
            {
                return;
            }
            else if (MyOperation.EmpPositon == "Seller")
            {
                btnImport.Visible = false;
                btnSellReport.Visible = false;
                btnEmployee.Visible = false;
                btnSupplier.Visible = false;
                btnProduct.Visible = false;
                btnCategory.Visible = false;
            }
            else if (MyOperation.EmpPositon == "Stock Manager")
            {
                btnEmployee.Visible = false;
                btnCustomer.Visible = false;
                btnSale.Visible = false;
                btnSellReport.Visible = false;
            }
            else
            {
                btnImport.Visible = false;
                btnSellReport.Visible = false;
                btnSupplier.Visible = false;
                btnProduct.Visible = false;
                btnCustomer.Visible = false;
                btnSale.Visible = false;
                btnSellReport.Visible = false;
                btnCategory.Visible = false;
            }
        }

        //private void btnCloseChildForm_Click(object sender, EventArgs e)
        //{
        //    if (activeForm != null)
        //    {
        //        DialogResult re;
        //        re = MessageBox.Show("Do you want to close ?", "Close", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (re == DialogResult.Yes)
        //        {
        //            activeForm.Close();
        //            Reset();
        //        }
        //    }
        //}

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void lblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmHome(), sender);
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmEmployee(), sender);
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCustomer(), sender);
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCategory(), sender);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmProduct(), sender);
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSupplier(), sender);
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmImport(), sender);
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSale(), sender);
        }

        //private void btnImportReport_Click(object sender, EventArgs e)
        //{
        //    OpenChildForm(new FrmImportReport(), sender);
        //}

        //private void btnSaleReport_Click(object sender, EventArgs e)
        //{
        //    OpenChildForm(new FrmImportReport(), sender);
        //}

        private void btnAccountSetting_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmAccountSetting(), sender);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult re = MessageBox.Show("Do you want to exit?", "Exit Program", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (re == DialogResult.Yes)
            {
                this.Hide();

                FrmLogin ln = new FrmLogin();
                ln.ShowDialog();

                this.Close();
            }
        }

    }
}
