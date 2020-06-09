using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplex_Method
{
    public sealed class Fractions
    {
        private int numerator;              // Числитель
        private int denominator;            // Знаменатель
        private int sign;                   // Знак перед дробью

        public Fractions(int numerator, int denominator)
        {
            if (denominator == 0)
            {
                throw new DivideByZeroException("Ошибка: деление на ноль.");
            }
            this.numerator = Math.Abs(numerator);
            this.denominator = Math.Abs(denominator);
            if (numerator * denominator < 0)
            {
                this.sign = -1;
            }
            else
            {
                this.sign = 1;
            }
        }

        // Вызов первого конструктора со знаменателем равным единице
        public Fractions(int number) : this(number, 1) { }

        // Возвращает наибольший общий делитель (Алгоритм Евклида)
        private static int getGreatestCommonDivisor(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        // Возвращает наименьшее общее кратное
        private static int getLeastCommonMultiple(int a, int b)
        {
            // В формуле опушен модуль, так как в классе
            // числитель всегда неотрицательный, а знаменатель -- положительный
            // ...
            // Деление здесь -- челочисленное, что не искажает результат, так как
            // числитель и знаменатель делятся на свои делители,
            // т.е. при делении не будет остатка
            return a * b / getGreatestCommonDivisor(a, b);
        }

        // Метод создан для устранения повторяющегося кода в методах сложения и вычитания дробей.
        // Возвращает дробь, которая является результатом сложения или вычитания дробей a и b,
        // В зависимости от того, какая операция передана в параметр operation.
        // P.S. использовать только для сложения и вычитания
        private static Fractions performOperation(Fractions a, Fractions b, Func<int, int, int> operation)
        {
            // Наименьшее общее кратное знаменателей
            int leastCommonMultiple = getLeastCommonMultiple(a.denominator, b.denominator);
            // Дополнительный множитель к первой дроби
            int additionalMultiplierFirst = leastCommonMultiple / a.denominator;
            // Дополнительный множитель ко второй дроби
            int additionalMultiplierSecond = leastCommonMultiple / b.denominator;
            // Результат операции
            int operationResult = operation(a.numerator * additionalMultiplierFirst * a.sign,
            b.numerator * additionalMultiplierSecond * b.sign);
            return new Fractions(operationResult, a.denominator * additionalMultiplierFirst);
        }

        // Возвращает дробь, обратную данной
        private Fractions GetReverse()
        {
            return new Fractions(this.denominator * this.sign, this.numerator);
        }
        // Возвращает дробь с противоположным знаком
        private Fractions GetWithChangedSign()
        {
            return new Fractions(-this.numerator * this.sign, this.denominator);
        }

        // Перегрузка оператора "+" для случая сложения двух дробей
        public static Fractions operator +(Fractions a, Fractions b)
        {
            return performOperation(a, b, (int x, int y) => x + y);
        }
        // Перегрузка оператора "+" для случая сложения дроби с числом
        public static Fractions operator +(Fractions a, int b)
        {
            return a + new Fractions(b);
        }
        // Перегрузка оператора "+" для случая сложения числа с дробью
        public static Fractions operator +(int a, Fractions b)
        {
            return b + a;
        }
        // Перегрузка оператора "-" для случая вычитания двух дробей
        public static Fractions operator -(Fractions a, Fractions b)
        {
            return performOperation(a, b, (int x, int y) => x - y);
        }
        // Перегрузка оператора "-" для случая вычитания из дроби числа
        public static Fractions operator -(Fractions a, int b)
        {
            return a - new Fractions(b);
        }
        // Перегрузка оператора "-" для случая вычитания из числа дроби
        public static Fractions operator -(int a, Fractions b)
        {
            return b - a;
        }
        // Перегрузка оператора "*" для случая произведения двух дробей
        public static Fractions operator *(Fractions a, Fractions b)
        {
            return new Fractions(a.numerator * a.sign * b.numerator * b.sign, a.denominator * b.denominator);
        }
        // Перегрузка оператора "*" для случая произведения дроби и числа
        public static Fractions operator *(Fractions a, int b)
        {
            return a * new Fractions(b);
        }
        // Перегрузка оператора "*" для случая произведения числа и дроби
        public static Fractions operator *(int a, Fractions b)
        {
            return b * a;
        }
        // Перегрузка оператора "/" для случая деления двух дробей
        public static Fractions operator /(Fractions a, Fractions b)
        {
            return a * b.GetReverse();
        }
        // Перегрузка оператора "/" для случая деления дроби на число
        public static Fractions operator /(Fractions a, int b)
        {
            return a / new Fractions(b);
        }
        // Перегрузка оператора "/" для случая деления числа на дробь
        public static Fractions operator /(int a, Fractions b)
        {
            return new Fractions(a) / b;
        }
        // Перегрузка оператора "унарный минус"
        public static Fractions operator -(Fractions a)
        {
            return a.GetWithChangedSign();
        }
        // Перегрузка оператора "++"
        public static Fractions operator ++(Fractions a)
        {
            return a + 1;
        }
        // Перегрузка оператора "--"
        public static Fractions operator --(Fractions a)
        {
            return a - 1;
        }

        // Мой метод Equals
        public bool Equals(Fractions that)
        {
            Fractions a = this.Reduce();
            Fractions b = that.Reduce();
            return a.numerator == b.numerator &&
            a.denominator == b.denominator &&
            a.sign == b.sign;
        }
        // Переопределение метода Equals
        public override bool Equals(object obj)
        {
            bool result = false;
            if (obj is Fractions)
            {
                result = this.Equals(obj as Fractions);
            }
            return result;
        }
        // Переопределение метода GetHashCode
        public override int GetHashCode()
        {
            return this.sign * (this.numerator * this.numerator + this.denominator * this.denominator);
        }

        // Метод сравнения двух дробей
        // Возвращает	 0, если дроби равны
        //				 1, если this больше that
        //				-1, если this меньше that
        private int CompareTo(Fractions that)
        {
            if (this.Equals(that))
            {
                return 0;
            }
            Fractions a = this.Reduce();
            Fractions b = that.Reduce();
            if (a.numerator * a.sign * b.denominator > b.numerator * b.sign * a.denominator)
            {
                return 1;
            }
            return -1;
        }

        // Перегрузка оператора "Равенство" для двух дробей
        public static bool operator ==(Fractions a, Fractions b)
        {
            // Приведение к Object необходимо для того, чтобы
            // можно было сравнивать дроби с null.
            // Обычное сравнение a.Equals(b) в данном случае не подходит,
            // так как если a есть null, то у него нет метода Equals,
            // следовательно будет выдано исключение, а если
            // b окажется равным null, то исключение будет вызвано в
            // методе this.Equals
            Object aAsObj = a as Object;
            Object bAsObj = b as Object;
            if (aAsObj == null || bAsObj == null)
            {
                return aAsObj == bAsObj;
            }
            return a.Equals(b);
        }
        // Перегрузка оператора "Равенство" для дроби и числа
        public static bool operator ==(Fractions a, int b)
        {
            return a == new Fractions(b);
        }
        // Перегрузка оператора "Равенство" для числа и дроби
        public static bool operator ==(int a, Fractions b)
        {
            return new Fractions(a) == b;
        }
        // Перегрузка оператора "Неравенство" для двух дробей
        public static bool operator !=(Fractions a, Fractions b)
        {
            return !(a == b);
        }
        // Перегрузка оператора "Неравенство" для дроби и числа
        public static bool operator !=(Fractions a, int b)
        {
            return a != new Fractions(b);
        }
        // Перегрузка оператора "Неравенство" для числа и дроби
        public static bool operator !=(int a, Fractions b)
        {
            return new Fractions(a) != b;
        }

        // Перегрузка оператора ">" для двух дробей
        public static bool operator >(Fractions a, Fractions b)
        {
            return a.CompareTo(b) > 0;
        }
        // Перегрузка оператора ">" для дроби и числа
        public static bool operator >(Fractions a, int b)
        {
            return a > new Fractions(b);
        }
        // Перегрузка оператора ">" для числа и дроби
        public static bool operator >(int a, Fractions b)
        {
            return new Fractions(a) > b;
        }
        // Перегрузка оператора "<" для двух дробей
        public static bool operator <(Fractions a, Fractions b)
        {
            return a.CompareTo(b) < 0;
        }
        // Перегрузка оператора "<" для дроби и числа
        public static bool operator <(Fractions a, int b)
        {
            return a < new Fractions(b);
        }
        // Перегрузка оператора "<" для числа и дроби
        public static bool operator <(int a, Fractions b)
        {
            return new Fractions(a) < b;
        }
        // Перегрузка оператора ">=" для двух дробей
        public static bool operator >=(Fractions a, Fractions b)
        {
            return a.CompareTo(b) >= 0;
        }
        // Перегрузка оператора ">=" для дроби и числа
        public static bool operator >=(Fractions a, int b)
        {
            return a >= new Fractions(b);
        }
        // Перегрузка оператора ">=" для числа и дроби
        public static bool operator >=(int a, Fractions b)
        {
            return new Fractions(a) >= b;
        }
        // Перегрузка оператора "<=" для двух дробей
        public static bool operator <=(Fractions a, Fractions b)
        {
            return a.CompareTo(b) <= 0;
        }
        // Перегрузка оператора "<=" для дроби и числа
        public static bool operator <=(Fractions a, int b)
        {
            return a <= new Fractions(b);
        }
        // Перегрузка оператора "<=" для числа и дроби
        public static bool operator <=(int a, Fractions b)
        {
            return new Fractions(a) <= b;
        }

        // Возвращает сокращенную дробь
        public Fractions Reduce()
        {
            Fractions result = this;
            int greatestCommonDivisor = getGreatestCommonDivisor(this.numerator, this.denominator);
            result.numerator /= greatestCommonDivisor;
            result.denominator /= greatestCommonDivisor;
            return result;
        }
        // Переопределение метода ToString
        public override string ToString()
        {
            if (this.numerator == 0)
            {
                return "0";
            }
            string result;
            if (this.sign < 0)
            {
                result = "-";
            }
            else
            {
                result = "";
            }
            if (this.numerator == this.denominator)
            {
                return result + "1";
            }
            if (this.denominator == 1)
            {
                return result + this.numerator;
            }
            return result + this.numerator + "/" + this.denominator;
        }

        internal Fractions Reduction()
        {
            int nod = 0;

            if (this.denominator != 1)
            {
                if (this.numerator % this.denominator == 0)
                {
                    this.numerator = this.numerator / this.denominator;
                    this.denominator = 1;
                }
            }
            if ((this.numerator != 0) && (this.numerator != 1))
            {
                if (this.denominator % this.numerator == 0 || getGreatestCommonDivisor(this.denominator, this.numerator) != 1)
                {
                    nod = getGreatestCommonDivisor(this.denominator, this.numerator);
                    this.numerator = this.numerator / nod;
                    this.denominator = this.denominator / nod;
                }
            }

            return this;
        }
    }
}