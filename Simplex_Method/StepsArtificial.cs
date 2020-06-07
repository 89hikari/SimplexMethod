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
    public partial class StepsArtificial : Form
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
        /// Текущий шаг.
        /// </summary>
        public static int step = 0;
        /// <summary>
        /// Счётчик шагов для основного симплекс-метода
        /// </summary>
        int step_1;
        /// <summary>
        /// Ищем минимум или максимум. 0 - выбран максимум, 1 - выбран минимум.
        /// </summary>
        int MinMax;
        /// <summary>
        /// Количество базисных переменных.
        /// </summary>
        int number_of_basix;
        /// <summary>
        /// Количество свободных переменных.
        /// </summary>
        int number_of_free_variables;
        /// <summary>
        /// Симплекс-таблица для искусственного базиса.
        /// </summary>
        SimplexTable simplextable;
        /// <summary>
        /// Симплекс-таблица для обычных проходок.
        /// </summary>
        SimplexTable simplextable1;
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
        /// Вспомогательный массив для отображения переменных
        /// </summary>
        List<int> basix_variable_visualization = new List<int>();
        /// <summary>
        /// Симплекс таблица была уже нарисована.
        /// </summary>
        bool simplex_table_was_draw = false;
        /// <summary>
        /// Угловая точка соответствующая решению была уже нарисована.
        /// </summary>
        bool corner_dot_was_added = false;
        /// <summary>
        /// Тип шага. True - обычный, false - холостой(целевая функция искуственного базиса вышла в 0).
        /// </summary>
        List<bool> type_of_step = new List<bool>();
        /// <summary>
        /// Искуственный базис вышел в нуль
        /// </summary>
        bool ArtificalBasixGoToNull = false;

        public StepsArtificial(List<List<double>> ogr, List<List<double>> cel_function, int rang, List<int> variable_visualization, int MinMax, bool decimal_or_radical_drob)
        {
            InitializeComponent();

            // переносим данные ограничений и целевой функциии
            this.ogr = ogr;
            this.cel_function = cel_function;

            // количество базисных переменных
            this.number_of_basix = rang;
            // количество свободных переменных
            this.number_of_free_variables = cel_function[0].Count - number_of_basix;

            // массив для визуализации
            this.variable_visualization = variable_visualization;

            // на мин или макс
            this.MinMax = MinMax;

            // какой режим дробей выбран
            this.Radical_or_Decimal = decimal_or_radical_drob;

            // шаг искуственного базиса
            step = 1;
            // шаг симплекс метода
            step_1 = 0;
            // была ли отрисована симплекс таблица
            simplex_table_was_draw = false;

            addGridParam(ogr, dataGridView3);

            //Процесс выполнения.
            Implementation();
        }

        public StepsArtificial(List<List<Fraction>> ogr_with_radicals, List<List<Fraction>> cel_function_with_radicals, int rang, List<int> variable_visualization, int MinMax, bool decimal_or_radical_drob)
        {
            InitializeComponent();

            // переносим данные ограничений и целевой функциии
            this.ogr_with_radicals = ogr_with_radicals;
            this.cel_function_with_radicals = cel_function_with_radicals;

            // количество базисных переменных
            this.number_of_basix = rang;
            // количество свободных переменных
            this.number_of_free_variables = cel_function_with_radicals[0].Count - number_of_basix;

            // массив для визуализации
            this.variable_visualization = variable_visualization;

            // на мин или макс
            this.MinMax = MinMax;

            // какой режим дробей выбран
            this.Radical_or_Decimal = decimal_or_radical_drob;

            // шаг искуственного базиса
            step = 1;
            // шаг симплекс метода
            step_1 = 0;
            // была ли отрисована симплекс таблица
            simplex_table_was_draw = false;

            addGridParam(ogr_with_radicals, dataGridView3);

            //Процесс выполнения.
            Implementation();
        }

        /// <summary>
        /// Выполнение.
        /// </summary>
        private void Implementation()
        {
            if (Radical_or_Decimal)
            {
                //создаём сиплекс-таблицу
                simplextable = new SimplexTable(number_of_basix, number_of_free_variables, ogr, cel_function, false, Radical_or_Decimal);
                //отрисовываем симплекс таблицу
                simplextable.DrawSimplexTable(ogr, dataGridView3);
            }
            else
            {
                //создаём сиплекс-таблицу
                simplextable = new SimplexTable(number_of_basix, number_of_free_variables, ogr_with_radicals, cel_function_with_radicals, false, Radical_or_Decimal);
                //отрисовываем симплекс таблицу
                simplextable.DrawSimplexTable(ogr_with_radicals, dataGridView3);
            }

            if (simplextable.ResponseCheck() == 1)
            {
                label1.Text = "Холостой шаг: Метод искусственного базиса. Выбор опорного элемента.";
             //   tabControl1.TabPages[0].Text = "Холостой шаг: Метод искусственного базиса. Выбор опорного элемента.";
                //холостой шаг
                //simplextable.IdleStep();
            }
            else
            {
                label1.Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
             //   tabControl1.TabPages[0].Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                //выбор опорного
                simplextable.SelectionOfTheSupportElement(dataGridView3);
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
            non_sort_for_columns();
            if (corner_dot_was_added == false)
                Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
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
            non_sort_for_columns();
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
            non_sort_for_columns();
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
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
        }

        /// <summary>
        /// Добавляем данные в ячейки и отрисовываем их для дробей - из List - двумерного списка по списку визуализации
        /// </summary>
        public void addGridParam_for_simplex_elements(List<List<Fraction>> N, DataGridView Grid, List<int> variable_visualization, List<int> basix_variable_visualization)

        {
            dataGridView3.Columns.Clear();
            while (N[0].Count > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{variable_visualization[Grid.ColumnCount]}", $"x{variable_visualization[Grid.ColumnCount]}");
            }
            for (int i = 0; i < N.Count; i++)
            {
                Grid.Rows.Add();
                Grid.Rows[i].HeaderCell.Value = "x" + basix_variable_visualization[i];
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j].Reduction();
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
            Grid.Rows[Grid.RowCount - 1].HeaderCell.Value = "F";
        }

        /// <summary>
        /// Добавляем данные в ячейки и отрисовываем их для обыкновенных - из List - двумерного списка по списку визуализации
        /// </summary>
        public void addGridParam_for_simplex_elements(List<List<double>> N, DataGridView Grid, List<int> variable_visualization, List<int> basix_variable_visualization)

        {
            dataGridView3.Columns.Clear();
            while (N[0].Count > Grid.ColumnCount)
            {
                Grid.Columns.Add($"x{variable_visualization[Grid.ColumnCount]}", $"x{variable_visualization[Grid.ColumnCount]}");
            }
            for (int i = 0; i < N.Count; i++)
            {
                Grid.Rows.Add();
                Grid.Rows[i].HeaderCell.Value = "x" + basix_variable_visualization[i];
                for (int j = 0; j < N[i].Count; j++)
                {
                    Grid.Rows[i].Cells[j].Value = N[i][j];
                }
            }
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
            Grid.Rows[Grid.RowCount - 1].HeaderCell.Value = "F";
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
            non_sort_for_columns();
            Grid.Columns[Grid.ColumnCount - 1].HeaderText = "d";
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

        private void buttonNext_Click(object sender, EventArgs e)
        {
            //для запоминания типа шага
            if (simplextable.ResponseCheck() == 1)
                type_of_step.Add(false);
            else
                type_of_step.Add(true);

            try
            {

                //выбран ли опорный элемент
                simplextable.ButtonPressedOrNot(dataGridView3);
                //смена местами переменной + буферизация
                simplextable.ChangeOfVisualizationVariables(dataGridView3);
                // Буферизируем визуализацию колонок
                simplextable.BufferingVariablevisualizationForDeleteColumns(dataGridView3);
                // Буферизируем визуализацию строк
                simplextable.BufferingVariablevisualizationForDeleteRows(dataGridView3);
                //буферизация элементов таблицы
                simplextable.BufferingSimplexTableValues_ForArtifical();
                //удаляем подсвеченные
                simplextable.delete_green_grids(dataGridView3);
                //вычисление согласно выбранному опорному элементу
                simplextable.CalculateSimplexTable(simplextable.row_of_the_support_element, simplextable.column_of_the_support_element);

                // обновление данных ячеек таблицы
                if (Radical_or_Decimal)
                    addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                else
                    addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);

                // Если искусственный базис ещё не пришёл в ноль
                if (!ArtificalBasixGoToNull)
                {
                    // создаём новую симплекс таблицу для дальнейшей работы
                    if (Radical_or_Decimal)
                        simplextable1 = new SimplexTable(simplextable);
                    else
                        simplextable1 = new SimplexTable(simplextable);
                }

                if (!ArtificalBasixGoToNull)
                    switch (simplextable1.ArtificialResponseCheck(variable_visualization, dataGridView3))
                    {
                        case 1:
                            //MessageBox.Show("Таблица пришла в ноль!");

                            step++;

                            if (!ArtificalBasixGoToNull)
                            {


                                // Удаляем столбцы с искусственными переменными.
                                simplextable.DeleteArtificalBasix(dataGridView3);

                                // Считываем ограничения заного
                                if (Radical_or_Decimal)
                                {
                                    ogr = new List<List<double>>();
                                    read_grids(dataGridView3, ogr);
                                    simplextable.simplex_elements = ogr;
                                    // удаляем строку с нулями из элементов симплекс таблицы
                                    simplextable.simplex_elements.RemoveAt(simplextable.simplex_elements.Count - 1);
                                    // удаляем последнюю строку с нулями из датаГрид
                                    dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 1);
                                    // отображаем новые данные
                                    addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                                }

                                else
                                {
                                    ogr_with_radicals = new List<List<Fraction>>();
                                    read_grids(dataGridView3, ogr_with_radicals);
                                    simplextable.simplex_elements_with_radicals = ogr_with_radicals;
                                    // удаляем строку с нулями из элементов симплекс таблицы
                                    simplextable.simplex_elements_with_radicals.RemoveAt(simplextable.simplex_elements_with_radicals.Count - 1);
                                    // удаляем последнюю строку с нулями из датаГрид
                                    dataGridView3.Rows.RemoveAt(dataGridView3.Rows.Count - 1);
                                    // отображаем новые данные
                                    addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);
                                }

                                // Высчитываем последнюю строку
                                simplextable.CalculateCelRow(dataGridView3);
                            }

                            ArtificalBasixGoToNull = true;

                            switch (simplextable.ResponseCheck())
                            {
                                case 0:
                                    //выбор опорного
                                    simplextable.SelectionOfTheSupportElement(dataGridView3);
                                    label1.Text = "Шаг " + step + ": Симплекс-таблица.";
                              //      tabControl1.TabPages[0].Text = "Шаг " + step + ": Симплекс-таблица.";
                                    step_1++;
                                    break;
                                case 1:
                                    // Подставляем ответ
                                    label1.Text = "Ответ готов!";
                                  //  tabControl1.TabPages[0].Text = "Ответ готов!";
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
                                        addGridParam(simplextable.ResponseDot(dataGridView3), dataGridViewCornerDot);
                                    }
                                    groupBoxCornerDot.Visible = true;
                                    label_answer.Visible = true;
                                    buttonNext.Enabled = false;
                                    step_1++;
                                    break;
                                case -1:
                                    label1.Text = "Линейная форма не ограничена сверху на множествен планов задачи.";
                                 //   tabControl1.TabPages[0].Text = "Линейная форма не ограничена сверху на множествен планов задачи.";
                                    buttonNext.Enabled = false;
                                    MessageBox.Show("Линейная форма не ограничена сверху на множествен планов задачи.", "Ответ готов!");
                                    step_1++;
                                    break;
                            }
                            break;
                        case 0:
                            if (simplextable.ResponseCheck() == 1)
                            {
                                // Если симплекс таблица решена
                                step++;
                                // Подставляем ответ
                                label1.Text = "Ответ готов!";
                             //   tabControl1.TabPages[0].Text = "Ответ готов!";
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
                                    addGridParam(simplextable.ResponseDot(dataGridView3), dataGridViewCornerDot);
                                }
                                groupBoxCornerDot.Visible = true;
                                label_answer.Visible = true;
                                buttonNext.Enabled = false;
                                step_1++;
                            }
                            else
                            {
                                step++;
                                label1.Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                              //  tabControl1.TabPages[0].Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                                //выбор опорного
                                simplextable.SelectionOfTheSupportElement(dataGridView3);
                            }
                            break;
                        case -1:
                            MessageBox.Show("Решения не имеет");
                            break;
                    }
                // Симплекс метод
                else
                {
                    switch (simplextable.ResponseCheck())
                    {
                        case 0:
                            //выбор опорного
                            simplextable.SelectionOfTheSupportElement(dataGridView3);
                            label1.Text = "Шаг " + step + ": Симплекс-таблица.";
                        //    tabControl1.TabPages[0].Text = "Шаг " + step + ": Симплекс-таблица.";
                            step_1++;
                            break;
                        case 1:
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
                                addGridParam(simplextable.ResponseDot(dataGridView3), dataGridViewCornerDot);
                            }
                            groupBoxCornerDot.Visible = true;
                            label_answer.Visible = true;
                            buttonNext.Enabled = false;
                            step_1++;
                            break;
                        case -1:
                            label1.Text = "Линейная форма не ограничена сверху на множествен планов задачи.";
                         //   tabControl1.TabPages[0].Text = "Линейная форма не ограничена сверху на множествен планов задачи.";
                            MessageBox.Show("Линейная форма не ограничена сверху на множествен планов задачи.", "Ответ готов!");
                            buttonNext.Enabled = false;
                            step_1++;
                            break;
                    }
                }
            }
            catch (Exception d)
            {
                MessageBox.Show(d.Message);
            }

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (step == 1)
            {
                bool Cancel = true;
                const string message =
                        "Предыдущего шага нет. Возврат приведёт к закрытию текущей задачи. Вы уверены?";
                const string caption = "Закрыть задачу?";
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
            }
            else
            {
                if (step_1 == 1)
                {
                    // Достаём из буфера визуализацию
                    variable_visualization = simplextable.GetOutTheBufferVariablevisualizationForDeleteColumns();

                    basix_variable_visualization = simplextable.GetOutTheBufferVariablevisualizationForDeleteRows();

                    simplextable = new SimplexTable(simplextable1);

                    if (Radical_or_Decimal)
                    {
                        addGridParam_for_simplex_elements(simplextable.buffer_simplex_elements[simplextable.buffer_simplex_elements.Count - 1], dataGridView3, variable_visualization, basix_variable_visualization);

                    }

                    else
                    {
                        addGridParam_for_simplex_elements(simplextable.buffer_simplex_elements_for_radicals[simplextable.buffer_simplex_elements_for_radicals.Count - 1], dataGridView3, variable_visualization, basix_variable_visualization);
                    }

                    // достаём из буффера симлпекс элементы
                    simplextable.GetOutOfTheBufferSimplex_ForArtifical();

                    step--;
                    step_1--;
                    label1.Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                  //  tabControl1.TabPages[0].Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                    //меняем местами переменные обратно
                    simplextable.ChangeOfVisualizationVariables_GetOutTheBuffer(dataGridView3);

                    simplextable.SelectionOfTheSupportElement(dataGridView3);

                    ArtificalBasixGoToNull = false;
                    buttonNext.Enabled = true;
                    return;
                }
                else if (step_1 > 1)
                {
                    buttonNext.Enabled = true;
                    label_answer.Visible = false;
                    groupBoxCornerDot.Visible = false;

                    //убираем кнопки
                    simplextable.delete_green_grids(dataGridView3);
                    //меняем местами переменные обратно
                    simplextable.ChangeOfVisualizationVariables_GetOutTheBuffer(dataGridView3);
                    //выводим данные из буфера
                    simplextable.GetOutOfTheBufferSimplex_ForArtifical();
                    //отрисовываем 
                    if (Radical_or_Decimal)
                        addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                    else
                        addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);

                    step_1--;
                    label1.Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                 //   tabControl1.TabPages[0].Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                    simplextable.SelectionOfTheSupportElement(dataGridView3);

                    // очищаем буфер на один шаг
                    simplextable.buffer_delete_artifical_columns.RemoveAt(simplextable.buffer_delete_artifical_columns.Count - 1);
                    simplextable.buffer_delete_artifical_rows.RemoveAt(simplextable.buffer_delete_artifical_rows.Count - 1);

                    return;
                }
                //убираем кнопки
                simplextable.delete_green_grids(dataGridView3);
                //меняем местами переменные обратно
                simplextable.ChangeOfVisualizationVariables_GetOutTheBuffer(dataGridView3);
                //выводим данные из буфера
                simplextable.GetOutOfTheBufferSimplex_ForArtifical();
                //отрисовываем 
                if (Radical_or_Decimal)
                    addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                else
                    addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);

                //simplextable.buffer_delete_artifical_columns.RemoveAt(simplextable.buffer_delete_artifical_columns.Count - 1);
                //simplextable.buffer_delete_artifical_rows.RemoveAt(simplextable.buffer_delete_artifical_rows.Count - 1);

                step--;
                label1.Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента";
              //  tabControl1.TabPages[0].Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                simplextable.SelectionOfTheSupportElement(dataGridView3);
            }
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

        private void StepsArtificial_FormClosed(object sender, FormClosedEventArgs e)
        {
            corner_dot_was_added = false;
            simplex_table_was_draw = false;
            ArtificalBasixGoToNull = false;
        }

        private void StepsArtificial_Load(object sender, EventArgs e)
        {
            var outPutDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var filePath = System.IO.Path.Combine(outPutDirectory, "data\\Help\\StepByStepForArtificalHelp.rtf");

            string file_path = new Uri(filePath).LocalPath; // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug
                                                            // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug\data\Help\MainPageHelp.rtf

    //        helpProvider1.HelpNamespace = file_path;
        }
    }
}
