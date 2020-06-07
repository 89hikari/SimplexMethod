namespace Simplex_Method
{
    partial class AutoIskBasis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoIskBasis));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.groupBoxCornerDot = new System.Windows.Forms.GroupBox();
            this.dataGridViewCornerDot = new System.Windows.Forms.DataGridView();
            this.label_answer = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).BeginInit();
            this.groupBoxCornerDot.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCornerDot)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView3);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(12, 57);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(631, 309);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Таблица";
            // 
            // dataGridView3
            // 
            this.dataGridView3.AllowUserToAddRows = false;
            this.dataGridView3.AllowUserToDeleteRows = false;
            this.dataGridView3.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView3.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView3.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dataGridView3.Location = new System.Drawing.Point(3, 17);
            this.dataGridView3.MultiSelect = false;
            this.dataGridView3.Name = "dataGridView3";
            this.dataGridView3.ReadOnly = true;
            this.dataGridView3.RowHeadersWidth = 60;
            this.dataGridView3.Size = new System.Drawing.Size(625, 289);
            this.dataGridView3.TabIndex = 0;
            // 
            // groupBoxCornerDot
            // 
            this.groupBoxCornerDot.Controls.Add(this.dataGridViewCornerDot);
            this.groupBoxCornerDot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBoxCornerDot.Location = new System.Drawing.Point(15, 372);
            this.groupBoxCornerDot.Name = "groupBoxCornerDot";
            this.groupBoxCornerDot.Size = new System.Drawing.Size(625, 80);
            this.groupBoxCornerDot.TabIndex = 6;
            this.groupBoxCornerDot.TabStop = false;
            this.groupBoxCornerDot.Text = "Угловая точка";
            // 
            // dataGridViewCornerDot
            // 
            this.dataGridViewCornerDot.AllowUserToAddRows = false;
            this.dataGridViewCornerDot.AllowUserToDeleteRows = false;
            this.dataGridViewCornerDot.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridViewCornerDot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewCornerDot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewCornerDot.Font = new System.Drawing.Font("Tahoma", 10F);
            this.dataGridViewCornerDot.Location = new System.Drawing.Point(3, 17);
            this.dataGridViewCornerDot.MultiSelect = false;
            this.dataGridViewCornerDot.Name = "dataGridViewCornerDot";
            this.dataGridViewCornerDot.ReadOnly = true;
            this.dataGridViewCornerDot.RowHeadersVisible = false;
            this.dataGridViewCornerDot.Size = new System.Drawing.Size(619, 60);
            this.dataGridViewCornerDot.TabIndex = 4;
            // 
            // label_answer
            // 
            this.label_answer.AutoSize = true;
            this.label_answer.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold);
            this.label_answer.Location = new System.Drawing.Point(33, 476);
            this.label_answer.Name = "label_answer";
            this.label_answer.Size = new System.Drawing.Size(95, 29);
            this.label_answer.TabIndex = 5;
            this.label_answer.Text = "Ответ: ";
            // 
            // AutoModeArtificalBasix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(881, 534);
            this.Controls.Add(this.label_answer);
            this.Controls.Add(this.groupBoxCornerDot);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AutoModeArtificalBasix";
            this.Text = "Искусственный базис, автоматически.";
            this.Load += new System.EventHandler(this.AutoModeArtificalBasix_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView3)).EndInit();
            this.groupBoxCornerDot.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewCornerDot)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.GroupBox groupBoxCornerDot;
        private System.Windows.Forms.DataGridView dataGridViewCornerDot;
        private System.Windows.Forms.Label label_answer;
    }
}