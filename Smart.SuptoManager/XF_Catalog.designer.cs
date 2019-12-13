namespace Smart.SuptoManager
{
    partial class XF_Catalog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XF_Catalog));
            this.gridData = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.progress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExportToCSV = new System.Windows.Forms.ToolStripButton();
            this.btnExportTables = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.user_code = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.sell_workplace_code = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.serial_number = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.sell_point_code = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.listQuery = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.unp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnSearch = new System.Windows.Forms.Button();
            this.toDate = new System.Windows.Forms.DateTimePicker();
            this.fromDate = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnLogs = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
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
            this.gridData.Location = new System.Drawing.Point(0, 25);
            this.gridData.Name = "gridData";
            this.gridData.ReadOnly = true;
            this.gridData.Size = new System.Drawing.Size(717, 679);
            this.gridData.TabIndex = 1;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.progress});
            this.statusStrip1.Location = new System.Drawing.Point(0, 707);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
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
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExportToCSV,
            this.btnExportTables,
            this.btnLogs});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1008, 28);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnExportToCSV
            // 
            this.btnExportToCSV.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExportToCSV.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnExportToCSV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportToCSV.Name = "btnExportToCSV";
            this.btnExportToCSV.Size = new System.Drawing.Size(136, 25);
            this.btnExportToCSV.Text = "Експорт към CSV";
            this.btnExportToCSV.Click += new System.EventHandler(this.btnExportToCSV_Click);
            // 
            // btnExportTables
            // 
            this.btnExportTables.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnExportTables.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnExportTables.Image = ((System.Drawing.Image)(resources.GetObject("btnExportTables.Image")));
            this.btnExportTables.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExportTables.Name = "btnExportTables";
            this.btnExportTables.Size = new System.Drawing.Size(283, 25);
            this.btnExportTables.Text = "Експорт на данни директно от базата";
            this.btnExportTables.Click += new System.EventHandler(this.btnExportTables_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.user_code);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.sell_workplace_code);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.serial_number);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.sell_point_code);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.listQuery);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.unp);
            this.panel1.Controls.Add(this.label8);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Controls.Add(this.toDate);
            this.panel1.Controls.Add(this.fromDate);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(723, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 679);
            this.panel1.TabIndex = 4;
            // 
            // user_code
            // 
            this.user_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.user_code.Location = new System.Drawing.Point(9, 395);
            this.user_code.Name = "user_code";
            this.user_code.Size = new System.Drawing.Size(264, 26);
            this.user_code.TabIndex = 26;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(56, 372);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 20);
            this.label4.TabIndex = 25;
            this.label4.Text = "Код на оператор";
            // 
            // sell_workplace_code
            // 
            this.sell_workplace_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sell_workplace_code.Location = new System.Drawing.Point(9, 343);
            this.sell_workplace_code.Name = "sell_workplace_code";
            this.sell_workplace_code.Size = new System.Drawing.Size(264, 26);
            this.sell_workplace_code.TabIndex = 24;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(35, 320);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(178, 20);
            this.label3.TabIndex = 23;
            this.label3.Text = "Код на работно място";
            // 
            // serial_number
            // 
            this.serial_number.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.serial_number.Location = new System.Drawing.Point(9, 291);
            this.serial_number.Name = "serial_number";
            this.serial_number.Size = new System.Drawing.Size(264, 26);
            this.serial_number.TabIndex = 22;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 268);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(224, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "Индивидуален номер на ФУ";
            // 
            // sell_point_code
            // 
            this.sell_point_code.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sell_point_code.Location = new System.Drawing.Point(9, 239);
            this.sell_point_code.Name = "sell_point_code";
            this.sell_point_code.Size = new System.Drawing.Size(264, 26);
            this.sell_point_code.TabIndex = 20;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(24, 216);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 20);
            this.label1.TabIndex = 19;
            this.label1.Text = "Код на търговския обект";
            // 
            // listQuery
            // 
            this.listQuery.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.listQuery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listQuery.FormattingEnabled = true;
            this.listQuery.Location = new System.Drawing.Point(9, 29);
            this.listQuery.Name = "listQuery";
            this.listQuery.Size = new System.Drawing.Size(264, 28);
            this.listQuery.TabIndex = 18;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(88, 6);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 20);
            this.label9.TabIndex = 17;
            this.label9.Text = "Справка";
            // 
            // unp
            // 
            this.unp.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.unp.Location = new System.Drawing.Point(9, 187);
            this.unp.Name = "unp";
            this.unp.Size = new System.Drawing.Size(264, 26);
            this.unp.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(103, 164);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(43, 20);
            this.label8.TabIndex = 14;
            this.label8.Text = "УНП";
            // 
            // btnSearch
            // 
            this.btnSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSearch.Location = new System.Drawing.Point(33, 427);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(182, 38);
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "Търси";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // toDate
            // 
            this.toDate.CustomFormat = "dd.MM.yyyy";
            this.toDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.toDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.toDate.Location = new System.Drawing.Point(9, 135);
            this.toDate.Name = "toDate";
            this.toDate.Size = new System.Drawing.Size(264, 26);
            this.toDate.TabIndex = 12;
            // 
            // fromDate
            // 
            this.fromDate.CustomFormat = "dd.MM.yyyy";
            this.fromDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.fromDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.fromDate.Location = new System.Drawing.Point(9, 83);
            this.fromDate.Name = "fromDate";
            this.fromDate.Size = new System.Drawing.Size(264, 26);
            this.fromDate.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(88, 112);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 20);
            this.label7.TabIndex = 10;
            this.label7.Text = "До дата";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(87, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(72, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "От дата";
            // 
            // btnLogs
            // 
            this.btnLogs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.btnLogs.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnLogs.Image = ((System.Drawing.Image)(resources.GetObject("btnLogs.Image")));
            this.btnLogs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLogs.Name = "btnLogs";
            this.btnLogs.Size = new System.Drawing.Size(65, 25);
            this.btnLogs.Text = "Логове";
            this.btnLogs.Click += new System.EventHandler(this.btnLogs_Click);
            // 
            // XF_Catalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 729);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gridData);
            this.DoubleBuffered = true;
            this.Name = "XF_Catalog";
            this.Text = "VETNavigator SUPTO VIEWER";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView gridData;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExportToCSV;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker toDate;
        private System.Windows.Forms.DateTimePicker fromDate;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.TextBox unp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox listQuery;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ToolStripProgressBar progress;
        private System.Windows.Forms.TextBox user_code;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox sell_workplace_code;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox serial_number;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox sell_point_code;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripButton btnExportTables;
        private System.Windows.Forms.ToolStripButton btnLogs;
    }
}