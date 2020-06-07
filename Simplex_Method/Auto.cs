using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Simplex_Method
{
    public partial class Auto : Form
    {
        /// <summary>
        /// Матрица коэффициентов системы ограничений-равенств.
        /// </summary>
        List<List<double>> ogr = new List<List<double>>();
        /// <summary>
        /// Матрица коэффициентов системы ограничений-равенств для обыкновенных дробей
        /// </summary>
        List<List<Fraction>> ogr_with_radicals = new List<List<Fraction>>();
        /// <summary>
        /// Целевая функция.
        /// </summary>
        List<List<double>> cel_function = new List<List<double>>();
        /// <summary>
        /// Целевая функция для обыкновенных дробей
        /// </summary>
        List<List<Fraction>> cel_function_with_radicals = new List<List<Fraction>>();
        /// <summary>
        /// Количество базисных переменных.
        /// </summary>
        int number_of_basix;
        /// <summary>
        /// Количество свободных переменных.
        /// </summary>
        int number_of_free_variables;
        /// <summary>
        /// Задана ли угловая точка(bool).
        /// </summary>
        bool CornerDot;
        /// <summary>
        /// Массив запоминающий индексы переменных для отображения
        /// </summary>
        List<int> variable_visualization = new List<int>();
        /// <summary>
        /// Ищем минимум или максимум. 0 - выбран максимум, 1 - выбран минимум.
        /// </summary>
        int MinMax;
        /// <summary>
        /// Симплекс-таблица.
        /// </summary>
        Simplex simplextable;
        ///<summary>
        ///Корневая точка
        ///</summary>
        List<List<double>> corner_dot = new List<List<double>>();
        ///<summary>
        ///Корневая точка для дробей
        ///</summary>
        List<List<Fraction>> corner_dot_with_radicals = new List<List<Fraction>>();
        /// <summary>
        /// Определитель - какой режим работы с дробями выбран. false - обыкновенные. true - десятичные
        /// </summary>
        bool Radical_or_Decimal;
        /// <summary>
        /// Была ли добавлена угловая точка
        /// </summary>
        private bool corner_dot_was_added;

        public Auto(List<List<double>> cel_function, List<List<double>> ogr, int MinMax, bool CornerDot, int number_of_basix, bool decimal_or_radical_drob, List<int> variable_visualization)
        {
            InitializeComponent();

            // Переносим данные целевой функции и ограничений.
            this.cel_function = cel_function;
            this.ogr = ogr;
            // Решаем задачу на минимум или максимум.
            this.MinMax = MinMax;
            // Была ли введена угловая точка.  true - да. false - нет.
            this.CornerDot = CornerDot;
            // Количество базисных переменных.
            this.number_of_basix = number_of_basix;
            // Количество свободных переменных.
            this.number_of_free_variables = cel_function[0].Count - number_of_basix;
            // Какая работа с дробями была выбрана. false - обыкновенные. true - десятичные.
            this.Radical_or_Decimal = decimal_or_radical_drob;
            // Массив для визуализации переменных
            this.variable_visualization = variable_visualization;

            addGridParam(ogr, dataGridView3, variable_visualization);

            //Процесс выполнения.
            Implementation();
        }

        public Auto(List<List<Fraction>> cel_function_with_radicals, List<List<Fraction>> ogr_with_radicals, int MinMax, bool CornerDot, int number_of_basix, bool decimal_or_radical_drob, List<int> variable_visualization)
        {
            InitializeComponent();

            // Переносим данные целевой функции и ограничений.
            this.cel_function_with_radicals = cel_function_with_radicals;
            this.ogr_with_radicals = ogr_with_radicals;
            // Решаем задачу на минимум или максимум.
            this.MinMax = MinMax;
            // Была ли введена угловая точка.  true - да. false - нет.
            this.CornerDot = CornerDot;
            // Количество базисных переменных.
            this.number_of_basix = number_of_basix;
            // Количество свободных переменных.
            this.number_of_free_variables = cel_function_with_radicals[0].Count - number_of_basix;
            // Какая работа с дробями была выбрана. false - обыкновенные. true - десятичные.
            this.Radical_or_Decimal = decimal_or_radical_drob;
            // Массив для визуализации переменных
            this.variable_visualization = variable_visualization;

            addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);

            //Процесс выполнения.
            Implementation();

        }

        public Auto(List<List<double>> cel_function, List<List<double>> ogr, int minMax, int rang, bool decimal_or_radical_drob)
        {
            InitializeComponent();
            // Переносим заполненный массив с главного окна
            this.cel_function = cel_function;
            // Переносим заполненный массив с главного окна
            this.ogr = ogr;
            // Задача на минимум или максимум(0 - максимум, 1 - минимум)
            this.MinMax = minMax;
            // Количество базисных переменных.
            this.number_of_basix = rang;
            // Вычисляем количество свободных переменных
            number_of_free_variables = cel_function[0].Count - number_of_basix;
            // Какие дроби выбраны. false - обыкновенные. true - десятичные
            this.Radical_or_Decimal = decimal_or_radical_drob;

            // добавляем в ячейки, то есть отрисовываем то, что есть
           // tabControl1.TabPages[0].Text = "Матрица коэффициентов системы ограничения равенств.";
            addGridParam(ogr, dataGridView3);

            //Процесс выполнения.
            Implementation();
        }

        public Auto(List<List<Fraction>> cel_function_with_radicals, List<List<Fraction>> ogr_with_radicals, int minMax, int rang, bool decimal_or_radical_drob)
        {
            InitializeComponent();
            // Переносим заполненный массив с главного окна
            this.cel_function_with_radicals = cel_function_with_radicals;
            this.ogr_with_radicals = ogr_with_radicals;

            // Задача на минимум или максимум(0 - максимум, 1 - минимум)
            this.MinMax = minMax;
            // Количество базисных переменных.
            this.number_of_basix = rang;
            // Вычисляем количество свободных переменных
            number_of_free_variables = cel_function_with_radicals[0].Count - number_of_basix;
            // Какие дроби выбраны. false - обыкновенные. true - десятичные
            this.Radical_or_Decimal = decimal_or_radical_drob;

            // добавляем в ячейки, то есть отрисовываем то, что есть
          //  tabControl1.TabPages[0].Text = "Матрица коэффициентов системы ограничения равенств.";
            addGridParam(ogr_with_radicals, dataGridView3);

            //Процесс выполнения.
            Implementation();
        }

        private void Implementation()
        {

            if (Radical_or_Decimal)
            {
                //Прямой ход метода Гаусса для приведения к треугольному виду.
                StepsSolve.Gauss(ogr, CornerDot);
                //Выражение базисных переменных и приведение к диагональному виду.
                StepsSolve.HoistingMatrix(ogr, number_of_basix);
                // Обновляем данные на таблице 
                //Обновление визуализации переменных.
                if (CornerDot)
                    addGridParam(ogr, dataGridView3, variable_visualization);
                else
                    addGridParam(ogr, dataGridView3);
                //создаём сиплекс-таблицу
                simplextable = new Simplex(number_of_basix, number_of_free_variables, ogr, cel_function, true, Radical_or_Decimal);
                DrawSimplexTable(ogr);
            }
            else
            {
                //Прямой ход метода Гаусса для приведения к треугольному виду.
                StepsSolve.Gauss(ogr_with_radicals, CornerDot);
                //Выражение базисных переменных и приведение к диагональному виду.
                StepsSolve.HoistingMatrix(ogr_with_radicals, number_of_basix);
                //Обновление визуализации переменных.
                if (CornerDot)
                    addGridParam(ogr_with_radicals, dataGridView3, variable_visualization);
                else
                    addGridParam(ogr_with_radicals, dataGridView3);
                //создаём сиплекс-таблицу
                simplextable = new Simplex(number_of_basix, number_of_free_variables, ogr_with_radicals, cel_function_with_radicals, true, Radical_or_Decimal);
                DrawSimplexTable(ogr_with_radicals);
            }

            int responce;
            int step = 1;

            while (true)
            {
                if ((responce = simplextable.ResponseCheck()) == 0)
                {
                    //выбор любого опорного
                    simplextable.SelectionRandomSupportElement();
                    //меняем местами переменные 
                    simplextable.ChangeOfVisualWithoutBuffer(dataGridView3);
                    // высчитывание по опорному
                    simplextable.CalculateSimplexTable(simplextable.row_of_the_support_element, simplextable.column_of_the_support_element);
                    // обновление данных симплекс таблицы
                    if (Radical_or_Decimal)
                        addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                    else
                        addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);
                    step++;
                }
                else if (responce == 1)
                {
                   // tabControl1.TabPages[0].Text = "Ответ готов!";
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
                    break;
                }
                else if (responce == -1)
                {
                    MessageBox.Show("Задача не разрешима!");
                  //  tabControl1.TabPages[0].Text = "Задача не разрешима!";
                    break;
                }
            }


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
            if (corner_dot_was_added == false)
                Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
        }

        /// <summary>
        /// высчитывание угловой точки
        /// </summary>
        /// <returns></returns>
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
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
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
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
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
                dataGridView3.Rows[simplextable.number_of_permutations].HeaderCell.Value = "F";
                for (int j = simplextable.number_of_permutations; j < simplextable.ogr_with_radicals[0].Count - 1; j++)
                {
                    //логика
                    a = new Fraction(0);
                    for (int i = 0; i < simplextable.ogr_with_radicals.Count; i++)
                    {
                        a += simplextable.ogr_with_radicals[i][j] * cel_function_with_radicals[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= simplextable.cel_function_with_radicals[0][Int32.Parse(dataGridView3.Columns[column_index].HeaderCell.Value.ToString().Replace("d", column_index.ToString()).Trim('x')) - 1];

                    ////отображение

                    dataGridView3.Rows[simplextable.number_of_permutations].Cells[column_index].Value = a * (-1);
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = new Fraction(0);
                for (int i = 0; i < simplextable.ogr_with_radicals.Count; i++)
                    a += simplextable.ogr_with_radicals[i][simplextable.ogr_with_radicals[0].Count - 1] * cel_function_with_radicals[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1] * (-1);

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
                dataGridView3.Rows[simplextable.number_of_permutations].HeaderCell.Value = "F";
                for (int j = simplextable.number_of_permutations; j < simplextable.ogr[0].Count - 1; j++)
                {
                    //логика
                    a = 0;
                    for (int i = 0; i < simplextable.ogr.Count; i++)
                    {
                        a += simplextable.ogr[i][j] * cel_function[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= simplextable.cel_function[0][Int32.Parse(dataGridView3.Columns[column_index].HeaderCell.Value.ToString().Replace("d", column_index.ToString()).Trim('x')) - 1]; // функция подставления в коэфф в целевую (возможно Replace не нужно)
                    a = a * (-1);
                    ////отображение
                    dataGridView3.Rows[simplextable.number_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = 0;
                for (int i = 0; i < simplextable.ogr.Count; i++)
                    a += simplextable.ogr[i][simplextable.ogr[0].Count - 1] * cel_function[0][Int32.Parse(dataGridView3.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1] * (-1);

                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[dataGridView3.Columns.Count - 1].Value = a * (-1);
                a = a * (-1);
                dataGridView3.Rows[dataGridView3.Rows.Count - 1].Cells[dataGridView3.Columns.Count - 1].Value = a * (-1);

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
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
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
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
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
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
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
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
        }

        private void AutoMode_Load(object sender, EventArgs e)
        {
            var outPutDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var filePath = System.IO.Path.Combine(outPutDirectory, "data\\Help\\AutoModeHelp.rtf");

            string file_path = new Uri(filePath).LocalPath; // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug
                                                            // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug\data\Help\MainPageHelp.rtf

  //          helpProvider1.HelpNamespace = file_path;
        }
    }
}
