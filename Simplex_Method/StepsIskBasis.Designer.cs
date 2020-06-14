using System.Drawing;
using System.Windows.Forms;

namespace Simplex_Method
{
    partial class StepsIskBasis
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StepsIskBasis));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView3 = new System.Windows.Forms.DataGridView();
            this.groupBoxCornerDot = new System.Windows.Forms.GroupBox();
            this.dataGridViewCornerDot = new System.Windows.Forms.DataGridView();
            this.buttonBack = new System.Windows.Forms.Button();
            this.label_answer = new System.Windows.Forms.Label();
            this.buttonNext = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
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
            this.groupBoxCornerDot.Size = new System.Drawing.Size(628, 80);
            this.groupBoxCornerDot.TabIndex = 6;
            this.groupBoxCornerDot.TabStop = false;
            this.groupBoxCornerDot.Text = "Угловая точка";
            this.groupBoxCornerDot.Visible = false;
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
            this.dataGridViewCornerDot.Name = "dataGridViewCornerDot";
            this.dataGridViewCornerDot.ReadOnly = true;
            this.dataGridViewCornerDot.RowHeadersVisible = false;
            this.dataGridViewCornerDot.Size = new System.Drawing.Size(622, 60);
            this.dataGridViewCornerDot.TabIndex = 4;
            // 
            // buttonBack
            // 
            this.buttonBack.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonBack.Location = new System.Drawing.Point(676, 252);
            this.buttonBack.Name = "buttonBack";
            this.buttonBack.Size = new System.Drawing.Size(171, 54);
            this.buttonBack.TabIndex = 7;
            this.buttonBack.Text = "Шаг назад";
            this.buttonBack.UseVisualStyleBackColor = true;
            this.buttonBack.Click += new System.EventHandler(this.buttonBack_Click);
            // 
            // label_answer
            // 
            this.label_answer.AutoSize = true;
            this.label_answer.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_answer.Location = new System.Drawing.Point(33, 476);
            this.label_answer.Name = "label_answer";
            this.label_answer.Size = new System.Drawing.Size(95, 29);
            this.label_answer.TabIndex = 8;
            this.label_answer.Text = "Ответ: ";
            this.label_answer.Visible = false;
            // 
            // buttonNext
            // 
            this.buttonNext.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonNext.Location = new System.Drawing.Point(676, 145);
            this.buttonNext.Name = "buttonNext";
            this.buttonNext.Size = new System.Drawing.Size(171, 54);
            this.buttonNext.TabIndex = 9;
            this.buttonNext.Text = "Шаг вперёд";
            this.buttonNext.UseVisualStyleBackColor = true;
            this.buttonNext.Click += new System.EventHandler(this.buttonNext_Click);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(880, 45);
            this.label1.TabIndex = 10;
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.BackgroundImage = global::Simplex_Method.Properties.Resources.help;
            this.button1.Location = new System.Drawing.Point(649, 74);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(34, 33);
            this.button1.TabIndex = 11;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StepsIskBasis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(881, 534);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonBack);
            this.Controls.Add(this.buttonNext);
            this.Controls.Add(this.label_answer);
            this.Controls.Add(this.groupBoxCornerDot);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StepsIskBasis";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Метод искусственного базиса, пошаговый режим.";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StepsArtificial_FormClosed);
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
        private System.Windows.Forms.Button buttonBack;
        private System.Windows.Forms.Label label_answer;
        private System.Windows.Forms.Button buttonNext;
        private System.Windows.Forms.Label label1;
        private Button button1;
    }
}