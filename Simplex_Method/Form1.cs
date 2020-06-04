using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex_Method
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Определитель - какой режим работы с дробями выбран. false - обыкновенные. true - десятичные
        /// </summary>
        bool decimal_or_radical_drob = false;

        public Form1()
        {
            InitializeComponent();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            clearGrids();
            drawing_function();
            drawing_org();
            drawing_corner_dot();
            non_sort_for_columns();
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            decimal_or_radical_drob = radioButton_decimal_drob.Checked;

            drawing_function();
            drawing_org();
            non_sort_for_columns();

            radioButton_min.Checked = true;
            radioButton_symplex.Checked = true;
            radioButton_default_drob.Checked = true;
            radioButton_step_by_step.Checked = true;

            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var filePath = Path.Combine(outPutDirectory, "data\\Help\\MainPageHelp.rtf");

            string file_path = new Uri(filePath).LocalPath; // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug
                                                            // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug\data\Help\MainPageHelp.rtf

   //         helpProvider1.HelpNamespace = file_path;
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            MessageBoxButtons msb = MessageBoxButtons.YesNo;
            String message = "Выйти из программы?";
            String caption = "Выход";
            if (MessageBox.Show(message, caption, msb) == DialogResult.Yes)
                this.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            clearGrids();
            drawing_function();
            drawing_org();
            non_sort_for_columns();
        }

        private void drawing_org()
        {
            for (int i = 0; i < Decimal.ToInt32(numericUpDown1.Value + 1); i++)
            {
                dataGridView2.Columns.Add($"ogr_x{i + 1}", $"x{i + 1}");

                if (i == Decimal.ToInt32(numericUpDown1.Value)) // именуем последний эл
                {
                    dataGridView2.Columns[i].HeaderText = "Свободный";
                }
            } // Создаём столбцы

            for (int i = 1; i < Decimal.ToInt32(numericUpDown2.Value + 1); i++)
            {
                dataGridView2.Rows.Insert(0, "0");
            } // Создаём строки

            for (int i = 0; i < Decimal.ToInt32(numericUpDown1.Value); i++)
            {
                for (int j = 0; j < Decimal.ToInt32(numericUpDown2.Value); j++)
                    dataGridView2.Rows[j].Cells[i].Value = "0";
            }
        }

        private void drawing_function()
        {
            for (int i = 1; i < Decimal.ToInt32(numericUpDown1.Value + 1); i++)
            {
                dataGridView1.Columns.Add($"function_x{i}", $"x{i}");
            } // Создаём столбцы

            dataGridView1.Rows.Insert(0, "0");

            for (int i = 0; i < Decimal.ToInt32(numericUpDown1.Value); i++)
            {
                dataGridView1.Rows[0].Cells[i].Value = "0";
            }

        }

        private void drawing_corner_dot()
        {
            dataGridView_CornerDot.Columns.Clear();

            // Создаём столбцы
            for (int i = 1; i < dataGridView1.Columns.Count + 1; i++)
            {
                dataGridView_CornerDot.Columns.Add($"function_x{i}", $"x{i}");
            }

            // Добавляем одну строку
            dataGridView_CornerDot.Rows.Insert(0, "0");

            // Заполняем ячейки нулями
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView_CornerDot.Rows[0].Cells[i].Value = "0";
            }
        }

        private void non_sort_for_columns()
        {
            foreach (DataGridViewColumn dgvc in dataGridView1.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            foreach (DataGridViewColumn dgvc in dataGridView2.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        } //Делаем колонки несортируемыми

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

        }

        public void read_grids(DataGridView Grid, List<List<double>> N)
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                N.Add(new List<double>());
                for (int j = 0; j < Grid.Columns.Count; j++)
                {
                    if (Grid.Rows[i].Cells[j].Value == null)
                        throw new Exception("Какой-то из элементов не заполнен. Пожалуйста попробуйте ещё раз");

                    N[i].Add(Convert.ToDouble(Grid.Rows[i].Cells[j].Value.ToString().Trim()));
                }
            }
        }

        public void read_grids(DataGridView Grid, List<List<Fraction>> N)
        {


            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                N.Add(new List<Fraction>());
                for (int j = 0; j < Grid.Columns.Count; j++)
                {
                    string[] tmp_fraction = new string[2];

                    if (Grid.Rows[i].Cells[j].Value == null)
                        throw new Exception("Какой-то из элементов не заполнен. Пожалуйста попробуйте ещё раз");

                    tmp_fraction = (Grid.Rows[i].Cells[j].Value.ToString().Split('/'));
                    if (tmp_fraction.Length == 1)
                        tmp_fraction = new string[] { tmp_fraction[0], "1" };

                    N[i].Add(new Fraction(Int32.Parse(tmp_fraction[0]), Int32.Parse(tmp_fraction[1])));
                }
            }
        }

        /// <summary>
        /// Считывание из колонок по DisplayIndex. Считывает не по относительному индексу, а по Display. 
        /// То есть значения считываются так, как они отображены в dataGridView
        /// </summary>
        /// <param name="Grid"></param>
        /// <param name="N"></param>
        public void read_grids_with_DisplayIndex(DataGridView Grid, List<List<Fraction>> N, List<int> variable_visualization)
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                N.Add(new List<Fraction>());
                for (int j = 0; j < Grid.Columns.Count; j++)
                {
                    string[] tmp_fraction = new string[2];

                    if (Grid.Rows[i].Cells[j].Value == null)
                        throw new Exception("Какой-то из элементов не заполнен. Пожалуйста попробуйте ещё раз");


                    tmp_fraction = (Grid.Rows[i].Cells[j].Value.ToString().Split('/'));


                    if (tmp_fraction.Length == 1)
                        tmp_fraction = new string[] { tmp_fraction[0], "1" };

                    N[i].Add(new Fraction(Int32.Parse(tmp_fraction[0]), Int32.Parse(tmp_fraction[1])));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Для работы с десятичными
            List<List<double>> ogr = new List<List<double>>();
            List<List<double>> cel_function = new List<List<double>>();
            // Для работы с обыкновенными дробями
            List<List<Fraction>> ogr_with_radicals = new List<List<Fraction>>();
            List<List<Fraction>> cel_function_with_radicals = new List<List<Fraction>>();

            // Если выбраны десятичные дроби
            if (decimal_or_radical_drob)
            {
                try
                {
                    // Считываем целевую функцию и ограничения как десятичные дроби
                    read_grids(dataGridView1, cel_function);
                    read_grids(dataGridView2, ogr);
                }
                catch (Exception d)
                {
                    MessageBox.Show(d.Message, "Ошибка!");
                    return;
                }

            }
            // Если выбраны обыкновенные дроби
            else
            {
                try
                {
                    // Считываем целевую функцию и ограничения как обыкновенные дроби
                    read_grids(dataGridView1, cel_function_with_radicals);
                    read_grids(dataGridView2, ogr_with_radicals);
                }
                catch (Exception d)
                {
                    MessageBox.Show(d.Message, "Ошибка!");
                    return;
                }
            }

            //вспомогательный массив для подсчёта ранга для десятичных
            List<List<double>> copy_elements = new List<List<double>>();
            //вспомогательный массив для подсчёта ранга для обыкновенных дробей
            List<List<Fraction>> copy_elements_with_radicals = new List<List<Fraction>>();
            // Высчитываем ранг матрицы, но по копированной матрице
            int rang = 0;

            if (decimal_or_radical_drob)
            {
                // Копируем элементы для десятичных
                for (int i = 0; i < ogr.Count; i++)
                {
                    copy_elements.Add(new List<double>());
                    for (int j = 0; j < ogr[0].Count; j++)
                    {
                        copy_elements[i].Add(ogr[i][j]);
                    }
                }
                rang = RangOfMatrix(copy_elements);
            }
            else
            {
                // Копируем элементы если выбраны дроби
                for (int i = 0; i < ogr_with_radicals.Count; i++)
                {
                    copy_elements_with_radicals.Add(new List<Fraction>());
                    for (int j = 0; j < ogr_with_radicals[0].Count; j++)
                    {
                        copy_elements_with_radicals[i].Add(ogr_with_radicals[i][j]);
                    }
                }
                rang = RangOfMatrix(copy_elements_with_radicals);
            }



            // Выбран мин или макс
            int MinMax = is_check_MinMax(); // 0 - выбран max, 1 - выбран min

            // Определитель - выбрана ли угловая точка
            bool CornerDot = checkBoxCornerDot.Checked;

            //если выбраны пошаговый режим, симплекс-метод и задана начальная угловая точка
            if ((radioButton_step_by_step.Checked == true) && (radioButton_symplex.Checked == true) && (checkBoxCornerDot.Checked == true))
            {
                ///<summary>
                ///Корневая точка List<List<double>>
                ///</summary>
                List<List<double>> corner_dot = new List<List<double>>();
                read_grids(dataGridView_CornerDot, corner_dot);

                List<List<Fraction>> corner_dot_with_radicals = new List<List<Fraction>>();
                read_grids(dataGridView_CornerDot, corner_dot_with_radicals);

                int index = 0;//индекс переменной, которая является базисной
                int count_basix_var = 0;//число базисных переменных

                if (decimal_or_radical_drob)
                {
                    //проверяем на возможность выражения базисных переменных для десятичных
                    for (int j = 0; j < corner_dot[0].Count; j++)
                    {
                        //проверяем базисная ли переменная
                        if (corner_dot[0][j] != 0)
                        {
                            CheckCanBeBasix(ogr, index);
                            count_basix_var++;
                        }
                        index++;
                    }
                }
                else
                {
                    //проверяем на возможность выражения базисных переменных для обыкновенных
                    for (int j = 0; j < corner_dot[0].Count; j++)
                    {
                        //проверяем базисная ли переменная
                        if (corner_dot_with_radicals[0][j] != 0)
                        {
                            CheckCanBeBasix(ogr_with_radicals, index);
                            count_basix_var++;
                        }
                        index++;
                    }
                }

                //проверяем совпадает ли число базисных переменных с рангом матрицы
                if (rang != count_basix_var)
                {
                    MessageBox.Show("Ранг матрицы (ранг=" + rang + ") не равен числу заданных базисных переменных (кол-во=" + count_basix_var + ").");
                    return;
                }

                if (decimal_or_radical_drob)
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr[0].Count; i++)
                        variable_visualization.Add(i + 1);

                    // Меняем колонки местами, чтобы обычный прямой ход Гаусса ставил базис именно по ненулевым столбцам корневой точки
                    ChangeColumnsForGauss(ogr, corner_dot, variable_visualization);

                    StepsSolve byStep = new StepsSolve(cel_function, ogr, MinMax, CornerDot, rang, decimal_or_radical_drob, variable_visualization);
                    byStep.ShowDialog();
                }
                else
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr_with_radicals[0].Count; i++)
                        variable_visualization.Add(i + 1);


                    // Меняем колонки местами, чтобы обычный прямой ход Гаусса ставил базис именно по ненулевым столбцам корневой точки
                    ChangeColumnsForGauss(ogr_with_radicals, corner_dot_with_radicals, variable_visualization);

                    StepsSolve byStep = new StepsSolve(cel_function_with_radicals, ogr_with_radicals, MinMax, CornerDot, rang, decimal_or_radical_drob, variable_visualization);
                    byStep.ShowDialog();
                }

            }
            //если выбраны пошаговый режим и симплекс-метод БЕЗ задания начальной угловой точки
            else if ((radioButton_step_by_step.Checked == true) && (radioButton_symplex.Checked == true) && (checkBoxCornerDot.Checked == false))
            {
                // Если выбраны десятичные дроби
                if (decimal_or_radical_drob == true)
                {
                    StepsSolve byStep = new StepsSolve(cel_function, ogr, MinMax, CornerDot, rang, decimal_or_radical_drob);
                    byStep.ShowDialog();
                }
                // Если выбраны обыкновенные дроби
                else if (decimal_or_radical_drob == false)
                {
                    StepsSolve byStep = new StepsSolve(cel_function_with_radicals, ogr_with_radicals, MinMax, CornerDot, rang, decimal_or_radical_drob);
                    byStep.ShowDialog();
                }
            }
            //если выбраны автоматический режим и симплекс метод и задана начальная угловая точка
            else if ((radioButton_auto_answer.Checked == true) && (radioButton_symplex.Checked == true) && (checkBoxCornerDot.Checked == true))
            {
                ///<summary>
                ///Корневая точка List<List<double>>
                ///</summary>
                List<List<double>> corner_dot = new List<List<double>>();
                read_grids(dataGridView_CornerDot, corner_dot);

                List<List<Fraction>> corner_dot_with_radicals = new List<List<Fraction>>();
                read_grids(dataGridView_CornerDot, corner_dot_with_radicals);

                int index = 0;//индекс переменной, которая является базисной
                int count_basix_var = 0;//число базисных переменных

                if (decimal_or_radical_drob)
                {
                    //проверяем на возможность выражения базисных переменных для десятичных
                    for (int j = 0; j < corner_dot[0].Count; j++)
                    {
                        //проверяем базисная ли переменная
                        if (corner_dot[0][j] != 0)
                        {
                            CheckCanBeBasix(ogr, index);
                            count_basix_var++;
                        }
                        index++;
                    }
                }
                else
                {
                    //проверяем на возможность выражения базисных переменных для обыкновенных
                    for (int j = 0; j < corner_dot[0].Count; j++)
                    {
                        //проверяем базисная ли переменная
                        if (corner_dot_with_radicals[0][j] != 0)
                        {
                            CheckCanBeBasix(ogr_with_radicals, index);
                            count_basix_var++;
                        }
                        index++;
                    }
                }

                //проверяем совпадает ли число базисных переменных с рангом матрицы
                if (rang != count_basix_var)
                {
                    MessageBox.Show("Ранг матрицы (ранг=" + rang + ") не равен числу заданных базисных переменных (кол-во=" + count_basix_var + ").");
                    return;
                }

                if (decimal_or_radical_drob)
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr[0].Count; i++)
                        variable_visualization.Add(i + 1);

                    // Меняем колонки местами, чтобы обычный прямой ход Гаусса ставил базис именно по ненулевым столбцам корневой точки
                    ChangeColumnsForGauss(ogr, corner_dot, variable_visualization);

                    AutoMode byAuto = new AutoMode(cel_function, ogr, MinMax, CornerDot, count_basix_var, decimal_or_radical_drob, variable_visualization);
                    byAuto.ShowDialog();
                }
                else
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr_with_radicals[0].Count; i++)
                        variable_visualization.Add(i + 1);


                    // Меняем колонки местами, чтобы обычный прямой ход Гаусса ставил базис именно по ненулевым столбцам корневой точки
                    ChangeColumnsForGauss(ogr_with_radicals, corner_dot_with_radicals, variable_visualization);

                    AutoMode byAuto = new AutoMode(cel_function_with_radicals, ogr_with_radicals, MinMax, CornerDot, count_basix_var, decimal_or_radical_drob, variable_visualization);
                    byAuto.ShowDialog();
                }
            }
            //если выбраны автоматический режим и симплекс метод и НЕ задана начальная угловая точка
            else if ((radioButton_auto_answer.Checked == true) && (radioButton_symplex.Checked == true) && (checkBoxCornerDot.Checked == false))
            {
                // Если выбраны десятичные дроби
                if (decimal_or_radical_drob == true)
                {
                    AutoMode byAuto = new AutoMode(cel_function, ogr, MinMax, rang, decimal_or_radical_drob);
                    byAuto.ShowDialog();
                }
                // Если выбраны обыкновенные дроби
                else if (decimal_or_radical_drob == false)
                {
                    AutoMode byAuto = new AutoMode(cel_function_with_radicals, ogr_with_radicals, MinMax, rang, decimal_or_radical_drob);
                    byAuto.ShowDialog();
                }
            }
            //если выбраны пошаговый режим и метод искусственного базиса БЕЗ задания начальной угловой точки
            else if ((radioButton_step_by_step.Checked == true) && (radioButton_imagine_b.Checked == true) && (checkBoxCornerDot.Checked == false))
            {
                if (decimal_or_radical_drob)
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr[0].Count; i++)
                        variable_visualization.Add(i + 1);

                    StepsArtificial stepByStepArtifical = new StepsArtificial(ogr, cel_function, rang, variable_visualization, MinMax, decimal_or_radical_drob);
                    stepByStepArtifical.Show();
                }
                else
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr_with_radicals[0].Count; i++)
                        variable_visualization.Add(i + 1);

                    StepsArtificial stepByStepArtifical = new StepsArtificial(ogr_with_radicals, cel_function_with_radicals, rang, variable_visualization, MinMax, decimal_or_radical_drob);
                    stepByStepArtifical.Show();
                }


            }
            //если выбраны авто режим и метод искуственного базиса и БЕЗ задания начальной угловой точки
            else if ((radioButton_auto_answer.Checked == true) && (radioButton_imagine_b.Checked == true) && (checkBoxCornerDot.Checked == false))
            {
                if (decimal_or_radical_drob)
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr[0].Count; i++)
                        variable_visualization.Add(i + 1);

                    AutoModeArtificalBasix autoModeArtifical = new AutoModeArtificalBasix(ogr, cel_function, rang, variable_visualization, MinMax, decimal_or_radical_drob);
                    autoModeArtifical.Show();
                }
                else
                {
                    // Массив запоминающий индексы переменных для отображения
                    List<int> variable_visualization = new List<int>();
                    //Заполняем массив визуализации переменных. Например, для x1,x2,x3,x4 заполним [1,2,3,4].
                    for (int i = 0; i < ogr_with_radicals[0].Count; i++)
                        variable_visualization.Add(i + 1);

                    AutoModeArtificalBasix autoModeArtifical = new AutoModeArtificalBasix(ogr_with_radicals, cel_function_with_radicals, rang, variable_visualization, MinMax, decimal_or_radical_drob);
                    autoModeArtifical.Show();
                }
            }
        }



        /// <summary>
        /// Меняем колонки местами для дробей
        /// </summary>
        /// <param name="dataGridView2"></param>
        /// <param name="corner_dot"></param>
        private void ChangeColumnsForGauss(List<List<Fraction>> ogr_with_radicals, List<List<Fraction>> corner_dot, List<int> variable_visualization)
        {
            int column_index = 0;
            int tmp_variable = 0;

            for (int j = 0; j < corner_dot[0].Count; j++)
            {
                // Еесли встретили ненулевой элемент корневой точки
                if (corner_dot[0][j] != 0)
                {
                    Fraction[] tmp_fractions = new Fraction[ogr_with_radicals.Count];

                    // Меняем местами столбцы
                    for (int i = 0; i < ogr_with_radicals.Count; i++)
                    {
                        // Копируем первый столбец
                        tmp_fractions[i] = ogr_with_radicals[i][column_index];
                    }

                    for (int i = 0; i < ogr_with_radicals.Count; i++)
                    {
                        // заносим на место первого j-тый - тот, на котором на встретился ненулевой элемент корневой точки
                        ogr_with_radicals[i][column_index] = ogr_with_radicals[i][j];
                    }

                    for (int i = 0; i < ogr_with_radicals.Count; i++)
                    {
                        // Из копии достаём и ставим
                        ogr_with_radicals[i][j] = tmp_fractions[i];
                    }

                    // Меняем местами в массиве для визуализации
                    tmp_variable = variable_visualization[column_index];
                    variable_visualization[column_index] = variable_visualization[j];
                    variable_visualization[j] = tmp_variable;

                    column_index++;
                }
            }
        }

        /// <summary>
        /// Меняем колонки местами для десятичных
        /// </summary>
        /// <param name="dataGridView2"></param>
        /// <param name="corner_dot"></param>
        private void ChangeColumnsForGauss(List<List<double>> ogr, List<List<double>> corner_dot, List<int> variable_visualization)
        {
            int column_index = 0;
            int tmp_variable = 0;

            for (int j = 0; j < corner_dot[0].Count; j++)
            {
                // Еесли встретили ненулевой элемент корневой точки
                if (corner_dot[0][j] != 0)
                {
                    double[] tmp_fractions = new double[ogr.Count];

                    // Меняем местами столбцы
                    for (int i = 0; i < ogr.Count; i++)
                    {
                        // Копируем первый столбец
                        tmp_fractions[i] = ogr[i][column_index];
                    }

                    for (int i = 0; i < ogr.Count; i++)
                    {
                        // заносим на место первого j-тый - тот, на котором на встретился ненулевой элемент корневой точки
                        ogr[i][column_index] = ogr[i][j];
                    }

                    for (int i = 0; i < ogr.Count; i++)
                    {
                        // Из копии достаём и ставим
                        ogr[i][j] = tmp_fractions[i];
                    }

                    // Меняем местами в массиве для визуализации
                    tmp_variable = variable_visualization[column_index];
                    variable_visualization[column_index] = variable_visualization[j];
                    variable_visualization[j] = tmp_variable;

                    column_index++;
                }
            }
        }

        /// <summary>
        /// Проверка возможности выражения базисной переменной.
        /// </summary>
        private void CheckCanBeBasix(List<List<double>> ogr, int index)
        {
            bool isnull = true;
            for (int i = 0; i < ogr.Count; i++)
                if (ogr[i][index] != 0)
                    isnull = false;
            if (isnull)
                throw new Exception("Невозможно выразить базисную переменную x" + (index + 1) + ", так как в соответствующем столбце №" + (index + 1) + " имеются только нули!");
        }

        /// <summary>
        /// Проверка возможности выражения базисной переменной.
        /// </summary>
        private void CheckCanBeBasix(List<List<Fraction>> ogr, int index)
        {
            bool isnull = true;
            for (int i = 0; i < ogr.Count; i++)
                if (ogr[i][index] != 0)
                    isnull = false;
            if (isnull)
                throw new Exception("Невозможно выразить базисную переменную x" + (index + 1) + ", так как в соответствующем столбце №" + (index + 1) + " имеются только нули!");
        }

        /// <summary>
        /// Конвертация из double в Fraction
        /// </summary>
        /// <param name="function"></param>
        /// <param name="function_with_radicals"></param>
        public void grid_to_radical(List<List<double>> function, List<List<Fraction>> function_with_radicals)
        {
            for (int i = 0; i < function.Count; i++)
            {
                function_with_radicals.Add(new List<Fraction>());
                for (int j = 0; j < function[i].Count; j++)
                {
                    function_with_radicals[i].Add(new Fraction(Int32.Parse(function[i][j].ToString())));
                }
            }
        }

        private int is_check_MinMax()
        {
            for (int i = 0; i < groupBox6.Controls.Count; i++)
            {
                if (((RadioButton)groupBox4.Controls[i]).Checked == true) { return i; }
            }
            return -1;
        }

        private void оПрограммеToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            About About = new About();
            About.ShowDialog();
        }

        private void OpenFile_Click(object sender, EventArgs e)
        {
            var outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var filePath = Path.Combine(outPutDirectory, "data");

            string file_path = new Uri(filePath).LocalPath; // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug

            openFileDialog1.InitialDirectory = file_path;
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;

            string filename = openFileDialog1.FileName;
            string fileText = System.IO.File.ReadAllText(filename);

            if (fileText == "")
            {
                MessageBox.Show("Программа ничего не считала. Вероятно - файл пуст.\nПожалуйста, проверьте файл и попробуйте ещё раз.", "Ошибка считывания файла!");
                return;
            }

            // Чистим массив от лишних символов
            fileText = fileText.Replace("\r", "");
            fileText = fileText.Replace("= ", "");

            // Чистим от последнего /n после, если пользователь вдруг нажал Enter после уравнения
            if (fileText[fileText.Length - 1] == '\n')
            {
                fileText = fileText.Remove(fileText.Length - 1);
            }

            // Чистим от лишних пробелов
            fileText = string.Join(" ", fileText.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));


            // Определяем макс или мин и очищаем от этого строку
            if (fileText.Contains("->max") || fileText.Contains("-> max"))
            {
                radioButton_max.Checked = true;
                fileText = fileText.Replace(" ->max", "");
                fileText = fileText.Replace(" -> max", "");
            }
            else
            {
                if (fileText.Contains("->min") || fileText.Contains("-> min"))
                {
                    radioButton_min.Checked = true;
                    fileText = fileText.Replace(" ->min", "");
                    fileText = fileText.Replace(" -> min", "");
                }
                else
                {
                    MessageBox.Show("В файле отсутствует условие максимизации или минимизации. Пожалуйста, установите это условие вручную.");
                }
            }

            string[] tmpText = fileText.Split('\n');


            // создаём и инициализируем двумерный массив - т.е. массив слов, а каждое слово - массив состоящий из букв
            string[][] _text = new string[tmpText.Length][];
            for (int i = 0; i < tmpText.Length; i++)
            {
                _text[i] = new string[tmpText[i].Length];
            }
            for (int i = 0; i < tmpText.Length; i++)
            {
                _text[i] = tmpText[i].Split(' ');
            }



            //Очищаем клетки
            clearGrids();

            // Добавляем целевую функцию и ограничения в клетки
            addGridParam(_text[0], dataGridView1);
            for (int i = 1; i < tmpText.Length; i++)
            {
                addGridParam(_text[i], dataGridView2);
            }
            non_sort_for_columns();
            dataGridView2.Columns[dataGridView2.ColumnCount - 1].HeaderText = "d";

            drawing_corner_dot();
        }


        public void addGridParam(string[] N, DataGridView Grid)

        {
            while (N.Length > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{Grid.ColumnCount + 1}", $"x{Grid.ColumnCount + 1}");
            }
            Grid.Rows.Add(N);
        }

        public void clearGrids()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();
            dataGridView2.Rows.Clear();
            dataGridView2.Columns.Clear();
        }

        /// <summary>
        /// Ищем ранг матрицы. Вместе с этим в функции уже удаляются "ненужные" строки.
        /// </summary>
        /// <returns>Возвращает ранг матрицы.</returns>
        private int RangOfMatrix(List<List<double>> elements_1)
        {
            double first_elem = 0;
            for (int i = 0; i < elements_1.Count; i++)
            {
                int j = 0;
                first_elem = elements_1[i][j];
                //находим ненулевой элемент в строке. если такого нет, то удаляем строку из нулей
                if (first_elem == 0)
                {
                    j = 1;
                    while (first_elem == 0)
                    {
                        first_elem = elements_1[i][j];
                        j++;
                        //если не нашли не нулевого, то удаляем строку из нулей
                        if (j == elements_1[0].Count)
                        {
                            elements_1.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                    j--;
                }

                //удалось найти не нулевой
                if (first_elem != 0)
                {
                    for (int p = 0; p < elements_1[0].Count; p++)
                        elements_1[i][p] /= first_elem;
                    for (int k = i + 1; k < elements_1.Count; k++)
                    {
                        if (elements_1[k][j] != 0)
                        {
                            double first_elem_1 = elements_1[k][j];
                            for (int m = 0; m < elements_1[0].Count; m++)
                            {
                                elements_1[k][m] = elements_1[k][m] - elements_1[i][m] * first_elem_1;
                            }
                        }
                    }
                }
            }

            return elements_1.Count;
        }

        /// <summary>
        /// Ищем ранг матрицы для дробей. Вместе с этим в функции уже удаляются "ненужные" строки.
        /// </summary>
        /// <returns>Возвращает ранг матрицы.</returns>
        private int RangOfMatrix(List<List<Fraction>> elements_1)
        {
            Fraction first_elem = new Fraction(0);
            for (int i = 0; i < elements_1.Count; i++)
            {
                int j = 0;
                first_elem = elements_1[i][j];
                //находим ненулевой элемент в строке. если такого нет, то удаляем строку из нулей
                if (first_elem == 0)
                {
                    j = 1;
                    while (first_elem == 0)
                    {
                        first_elem = elements_1[i][j];
                        j++;
                        //если не нашли не нулевого, то удаляем строку из нулей
                        if (j == elements_1[0].Count)
                        {
                            elements_1.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                    j--;
                }

                //удалось найти не нулевой
                if (first_elem != 0)
                {
                    for (int p = 0; p < elements_1[0].Count; p++)
                        elements_1[i][p] /= first_elem;
                    for (int k = i + 1; k < elements_1.Count; k++)
                    {
                        if (elements_1[k][j] != 0)
                        {
                            Fraction first_elem_1 = elements_1[k][j];
                            for (int m = 0; m < elements_1[0].Count; m++)
                            {
                                elements_1[k][m] = elements_1[k][m] - elements_1[i][m] * first_elem_1;
                            }
                        }
                    }
                }
            }

            return elements_1.Count;
        }

        private void checkBoxCornerDot_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxCornerDot.Checked == true)
            {
                drawing_corner_dot();

                dataGridView_CornerDot.Visible = true;
            }
            else
            {
                dataGridView_CornerDot.Visible = false;
            }
        }

        private void radioButton_default_drob_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_decimal_drob.Checked)
            {
                decimal_or_radical_drob = true;
            }
            else if (radioButton_default_drob.Checked)
            {
                decimal_or_radical_drob = false;
            }
        }

        private void SaveFile_Click(object sender, EventArgs e)
        {
            // Для работы с десятичными
            List<List<string>> general = new List<List<string>>(); // общая
            List<List<double>> ogr = new List<List<double>>();
            List<List<double>> cel_function = new List<List<double>>();
            // Для работы с обыкновенными дробями
            List<List<string>> general_with_radicals = new List<List<string>>(); // общая
            List<List<Fraction>> ogr_with_radicals = new List<List<Fraction>>();
            List<List<Fraction>> cel_function_with_radicals = new List<List<Fraction>>();

            // Если выбраны десятичные дроби
            if (decimal_or_radical_drob)
            {
                try
                {
                    read_grids(dataGridView1, cel_function);
                    read_grids(dataGridView2, ogr);
                }
                catch (Exception d)
                {
                    MessageBox.Show("Следующая ошибка возникает при:\n\n1) " + d.Message + "\n\n2) Возможно, при заполнении ячеек, последнее значение не было сохранено. " +
                        "Обратите внимание, ни одна из ячеек не должна быть в состоянии редактирования. " +
                        "Если вы изменяете какую-либо ячейку, сохраняйте её значение с помощью клавиши Enter" +
                        "\n\n3) Так же следует помнить, что на месте отсутствуещего коэффициента должен стоять 0, а не пустое значение.", "Ошибка при считывании коэффициентов.");
                    return;
                }

                general.Add(new List<string>());
                for (int i = 0; i < cel_function[0].Count; i++)
                {
                    general[0].Add(cel_function[0][i].ToString());
                }

                if (radioButton_max.Checked)
                {
                    general[0].Add("->max");
                }
                else
                {
                    general[0].Add("->min");
                }

                for (int i = 0; i < ogr.Count; i++)
                {
                    general.Add(new List<string>());
                    for (int j = 0; j < ogr[i].Count; j++)
                    {
                        general[i + 1].Add(ogr[i][j].ToString());
                    }
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;
                TextWriter tw = new StreamWriter(filename);

                for (int i = 0; i < general.Count; i++)
                {
                    for (int j = 0; j < general[i].Count; j++)
                    {
                        tw.Write(general[i][j]);
                        if (!(j == general[i].Count - 1))
                            tw.Write(' ');
                    }
                    if (!(i == general.Count - 1))
                        tw.WriteLine();
                }
                tw.Close();

                MessageBox.Show("Файл успешно сохранён.", "Сохранение задачи");
            }
            // Если выбраны обыкновенные дроби
            else
            {
                try
                {
                    read_grids(dataGridView1, cel_function_with_radicals);
                    read_grids(dataGridView2, ogr_with_radicals);
                }
                catch (Exception d)
                {
                    MessageBox.Show("Следующая ошибка возникает при:\n\n1) " + d.Message + "\n\n2) Возможно, при заполнении ячеек, последнее значение не было сохранено. " +
                        "Обратите внимание, ни одна из ячеек не должна быть в состоянии редактирования. " +
                        "Если вы изменяете какую-либо ячейку, сохраняйте её значение с помощью клавиши Enter" +
                        "\n\n3) Так же следует помнить, что на месте отсутствуещего коэффициента должен стоять 0, а не пустое значение.", "Ошибка при считывании коэффициентов.");
                    return;
                }

                general_with_radicals.Add(new List<string>());

                for (int i = 0; i < cel_function_with_radicals[0].Count; i++)
                {
                    general_with_radicals[0].Add(cel_function_with_radicals[0][i].ToString());
                }

                if (radioButton_max.Checked)
                {
                    general_with_radicals[0].Add("->max");
                }
                else
                {
                    general_with_radicals[0].Add("->min");
                }

                for (int i = 0; i < ogr_with_radicals.Count; i++)
                {
                    general_with_radicals.Add(new List<string>());
                    for (int j = 0; j < ogr_with_radicals[i].Count; j++)
                    {
                        general_with_radicals[i + 1].Add(ogr_with_radicals[i][j].ToString());
                    }
                }

                if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                    return;

                // получаем выбранный файл
                string filename = saveFileDialog1.FileName;
                TextWriter tw = new StreamWriter(filename);

                for (int i = 0; i < general_with_radicals.Count; i++)
                {
                    for (int j = 0; j < general_with_radicals[i].Count; j++)
                    {
                        tw.Write(general_with_radicals[i][j]);
                        if (!(j == general_with_radicals[i].Count - 1))
                            tw.Write(' ');
                    }
                    if (!(i == general_with_radicals.Count - 1))
                        tw.WriteLine();
                }
                tw.Close();

                MessageBox.Show("Файл успешно сохранён.", "Сохранение задачи");
            }
        }

        private void radioButton_imagine_b_CheckedChanged(object sender, EventArgs e)
        {
            // Если включили режим искуственного базиса
            if (radioButton_imagine_b.Checked)
            {
                // убираем возможность добавления корневой точки
                checkBoxCornerDot.Checked = false;
                checkBoxCornerDot.Visible = false;

            }
            // Если выбрали другой режим
            if (!radioButton_imagine_b.Checked)
            {
                // Добавляем возможность добавления корневой точки
                checkBoxCornerDot.Visible = true;

            }
        }

        private void справкаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{F1}");
        }
    }
}
