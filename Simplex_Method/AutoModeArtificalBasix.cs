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
    public partial class AutoModeArtificalBasix : Form
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

        // для работы с десятичными дробями
        public AutoModeArtificalBasix(List<List<double>> ogr, List<List<double>> cel_function, int rang, List<int> variable_visualization, int MinMax, bool decimal_or_radical_drob)
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

        // для работы с обыкновенными дробями
        public AutoModeArtificalBasix(List<List<Fraction>> ogr_with_radicals, List<List<Fraction>> cel_function_with_radicals, int rang, List<int> variable_visualization, int MinMax, bool decimal_or_radical_drob)
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
                tabControl1.TabPages[0].Text = "Холостой шаг: Метод искусственного базиса. Выбор опорного элемента.";
                //холостой шаг
                //simplextable.IdleStep();
            }
            else
            {
                tabControl1.TabPages[0].Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                //выбор опорного
                simplextable.SelectionRandomSupportElement();
            }

            // Делаем пока таблица искус базиса не вышла в ноль
            while (ArtificalBasixGoToNull == false)
            {

                //смена местами переменной + буферизация
                simplextable.ChangeOfVisualizationVariables(dataGridView3);
                //вычисление согласно выбранному опорному элементу
                simplextable.CalculateSimplexTable(simplextable.row_of_the_support_element, simplextable.column_of_the_support_element);

                // обновление данных ячеек таблицы
                if (Radical_or_Decimal)
                    addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                else
                    addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);

                switch (simplextable.ArtificialResponseCheck(variable_visualization, dataGridView3))
                {
                    case 1:
                        //MessageBox.Show("Таблица пришла в ноль!");

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

                        // Если ответ готов сразу же
                        switch (simplextable.ResponseCheck())
                        {
                            case 0:
                                //выбор опорного
                                simplextable.SelectionRandomSupportElement();
                                break;
                            case 1:
                                // Подставляем ответ
                                tabControl1.TabPages[0].Text = "Ответ готов!";
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

                                break;
                            case -1:
                                tabControl1.TabPages[0].Text = "Линейная форма не ограничена сверху на множествен планов задачи.";

                                MessageBox.Show("Линейная форма не ограничена сверху на множествен планов задачи.", "Ответ готов!");

                                break;
                        }



                        break;

                    case 0:
                        if (simplextable.ResponseCheck() == 1)
                        {
                            // Если симплекс таблица решена

                            // Подставляем ответ
                            tabControl1.TabPages[0].Text = "Ответ готов!";
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

                        }
                        else
                        {
                            tabControl1.TabPages[0].Text = "Шаг " + step + ": Метод искусственного базиса. Выбор опорного элемента.";
                            //выбор опорного
                            simplextable.SelectionRandomSupportElement();
                        }
                        break;
                    case -1:
                        MessageBox.Show("Решения не имеет");
                        break;
                }
            }

            // Переходим к симплекс таблице, если она не имеет решения или пока ещё не решена, и решаем её до тех пор
            while (simplextable.ResponseCheck() != 1 && simplextable.ResponseCheck() != -1)
            {
                //смена местами переменной + буферизация
                simplextable.ChangeOfVisualizationVariables(dataGridView3);
                //вычисление согласно выбранному опорному элементу
                simplextable.CalculateSimplexTable(simplextable.row_of_the_support_element, simplextable.column_of_the_support_element);

                // обновление данных ячеек таблицы
                if (Radical_or_Decimal)
                    addGridParam_for_simplex_elements(simplextable.simplex_elements, dataGridView3);
                else
                    addGridParam_for_simplex_elements(simplextable.simplex_elements_with_radicals, dataGridView3);

                switch (simplextable.ResponseCheck())
                {
                    case 0:
                        //выбор опорного
                        simplextable.SelectionRandomSupportElement();
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

                        break;
                    case -1:
                        tabControl1.TabPages[0].Text = "Линейная форма не ограничена сверху на множествен планов задачи.";
                        MessageBox.Show("Линейная форма не ограничена сверху на множествен планов задачи.", "Ответ готов!");
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

        private void AutoModeArtificalBasix_Load(object sender, EventArgs e)
        {
            var outPutDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);
            var filePath = System.IO.Path.Combine(outPutDirectory, "data\\Help\\AutoModeHelp.rtf");

            string file_path = new Uri(filePath).LocalPath; // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug
                                                            // C:\Users\Kis\source\repos\_Legkov\SymplexMethodCsharp\bin\Debug\data\Help\MainPageHelp.rtf

     //       helpProvider1.HelpNamespace = file_path;
        }
    }
}
