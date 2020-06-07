using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex_Method
{
   public partial class StepsSolve : Form
    {
        /// <summary>
        /// Матрица коэффициентов системы ограничений-равенств.
        /// </summary>
        List<List<double>> ogr = new List<List<double>>();
        /// <summary>
        /// Целевая функция.
        /// </summary>
        List<List<double>> cel_function = new List<List<double>>();
        /// <summary>
        /// Буфер для матрицы коэффициентов системы ограничений-равенств.
        /// </summary>
        List<List<List<double>>> buffer_elements = new List<List<List<double>>>();
        /// <summary>
        /// Матрица коэффициентов системы ограничений-равенств для обыкновенных дробей
        /// </summary>
        List<List<Fraction>> ogr_with_radicals = new List<List<Fraction>>();
        /// <summary>
        /// Целевая функция для обыкновенных дробей
        /// </summary>
        List<List<Fraction>> cel_function_with_radicals = new List<List<Fraction>>();
        /// <summary>
        /// Буфер для матрицы коэффициентов системы ограничений-равенств для обыкновенных дробей
        /// </summary>
        List<List<List<Fraction>>> buffer_elements_for_radicals = new List<List<List<Fraction>>>();
        /// <summary>
        /// Возможные координаты опорного элемента.
        /// </summary>
        List<List<int>> the_coordinates_of_the_support_element;
        /// <summary>
        /// Текущий шаг.
        /// </summary>
        public static int step = 0;
        /// <summary>
        /// Ищем минимум или максимум. 0 - выбран максимум, 1 - выбран минимум.
        /// </summary>
        int MinMax;
        /// <summary>
        /// Количество базисных переменных.
        /// </summary>
        int number_of_basix_permutations;
        /// <summary>
        /// Количество свободных переменных.
        /// </summary>
        int number_of_free_variables;
        /// <summary>
        /// Вспомогательная переменная, которая определяет была ли создана симплекс-таблица. True - была создана. False - не была создана.
        /// </summary>
        public static bool simplex_table_was_draw;
        /// <summary>
        /// Симплекс-таблица.
        /// </summary>
        Simplex simplextable;
        /// <summary>
        /// Разрешающая строка.
        /// </summary>
        int row_of_the_support_element;
        /// <summary>
        /// Разрешающий столбец.
        /// </summary>
        int column_of_the_support_element;
        /// <summary>
        /// Задана ли угловая точка(bool).
        /// </summary>
        bool CornerDot;
        /// <summary>
        /// Определитель - какой режим работы с дробями выбран. false - обыкновенные. true - десятичные
        /// </summary>
        bool Radical_or_Decimal;
        /// <summary>
        /// Буфер для сохранения расположения переменных в симплекс таблице
        /// </summary>
        List<List<int>> buffer_variable_visualization = new List<List<int>>();
        /// <summary>
        /// Вспомогательный массив для отображения переменных
        /// </summary>
        List<int> variable_visualization = new List<int>();
        /// <summary>
        /// Была ли добавлена угловая точка
        /// </summary>
        private bool corner_dot_was_added;

        public StepsSolve(List<List<double>> cel_function, List<List<double>> ogr, int MinMax, bool CornerDot, int rang, bool radical_or_decimal)
        {
            InitializeComponent();

            // Переносим заполненный массив с главного окна
            this.cel_function = cel_function;
            // Переносим заполненный массив с главного окна
            this.ogr = ogr;
            // изначально мы на нулевом шаге
            step = 0;
            // Задача на минимум или максимум(0 - максимум, 1 - минимум)
            this.MinMax = MinMax;
            // Выбрана ли угловая точка
            this.CornerDot = CornerDot;
            // Количество базисных переменных.
            this.number_of_basix_permutations = rang;
            // Вычисляем количество свободных переменных
            number_of_free_variables = cel_function[0].Count - number_of_basix_permutations;
            this.Radical_or_Decimal = radical_or_decimal;


            // добавляем в ячейки, то есть отрисовываем то, что есть
            //  label1.Text
            label1.Text = "Матрица коэффициентов системы ограничения равенств.";
            addGridParam(ogr, dataGridView3);
        }

        public StepsSolve(List<List<Fraction>> cel_function_with_radicals, List<List<Fraction>> ogr_with_radicals, int minMax, bool cornerDot, int rang, bool decimal_or_radical_drob)
        {
            InitializeComponent();

            // Переносим заполненный массив с главного окна
            this.cel_function_with_radicals = cel_function_with_radicals;
            // Переносим заполненный массив с главного окна
            this.ogr_with_radicals = ogr_with_radicals;
            // изначально мы на нулевом шаге
            step = 0;
            // Задача на минимум или максимум(0 - максимум, 1 - минимум)
            this.MinMax = minMax;
            // Выбрана ли угловая точка
            this.CornerDot = cornerDot;
            // Количество базисных переменных.
            this.number_of_basix_permutations = rang;
            // Вычисляем количество свободных переменных
            number_of_free_variables = cel_function_with_radicals[0].Count - number_of_basix_permutations;
            // Какой режим работы с дробями выбран. false - обыкновенные. true - десятичные
            this.Radical_or_Decimal = decimal_or_radical_drob;

            // добавляем в ячейки, то есть отрисовываем то, что есть
            label1.Text = "Матрица коэффициентов системы ограничения равенств.";
            addGridParam(ogr_with_radicals, dataGridView3);
        }

        public StepsSolve(List<List<Fraction>> cel_function_with_radicals, List<List<Fraction>> ogr_with_radicals, int minMax, bool cornerDot, int rang, bool decimal_or_radical_drob, List<int> variable_visualization)
        {
            InitializeComponent();

            // Переносим заполненный массив с главного окна
            this.cel_function_with_radicals = cel_function_with_radicals;
            // Переносим заполненный массив с главного окна
            this.ogr_with_radicals = ogr_with_radicals;
            // изначально мы на нулевом шаге
            step = 0;
            // Задача на минимум или максимум(0 - максимум, 1 - минимум)
            this.MinMax = minMax;
            // Выбрана ли угловая точка
            this.CornerDot = cornerDot;
            // Количество базисных переменных.
            this.number_of_basix_permutations = rang;
            // Вычисляем количество свободных переменных
            number_of_free_variables = cel_function_with_radicals[0].Count - number_of_basix_permutations;
            // Какой режим работы с дробями выбран. false - обыкновенные. true - десятичные
            this.Radical_or_Decimal = decimal_or_radical_drob;
            // Добавляем список для визуализации переменных
            this.variable_visualization = variable_visualization;

            // добавляем в ячейки, то есть отрисовываем то, что есть
            label1.Text = "Матрица коэффициентов системы ограничения равенств.";
            addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);
        }

        public StepsSolve(List<List<double>> cel_function, List<List<double>> ogr, int MinMax, bool CornerDot, int rang, bool radical_or_decimal, List<int> variable_visualization)
        {
            InitializeComponent();

            // Переносим заполненный массив с главного окна
            this.cel_function = cel_function;
            // Переносим заполненный массив с главного окна
            this.ogr = ogr;
            // изначально мы на нулевом шаге
            step = 0;
            // Задача на минимум или максимум(0 - максимум, 1 - минимум)
            this.MinMax = MinMax;
            // Выбрана ли угловая точка
            this.CornerDot = CornerDot;
            // Количество базисных переменных.
            this.number_of_basix_permutations = rang;
            // Вычисляем количество свободных переменных
            number_of_free_variables = cel_function[0].Count - number_of_basix_permutations;
            // Какой режим работы с дробями выбран. false - обыкновенные. true - десятичные
            this.Radical_or_Decimal = radical_or_decimal;
            // Добавляем список для визуализации переменных
            this.variable_visualization = variable_visualization;

            // добавляем в ячейки, то есть отрисовываем то, что есть
            label1.Text = "Матрица коэффициентов системы ограничения равенств.";
            addGridParam(ogr, dataGridView3, variable_visualization);
        }

        private void StepByStep_Load(object sender, EventArgs e)
        {
            var outPutDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
            var filePath = System.IO.Path.Combine(outPutDirectory, "data\\Help\\StepByStepHelp.rtf");

            string file_path = new Uri(filePath).LocalPath; // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug
                                                            // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug\data\Help\MainPageHelp.rtf

   //         helpProvider1.HelpNamespace = file_path;
        }

        /// <summary>
        /// Добавляем данные в ячейки и отрисовываем их - из String - двумерного массива
        /// </summary>
        public void addGridParam(string[] N, DataGridView Grid)

        {
            if (corner_dot_was_added == false)
                dataGridView3.Rows.Clear();
            while (N.Length > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{Grid.ColumnCount + 1}", $"x{Grid.ColumnCount + 1}");
            }
            Grid.Rows.Add(N);
            non_sort_for_columns();
            if (corner_dot_was_added == false)
                Grid.Columns[Grid.ColumnCount - 1].HeaderText = "Своб.";
        }

        /// <summary>
        /// Добавляем данные в ячейки и отрисовываем их - из List - двумерного списка
        /// </summary>
        public void addGridParam(List<List<double>> N, DataGridView Grid)

        {
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            while (N[0].Count > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{Grid.ColumnCount + 1}", $"x{Grid.ColumnCount + 1}");
            }
            for (int i = 0; i < N.Count; i++)       
            {
                Grid.Rows.Add();
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j];
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "Своб.";
        }

        /// <summary>
        /// Добавляем данные в ячейки и отрисовываем их для дробей - из List - двумерного списка
        /// </summary>
        public void addGridParam(List<List<Fraction>> N, DataGridView Grid)

        {
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            while (N[0].Count > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{Grid.ColumnCount + 1}", $"x{Grid.ColumnCount + 1}");
            }
            for (int i = 0; i < N.Count; i++)
            {
                Grid.Rows.Add();
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j].Reduction();
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "Своб.";
        }

        /// <summary>
        /// Добавляем данные в ячейки и отрисовываем их для дробей - из List - двумерного списка по списку визуализации
        /// </summary>
        public void addGridParam(List<List<Fraction>> N, DataGridView Grid, List<int> variable_visualization)

        {
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            while (N[0].Count > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{variable_visualization[Grid.ColumnCount]}", $"x{variable_visualization[Grid.ColumnCount]}");
            }
            for (int i = 0; i < N.Count; i++)
            {
                Grid.Rows.Add();
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j].Reduction();
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "Своб.";
        }

        /// <summary>
        /// Добавляем данные в ячейки и отрисовываем их для обыкновенных - из List - двумерного списка по списку визуализации
        /// </summary>
        public void addGridParam(List<List<double>> N, DataGridView Grid, List<int> variable_visualization)

        {
            dataGridView3.Rows.Clear();
            dataGridView3.Columns.Clear();
            while (N[0].Count > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{variable_visualization[Grid.ColumnCount]}", $"x{variable_visualization[Grid.ColumnCount]}");
            }
            for (int i = 0; i < N.Count; i++)
            {
                Grid.Rows.Add();
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j];
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "Своб.";
        }

        /// <summary>
        /// Добавляем данные симплекс таблицы в ячейки и отрисовываем их - из List - двумерного списка
        /// </summary>
        public void addGridParam_for_simplex_elements(List<List<double>> N, DataGridView Grid)
        {
            for (int i = 0; i < N.Count; i++)
            {
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j];
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "Своб.";
        }

        /// <summary>
        /// Добавляем данные симплекс таблицы в ячейки и отрисовываем их - из List - двумерного списка
        /// </summary>
        public void addGridParam_for_simplex_elements(List<List<Fraction>> N, DataGridView Grid)
        {
            for (int i = 0; i < N.Count; i++)
            {
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j].Reduction();
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "Своб.";
        }

        /// <summary>
        /// Делаем колонки несортируемыми
        /// </summary>
        private void non_sort_for_columns()
        {
            foreach (DataGridViewColumn dgvc in dataGridView3.Columns)
            {
                dgvc.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
        }

        public void read_grids(DataGridView Grid, List<List<double>> N)
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                N.Add(new List<double>());
                for (int j = 0; j < Grid.Columns.Count; j++)
                {
                    N[i].Add(Convert.ToDouble(Grid.Rows[i].Cells[j].Value.ToString().Trim()));
                }
            }
        }

        private void buttonNext_Click(object sender, EventArgs e)
        {
            switch (step)
            {
                case 0:
                    try
                    {
                        if (Radical_or_Decimal)
                        {
                            //буферизация данных
                            BufferingTableValues(ogr);
                            //прямой ход Гаусса
                            Gauss(ogr, CornerDot);
                            //Обновление визуализации переменных.
                            if (CornerDot)
                                addGridParam(ogr, dataGridView3, variable_visualization);
                            else
                                addGridParam(ogr, dataGridView3);
                        }
                        else
                        {
                            //буферизация данных для дробей
                            BufferingTableValues(ogr_with_radicals);
                            //прямой ход Гаусса для дробей
                            Gauss(ogr_with_radicals, CornerDot);
                            //Обновление визуализации переменных.
                            if (CornerDot)
                                addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);
                            else
                                addGridParam(ogr_with_radicals, dataGridView3);
                        }

                    }
                    catch (Exception d)
                    {
                        MessageBox.Show(d.Message);
                    }
                    step++;
                    label1.Text = "Шаг 1: Прямой ход метода Гаусса.";
                    break;

                case 1:
                    if (Radical_or_Decimal)
                    {
                        //буферизация данных
                        BufferingTableValues(ogr);
                        //Выражение базисных переменных.
                        HoistingMatrix(ogr, number_of_basix_permutations);
                        //Обновление визуализации переменных.
                        if (CornerDot)
                            addGridParam(ogr, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr, dataGridView3);
                    }
                    else
                    {
                        //буферизация данных для дробей
                        BufferingTableValues(ogr_with_radicals);
                        //Выражение базисных переменных для дробей
                        HoistingMatrix(ogr_with_radicals, number_of_basix_permutations);
                        //Обновление визуализации переменных.
                        if (CornerDot)
                            addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr_with_radicals, dataGridView3);
                    }
                    step++;
                    label1.Text = "Шаг 2: Выражение базисных переменных.";
                    break;

                case 2:
                    if (Radical_or_Decimal)
                    {
                        // буферизация данных
                        BufferingTableValues(ogr);

                        if (simplex_table_was_draw == false)
                        {
                            simplextable = new Simplex(number_of_basix_permutations, number_of_free_variables, ogr, cel_function, true, Radical_or_Decimal);
                            DrawSimplexTable(ogr);
                            //Симплекс-таблица была создана
                            simplex_table_was_draw = true;
                        }
                    }
                    else
                    {
                        // буферизация данных для дробей
                        BufferingTableValues(ogr_with_radicals);

                        if (simplex_table_was_draw == false)
                        {
                            simplextable = new Simplex(number_of_basix_permutations, number_of_free_variables, ogr_with_radicals, cel_function_with_radicals, true, Radical_or_Decimal);
                            DrawSimplexTable(ogr_with_radicals);
                            //Симплекс-таблица была создана
                            simplex_table_was_draw = true;
                        }
                    }

                    switch (simplextable.ResponseCheck())
                    {
                        case 0:
                            step++;
                            label1.Text = "Шаг 3: Симплекс-таблица.";
                            break;
                        case 1:
                            // Если ответ готов сразу без выбора опорного элемента
                            label1.Text = "Ответ";
                            // Увиличиваем шаг. И делаем видимыми элементы
                            step++;
                            label_answer.Visible = true;
                            groupBoxCornerDot.Visible = true;

                            // Подставляем ответ
                            if (MinMax == 0)
                            {
                                if (Radical_or_Decimal)
                                    label_answer.Text = "Ответ :" + simplextable.Response();
                                else
                                    label_answer.Text = "Ответ :" + simplextable.Responce_for_radicals().Reduction();
                            }
                            else
                            {
                                if (Radical_or_Decimal)
                                    label_answer.Text = "Ответ: " + simplextable.Response() * (-1);
                                else
                                    label_answer.Text = "Ответ :" + simplextable.Responce_for_radicals().Reduction() * (-1);
                            }

                            // Выводим точку X*
                            if (corner_dot_was_added == false)
                            {
                                corner_dot_was_added = true;
                                //добавляем точку
                                addGridParam(ResponseDot(), dataGridViewCornerDot);
                            }

                            buttonNext.Enabled = false;
                            break;
                        case -1:
                            step++;
                            MessageBox.Show("Задача не разрешима");
                            buttonNext.Enabled = false;
                            break;
                    }
                    break;

                case 3:
                    //выбор опорного
                    SelectionOfTheSupportElement(simplextable);
                    step++;
                    label1.Text = "Шаг" + step + ": Выбор опорного элемента";
                    break;
                default:
                    try
                    {
                        //выбран ли опорный элемент
                        ButtonPressedOrNot(simplextable);
                        //Смена местами визуализаций переменных(после выбора опорного элемента) + буферизация координат опорного элемента.
                        ChangeOfVisualizationVariables(simplextable);
                        //Буферизация элементов симплекс таблицы
                        simplextable.BufferingSimplexTableValues(step);
                        //удаляем подсвеченные ячейки
                        delete_green_grids();
                        //вычисление симплекс таблицы по выбранному опорному элементу
                        simplextable.CalculateSimplexTable(row_of_the_support_element, column_of_the_support_element);
                        // обновление данных ячеек таблицы
                        if (Radical_or_Decimal)
                            addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                        else
                            addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);

                        switch (simplextable.ResponseCheck())
                        {
                            case 0:
                                step++;
                                label1.Text = "Шаг" + step + ": Выбор опорного элемента";
                                //выбор опорного
                                SelectionOfTheSupportElement(simplextable);
                                break;
                            case 1:
                                label1.Text = "Ответ готов!";
                                step++;
                                label_answer.Visible = true;
                                groupBoxCornerDot.Visible = true;
                                if (MinMax == 0)
                                {
                                    if (Radical_or_Decimal)
                                        label_answer.Text = "Ответ :" + simplextable.Response();
                                    else
                                        label_answer.Text = "Ответ :" + simplextable.Responce_for_radicals().Reduction();
                                }
                                else
                                {
                                    if (Radical_or_Decimal)
                                        label_answer.Text = "Ответ: " + simplextable.Response() * (-1);
                                    else
                                        label_answer.Text = "Ответ :" + simplextable.Responce_for_radicals().Reduction() * (-1);
                                }

                                if (corner_dot_was_added == false)
                                {
                                    corner_dot_was_added = true;
                                    //добавляем точку
                                    addGridParam(ResponseDot(), dataGridViewCornerDot);
                                }

                                buttonNext.Enabled = false;
                                break;
                            case -1:
                                step++;
                                MessageBox.Show("Задача не разрешима");
                                buttonNext.Enabled = false;
                                break;
                        }

                    }
                    catch (Exception d)
                    {
                        MessageBox.Show(d.Message);
                    }

                    break;
            }
        }



        public string[] ResponseDot()
        {
            if (Radical_or_Decimal)
            {
                //угловая точка соответствующая решению 
                string[] finish_corner_dot = new string[cel_function[0].Count];

                // по умолчанию элементы - 0
                for (int i = 0; i < finish_corner_dot.Length; i++)
                    finish_corner_dot[i] = "0";

                //вспомогательная переменная
                int temp;

                //заполняем коэффициентами
                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {
                    temp = Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x'));
                    finish_corner_dot[temp - 1] = simplextable.simplex_elements[i][simplextable.simplex_elements[0].Count - 1].ToString();
                }

                return finish_corner_dot;
            }
            else
            {
                //угловая точка соответствующая решению 
                string[] finish_corner_dot = new string[cel_function_with_radicals[0].Count];

                // по умолчанию элементы - 0
                for (int i = 0; i < finish_corner_dot.Length; i++)
                    finish_corner_dot[i] = "0";

                //вспомогательная переменная
                int temp;

                //заполняем коэффициентами
                for (int i = 0; i < dataGridView3.Rows.Count - 1; i++)
                {
                    temp = Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x'));
                    finish_corner_dot[temp - 1] = simplextable.simplex_elements_with_radicals[i][simplextable.simplex_elements_with_radicals[0].Count - 1].ToString();
                }

                return finish_corner_dot;
            }
        }



        private void ChangeOfVisualizationVariables(Simplex simplextable)
        {
            string tmp_x;

            // Буферизация координат опорного элемента до смены переменных местами - по этим координатам мы сможем поменять элементы обратно, если это потребуется
            buffer_variable_visualization.Add(new List<int>());
            buffer_variable_visualization[buffer_variable_visualization.Count - 1].Add(row_of_the_support_element);
            buffer_variable_visualization[buffer_variable_visualization.Count - 1].Add(column_of_the_support_element);

            // Меняем названия переменных местами
            tmp_x = (string)dataGridView3.Rows[row_of_the_support_element].HeaderCell.Value;
            dataGridView3.Rows[row_of_the_support_element].HeaderCell.Value = dataGridView3.Columns[column_of_the_support_element].HeaderText;
            dataGridView3.Columns[column_of_the_support_element].HeaderText = tmp_x;
        }

        private void ChangeOfVisualizationVariables_without_bufferization(Simplex simplextable)
        {
            string tmp_x;
            int[] tmp_coordination = new int[2];

            // Достаём из буфера последние координаты опорного элемента
            tmp_coordination[0] = buffer_variable_visualization[buffer_variable_visualization.Count - 1][0];
            tmp_coordination[1] = buffer_variable_visualization[buffer_variable_visualization.Count - 1][1];

            // Меняем названия переменных местами
            tmp_x = (string)dataGridView3.Rows[tmp_coordination[0]].HeaderCell.Value;
            dataGridView3.Rows[tmp_coordination[0]].HeaderCell.Value = dataGridView3.Columns[tmp_coordination[1]].HeaderText;
            dataGridView3.Columns[tmp_coordination[1]].HeaderText = tmp_x;

            buffer_variable_visualization.RemoveAt(buffer_variable_visualization.Count - 1);
        }

        /// <summary>
        /// Проверяем, выбран ли опорный элемент
        /// </summary>
        /// <param name="simplextable"></param>
        private void ButtonPressedOrNot(Simplex simplextable)
        {
            for (int i = 0; i < the_coordinates_of_the_support_element.Count; i++)
            {
                if (dataGridView3.Rows[the_coordinates_of_the_support_element[i][0]].Cells[the_coordinates_of_the_support_element[i][1]] == dataGridView3.CurrentCell)
                {
                    row_of_the_support_element = the_coordinates_of_the_support_element[i][0];
                    column_of_the_support_element = the_coordinates_of_the_support_element[i][1];
                    return;
                }
            }
            throw new Exception("Выберите опорный элемент.");
        }

        /// <summary>
        /// Нахождение всех опорных элементов и их выделение
        /// </summary>
        /// <param name="simplextable"></param>
        public void SelectionOfTheSupportElement(Simplex simplextable)
        {
            if (Radical_or_Decimal)
            {
                //координаты минимального элемента в столбце
                int[] minimum = new int[2];
                the_coordinates_of_the_support_element = new List<List<int>>();
                int index = 0; //для счёта координат

                //ищем отрицательный элемент в последней строке
                for (int j = 0; j < simplextable.simplex_elements[0].Count - 1; j++)
                {
                    if (simplextable.simplex_elements[simplextable.simplex_elements.Count - 1][j] < 0)
                    {

                        minimum[0] = -1;
                        minimum[1] = -1;
                        //ищем подходящий не отрицательный элемент в столбце
                        for (int i = 0; i < simplextable.simplex_elements.Count - 1; i++)
                        {
                            if (simplextable.simplex_elements[i][j] > 0)
                            {
                                // Если минимального не встречалось - назначем
                                if ((minimum[0] == -1) && (minimum[1] == -1))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                                // Если минимальный уже есть - сравниваем
                                else if ((simplextable.simplex_elements[minimum[0]][simplextable.simplex_elements[0].Count - 1] / simplextable.simplex_elements[minimum[0]][minimum[1]]) > (simplextable.simplex_elements[i][simplextable.simplex_elements[0].Count - 1] / simplextable.simplex_elements[i][j]))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                            }
                        }

                        //если есть минимальный, то делаем его подсвеченным
                        if ((minimum[0] != -1) && (minimum[1] != -1))
                        {
                            dataGridView3.Rows[minimum[0]].Cells[minimum[1]].Style.BackColor = System.Drawing.Color.Green;

                            //координаты возможного опорного элемента
                            the_coordinates_of_the_support_element.Add(new List<int>());
                            the_coordinates_of_the_support_element[index].Add(minimum[0]);
                            the_coordinates_of_the_support_element[index].Add(minimum[1]);
                            index++;
                        }
                    }
                }
            }
            // Если выбраны обыкновенные дроби
            else
            {
                //координаты минимального элемента в столбце
                int[] minimum = new int[2];
                the_coordinates_of_the_support_element = new List<List<int>>();
                int index = 0; //для счёта координат

                //ищем отрицательный элемент в последней строке
                for (int j = 0; j < simplextable.simplex_elements_with_radicals[0].Count - 1; j++)
                {
                    if (simplextable.simplex_elements_with_radicals[simplextable.simplex_elements_with_radicals.Count - 1][j] < 0)
                    {

                        minimum[0] = -1;
                        minimum[1] = -1;
                        //ищем подходящий не отрицательный элемент в столбце
                        for (int i = 0; i < simplextable.simplex_elements_with_radicals.Count - 1; i++)
                        {
                            if (simplextable.simplex_elements_with_radicals[i][j] > 0)
                            {
                                // Если минимального не встречалось - назначем
                                if ((minimum[0] == -1) && (minimum[1] == -1))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                                // Если минимальный уже есть - сравниваем
                                else if ((simplextable.simplex_elements_with_radicals[minimum[0]][simplextable.simplex_elements_with_radicals[0].Count - 1] / simplextable.simplex_elements_with_radicals[minimum[0]][minimum[1]]) > (simplextable.simplex_elements_with_radicals[i][simplextable.simplex_elements_with_radicals[0].Count - 1] / simplextable.simplex_elements_with_radicals[i][j]))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                            }
                        }

                        //если есть минимальный, то делаем его подсвеченным
                        if ((minimum[0] != -1) && (minimum[1] != -1))
                        {
                            dataGridView3.Rows[minimum[0]].Cells[minimum[1]].Style.BackColor = System.Drawing.Color.Green;

                            //координаты возможного опорного элемента
                            the_coordinates_of_the_support_element.Add(new List<int>());
                            the_coordinates_of_the_support_element[index].Add(minimum[0]);
                            the_coordinates_of_the_support_element[index].Add(minimum[1]);
                            index++;
                        }
                    }
                }
            }

        }



        /// <summary>
        /// Преобразование матрицы в симплекс таблицу
        /// </summary>
        public void DrawSimplexTable(List<List<double>> ogr)
        {
            //для сиплекс метода
            if (simplextable.simplex_or_artificial == true)
            {

                double a = 0;
                //счёт столбца
                int column_index = 1;


                // Отображаем таблицу в её естественном виде // убираем базис и переносим его в левую колонку
                string now_basix_name = "";
                // Алгоритм определяющий единственные единицы в столбцах
                for (int j = 0; j < dataGridView3.Columns.Count - 1; j++) // Проходимся по всем колонкам
                {
                    a = 0;
                    column_index = 0;
                    bool only_one_in_column = false; // Единственная единица в столбце

                    for (int i = 0; i < dataGridView3.Rows.Count; i++) // Проходимся по всем элементам в колонке, кроме последнего(т.е. кроме целевой функции)
                    {
                        a += (double)dataGridView3.Rows[i].Cells[j].Value;

                        // Если нам встречается единица, считаем, что колонка базисная
                        if ((double)dataGridView3.Rows[i].Cells[j].Value == 1)
                        {
                            now_basix_name = dataGridView3.Columns[j].Name;
                            column_index = i;
                            only_one_in_column = true;
                        }
                        // Если встретили отличное от единицы и это не ноль, смотрим, встречалась ли нам единица раньше. Если встречалась - значит считаем колонку НЕ базисной
                        else if (only_one_in_column == true && !((double)dataGridView3.Rows[i].Cells[j].Value == 0))
                        {
                            only_one_in_column = false;
                            break;
                        }

                        // Если дошли до последнего элемента в столбце и сумма всех не равна единице, то считаем НЕ базисной
                        if ((i == dataGridView3.Rows.Count - 1) && (a != 1))
                        {
                            only_one_in_column = false;
                            break;
                        }
                    }
                    if (only_one_in_column)
                    {
                        dataGridView3.Rows[column_index].HeaderCell.Value = now_basix_name;
                        dataGridView3.Columns.Remove(dataGridView3.Columns[j]);
                        j--;
                    }
                }

                column_index = 0;
                // считаем коэффициенты последней строки
                dataGridView3.Rows.Add();
                dataGridView3.Rows[simplextable.number_of_permutations].HeaderCell.Value = "f(x)";
                for (int j = simplextable.number_of_permutations; j < simplextable.ogr[0].Count - 1; j++)
                {
                    //логика
                    a = 0;
                    for (int i = 0; i < simplextable.ogr.Count; i++)
                    {
                        a += simplextable.ogr[i][j] * cel_function[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= simplextable.cel_function[0][Int32.Parse(dataGridView3.Columns[column_index].HeaderCell.Value.ToString().Replace("Своб.", column_index.ToString()).Trim('x')) - 1]; // функция подставления в коэфф в целевую (возможно Replace не нужно)

                    ////отображение
                    dataGridView3.Rows[simplextable.number_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = 0;
                for (int i = 0; i < simplextable.ogr.Count; i++)
                    a += simplextable.ogr[i][simplextable.ogr[0].Count - 1] * cel_function[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];

                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[dataGridView3.Columns.Count - 1].Value = a;

                //заполняем рабочий массив
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    simplextable.simplex_elements.Add(new List<double>());
                    for (int j = 0; j < dataGridView3.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplextable.simplex_elements[i].Add((double)dataGridView3.Rows[i].Cells[j].Value);
                    }
                }

            }
        }

        /// <summary>
        /// Преобразование матрицы в симплекс таблицу для дробей
        /// </summary>
        /// <param name="ogr"></param>
        public void DrawSimplexTable(List<List<Fraction>> ogr)
        {
            //для сиплекс метода
            if (simplextable.simplex_or_artificial == true)
            {

                Fraction a;
                //счёт столбца
                int column_index = 1;


                // Отображаем таблицу в её естественном виде // убираем базис и переносим его в левую колонку
                string now_basix_name = "";
                // Алгоритм определяющий единственные единицы в столбцах
                for (int j = 0; j < dataGridView3.Columns.Count - 1; j++) // Проходимся по всем колонкам
                {
                    a = new Fraction(0);
                    column_index = 0;
                    bool only_one_in_column = false; // Единственная единица в столбце

                    for (int i = 0; i < dataGridView3.Rows.Count; i++) // Проходимся по всем элементам в колонке, кроме последнего(т.е. кроме целевой функции)
                    {
                        a += (Fraction)dataGridView3.Rows[i].Cells[j].Value;

                        // Если нам встречается единица, считаем, что колонка базисная
                        if ((Fraction)dataGridView3.Rows[i].Cells[j].Value == new Fraction(1))
                        {
                            now_basix_name = dataGridView3.Columns[j].Name;
                            column_index = i;
                            only_one_in_column = true;
                        }
                        // Если встретили отличное от единицы и это не ноль, смотрим, встречалась ли нам единица раньше. Если встречалась - значит считаем колонку НЕ базисной
                        else if (only_one_in_column == true && !((Fraction)dataGridView3.Rows[i].Cells[j].Value == new Fraction(0)))
                        {
                            only_one_in_column = false;
                            break;
                        }

                        // Если дошли до последнего элемента в столбце и сумма всех не равна единице, то считаем НЕ базисной
                        if ((i == dataGridView3.Rows.Count - 1) && (a != 1))
                        {
                            only_one_in_column = false;
                            break;
                        }
                    }
                    if (only_one_in_column)
                    {
                        dataGridView3.Rows[column_index].HeaderCell.Value = now_basix_name;
                        dataGridView3.Columns.Remove(dataGridView3.Columns[j]);
                        j--;
                    }
                }

                column_index = 0;
                // считаем коэффициенты последней строки
                dataGridView3.Rows.Add();
                dataGridView3.Rows[simplextable.number_of_permutations].HeaderCell.Value = "f(x)";
                for (int j = simplextable.number_of_permutations; j < simplextable.ogr_with_radicals[0].Count - 1; j++)
                {
                    //логика
                    a = new Fraction(0);
                    for (int i = 0; i < simplextable.ogr_with_radicals.Count; i++)
                    {
                        a += simplextable.ogr_with_radicals[i][j] * cel_function_with_radicals[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= simplextable.cel_function_with_radicals[0][Int32.Parse(dataGridView3.Columns[column_index].HeaderCell.Value.ToString().Replace("Своб.", column_index.ToString()).Trim('x')) - 1];

                    ////отображение

                    dataGridView3.Rows[simplextable.number_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = new Fraction(0);
                for (int i = 0; i < simplextable.ogr_with_radicals.Count; i++)
                    a += simplextable.ogr_with_radicals[i][simplextable.ogr_with_radicals[0].Count - 1] * cel_function_with_radicals[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];

                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[dataGridView3.Columns.Count - 1].Value = a * (-1);
                a = a * (-1);
                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[dataGridView3.Columns.Count - 1].Value = a * (-1);

                //заполняем рабочий массив
                for (int i = 0; i < dataGridView3.Rows.Count; i++)
                {
                    simplextable.simplex_elements_with_radicals.Add(new List<Fraction>());
                    for (int j = 0; j < dataGridView3.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplextable.simplex_elements_with_radicals[i].Add((Fraction)dataGridView3.Rows[i].Cells[j].Value);
                    }
                }

            }
        }

        /// <summary>
        /// Метод Гаусса
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="CornerDot"></param>
        public static void Gauss(List<List<double>> elements, bool? CornerDot)
        {
            for (int global = 0; global < elements.Count; global++)
            {
                for (int i = global; i < elements.Count; i++)
                {
                    if (i == global)
                    {
                        //проверяем возможность выражения переменной
                        double first_elem = elements[i][global];
                        bool responce = true; //можно ли вообще выразить
                        if (first_elem == 0)
                        {
                            responce = false;
                            for (int k = i + 1; k < elements.Count; k++)
                                if (elements[k][global] != 0)
                                {
                                    responce = true;
                                    first_elem = elements[k][global];
                                    double temp;
                                    //смена строк
                                    for (int j = 0; j < elements[0].Count; j++)
                                    {
                                        temp = elements[i][j];
                                        elements[i][j] = elements[k][j];
                                        elements[k][j] = temp;
                                    }
                                    break;
                                }
                        }

                        //если не получилось выразить переменную и была задана начальная угловая точка
                        if ((responce == false) && (CornerDot == true))
                            throw new Exception("Проверьте правильно ли введены коэффициенты.");
                        //если не получилось выразить переменную и НЕ была задана начальная угловая точка
                        else if ((responce == false) && (CornerDot == false))
                        {
                            //то ищем в других столбцах
                            bool check = false;
                            for (int column = global + 1; column < elements[0].Count; column++)
                            {
                                for (int row = i; row < elements.Count; row++)
                                {
                                    if (elements[row][column] != 0)
                                    {
                                        check = true;
                                        first_elem = elements[row][column];
                                        double temp; //вспомогательная переменная

                                        //смена строк
                                        for (int j = 0; j < elements[0].Count; j++)
                                        {
                                            //для элементов матрицы
                                            temp = elements[i][j];
                                            elements[i][j] = elements[row][j];
                                            elements[row][j] = temp;
                                        }

                                        //смена столбцов
                                        for (int k = 0; k < elements.Count; k++)
                                        {
                                            //для элементов матрицы
                                            temp = elements[k][global];
                                            elements[k][global] = elements[k][column];
                                            elements[k][column] = temp;
                                        }
                                    }

                                    if (check)
                                        break;
                                }
                                if (check)
                                    break;
                            }

                            //Такого случая возможно не может быть. Поэтому это излишне.
                            if (check == false)
                                throw new Exception("Проверьте правильно ли введены коэффициенты.");
                        }



                        for (int j = 0; j < elements[0].Count; j++)
                            elements[i][j] /= first_elem;
                    }
                    else
                    {
                        double first_elem = elements[i][global];
                        for (int j = 0; j < elements[0].Count; j++)
                        {
                            elements[i][j] -= elements[global][j] * first_elem;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Метод Гаусса
        /// </summary>
        /// <param name="elements"></param>
        /// <param name="CornerDot"></param>
        public static void Gauss(List<List<Fraction>> elements, bool? CornerDot)
        {
            for (int global = 0; global < elements.Count; global++)
            {
                for (int i = global; i < elements.Count; i++)
                {
                    if (i == global)
                    {
                        //проверяем возможность выражения переменной
                        Fraction first_elem = elements[i][global];
                        bool responce = true; //можно ли вообще выразить
                        if (first_elem == 0)
                        {
                            responce = false;
                            for (int k = i + 1; k < elements.Count; k++)
                                if (elements[k][global] != 0)
                                {
                                    responce = true;
                                    first_elem = elements[k][global];
                                    Fraction temp;
                                    //смена строк
                                    for (int j = 0; j < elements[0].Count; j++)
                                    {
                                        temp = elements[i][j];
                                        elements[i][j] = elements[k][j];
                                        elements[k][j] = temp;
                                    }
                                    break;
                                }
                        }

                        //если не получилось выразить переменную и была задана начальная угловая точка
                        if ((responce == false) && (CornerDot == true))
                            throw new Exception("Проверьте правильно ли введены коэффициенты.");
                        //если не получилось выразить переменную и НЕ была задана начальная угловая точка
                        else if ((responce == false) && (CornerDot == false))
                        {
                            //то ищем в других столбцах
                            bool check = false;
                            for (int column = global + 1; column < elements[0].Count; column++)
                            {
                                for (int row = i; row < elements.Count; row++)
                                {
                                    if (elements[row][column] != 0)
                                    {
                                        check = true;
                                        first_elem = elements[row][column];
                                        Fraction temp; //вспомогательная переменная

                                        //смена строк
                                        for (int j = 0; j < elements[0].Count; j++)
                                        {
                                            //для элементов матрицы
                                            temp = elements[i][j];
                                            elements[i][j] = elements[row][j];
                                            elements[row][j] = temp;
                                        }

                                        //смена столбцов
                                        for (int k = 0; k < elements.Count; k++)
                                        {
                                            //для элементов матрицы
                                            temp = elements[k][global];
                                            elements[k][global] = elements[k][column];
                                            elements[k][column] = temp;
                                        }
                                    }

                                    if (check)
                                        break;
                                }
                                if (check)
                                    break;
                            }

                            //Такого случая возможно не может быть. Поэтому это излишне.
                            if (check == false)
                                throw new Exception("Проверьте правильно ли введены коэффициенты.");
                        }



                        for (int j = 0; j < elements[0].Count; j++)
                            elements[i][j] /= first_elem;
                    }
                    else
                    {
                        Fraction first_elem = elements[i][global];
                        for (int j = 0; j < elements[0].Count; j++)
                        {
                            elements[i][j] -= elements[global][j] * first_elem;
                        }
                    }
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            switch (step)
            {
                case 0:
                    bool Cancel = true;
                    const string message =
                            "Закрыть?";
                    const string caption = "";
                    var result = MessageBox.Show(message, caption,
                                                 MessageBoxButtons.YesNo,
                                                 MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // cancel the closure of the form.
                        Cancel = false;
                    }

                    if (!Cancel)
                        this.Close();
                    break;
                case 1:
                    // Если выбраны десятичные дроби
                    if (Radical_or_Decimal)
                    {

                        // Возвращаем данные из буфера
                        GetOutOfTheBuffer(ogr);
                        // Отрисовываем новые 
                        if (CornerDot)
                            addGridParam(ogr, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr, dataGridView3);
                    }
                    // Если выбраны обыкновенные дроби
                    else
                    {
                        // Возвращаем данные из буфера для дробей
                        GetOutOfTheBuffer(ogr_with_radicals);
                        if (CornerDot)
                            addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr_with_radicals, dataGridView3);
                    }
                    step--;
                 //   label1.Text = "Матрица коэффициентов системы ограничений равенств.";
                    break;
                case 2:
                    if (Radical_or_Decimal)
                    {
                        // Возвращаем данные из буфера
                        GetOutOfTheBuffer(ogr);
                        // Отрисовываем новые 
                        if (CornerDot)
                            addGridParam(ogr, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr, dataGridView3);
                    }
                    else
                    {
                        // Возвращаем данные из буфера для дробей
                        GetOutOfTheBuffer(ogr_with_radicals);
                        // Отрисовываем новые
                        if (CornerDot)
                            addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr_with_radicals, dataGridView3);
                    }
                    step--;
                  //  label1.Text = "Шаг 1: Прямой ход метода Гаусса.";
                    break;
                case 3:
                    if (Radical_or_Decimal)
                    {
                        // Возвращаем данные из буфера
                        GetOutOfTheBuffer(ogr);
                        // Отрисовываем новые 
                        if (CornerDot)
                            addGridParam(ogr, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr, dataGridView3);
                    }
                    else
                    {
                        // Возвращаем данные из буфера для дробей
                        GetOutOfTheBuffer(ogr_with_radicals);
                        // Отрисовываем новые
                        if (CornerDot)
                            addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);
                        else
                            addGridParam(ogr_with_radicals, dataGridView3);
                    }

                    simplex_table_was_draw = false;
                    step--;
                   // tabControl1.TabPages[0].Text = "Шаг 2: Выражение базисных переменных.";
                    label_answer.Visible = false;
                    groupBoxCornerDot.Visible = false;
                    buttonNext.Enabled = true;
                    break;
                case 4:
                    // Делаем все ячейки без зелёного выделения
                    delete_green_grids();
                    step--;
                  //  tabControl1.TabPages[0].Text = "Шаг 3: Симплекс-таблица.";
                    break;
                default:
                    // Делаем все ячейки без зелёного выделения
                    delete_green_grids();
                    //возвращение данных из буфера
                    simplextable.GetOutOfTheBufferSimplex(step);
                    // меняем обратно местами те иксы, которые меняли в прошлый раз
                    ChangeOfVisualizationVariables_without_bufferization(simplextable);
                    if (Radical_or_Decimal)
                    {
                        // отображаем то, что вернули из буфера
                        addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                    }
                    else
                    {
                        addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);
                    }
                    //выбор опорного
                    SelectionOfTheSupportElement(simplextable);
                    step--;
                 //   tabControl1.TabPages[0].Text = "Шаг" + step + ": Выбор опорного элемента";
                    label_answer.Visible = false;
                    groupBoxCornerDot.Visible = false;
                    buttonNext.Enabled = true;
                    break;
            }
        }

        private void delete_green_grids()
        {
            for (int i = 0; i < dataGridView3.Rows.Count; i++)
            {
                for (int j = 0; j < dataGridView3.Columns.Count; j++)
                {
                    dataGridView3.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
            }
        }

        /// <summary>
        /// Загрузить в буфер.
        /// </summary>
        private void BufferingTableValues(List<List<double>> ogr)
        {
            buffer_elements.Add(new List<List<double>>());
            for (int i = 0; i < ogr.Count; i++)
            {
                buffer_elements[step].Add(new List<double>());
                for (int j = 0; j < ogr[0].Count; j++)
                    buffer_elements[step][i].Add(ogr[i][j]);
            }
        }

        /// <summary>
        /// Загрузить в буфер для дробей
        /// </summary>
        private void BufferingTableValues(List<List<Fraction>> ogr)
        {
            buffer_elements_for_radicals.Add(new List<List<Fraction>>());
            for (int i = 0; i < ogr.Count; i++)
            {
                buffer_elements_for_radicals[step].Add(new List<Fraction>());
                for (int j = 0; j < ogr[0].Count; j++)
                    buffer_elements_for_radicals[step][i].Add(ogr[i][j]);
            }
        }

        /// <summary>
        /// Выгрузить из буфера
        /// </summary>
        private void GetOutOfTheBuffer(List<List<double>> ogr)
        {
            for (int i = 0; i < buffer_elements[step - 1].Count; i++)
                for (int j = 0; j < buffer_elements[step - 1][0].Count; j++)
                    ogr[i][j] = buffer_elements[step - 1][i][j];
            buffer_elements.RemoveAt(step - 1);
        }

        /// <summary>
        /// Выгрузить из буфера для дробей
        /// </summary>
        private void GetOutOfTheBuffer(List<List<Fraction>> ogr)
        {
            for (int i = 0; i < buffer_elements_for_radicals[step - 1].Count; i++)
                for (int j = 0; j < buffer_elements_for_radicals[step - 1][0].Count; j++)
                    ogr[i][j] = buffer_elements_for_radicals[step - 1][i][j];
            buffer_elements_for_radicals.RemoveAt(step - 1);
        }

        /// <summary>
        /// Выражение базисных переменных + обратный ход Гаусса
        /// </summary>
        public static void HoistingMatrix(List<List<double>> elements, int number)
        {
            for (int global = 1; global < number; global++)
            {
                for (int i = global - 1; i >= 0; i--)
                {
                    double first_elem = elements[i][global];
                    for (int j = global; j < elements[0].Count; j++)
                    {
                        elements[i][j] -= elements[global][j] * first_elem;
                    }
                }
            }
        }

        /// <summary>
        /// Выражение базисных переменных + обратный ход Гаусса для дробей
        /// </summary>
        public static void HoistingMatrix(List<List<Fraction>> elements, int number)
        {
            for (int global = 1; global < number; global++)
            {
                for (int i = global - 1; i >= 0; i--)
                {
                    Fraction first_elem = elements[i][global];
                    for (int j = global; j < elements[0].Count; j++)
                    {
                        elements[i][j] -= elements[global][j] * first_elem;
                    }
                }
            }
        }

        private void StepByStep_FormClosing(object sender, FormClosingEventArgs e)
        {
            simplex_table_was_draw = false;
            corner_dot_was_added = false;
        }
    }
}
