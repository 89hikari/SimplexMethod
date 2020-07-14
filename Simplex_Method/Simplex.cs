using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace Simplex_Method
{
    public partial class Simplex
    {
        /// <summary>
        /// Список коэфф-тов системы ограничений-равенств.
        /// </summary>
        public List<List<double>> ogr = new List<List<double>>();
        /// <summary>
        /// Список чисел в  симплекс таблице для чисел с плав. точкой
        /// </summary>
        public List<List<double>> simplex_elements = new List<List<double>>();
        /// <summary>
        /// Буфер для коэффициентов симплекс-таблицы.
        /// </summary>
        public List<List<List<double>>> buffer_simplex_elements = new List<List<List<double>>>();
        /// <summary>
        /// Коэффициенты целевой функции.
        /// </summary>
        public List<List<double>> cel_function;
        /// <summary>
        /// Матрица коэффициентов системы ограничений-равенств для дробей
        /// </summary>
        public List<List<Fractions>> ogr_with_radicals = new List<List<Fractions>>();
        /// <summary>
        /// Коэффициенты симплекс-таблицы для дробей
        /// </summary>
        public List<List<Fractions>> simplex_elements_with_radicals = new List<List<Fractions>>();
        /// <summary>
        /// Буфер для коэффициентов симплекс-таблицы для дробей
        /// </summary>
        public List<List<List<Fractions>>> buffer_simplex_elements_for_radicals = new List<List<List<Fractions>>>();
        /// <summary>
        /// Коэффициенты целевой функции для дробей
        /// </summary>
        public List<List<Fractions>> cel_function_with_radicals;
        /// <summary>
        /// Симплекс-метод(true) или метод искусственного базиса(false).
        /// </summary>
        public bool simplex_or_artificial;
        /// <summary>
        /// Опорный элемент для десятич.
        /// </summary>
        double supporting_member;
        /// <summary>
        /// Опорный элемент для дроби
        /// </summary>
        Fractions supporting_member_for_drob;
        /// <summary>
        /// Какие дроби выбраны
        /// </summary>
        bool drob_or_desyat;
        /// <summary>
        /// Разрешающая строка.
        /// </summary>
        public int row_of_the_support_element;
        /// <summary>
        /// Разрешающий столбец.
        /// </summary>
        public int column_of_the_support_element;
        /// <summary>
        /// Координаты опорного элемента
        /// </summary>
        List<List<int>> the_coordinates_of_the_support_element;
        /// <summary>
        /// Буфер для сохранения расположения переменных в симплекс таблице
        /// </summary>
        List<List<int>> buffer_variable_visualization = new List<List<int>>();
        /// <summary>
        /// Буфер для удалённых колонок
        /// </summary>
        public List<List<int>> buffer_delete_artifical_columns = new List<List<int>>();
        /// <summary>
        /// Буфер для строк искуственного базиса
        /// </summary>
        public List<List<int>> buffer_delete_artifical_rows = new List<List<int>>();
        private Simplex simplextable;
        public int count_of_permutations; //кол-во перестановок
        public int count_of_free_variables; //кол-во переменных

        public Simplex(int number_of_permutations, int number_of_free_variables, List<List<double>> ogr, List<List<double>> cel_function, bool simplex_or_artificial, bool radical_or_decimal)
        {
            this.count_of_permutations = number_of_permutations;
            this.count_of_free_variables = number_of_free_variables;

            this.ogr = new List<List<double>>();
            Copy_List(ogr, this.ogr);

            this.cel_function = new List<List<double>>();
            Copy_List(cel_function, this.cel_function);

            this.simplex_or_artificial = simplex_or_artificial;
            this.drob_or_desyat = radical_or_decimal;
        }

        public Simplex(int number_of_basix_permutations, int number_of_free_variables, List<List<Fractions>> ogr_with_radicals, List<List<Fractions>> cel_function_with_radicals, bool simplex_or_artificial, bool radical_or_decimal)
        {
            this.count_of_permutations = number_of_basix_permutations;
            this.count_of_free_variables = number_of_free_variables;

            this.ogr_with_radicals = new List<List<Fractions>>();
            Copy_List(ogr_with_radicals, this.ogr_with_radicals);

            this.cel_function_with_radicals = new List<List<Fractions>>();
            Copy_List(cel_function_with_radicals, this.cel_function_with_radicals);

            this.simplex_or_artificial = simplex_or_artificial;
            this.drob_or_desyat = radical_or_decimal;
        }

        public Simplex(Simplex simplextable)
        {
            this.buffer_simplex_elements = simplextable.buffer_simplex_elements;
            this.buffer_simplex_elements_for_radicals = simplextable.buffer_simplex_elements_for_radicals;
            this.buffer_variable_visualization = simplextable.buffer_variable_visualization;

            this.cel_function = simplextable.cel_function;
            this.cel_function_with_radicals = simplextable.cel_function_with_radicals;
            this.ogr = simplextable.ogr;
            this.ogr_with_radicals = simplextable.ogr_with_radicals;

            this.column_of_the_support_element = simplextable.column_of_the_support_element;
            this.row_of_the_support_element = simplextable.row_of_the_support_element;

            this.simplex_elements = simplextable.simplex_elements;
            this.simplex_elements_with_radicals = simplextable.simplex_elements_with_radicals;

            this.simplex_or_artificial = simplextable.simplex_or_artificial;

            this.supporting_member = simplextable.supporting_member;

            this.the_coordinates_of_the_support_element = simplextable.the_coordinates_of_the_support_element;

            this.count_of_permutations = simplextable.count_of_permutations;
            this.count_of_free_variables = simplextable.count_of_free_variables;

            this.drob_or_desyat = simplextable.drob_or_desyat;
        }

        /// <summary>
        /// Метод копирующий двумерные List. Перед копированием, нужно инициализировать оба List
        /// </summary>
        public void Copy_List(List<List<double>> original, List<List<double>> copied)
        {
            for (int i = 0; i < original.Count; i++)
            {
                copied.Add(new List<double>());
                for (int j = 0; j < original[0].Count; j++)
                {
                    copied[i].Add(original[i][j]);
                }
            }
        }

        /// <summary>
        /// Метод копирующий двумерные List для Дробей. Перед копированием, нужно инициализировать оба List
        /// </summary>
        public void Copy_List(List<List<Fractions>> original, List<List<Fractions>> copied)
        {
            for (int i = 0; i < original.Count; i++)
            {
                copied.Add(new List<Fractions>());
                for (int j = 0; j < original[0].Count; j++)
                {
                    copied[i].Add(original[i][j]);
                }
            }
        }

        /// <summary>
        /// Проверка на решение. 0 - продолжаем искать. 1 - ответ готов. -1 - задача не разрешима.
        /// </summary>
        public int ResponseCheck()
        {
            // Если десятичные
            if (drob_or_desyat == true)
            {
                // неразрешимо?
                bool insoluble = false;
                for (int j = 0; j < simplex_elements[0].Count - 1; j++)
                {
                    if (simplex_elements[simplex_elements.Count - 1][j] < 0) // Проходимся по последней строке. Если есть отрицательне, то...
                    {
                        // Предполагаем, что она неразрешима, и смотрим дальше
                        insoluble = true;
                        for (int i = 0; i < simplex_elements.Count - 1; i++) // Проходимся по всем элеметам, кроме последней(f) строки и (d)столбца
                        {
                            if (simplex_elements[i][j] > 0)
                            {
                                // Если найден хоть один положительный элемент, считаем симплекс таблицу разрешимой
                                insoluble = false;
                                break;
                            }
                            // Если все элементы отрицательные - оставляем её неразрешимой
                        }
                    }
                    //неразрешима
                    if (insoluble)
                        return -1;
                }

                // если нет отрицательных элементов в последней строке
                insoluble = true;
                //проверяем
                for (int j = 0; j < simplex_elements[0].Count - 1; j++) // Проходимся по последней (f) строке
                    if (simplex_elements[simplex_elements.Count - 1][j] < 0)
                    {
                        //если есть отрицательный элемент, то ищем решение дальше
                        insoluble = false;
                        break;
                    }
                //есть ответ
                if (insoluble)
                    return 1;

                //продолжение поиска решения
                return 0;
            }

            // Если выбраны обыкновенные дроби
            else
            {
                //неразрешимо?
                bool insoluble = false;
                for (int j = 0; j < simplex_elements_with_radicals[0].Count - 1; j++)
                {
                    if (simplex_elements_with_radicals[simplex_elements_with_radicals.Count - 1][j] < new Fractions(0)) // Проходимся по последней строке. Если нашли отрицательный, то...
                    {
                        // Предполагаем, что она неразрешима, и смотрим дальше...
                        insoluble = true;
                        for (int i = 0; i < simplex_elements_with_radicals.Count - 1; i++) // Проходимся по всем элеметам, кроме последней(f) строки и (d)столбца
                        {
                            if (simplex_elements_with_radicals[i][j] > 0)
                            {
                                // Если найден хоть один положительный элемент, считаем симплекс таблицу разрешимой
                                insoluble = false;
                                break;
                            }
                            // Если все элементы отрицательные - оставляем её неразрешимой
                        }
                    }
                    //неразрешима
                    if (insoluble)
                        return -1;
                }

                //предполагаем, что нет отрицательных элементов в последней строке
                insoluble = true;
                //проверяем
                for (int j = 0; j < simplex_elements_with_radicals[0].Count - 1; j++) // Проходимся по последней (f) строке
                    if (simplex_elements_with_radicals[simplex_elements_with_radicals.Count - 1][j] < 0)
                    {
                        //если есть отрицательный элемент, то ищем решение дальше
                        insoluble = false;
                        break;
                    }
                //есть ответ
                if (insoluble)
                    return 1;

                //продолжение поиска решения
                return 0;
            }
        }

        /// <summary>
        /// Буферизация элементов симплекс-таблицы.
        /// </summary>
        public void BufferingSimplexTableValues(int step)
        {
            // Если выбраны десятичные
            if (drob_or_desyat)
            {
                buffer_simplex_elements.Add(new List<List<double>>());
                for (int i = 0; i < simplex_elements.Count; i++)
                {
                    buffer_simplex_elements[step - 4].Add(new List<double>());
                    for (int j = 0; j < simplex_elements[0].Count; j++)
                        buffer_simplex_elements[step - 4][i].Add(simplex_elements[i][j]);
                }
            }
            // Если выбраны дроби
            else
            {
                buffer_simplex_elements_for_radicals.Add(new List<List<Fractions>>());
                for (int i = 0; i < simplex_elements_with_radicals.Count; i++)
                {
                    buffer_simplex_elements_for_radicals[step - 4].Add(new List<Fractions>());
                    for (int j = 0; j < simplex_elements_with_radicals[0].Count; j++)
                        buffer_simplex_elements_for_radicals[step - 4][i].Add(simplex_elements_with_radicals[i][j]);
                }
            }
        }

        /// <summary>
        /// Буферизация элементов симплекс-таблицы.
        /// </summary>
        public void BufferingSimplexTableValues_ForArtifical()
        {
            // Если выбраны десятичные
            if (drob_or_desyat)
            {
                buffer_simplex_elements.Add(new List<List<double>>());
                for (int i = 0; i < simplex_elements.Count; i++)
                {
                    buffer_simplex_elements[buffer_simplex_elements.Count - 1].Add(new List<double>());
                    for (int j = 0; j < simplex_elements[0].Count; j++)
                        buffer_simplex_elements[buffer_simplex_elements.Count - 1][i].Add(simplex_elements[i][j]);
                }
            }
            // Если выбраны дроби
            else
            {
                buffer_simplex_elements_for_radicals.Add(new List<List<Fractions>>());
                for (int i = 0; i < simplex_elements_with_radicals.Count; i++)
                {
                    buffer_simplex_elements_for_radicals[buffer_simplex_elements_for_radicals.Count - 1].Add(new List<Fractions>());
                    for (int j = 0; j < simplex_elements_with_radicals[0].Count; j++)
                        buffer_simplex_elements_for_radicals[buffer_simplex_elements_for_radicals.Count - 1][i].Add(simplex_elements_with_radicals[i][j]);
                }
            }
        }

        /// <summary>
        /// Буферизируем визуализацию
        /// </summary>
        /// <param name="Grid"></param>
        public void BufferingVariablevisualizationForDeleteColumns(DataGridView Grid)
        {
            buffer_delete_artifical_columns.Add(new List<int>());

            for (int j = 0; j < Grid.ColumnCount; j++)
            {
                buffer_delete_artifical_columns[buffer_delete_artifical_columns.Count - 1].Add(Int32.Parse(Grid.Columns[j].HeaderText.Replace("Своб.", (j + 1).ToString()).Trim('x')));
            }
        }

        public void BufferingVariablevisualizationForDeleteRows(DataGridView Grid)
        {
            buffer_delete_artifical_rows.Add(new List<int>());
            for (int i = 0; i < Grid.RowCount; i++)
            {
                buffer_delete_artifical_rows[buffer_delete_artifical_rows.Count - 1].Add(Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Replace("f(x)", (i + 1).ToString()).Trim('x')));
            }
        }

        public List<int> GetOutTheBufferVariablevisualizationForDeleteColumns()
        {
            List<int> temp = new List<int>();

            temp = buffer_delete_artifical_columns[buffer_delete_artifical_columns.Count - 1];

            buffer_delete_artifical_columns.RemoveAt(buffer_delete_artifical_columns.Count - 1);

            return temp;
        }

        public List<int> GetOutTheBufferVariablevisualizationForDeleteRows()
        {
            List<int> temp = new List<int>();

            temp = buffer_delete_artifical_rows[buffer_delete_artifical_rows.Count - 1];

            buffer_delete_artifical_rows.RemoveAt(buffer_delete_artifical_rows.Count - 1);

            return temp;
        }

        /// <summary>
        /// Удаляем базисные переменные
        /// </summary>
        /// <param name="Grid"></param>
        public void DeleteArtificalBasix(DataGridView Grid)
        {
            if (drob_or_desyat)
            {
                for (int j = 0; j < Grid.ColumnCount - 1; j++)
                {
                    if ((double)Grid.Rows[simplex_elements.Count - 1].Cells[j].Value == 1)
                    {
                        Grid.Columns.RemoveAt(j);
                        j--;
                    }

                }
            }
            else
            {
                for (int j = 0; j < Grid.ColumnCount - 1; j++)
                {
                    if ((Fractions)Grid.Rows[simplex_elements_with_radicals.Count - 1].Cells[j].Value == new Fractions(1))
                    {
                        Grid.Columns.RemoveAt(j);
                        j--;
                    }

                }
            }
        }

        public void CalculateCelRow(DataGridView Grid)
        {
            if (drob_or_desyat)
            {
                double a = 0;
                int column_index = 0;
                // считаем коэффициенты последней строки
                Grid.Rows.Add();
                Grid.Rows[count_of_permutations].HeaderCell.Value = "f(x)";
                for (int j = 0; j < simplex_elements[0].Count - 1; j++)
                {
                    //логика
                    a = 0;
                    for (int i = 0; i < simplex_elements.Count; i++)
                    {
                        a += simplex_elements[i][j] * cel_function[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= cel_function[0][Int32.Parse(Grid.Columns[column_index].HeaderCell.Value.ToString().Replace("Своб.", column_index.ToString()).Trim('x')) - 1]; // функция подставления в коэфф в целевую функцию

                    ////отображение
                    Grid.Rows[count_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = 0;
                for (int i = 0; i < simplex_elements.Count; i++)
                    a += simplex_elements[i][simplex_elements[0].Count - 1] * cel_function[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1] * (-1);

                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = a * (-1);
                a = a * (-1);
                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = a;

                // добавляем в рабочий массив последнюю посчитанную строку
                for (int i = Grid.Rows.Count - 1; i < Grid.Rows.Count; i++)
                {
                    simplex_elements.Add(new List<double>());
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplex_elements[i].Add((double)Grid.Rows[i].Cells[j].Value);
                    }
                }
            }
            else
            {
                Fractions a = new Fractions(0);
                int column_index = 0;
                // считаем коэффициенты последней строки
                Grid.Rows.Add();
                Grid.Rows[count_of_permutations].HeaderCell.Value = "f(x)";
                for (int j = 0; j < simplex_elements_with_radicals[0].Count - 1; j++)
                {
                    //логика
                    a = new Fractions(0);
                    for (int i = 0; i < simplex_elements_with_radicals.Count; i++)
                    {
                        a += simplex_elements_with_radicals[i][j] * cel_function_with_radicals[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= cel_function_with_radicals[0][Int32.Parse(Grid.Columns[column_index].HeaderCell.Value.ToString().Replace("Своб.", column_index.ToString()).Trim('x')) - 1];

                    ////отображение

                    Grid.Rows[count_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //значение функции
                Fractions b = new Fractions(0);
                for (int i = 0; i < simplex_elements_with_radicals.Count; i++)
                    b += simplex_elements_with_radicals[i][simplex_elements_with_radicals[0].Count - 1] * cel_function_with_radicals[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1] * (-1);

                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = b * (-1);
                b = b * (-1); //убейте меня
                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = b;

                // добавляем в рабочий массив последнюю посчитанную строку
                for (int i = Grid.Rows.Count - 1; i < Grid.Rows.Count; i++)
                {
                    simplex_elements_with_radicals.Add(new List<Fractions>());
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplex_elements_with_radicals[i].Add((Fractions)Grid.Rows[i].Cells[j].Value);
                    }
                }
            }
        }

        /// <summary>
        /// Проверка выражения искусственного базиса.
        /// </summary>
        /// <returns>True - решение готово, false - продолжаем ешать.</returns>
        public int ArtificialResponseCheck(List<int> variable_visualization, DataGridView Grid)
        {
            int count = 0;

            // Считаем, сколько переменных выразили
            for (int j = 0; j < variable_visualization.Count - 1; j++)
            {
                if (Int32.Parse(Grid.Columns[j].HeaderText.Trim('x')) > variable_visualization.Count - 1)
                    count++;
            }

            if (drob_or_desyat)
            {

                if (simplex_elements[simplex_elements.Count - 1][simplex_elements[0].Count - 1] == 0)
                {
                    // смотрим все ли искуственные переменные мы выразили
                    if (count == Grid.RowCount - 1)
                    {
                        return 1;
                    }
                    // иначе продолжаем искать решение
                    else
                    {
                        return 0;
                    }
                }
                else if (simplex_elements[simplex_elements.Count - 1][simplex_elements[0].Count - 1] > 0)
                    return -1;
                else //if (simplex_elements[simplex_elements.Count - 1][simplex_elements[0].Count - 1] < 0)
                    return 0;
            }
            else
            {
                if (simplex_elements_with_radicals[simplex_elements_with_radicals.Count - 1][simplex_elements_with_radicals[0].Count - 1] == 0)
                {
                    // смотрим все ли искуственные переменные мы выразили
                    if (count == Grid.RowCount - 1)
                    {
                        return 1;
                    }
                    // иначе продолжаем искать решение
                    else
                    {
                        return 0;
                    }
                }

                else if (simplex_elements_with_radicals[simplex_elements_with_radicals.Count - 1][simplex_elements_with_radicals[0].Count - 1] > 0)
                    return -1;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Вычисление симплекс таблицы по выбранному опорному элементу.
        /// </summary>
        public void CalculateSimplexTable(int row_of_the_support_element, int column_of_the_support_element)
        {
            // Если выбраны десятичные
            if (drob_or_desyat)
            {
                //записываем значение опорного элемента
                supporting_member = simplex_elements[row_of_the_support_element][column_of_the_support_element];

                //вычисление остальных строк сиплекс-таблицы

                for (int i = 0; i < simplex_elements.Count; i++)
                {
                    if (i != row_of_the_support_element) // Исключаем строку с опорным элементом
                    {
                        for (int j = 0; j < simplex_elements[0].Count; j++)
                        {
                            if (j != column_of_the_support_element) // Исключаем колонку с опорным элементом
                            {
                                // вычисляем i-тый, j-тый элемент по формуле.
                                simplex_elements[i][j] = ((simplex_elements[i][j] * simplex_elements[row_of_the_support_element][column_of_the_support_element]) - (simplex_elements[row_of_the_support_element][j] * simplex_elements[i][column_of_the_support_element])) / supporting_member;
                            }
                        }
                    }
                }

                //вычисление на месте опорного
                simplex_elements[row_of_the_support_element][column_of_the_support_element] = 1 / supporting_member; // на место опорного элемента подставляем 1 поделить на опорный

                // Стоит после вычисления всех строк, потому что домнажать по формуле мы должны на старые значения, а не новые.
                //вычисление разрешающей строки
                for (int j = 0; j < simplex_elements[0].Count; j++) // Проходимся столько раз, сколько ширина симплекс таблицы (слева направо)
                {
                    if (j != column_of_the_support_element) // Если текущая колонка не с опорным элементом, то делаем...
                    {
                        simplex_elements[row_of_the_support_element][j] /= supporting_member; // в строке с опорным элементом делим ячейку на опорный

                    }
                }

                //вычисление разрешающего столбца
                for (int i = 0; i < simplex_elements.Count; i++) // Проходимся столько раз, сколько высота симплекс таблицы (сверху вниз)
                {
                    if (i != row_of_the_support_element) // Если текущий элемент не в строке опорного, то
                    {
                        simplex_elements[i][column_of_the_support_element] /= supporting_member * (-1); // в колонке опорного делим ячейку на опорный домноженную на -1
                    }
                }
            }
            // Если выбраны обыкновенные дроби
            else
            {
                //записываем значение опорного элемента
                supporting_member_for_drob = simplex_elements_with_radicals[row_of_the_support_element][column_of_the_support_element];

                //вычисление остальных строк сиплекс-таблицы

                for (int i = 0; i < simplex_elements_with_radicals.Count; i++)
                {
                    if (i != row_of_the_support_element) // Исключаем строку с опорным элементом
                    {
                        for (int j = 0; j < simplex_elements_with_radicals[0].Count; j++)
                        {
                            if (j != column_of_the_support_element) // Исключаем колонку с опорным элементом
                            {
                                // вычисляем i-тый, j-тый элемент по формуле.
                                simplex_elements_with_radicals[i][j] = (((simplex_elements_with_radicals[i][j] * simplex_elements_with_radicals[row_of_the_support_element][column_of_the_support_element]) - (simplex_elements_with_radicals[row_of_the_support_element][j] * simplex_elements_with_radicals[i][column_of_the_support_element])) / supporting_member_for_drob).Reduction();
                            }
                        }
                    }
                }

                //вычисление на месте опорного
                simplex_elements_with_radicals[row_of_the_support_element][column_of_the_support_element] = 1 / supporting_member_for_drob; // на место опорного элемента подставляем 1 поделить на опорный

                // Стоит после вычисления всех строк, потому что домнажать по формуле мы должны на старые значения, а не новые.
                //вычисление разрешающей строки
                for (int j = 0; j < simplex_elements_with_radicals[0].Count; j++) // Проходимся столько раз, сколько ширина симплекс таблицы (слева направо)
                {
                    if (j != column_of_the_support_element) // Если текущая колонка не с опорным элементом, то делаем...
                    {
                        simplex_elements_with_radicals[row_of_the_support_element][j] /= supporting_member_for_drob; // в строке с опорным элементом делим ячейку на опорный

                    }
                }

                //вычисление разрешающего столбца
                for (int i = 0; i < simplex_elements_with_radicals.Count; i++) // Проходимся столько раз, сколько высота симплекс таблицы (сверху вниз)
                {
                    if (i != row_of_the_support_element) // Если текущий элемент не в строке опорного, то
                    {
                        simplex_elements_with_radicals[i][column_of_the_support_element] /= supporting_member_for_drob * (-1); // в колонке опорного делим ячейку на опорный домноженную на -1
                    }
                }
            }
        }

        /// <summary>
        /// Возврат элементов таблицы симплекс-метода из буфера.
        /// </summary>
        public void GetOutOfTheBufferSimplex(int step)
        {
            // Если выбраны десятичные
            if (drob_or_desyat)
            {
                for (int i = 0; i < buffer_simplex_elements[step - 5].Count; i++)
                    for (int j = 0; j < buffer_simplex_elements[step - 5][0].Count; j++)
                        simplex_elements[i][j] = buffer_simplex_elements[step - 5][i][j];
                buffer_simplex_elements.RemoveAt(step - 5);
            }
            // Если выбраны дроби
            else
            {
                for (int i = 0; i < buffer_simplex_elements_for_radicals[step - 5].Count; i++)
                    for (int j = 0; j < buffer_simplex_elements_for_radicals[step - 5][0].Count; j++)
                        simplex_elements_with_radicals[i][j] = buffer_simplex_elements_for_radicals[step - 5][i][j];
                buffer_simplex_elements_for_radicals.RemoveAt(step - 5);
            }
        }

        /// <summary>
        /// Возврат элементов таблицы симплекс-метода из буфера.
        /// </summary>
        public void GetOutOfTheBufferSimplex_ForArtifical()
        {
            // Если выбраны десятичные
            if (drob_or_desyat)
            {
                simplex_elements = buffer_simplex_elements[buffer_simplex_elements.Count - 1];
                buffer_simplex_elements.RemoveAt(buffer_simplex_elements.Count - 1);
            }
            // Если выбраны дроби
            else
            {
                simplex_elements_with_radicals = buffer_simplex_elements_for_radicals[buffer_simplex_elements_for_radicals.Count - 1];
                buffer_simplex_elements_for_radicals.RemoveAt(buffer_simplex_elements_for_radicals.Count - 1);
            }
        }

        public double Response()
        {
            return simplex_elements[simplex_elements.Count - 1][simplex_elements[0].Count - 1];
        }

        public Fractions Responce_for_radicals()
        {
            return simplex_elements_with_radicals[simplex_elements_with_radicals.Count - 1][simplex_elements_with_radicals[0].Count - 1];
        }

        /// <summary>
        /// Выбор рандомного опорного элемента.
        /// </summary>
        public void SelectionRandomSupportElement()
        {
            if (drob_or_desyat)
            {
                //координаты минимума в столбце
                int[] minimum = new int[2];

                //ищем отрицательный элемент в последней строке
                for (int j = 0; j < simplex_elements[0].Count - 1; j++)
                {
                    if (simplex_elements[simplex_elements.Count - 1][j] < 0)
                    {
                        minimum[0] = -1;
                        minimum[1] = -1;
                        //ищем подходящий не отрицательный элемент в столбце
                        for (int i = 0; i < simplex_elements.Count - 1; i++)
                        {
                            if (simplex_elements[i][j] > 0)
                            {
                                if ((minimum[0] == -1) && (minimum[1] == -1))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                                else if ((simplex_elements[minimum[0]][simplex_elements[0].Count - 1] / simplex_elements[minimum[0]][minimum[1]]) > (simplex_elements[i][simplex_elements[0].Count - 1] / simplex_elements[i][j]))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                            }
                        }
                        //если есть минимальный элемент, то делаем его опорным
                        if ((minimum[0] != -1) && (minimum[1] != -1))
                        {
                            row_of_the_support_element = minimum[0];
                            column_of_the_support_element = minimum[1];
                            break;
                        }
                    }
                }
            }
            // Если выбраны обыкновенные
            else
            {
                //координаты минимума в столбце
                int[] minimum = new int[2];

                //ищем отрицательный элемент в последней строке
                for (int j = 0; j < simplex_elements_with_radicals[0].Count - 1; j++)
                {
                    if (simplex_elements_with_radicals[simplex_elements_with_radicals.Count - 1][j] < 0)
                    {
                        minimum[0] = -1;
                        minimum[1] = -1;
                        //ищем подходящий не отрицательный элемент в столбце
                        for (int i = 0; i < simplex_elements_with_radicals.Count - 1; i++)
                        {
                            if (simplex_elements_with_radicals[i][j] > 0)
                            {
                                if ((minimum[0] == -1) && (minimum[1] == -1))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                                else if ((simplex_elements_with_radicals[minimum[0]][simplex_elements_with_radicals[0].Count - 1] / simplex_elements_with_radicals[minimum[0]][minimum[1]]) > (simplex_elements_with_radicals[i][simplex_elements_with_radicals[0].Count - 1] / simplex_elements_with_radicals[i][j]))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                            }
                        }
                        //если есть минимальный элемент, то делаем его опорным
                        if ((minimum[0] != -1) && (minimum[1] != -1))
                        {
                            row_of_the_support_element = minimum[0];
                            column_of_the_support_element = minimum[1];
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Нахождение всех опорных элементов и их выделение
        /// </summary>
        /// <param name="simplextable"></param>
        public void SelectionOfTheSupportElement(DataGridView Grid)
        {
            if (drob_or_desyat)
            {
                //координаты минимального элемента в столбце
                int[] minimum = new int[2];
                the_coordinates_of_the_support_element = new List<List<int>>();
                int index = 0; //для счёта координат

                //ищем отрицательный элемент в последней строке
                for (int j = 0; j < simplex_elements[0].Count - 1; j++)
                {
                    if (simplex_elements[simplex_elements.Count - 1][j] < 0)
                    {

                        minimum[0] = -1;
                        minimum[1] = -1;
                        //ищем подходящий не отрицательный элемент в столбце
                        for (int i = 0; i < simplex_elements.Count - 1; i++)
                        {
                            if (simplex_elements[i][j] > 0)
                            {
                                // Если минимального не встречалось - назначем
                                if ((minimum[0] == -1) && (minimum[1] == -1))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                                // Если минимальный уже есть - сравниваем
                                else if ((simplex_elements[minimum[0]][simplex_elements[0].Count - 1] / simplex_elements[minimum[0]][minimum[1]]) > (simplex_elements[i][simplex_elements[0].Count - 1] / simplex_elements[i][j]))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                            }
                        }

                        //если есть минимальный, то делаем его подсвеченным
                        if ((minimum[0] != -1) && (minimum[1] != -1))
                        {
                            Grid.Rows[minimum[0]].Cells[minimum[1]].Style.BackColor = System.Drawing.Color.Green;

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
                for (int j = 0; j < simplex_elements_with_radicals[0].Count - 1; j++)
                {
                    if (simplex_elements_with_radicals[simplex_elements_with_radicals.Count - 1][j] < 0)
                    {

                        minimum[0] = -1;
                        minimum[1] = -1;
                        //ищем подходящий не отрицательный элемент в столбце
                        for (int i = 0; i < simplex_elements_with_radicals.Count - 1; i++)
                        {
                            if (simplex_elements_with_radicals[i][j] > 0)
                            {
                                // Если минимального не встречалось - назначем
                                if ((minimum[0] == -1) && (minimum[1] == -1))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                                // Если минимальный уже есть - сравниваем
                                else if ((simplex_elements_with_radicals[minimum[0]][simplex_elements_with_radicals[0].Count - 1] / simplex_elements_with_radicals[minimum[0]][minimum[1]]) > (simplex_elements_with_radicals[i][simplex_elements_with_radicals[0].Count - 1] / simplex_elements_with_radicals[i][j]))
                                {
                                    minimum[0] = i;
                                    minimum[1] = j;
                                }
                            }
                        }

                        //если есть минимальный, то делаем его подсвеченным
                        if ((minimum[0] != -1) && (minimum[1] != -1))
                        {
                            Grid.Rows[minimum[0]].Cells[minimum[1]].Style.BackColor = System.Drawing.Color.Green;

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
        /// Смена переменных без буферизации
        /// </summary>
        /// <param name="Grid"></param>
        public void ChangeOfVisualWithoutBuffer(DataGridView Grid)
        {
            string tmp_x;

            // Меняем названия переменных местами
            tmp_x = (string)Grid.Rows[row_of_the_support_element].HeaderCell.Value;
            Grid.Rows[row_of_the_support_element].HeaderCell.Value = Grid.Columns[column_of_the_support_element].HeaderText;
            Grid.Columns[column_of_the_support_element].HeaderText = tmp_x;
        }

        /// <summary>
        /// Преобразование матрицы в симплекс таблицу
        /// </summary>
        public void DrawSimplexTable(List<List<double>> ogr, DataGridView Grid)
        {
            //для сиплекс метода
            if (simplex_or_artificial == true)
            {

                double a = 0;
                //счёт столбца
                int column_index = 1;


                // Отображаем таблицу в её естественном виде // убираем базис и переносим его в левую колонку
                string now_basix_name = "";
                // Алгоритм определяющий единственные единицы в столбцах
                for (int j = 0; j < Grid.Columns.Count - 1; j++) // Проходимся по всем колонкам
                {
                    a = 0;
                    column_index = 0;
                    bool only_one_in_column = false; // Единственная единица в столбце

                    for (int i = 0; i < Grid.Rows.Count; i++) // Проходимся по всем элементам в колонке, кроме последнего(т.е. кроме целевой функции)
                    {
                        a += (double)Grid.Rows[i].Cells[j].Value;

                        // Если нам встречается единица, считаем, что колонка базисная
                        if ((double)Grid.Rows[i].Cells[j].Value == 1)
                        {
                            now_basix_name = Grid.Columns[j].Name;
                            column_index = i;
                            only_one_in_column = true;
                        }
                        // Если встретили отличное от единицы и это не ноль, смотрим, встречалась ли нам единица раньше. Если встречалась - значит считаем колонку НЕ базисной
                        else if (only_one_in_column == true && !((double)Grid.Rows[i].Cells[j].Value == 0))
                        {
                            only_one_in_column = false;
                            break;
                        }

                        // Если дошли до последнего элемента в столбце и сумма всех не равна единице, то считаем НЕ базисной
                        if ((i == Grid.Rows.Count - 1) && (a != 1))
                        {
                            only_one_in_column = false;
                            break;
                        }
                    }
                    if (only_one_in_column)
                    {
                        Grid.Rows[column_index].HeaderCell.Value = now_basix_name;
                        Grid.Columns.Remove(Grid.Columns[j]);
                        j--;
                    }
                }

                column_index = 0;
                // считаем коэффициенты последней строки
                Grid.Rows.Add();
                Grid.Rows[count_of_permutations].HeaderCell.Value = "f(x)";
                for (int j = count_of_permutations; j < ogr[0].Count - 1; j++)
                {
                    //логика
                    a = 0;
                    for (int i = 0; i < ogr.Count; i++)
                    {
                        a += ogr[i][j] * cel_function[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= cel_function[0][Int32.Parse(Grid.Columns[column_index].HeaderCell.Value.ToString().Replace("Своб.", column_index.ToString()).Trim('x')) - 1]; // функция подставления в коэфф в целевую (возможно Replace не нужно)

                    ////отображение
                    ///
                    Grid.Rows[count_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = 0;
                for (int i = 0; i < ogr.Count; i++)
                    a += ogr[i][ogr[0].Count - 1] * cel_function[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];

                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = a;

                //заполняем рабочий массив
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    simplex_elements.Add(new List<double>());
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplex_elements[i].Add((double)Grid.Rows[i].Cells[j].Value);
                    }
                }

            }
            //для искусственного базиса
            else
            {
                double a;
                //счёт столбца
                int column_index = 1;

                for (int i = 0; i < ogr.Count; i++)
                {
                    Grid.Rows[i].HeaderCell.Value = "x" + (ogr[i].Count + i);
                }
                // добавляем строку целевой функции
                Grid.Rows.Add();
                Grid.Rows[count_of_permutations].HeaderCell.Value = "f(x)";

                // считаем коэффициенты последней строки
                column_index = 0;
                for (int j = 0; j < ogr[0].Count - 1; j++)
                {
                    //логика
                    a = 0;
                    for (int i = 0; i < ogr.Count; i++)
                    {
                        a += ogr[i][j] * (-1);
                    }
                    ////отображение

                    Grid.Rows[count_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = 0;
                for (int i = 0; i < ogr.Count; i++)
                    a += ogr[i][ogr[0].Count - 1] * (-1);

                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = a;

                //заполняем рабочий массив
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    simplex_elements.Add(new List<double>());
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplex_elements[i].Add((double)Grid.Rows[i].Cells[j].Value);
                    }
                }
            }
        }

        /// <summary>
        /// Проверяем, выбран ли опорный элемент
        /// </summary>
        /// <param name="simplextable"></param>
        public void ButtonPressedOrNot(DataGridView Grid)
        {
            for (int i = 0; i < the_coordinates_of_the_support_element.Count; i++)
            {
                if (Grid.Rows[the_coordinates_of_the_support_element[i][0]].Cells[the_coordinates_of_the_support_element[i][1]] == Grid.CurrentCell)
                {
                    row_of_the_support_element = the_coordinates_of_the_support_element[i][0];
                    column_of_the_support_element = the_coordinates_of_the_support_element[i][1];
                    return;
                }
            }
            throw new Exception("Выберите опорный элемент");
        }

        /// <summary>
        /// Очистить от зелёного подсвечивания ячеек
        /// </summary>
        /// <param name="Grid"></param>
        public void delete_green_grids(DataGridView Grid)
        {
            for (int i = 0; i < Grid.Rows.Count; i++)
            {
                for (int j = 0; j < Grid.Columns.Count; j++)
                {
                    Grid.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
            }
        }

        /// <summary>
        /// Смена переменных местами + буферизация
        /// </summary>
        /// <param name="Grid"></param>
        public void ChangeOfVisualizationVariables(DataGridView Grid)
        {
            string tmp_x;

            // Буферизация координат опорного элемента до смены переменных местами - по этим координатам мы сможем поменять элементы обратно, если это потребуется
            buffer_variable_visualization.Add(new List<int>());
            buffer_variable_visualization[buffer_variable_visualization.Count - 1].Add(row_of_the_support_element);
            buffer_variable_visualization[buffer_variable_visualization.Count - 1].Add(column_of_the_support_element);

            // Меняем названия переменных местами
            tmp_x = (string)Grid.Rows[row_of_the_support_element].HeaderCell.Value;
            Grid.Rows[row_of_the_support_element].HeaderCell.Value = Grid.Columns[column_of_the_support_element].HeaderText;
            Grid.Columns[column_of_the_support_element].HeaderText = tmp_x;
        }

        /// <summary>
        /// Смена переменных местами по последним данным в буфере
        /// </summary>
        /// <param name="Grid"></param>
        public void ChangeOfVisualizationVariables_GetOutTheBuffer(DataGridView Grid)
        {
            string tmp_x;
            int[] tmp_coordination = new int[2];

            // Достаём из буфера последние координаты опорного элемента
            tmp_coordination[0] = buffer_variable_visualization[buffer_variable_visualization.Count - 1][0];
            tmp_coordination[1] = buffer_variable_visualization[buffer_variable_visualization.Count - 1][1];

            // Меняем названия переменных местами
            tmp_x = (string)Grid.Rows[tmp_coordination[0]].HeaderCell.Value;
            Grid.Rows[tmp_coordination[0]].HeaderCell.Value = Grid.Columns[tmp_coordination[1]].HeaderText;
            Grid.Columns[tmp_coordination[1]].HeaderText = tmp_x;


            buffer_variable_visualization.RemoveAt(buffer_variable_visualization.Count - 1);
        }


        /// <summary>
        /// Преобразование матрицы в симплекс таблицу для дробей
        /// </summary>
        /// <param name="ogr"></param>
        public void DrawSimplexTable(List<List<Fractions>> ogr, DataGridView Grid)
        {
            //для сиплекс метода
            if (simplex_or_artificial == true)
            {

                Fractions a;
                //счёт столбца
                int column_index = 1;


                // Отображаем таблицу в её естественном виде // убираем базис и переносим его в левую колонку
                string now_basix_name = "";
                // Алгоритм определяющий единственные единицы в столбцах
                for (int j = 0; j < Grid.Columns.Count - 1; j++) // Проходимся по всем колонкам
                {
                    a = new Fractions(0);
                    column_index = 0;
                    bool only_one_in_column = false; // Единственная единица в столбце

                    for (int i = 0; i < Grid.Rows.Count; i++) // Проходимся по всем элементам в колонке, кроме последнего(т.е. кроме целевой функции)
                    {
                        a += (Fractions)Grid.Rows[i].Cells[j].Value;

                        // Если нам встречается единица, считаем, что колонка базисная
                        if ((Fractions)Grid.Rows[i].Cells[j].Value == new Fractions(1))
                        {
                            now_basix_name = Grid.Columns[j].Name;
                            column_index = i;
                            only_one_in_column = true;
                        }
                        // Если встретили отличное от единицы и это не ноль, смотрим, встречалась ли нам единица раньше. Если встречалась - значит считаем колонку НЕ базисной
                        else if (only_one_in_column == true && !((Fractions)Grid.Rows[i].Cells[j].Value == new Fractions(0)))
                        {
                            only_one_in_column = false;
                            break;
                        }

                        // Если дошли до последнего элемента в столбце и сумма всех не равна единице, то считаем НЕ базисной
                        if ((i == Grid.Rows.Count - 1) && (a != 1))
                        {
                            only_one_in_column = false;
                            break;
                        }
                    }
                    if (only_one_in_column)
                    {
                        Grid.Rows[column_index].HeaderCell.Value = now_basix_name;
                        Grid.Columns.Remove(Grid.Columns[j]);
                        j--;
                    }
                }

                column_index = 0;
                // считаем коэффициенты последней строки
                Grid.Rows.Add();
                Grid.Rows[count_of_permutations].HeaderCell.Value = "f(x)";
                for (int j = count_of_permutations; j < ogr_with_radicals[0].Count - 1; j++)
                {
                    //логика
                    a = new Fractions(0);
                    for (int i = 0; i < ogr_with_radicals.Count; i++)
                    {
                        a += ogr_with_radicals[i][j] * cel_function_with_radicals[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];
                    }
                    a -= cel_function_with_radicals[0][Int32.Parse(Grid.Columns[column_index].HeaderCell.Value.ToString().Replace("Своб.", column_index.ToString()).Trim('x')) - 1];

                    ////отображение

                    Grid.Rows[count_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = new Fractions(0);
                for (int i = 0; i < ogr_with_radicals.Count; i++)
                    a += ogr_with_radicals[i][ogr_with_radicals[0].Count - 1] * cel_function_with_radicals[0][Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x')) - 1];

                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = a;

                //заполняем рабочий массив
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    simplex_elements_with_radicals.Add(new List<Fractions>());
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplex_elements_with_radicals[i].Add((Fractions)Grid.Rows[i].Cells[j].Value);
                    }
                }

            }
            //для искусственного базиса
            else
            {
                Fractions a;
                //счёт столбца
                int column_index = 1;

                // вводим новые базисные переменные
                for (int i = 0; i < ogr.Count; i++)
                {
                    Grid.Rows[i].HeaderCell.Value = "x" + (ogr[i].Count + i);
                }

                // добавляем строку целевой функции
                Grid.Rows.Add();
                Grid.Rows[count_of_permutations].HeaderCell.Value = "f(x)";

                // считаем коэффициенты последней строки
                column_index = 0;
                for (int j = 0; j < ogr_with_radicals[0].Count - 1; j++)
                {
                    //логика
                    a = new Fractions(0);
                    for (int i = 0; i < ogr_with_radicals.Count; i++)
                    {
                        a += ogr_with_radicals[i][j] * (-1);
                    }
                    ////отображение

                    Grid.Rows[count_of_permutations].Cells[column_index].Value = a;
                    column_index++;
                }

                //коэффициент в нижнем правом углу симплекс таблицы
                a = new Fractions(0);
                for (int i = 0; i < ogr_with_radicals.Count; i++)
                    a += ogr_with_radicals[i][ogr_with_radicals[0].Count - 1] * (-1);

                Grid.Rows[Grid.Rows.Count - 1].Cells[Grid.Columns.Count - 1].Value = a;

                //заполняем рабочий массив
                for (int i = 0; i < Grid.Rows.Count; i++)
                {
                    simplex_elements_with_radicals.Add(new List<Fractions>());
                    for (int j = 0; j < Grid.Columns.Count; j++)
                    {
                        //добавляем в массив число
                        simplex_elements_with_radicals[i].Add((Fractions)Grid.Rows[i].Cells[j].Value);
                    }
                }
            }
        }

        public string[] ResponseDot(DataGridView Grid)
        {
            if (drob_or_desyat)
            {
                //угловая точка соответствующая решению 
                string[] finish_corner_dot = new string[cel_function[0].Count];

                // по умолчанию элементы - 0
                for (int i = 0; i < finish_corner_dot.Length; i++)
                    finish_corner_dot[i] = "0";

                //вспомогательная переменная
                int temp;

                //заполняем коэффициентами
                
                    for (int i = 0; i < Grid.Rows.Count - 1; i++)
                    {
                        temp = Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x'));
                        finish_corner_dot[temp - 1] = simplex_elements[i][simplex_elements[0].Count - 1].ToString();
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

                    for (int i = 0; i < Grid.Rows.Count - 1; i++)
                    {
                        temp = Int32.Parse(Grid.Rows[i].HeaderCell.Value.ToString().Trim('x'));
                        finish_corner_dot[temp - 1] = simplex_elements_with_radicals[i][simplex_elements_with_radicals[0].Count - 1].ToString();
                    } 

                return finish_corner_dot;
            }
        }
    }
}