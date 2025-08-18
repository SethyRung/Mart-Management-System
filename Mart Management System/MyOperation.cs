using System.Data;
using System.Data.SqlClient;

namespace Mart_Management_System
{
    internal class MyOperation
    {
        public static SqlConnection? con;
        public static string EmpID;
        public static string EmpKhName;
        public static string EmpEnName;
        public static string EmpPositon;
        public static void MyConnection()
        {
            string conStr = "Data Source = DESKTOP-VSTE6JK;Initial Catalog = MartDB;Integrated Security=True;";
            try
            {
                con = new SqlConnection(conStr);
                con.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error connected < {ex.Message}", "Connection To DataBase", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                Application.Exit();
            }
        }

        public static void OnOFFControl(Form f, bool b)
        {
            foreach(Control ct in f.Controls)
            {
                if (!(ct is Label))
                {
                    if (string.Equals(ct.Tag, "*"))
                    {

                    }
                    else
                    {
                        ct.Enabled = b;
                    }
                }
            }
        }

        public static void ClearData(Form frm)
        {
            foreach (Control ct in frm.Controls)
            {
                if (ct is TextBox || ct is MaskedTextBox || ct is ComboBox)
                {
                    ct.Text = null;
                }
                else if (ct is RadioButton)
                {
                    ((RadioButton)ct).Checked = false;
                }
                else if (ct is DateTimePicker)
                {
                    if (ct.Tag == null)
                        ((DateTimePicker)ct).CustomFormat = " ";
                }
                else if (ct is PictureBox)
                {
                    if (ct.Tag == null)
                    {
                        ((PictureBox)ct).Image = null;
                    }
                }

            }
        }

        public static void KeyControl(Form frm, object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Down)
            {
                frm.SelectNextControl((Control)sender, true, true, true, true);
            }
            if (e.KeyCode == Keys.Up)
            {
                frm.SelectNextControl((Control)sender, false, true, true, true);
            }
        }

        public static void FillCbo(ComboBox combobox, string value, string display, string SqlTable)
        {
            SqlDataAdapter da;
            DataTable dt;
            da = new SqlDataAdapter("SELECT " + value + "," + display + " FROM " + SqlTable, con);
            dt = new DataTable();
            da.Fill(dt);
            combobox.DataSource = null;
            combobox.Items.Clear();
            combobox.DataSource = dt;
            combobox.DisplayMember = display;
            combobox.ValueMember = value;
        }

    }
}
