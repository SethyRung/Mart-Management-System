namespace Mart_Management_System
{
    partial class FrmProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPro = new System.Windows.Forms.DataGridView();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtUPIS = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtQty = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSUP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPro)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPro
            // 
            this.dgvPro.AllowUserToAddRows = false;
            this.dgvPro.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.dgvPro.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvPro.BackgroundColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("!Khmer OS Siemreap", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPro.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvPro.ColumnHeadersHeight = 40;
            this.dgvPro.EnableHeadersVisualStyles = false;
            this.dgvPro.Location = new System.Drawing.Point(69, 396);
            this.dgvPro.Margin = new System.Windows.Forms.Padding(0);
            this.dgvPro.Name = "dgvPro";
            this.dgvPro.ReadOnly = true;
            this.dgvPro.RowHeadersVisible = false;
            this.dgvPro.RowTemplate.Height = 60;
            this.dgvPro.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPro.Size = new System.Drawing.Size(1125, 370);
            this.dgvPro.TabIndex = 42;
            this.dgvPro.Tag = "*";
            this.dgvPro.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPro_CellClick);
            // 
            // txtSearch
            // 
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(69, 315);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(0);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "ស្វែងរកទំនិញ";
            this.txtSearch.Size = new System.Drawing.Size(1125, 40);
            this.txtSearch.TabIndex = 7;
            this.txtSearch.Tag = "*";
            this.txtSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSearch_KeyUp);
            // 
            // btnDelete
            // 
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDelete.Image = global::Mart_Management_System.Properties.Resources.delete_document_32px;
            this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDelete.Location = new System.Drawing.Point(1054, 133);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(0);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnDelete.Size = new System.Drawing.Size(140, 40);
            this.btnDelete.TabIndex = 9;
            this.btnDelete.Text = "Delete";
            this.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEdit.Image = global::Mart_Management_System.Properties.Resources.edit_property_32px;
            this.btnEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEdit.Location = new System.Drawing.Point(1054, 61);
            this.btnEdit.Margin = new System.Windows.Forms.Padding(0);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnEdit.Size = new System.Drawing.Size(140, 40);
            this.btnEdit.TabIndex = 8;
            this.btnEdit.Text = "Edit";
            this.btnEdit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // cboCategory
            // 
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Items.AddRange(new object[] {
            "Admin",
            "Stock Manager",
            "Seller"});
            this.cboCategory.Location = new System.Drawing.Point(704, 132);
            this.cboCategory.Margin = new System.Windows.Forms.Padding(0);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(230, 41);
            this.cboCategory.TabIndex = 4;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(562, 132);
            this.label9.Margin = new System.Windows.Forms.Padding(0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(142, 40);
            this.label9.TabIndex = 31;
            this.label9.Text = "ប្រភេទទំនិញ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtUPIS
            // 
            this.txtUPIS.Location = new System.Drawing.Point(211, 202);
            this.txtUPIS.Margin = new System.Windows.Forms.Padding(0);
            this.txtUPIS.Name = "txtUPIS";
            this.txtUPIS.Size = new System.Drawing.Size(230, 40);
            this.txtUPIS.TabIndex = 5;
            this.txtUPIS.Enter += new System.EventHandler(this.txtUPIS_Enter);
            this.txtUPIS.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUPIS_KeyPress);
            this.txtUPIS.Leave += new System.EventHandler(this.txtUPIS_Leave);
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(69, 202);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(142, 40);
            this.label4.TabIndex = 32;
            this.label4.Text = "តម្លៃនាំចូល";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtQty
            // 
            this.txtQty.Location = new System.Drawing.Point(211, 132);
            this.txtQty.Margin = new System.Windows.Forms.Padding(0);
            this.txtQty.Name = "txtQty";
            this.txtQty.Size = new System.Drawing.Size(230, 40);
            this.txtQty.TabIndex = 3;
            this.txtQty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQty_KeyPress);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(69, 131);
            this.label3.Margin = new System.Windows.Forms.Padding(0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(142, 40);
            this.label3.TabIndex = 33;
            this.label3.Text = "បរិមាណទំនិញ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProID
            // 
            this.txtProID.Enabled = false;
            this.txtProID.Location = new System.Drawing.Point(211, 62);
            this.txtProID.Margin = new System.Windows.Forms.Padding(0);
            this.txtProID.Name = "txtProID";
            this.txtProID.Size = new System.Drawing.Size(230, 40);
            this.txtProID.TabIndex = 1;
            this.txtProID.Tag = "*";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(69, 61);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 40);
            this.label1.TabIndex = 34;
            this.label1.Text = "លេខកូដទំនិញ";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtSUP
            // 
            this.txtSUP.Location = new System.Drawing.Point(704, 203);
            this.txtSUP.Margin = new System.Windows.Forms.Padding(0);
            this.txtSUP.Name = "txtSUP";
            this.txtSUP.Size = new System.Drawing.Size(230, 40);
            this.txtSUP.TabIndex = 6;
            this.txtSUP.Enter += new System.EventHandler(this.txtSUP_Enter);
            this.txtSUP.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSUP_KeyPress);
            this.txtSUP.Leave += new System.EventHandler(this.txtSUP_Leave);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(562, 203);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(142, 40);
            this.label2.TabIndex = 43;
            this.label2.Text = "តម្លៃលក់ចេញ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtProName
            // 
            this.txtProName.Location = new System.Drawing.Point(704, 62);
            this.txtProName.Margin = new System.Windows.Forms.Padding(0);
            this.txtProName.Name = "txtProName";
            this.txtProName.Size = new System.Drawing.Size(230, 40);
            this.txtProName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(562, 62);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(142, 40);
            this.label5.TabIndex = 45;
            this.label5.Text = "ឈ្មោះទំនិញ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::Mart_Management_System.Properties.Resources.save_32px;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1054, 203);
            this.btnSave.Margin = new System.Windows.Forms.Padding(0);
            this.btnSave.Name = "btnSave";
            this.btnSave.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.btnSave.Size = new System.Drawing.Size(140, 40);
            this.btnSave.TabIndex = 46;
            this.btnSave.Text = "Save";
            this.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // FrmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 33F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(249)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(1262, 853);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.txtProName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtSUP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvPro);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.cboCategory);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtUPIS);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtQty);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtProID);
            this.Controls.Add(this.label1);
            this.Font = new System.Drawing.Font("!Khmer OS Siemreap", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4, 7, 4, 7);
            this.Name = "FrmProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ទំនិញ";
            this.Load += new System.EventHandler(this.FrmProduct_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPro)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dgvPro;
        private TextBox txtSearch;
        private Button btnDelete;
        private Button btnEdit;
        private ComboBox cboCategory;
        private Label label9;
        private TextBox txtUPIS;
        private Label label4;
        private TextBox txtQty;
        private Label label3;
        private TextBox txtProID;
        private Label label1;
        private TextBox txtSUP;
        private Label label2;
        private TextBox txtProName;
        private Label label5;
        private Button btnSave;
    }
}