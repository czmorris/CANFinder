namespace CANFinder
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.listIDs = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtLogFilePath = new System.Windows.Forms.TextBox();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.btnLoad = new System.Windows.Forms.Button();
            this.dgvMessages = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowHex = new System.Windows.Forms.CheckBox();
            this.btnSaveParsed = new System.Windows.Forms.Button();
            this.dlgSaveParse = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).BeginInit();
            this.SuspendLayout();
            // 
            // listIDs
            // 
            this.listIDs.FormattingEnabled = true;
            this.listIDs.ItemHeight = 15;
            this.listIDs.Location = new System.Drawing.Point(12, 101);
            this.listIDs.Name = "listIDs";
            this.listIDs.ScrollAlwaysVisible = true;
            this.listIDs.Size = new System.Drawing.Size(235, 529);
            this.listIDs.TabIndex = 0;
            this.listIDs.SelectedIndexChanged += new System.EventHandler(this.listIDs_SelectedIndexChanged);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // txtLogFilePath
            // 
            this.txtLogFilePath.Location = new System.Drawing.Point(12, 36);
            this.txtLogFilePath.Name = "txtLogFilePath";
            this.txtLogFilePath.Size = new System.Drawing.Size(262, 23);
            this.txtLogFilePath.TabIndex = 1;
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(12, 13);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(75, 15);
            this.lblFilePath.TabIndex = 2;
            this.lblFilePath.Text = "Log File Path";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(281, 35);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(74, 23);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "Browse";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(361, 35);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(74, 23);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "LOAD";
            this.btnLoad.UseVisualStyleBackColor = true;
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dgvMessages
            // 
            this.dgvMessages.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dgvMessages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvMessages.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvMessages.Location = new System.Drawing.Point(253, 101);
            this.dgvMessages.Name = "dgvMessages";
            this.dgvMessages.Size = new System.Drawing.Size(989, 529);
            this.dgvMessages.TabIndex = 4;
            this.dgvMessages.Text = "dataGridView1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(253, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(196, 15);
            this.label1.TabIndex = 5;
            this.label1.Text = "All messages found with selected ID";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 83);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 15);
            this.label2.TabIndex = 6;
            this.label2.Text = "IDs Found in Log";
            // 
            // chkShowHex
            // 
            this.chkShowHex.AutoSize = true;
            this.chkShowHex.Location = new System.Drawing.Point(509, 71);
            this.chkShowHex.Name = "chkShowHex";
            this.chkShowHex.Size = new System.Drawing.Size(115, 19);
            this.chkShowHex.TabIndex = 7;
            this.chkShowHex.Text = "Show Hex Values";
            this.chkShowHex.UseVisualStyleBackColor = true;
            this.chkShowHex.CheckedChanged += new System.EventHandler(this.chkShowHex_CheckedChanged);
            // 
            // btnSaveParsed
            // 
            this.btnSaveParsed.Location = new System.Drawing.Point(704, 35);
            this.btnSaveParsed.Name = "btnSaveParsed";
            this.btnSaveParsed.Size = new System.Drawing.Size(75, 23);
            this.btnSaveParsed.TabIndex = 8;
            this.btnSaveParsed.Text = "Parse Log";
            this.btnSaveParsed.UseVisualStyleBackColor = true;
            this.btnSaveParsed.Click += new System.EventHandler(this.btnSaveParsed_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1254, 648);
            this.Controls.Add(this.btnSaveParsed);
            this.Controls.Add(this.chkShowHex);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvMessages);
            this.Controls.Add(this.btnLoad);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.txtLogFilePath);
            this.Controls.Add(this.listIDs);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "CANViewer";
            ((System.ComponentModel.ISupportInitialize)(this.dgvMessages)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listIDs;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtLogFilePath;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.Button btnLoad;
        private System.Windows.Forms.DataGridView dgvMessages;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkShowHex;
        private System.Windows.Forms.Button btnSaveParsed;
        private System.Windows.Forms.SaveFileDialog dlgSaveParse;
    }
}

