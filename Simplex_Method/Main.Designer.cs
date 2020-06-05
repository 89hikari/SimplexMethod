﻿using System.Drawing;

namespace Simplex_Method
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.FileActions = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenFile = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveFile = new System.Windows.Forms.ToolStripMenuItem();
            this.AboutProgramm = new System.Windows.Forms.ToolStripMenuItem();
            this.Spravka = new System.Windows.Forms.ToolStripMenuItem();
            this.solve_Button = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.radioButton_auto_answer = new System.Windows.Forms.RadioButton();
            this.radioButton_step_by_step = new System.Windows.Forms.RadioButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.radioButton_decimal_drob = new System.Windows.Forms.RadioButton();
            this.radioButton_default_drob = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.radioButton_imagine_b = new System.Windows.Forms.RadioButton();
            this.radioButton_symplex = new System.Windows.Forms.RadioButton();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.radioButton_max = new System.Windows.Forms.RadioButton();
            this.radioButton_min = new System.Windows.Forms.RadioButton();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            this.checkBoxCornerDot = new System.Windows.Forms.CheckBox();
            this.dataGridView_CornerDot = new System.Windows.Forms.DataGridView();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ExitButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CornerDot)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileActions,
            this.AboutProgramm,
            this.Spravka});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1191, 24);
            this.menuStrip1.TabIndex = 6;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // FileActions
            // 
            this.FileActions.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenFile,
            this.SaveFile});
            this.FileActions.Name = "FileActions";
            this.FileActions.Size = new System.Drawing.Size(52, 20);
            this.FileActions.Text = "Файл";
            // 
            // OpenFile
            // 
            this.OpenFile.Name = "OpenFile";
            this.OpenFile.Size = new System.Drawing.Size(288, 22);
            this.OpenFile.Text = "Открыть файл с задачей";
            this.OpenFile.Click += new System.EventHandler(this.OpenFile_Click);
            // 
            // SaveFile
            // 
            this.SaveFile.Name = "SaveFile";
            this.SaveFile.Size = new System.Drawing.Size(288, 22);
            this.SaveFile.Text = "Сохранить в файл текущую задачу";
            this.SaveFile.Click += new System.EventHandler(this.SaveFile_Click);
            // 
            // AboutProgramm
            // 
            this.AboutProgramm.Name = "AboutProgramm";
            this.AboutProgramm.Size = new System.Drawing.Size(99, 20);
            this.AboutProgramm.Text = "О программе";
            // 
            // Spravka
            // 
            this.Spravka.Name = "Spravka";
            this.Spravka.Size = new System.Drawing.Size(70, 20);
            this.Spravka.Text = "Справка";
            // 
            // solve_Button
            // 
            this.solve_Button.BackColor = System.Drawing.Color.LightGreen;
            this.solve_Button.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.solve_Button.Location = new System.Drawing.Point(944, 201);
            this.solve_Button.Name = "solve_Button";
            this.solve_Button.Size = new System.Drawing.Size(192, 62);
            this.solve_Button.TabIndex = 9;
            this.solve_Button.Text = "Решать";
            this.solve_Button.UseVisualStyleBackColor = false;
            this.solve_Button.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.radioButton_auto_answer);
            this.groupBox7.Controls.Add(this.radioButton_step_by_step);
            this.groupBox7.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox7.Location = new System.Drawing.Point(912, 27);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(277, 72);
            this.groupBox7.TabIndex = 8;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Режим решения";
            // 
            // radioButton_auto_answer
            // 
            this.radioButton_auto_answer.AutoSize = true;
            this.radioButton_auto_answer.Location = new System.Drawing.Point(8, 42);
            this.radioButton_auto_answer.Name = "radioButton_auto_answer";
            this.radioButton_auto_answer.Size = new System.Drawing.Size(117, 19);
            this.radioButton_auto_answer.TabIndex = 5;
            this.radioButton_auto_answer.TabStop = true;
            this.radioButton_auto_answer.Text = "Автоматический";
            this.radioButton_auto_answer.UseVisualStyleBackColor = true;
            // 
            // radioButton_step_by_step
            // 
            this.radioButton_step_by_step.AutoSize = true;
            this.radioButton_step_by_step.Location = new System.Drawing.Point(8, 19);
            this.radioButton_step_by_step.Name = "radioButton_step_by_step";
            this.radioButton_step_by_step.Size = new System.Drawing.Size(90, 19);
            this.radioButton_step_by_step.TabIndex = 4;
            this.radioButton_step_by_step.TabStop = true;
            this.radioButton_step_by_step.Text = "Пошаговый";
            this.radioButton_step_by_step.UseVisualStyleBackColor = true;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.radioButton_decimal_drob);
            this.groupBox6.Controls.Add(this.radioButton_default_drob);
            this.groupBox6.Location = new System.Drawing.Point(627, 27);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(279, 72);
            this.groupBox6.TabIndex = 7;
            this.groupBox6.TabStop = false;
            // 
            // radioButton_decimal_drob
            // 
            this.radioButton_decimal_drob.AutoSize = true;
            this.radioButton_decimal_drob.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton_decimal_drob.Location = new System.Drawing.Point(8, 40);
            this.radioButton_decimal_drob.Name = "radioButton_decimal_drob";
            this.radioButton_decimal_drob.Size = new System.Drawing.Size(261, 19);
            this.radioButton_decimal_drob.TabIndex = 5;
            this.radioButton_decimal_drob.TabStop = true;
            this.radioButton_decimal_drob.Text = "Использовать числа с плавающей точкой";
            this.radioButton_decimal_drob.UseVisualStyleBackColor = true;
            // 
            // radioButton_default_drob
            // 
            this.radioButton_default_drob.AutoSize = true;
            this.radioButton_default_drob.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton_default_drob.Location = new System.Drawing.Point(8, 15);
            this.radioButton_default_drob.Name = "radioButton_default_drob";
            this.radioButton_default_drob.Size = new System.Drawing.Size(233, 19);
            this.radioButton_default_drob.TabIndex = 4;
            this.radioButton_default_drob.TabStop = true;
            this.radioButton_default_drob.Text = "Использовать обыкновенные дроби";
            this.radioButton_default_drob.UseVisualStyleBackColor = true;
            this.radioButton_default_drob.CheckedChanged += new System.EventHandler(this.radioButton_default_drob_CheckedChanged);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.radioButton_imagine_b);
            this.groupBox5.Controls.Add(this.radioButton_symplex);
            this.groupBox5.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox5.Location = new System.Drawing.Point(344, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(279, 72);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Метод решения";
            // 
            // radioButton_imagine_b
            // 
            this.radioButton_imagine_b.AutoSize = true;
            this.radioButton_imagine_b.Location = new System.Drawing.Point(6, 42);
            this.radioButton_imagine_b.Name = "radioButton_imagine_b";
            this.radioButton_imagine_b.Size = new System.Drawing.Size(192, 19);
            this.radioButton_imagine_b.TabIndex = 3;
            this.radioButton_imagine_b.TabStop = true;
            this.radioButton_imagine_b.Text = "Метод искусственного базиса";
            this.radioButton_imagine_b.UseVisualStyleBackColor = true;
            this.radioButton_imagine_b.CheckedChanged += new System.EventHandler(this.radioButton_imagine_b_CheckedChanged);
            // 
            // radioButton_symplex
            // 
            this.radioButton_symplex.AutoSize = true;
            this.radioButton_symplex.Location = new System.Drawing.Point(6, 19);
            this.radioButton_symplex.Name = "radioButton_symplex";
            this.radioButton_symplex.Size = new System.Drawing.Size(121, 19);
            this.radioButton_symplex.TabIndex = 2;
            this.radioButton_symplex.TabStop = true;
            this.radioButton_symplex.Text = "Симплекс метод";
            this.radioButton_symplex.UseVisualStyleBackColor = true;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.radioButton_max);
            this.groupBox4.Controls.Add(this.radioButton_min);
            this.groupBox4.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox4.Location = new System.Drawing.Point(218, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(123, 72);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Решать задачу на";
            // 
            // radioButton_max
            // 
            this.radioButton_max.AutoSize = true;
            this.radioButton_max.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton_max.Location = new System.Drawing.Point(8, 44);
            this.radioButton_max.Name = "radioButton_max";
            this.radioButton_max.Size = new System.Drawing.Size(83, 19);
            this.radioButton_max.TabIndex = 1;
            this.radioButton_max.TabStop = true;
            this.radioButton_max.Text = "Максимум";
            this.radioButton_max.UseVisualStyleBackColor = true;
            this.radioButton_max.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton_min
            // 
            this.radioButton_min.AutoSize = true;
            this.radioButton_min.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton_min.Location = new System.Drawing.Point(8, 19);
            this.radioButton_min.Name = "radioButton_min";
            this.radioButton_min.Size = new System.Drawing.Size(78, 19);
            this.radioButton_min.TabIndex = 0;
            this.radioButton_min.TabStop = true;
            this.radioButton_min.Text = "Минимум";
            this.radioButton_min.UseVisualStyleBackColor = true;
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(168, 70);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown2.TabIndex = 4;
            this.numericUpDown2.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDown2_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(12, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Количество ограничений:";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(168, 33);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(44, 20);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(12, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(150, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Количество переменных:";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dataGridView1);
            this.groupBox2.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox2.Location = new System.Drawing.Point(15, 107);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(881, 75);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Функция";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 17);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(875, 55);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.Font = new Font("Tahoma", 10);
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            this.dataGridView1.DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
       
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.dataGridView2);
            this.groupBox3.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox3.Location = new System.Drawing.Point(15, 184);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(881, 276);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Ограничения-равенства";
            // 
            // dataGridView2
            // 
            this.dataGridView2.AllowUserToAddRows = false;
            this.dataGridView2.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView2.Location = new System.Drawing.Point(3, 17);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.Size = new System.Drawing.Size(875, 256);
            this.dataGridView2.TabIndex = 0;
            this.dataGridView2.Font = new Font("Tahoma", 10);
            this.dataGridView2.RowHeadersVisible = false;
            this.dataGridView2.DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
            // 
            // checkBoxCornerDot
            // 
            this.checkBoxCornerDot.AutoSize = true;
            this.checkBoxCornerDot.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.checkBoxCornerDot.Location = new System.Drawing.Point(922, 124);
            this.checkBoxCornerDot.Name = "checkBoxCornerDot";
            this.checkBoxCornerDot.Size = new System.Drawing.Size(224, 19);
            this.checkBoxCornerDot.TabIndex = 12;
            this.checkBoxCornerDot.Text = "Задать начальную угловую точку x0";
            this.checkBoxCornerDot.UseVisualStyleBackColor = true;
            this.checkBoxCornerDot.CheckedChanged += new System.EventHandler(this.checkBoxCornerDot_CheckedChanged);
            // 
            // dataGridView_CornerDot
            // 
            this.dataGridView_CornerDot.AllowUserToAddRows = false;
            this.dataGridView_CornerDot.BackgroundColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.dataGridView_CornerDot.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_CornerDot.Location = new System.Drawing.Point(0, 16);
            this.dataGridView_CornerDot.Name = "dataGridView_CornerDot";
            this.dataGridView_CornerDot.Size = new System.Drawing.Size(875, 73);
            this.dataGridView_CornerDot.TabIndex = 13;
            this.dataGridView_CornerDot.Font = new Font("Tahoma", 10);
            this.dataGridView_CornerDot.Visible = false;
            this.dataGridView_CornerDot.RowHeadersVisible = false;
            this.dataGridView_CornerDot.DefaultCellStyle.SelectionBackColor = Color.PaleGreen;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "txt";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Текстовые файлы(*.txt)|*.txt";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "Текстовые файлы(*.txt)|*.txt";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView_CornerDot);
            this.groupBox1.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(18, 466);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(878, 89);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Начальная угловая точка х0";
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.ExitButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ExitButton.Location = new System.Drawing.Point(944, 329);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(192, 62);
            this.ExitButton.TabIndex = 15;
            this.ExitButton.Text = "Выход";
            this.ExitButton.UseVisualStyleBackColor = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ClientSize = new System.Drawing.Size(1191, 564);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.checkBoxCornerDot);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.numericUpDown2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.solve_Button);
            this.Controls.Add(this.numericUpDown1);
            this.Controls.Add(this.groupBox6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox7);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Симплекс Метод";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_CornerDot)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem FileActions;
        private System.Windows.Forms.ToolStripMenuItem OpenFile;
        private System.Windows.Forms.ToolStripMenuItem SaveFile;
        private System.Windows.Forms.ToolStripMenuItem AboutProgramm;
        private System.Windows.Forms.ToolStripMenuItem Spravka;
        private System.Windows.Forms.Button solve_Button;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.RadioButton radioButton_auto_answer;
        private System.Windows.Forms.RadioButton radioButton_step_by_step;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.RadioButton radioButton_decimal_drob;
        private System.Windows.Forms.RadioButton radioButton_default_drob;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.RadioButton radioButton_imagine_b;
        private System.Windows.Forms.RadioButton radioButton_symplex;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.RadioButton radioButton_max;
        private System.Windows.Forms.RadioButton radioButton_min;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.CheckBox checkBoxCornerDot;
        private System.Windows.Forms.DataGridView dataGridView_CornerDot;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button ExitButton;
    }
}
