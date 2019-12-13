namespace Smart.SuptoManager
{
    partial class XF_Logs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XF_Logs));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            this.gridData = new System.Windows.Forms.DataGridView();
            this.key_id = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.key_name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.user_code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.unp = new System.Windows.Forms.TextBox();
            this.fromDateTime = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.toDateTime = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExport = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.col_LogDateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_UserCode = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_UserFullName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Unp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_ActionTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_TableName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.col_Preview = new System.Windows.Forms.DataGridViewButtonColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridData
            // 
            this.gridData.AllowUserToAddRows = false;
            this.gridData.AllowUserToDeleteRows = false;
            this.gridData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.col_LogDateTime,
            this.col_UserCode,
            this.col_UserFullName,
            this.col_Unp,
            this.col_ActionTypeName,
            this.col_TableName,
            this.col_Preview});
            this.gridData.Location = new System.Drawing.Point(0, 28);
            this.gridData.Name = "gridData";
            this.gridData.ReadOnly = true;
            this.gridData.Size = new System.Drawing.Size(780, 678);
            this.gridData.TabIndex = 0;
            // 
            // key_id
            // 
            this.key_id.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.key_id.Location = new System.Drawing.Point(14, 33);
            this.key_id.Name = "key_id";
            this.key_id.Size = new System.Drawing.Size(259, 26);
            this.key_id.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(95, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "ID на записа:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(95, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Таблица:";
            // 
            // key_name
            // 
            this.key_name.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.key_name.Location = new System.Drawing.Point(14, 85);
            this.key_name.Name = "key_name";
            this.key_name.Size = new System.Drawing.Size(259, 26);
            this.key_name.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(95, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 20);
            this.label3.TabIndex = 5;
            this.label3.Text = "Код на оператора:";
            // 
            // user_code
            // 
            this.user_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.user_code.Location = new System.Drawing.Point(14, 137);
            this.user_code.Name = "user_code";
            this.user_code.Size = new System.Drawing.Size(259, 26);
            this.user_code.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(95, 166);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 20);
            this.label4.TabIndex = 7;
            this.label4.Text = "УНП:";
            // 
            // unp
            // 
            this.unp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unp.Location = new System.Drawing.Point(14, 189);
            this.unp.Name = "unp";
            this.unp.Size = new System.Drawing.Size(259, 26);
            this.unp.TabIndex = 6;
            // 
            // fromDateTime
            // 
            this.fromDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromDateTime.Location = new System.Drawing.Point(14, 241);
            this.fromDateTime.Name = "fromDateTime";
            this.fromDateTime.Size = new System.Drawing.Size(259, 26);
            this.fromDateTime.TabIndex = 8;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(95, 218);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(119, 20);
            this.label5.TabIndex = 9;
            this.label5.Text = "От дата и час:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(95, 270);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 20);
            this.label6.TabIndex = 11;
            this.label6.Text = "До дата и час:";
            // 
            // toDateTime
            // 
            this.toDateTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toDateTime.Location = new System.Drawing.Point(14, 293);
            this.toDateTime.Name = "toDateTime";
            this.toDateTime.Size = new System.Drawing.Size(259, 26);
            this.toDateTime.TabIndex = 10;
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearch.Location = new System.Drawing.Point(63, 325);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(182, 38);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Търси";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExport});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1071, 28);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnExport
            // 
            this.btnExport.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExport.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(71, 25);
            this.btnExport.Text = "Експорт";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 706);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1071, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // progress
            // 
            this.progress.Name = "progress";
            this.progress.Size = new System.Drawing.Size(100, 16);
            this.progress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progress.Visible = false;
            // 
            // col_LogDateTime
            // 
            this.col_LogDateTime.DataPropertyName = "log_datetime";
            dataGridViewCellStyle7.Format = "dd.MM.yyyy HH:mm:ss";
            dataGridViewCellStyle7.NullValue = null;
            this.col_LogDateTime.DefaultCellStyle = dataGridViewCellStyle7;
            this.col_LogDateTime.HeaderText = "Дата/час";
            this.col_LogDateTime.Name = "col_LogDateTime";
            this.col_LogDateTime.ReadOnly = true;
            this.col_LogDateTime.Width = 120;
            // 
            // col_UserCode
            // 
            this.col_UserCode.DataPropertyName = "user_code";
            this.col_UserCode.HeaderText = "Потр. код";
            this.col_UserCode.Name = "col_UserCode";
            this.col_UserCode.ReadOnly = true;
            // 
            // col_UserFullName
            // 
            this.col_UserFullName.DataPropertyName = "full_name";
            this.col_UserFullName.HeaderText = "Потребител";
            this.col_UserFullName.Name = "col_UserFullName";
            this.col_UserFullName.ReadOnly = true;
            // 
            // col_Unp
            // 
            this.col_Unp.DataPropertyName = "unp";
            this.col_Unp.HeaderText = "УНП";
            this.col_Unp.Name = "col_Unp";
            this.col_Unp.ReadOnly = true;
            // 
            // col_ActionTypeName
            // 
            this.col_ActionTypeName.DataPropertyName = "action_type_name";
            this.col_ActionTypeName.HeaderText = "Действие";
            this.col_ActionTypeName.Name = "col_ActionTypeName";
            this.col_ActionTypeName.ReadOnly = true;
            // 
            // col_TableName
            // 
            this.col_TableName.DataPropertyName = "table_name";
            this.col_TableName.HeaderText = "Таблица(запис)";
            this.col_TableName.Name = "col_TableName";
            this.col_TableName.ReadOnly = true;
            this.col_TableName.Width = 150;
            // 
            // col_Preview
            // 
            this.col_Preview.HeaderText = "Преглед";
            this.col_Preview.Name = "col_Preview";
            this.col_Preview.ReadOnly = true;
            this.col_Preview.Text = "Преглед";
            this.col_Preview.UseColumnTextForButtonValue = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.key_id);
            this.panel1.Controls.Add(this.toDateTime);
            this.panel1.Controls.Add(this.key_name);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.fromDateTime);
            this.panel1.Controls.Add(this.user_code);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.unp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(786, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 678);
            this.panel1.TabIndex = 3;
            // 
            // XF_Logs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1071, 728);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.gridData);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "XF_Logs";
            this.Text = "Логове";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.TextBox key_id;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox unp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox user_code;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox key_name;
        private System.Windows.Forms.DateTimePicker fromDateTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker toDateTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExport;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_LogDateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_UserCode;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_UserFullName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_Unp;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_ActionTypeName;
        private System.Windows.Forms.DataGridViewTextBoxColumn col_TableName;
        private System.Windows.Forms.DataGridViewButtonColumn col_Preview;
        private System.Windows.Forms.Panel panel1;
    }
}