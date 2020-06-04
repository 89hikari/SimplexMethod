namespace Simplex_Method
{
    partial class AutoModeArtificalBasix
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBoxCornerDot = new System.Windows.Forms.GroupBox();
            this.dataGridViewCornerDot = new System.Windows.Forms.DataGridView();
            this.label_answer = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBoxCornerDot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCornerDot)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(800, 520);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBoxCornerDot);
            this.tabPage1.Controls.Add(this.label_answer);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(792, 494);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Выберите действие";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBoxCornerDot
            // 
            this.groupBoxCornerDot.Controls.Add(this.dataGridViewCornerDot);
            this.groupBoxCornerDot.Location = new System.Drawing.Point(35, 383);
            this.groupBoxCornerDot.Name = "groupBoxCornerDot";
            this.groupBoxCornerDot.Size = new System.Drawing.Size(450, 80);
            this.groupBoxCornerDot.TabIndex = 5;
            this.groupBoxCornerDot.TabStop = false;
            this.groupBoxCornerDot.Text = "Угловая точка";
            // 
            // dataGridViewCornerDot
            // 
            this.dataGridViewCornerDot.AllowUserToAddRows = false;
            this.dataGridViewCornerDot.AllowUserToDeleteRows = false;
            this.dataGridViewCornerDot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCornerDot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCornerDot.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewCornerDot.Name = "dataGridViewCornerDot";
            this.dataGridViewCornerDot.ReadOnly = true;
            this.dataGridViewCornerDot.Size = new System.Drawing.Size(444, 61);
            this.dataGridViewCornerDot.TabIndex = 4;
            // 
            // label_answer
            // 
            this.label_answer.AutoSize = true;
            this.label_answer.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_answer.Location = new System.Drawing.Point(491, 399);
            this.label_answer.Name = "label_answer";
            this.label_answer.Size = new System.Drawing.Size(65, 20);
            this.label_answer.TabIndex = 3;
            this.label_answer.Text = "Ответ: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView3);
            this.groupBox1.Location = new System.Drawing.Point(32, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 349);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Текущая задача";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Location = new System.Drawing.Point(3, 16);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 60;
            this.dataGridView3.Size = new System.Drawing.Size(625, 330);
            this.dataGridView3.TabIndex = 0;
            // 
            // AutoModeArtificalBasix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 520);
            this.Controls.Add(this.tabControl1);
            this.Name = "AutoModeArtificalBasix";
            this.Text = "AutoModeArtificalBasix";
            this.Load += new System.EventHandler(this.AutoModeArtificalBasix_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.groupBoxCornerDot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCornerDot)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBoxCornerDot;
        private System.Windows.Forms.DataGridView dataGridViewCornerDot;
        private System.Windows.Forms.Label label_answer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView3;
    }
}