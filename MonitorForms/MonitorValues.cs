using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace MonitorForms
{
    public class MonitorValues
    {
        /// <summary> Значения отслеживаемых величин </summary>
        public List<double> Values { get; }
        /// <summary> Минимумы для каждой величины </summary>
        public List<double> Mins { get; }
        /// <summary> Максимумы для каждой величины </summary>
        public List<double> Maxs { get; }

        public MonitorValues()
            : this(new List<double>(), new List<double>(), new List<double>())
        {

        }

        /// <summary>
        /// Создание нового класса с набором отслеживаемых величин
        /// </summary>
        /// <param name="values">Набор значений</param>
        /// <param name="mins">Набор возможных минимумов значений</param>
        /// <param name="maxs">Набор возможных максимумов значений</param>
        public MonitorValues(IEnumerable<double> values, IEnumerable<double> mins, IEnumerable<double> maxs)
        {
            Values = values.ToList();
            Mins = mins.ToList();
            Maxs = maxs.ToList();
        }

        /// <summary>
        /// Получение требуемого значения
        /// </summary>
        /// <param name="index">Индекс значения</param>
        /// <param name="normal">Нормировать значение?</param>
        /// <returns>Возвращает указанное значение из списка. Если указан флаг <see cref="normal"/>, то значение нормируется в соответствии с заданными пределами.</returns>
        public double this[int index, bool normal = true]
        {
            get
            {
                if (index > Values.Count - 1 || index < 0)
                {
                    return double.NaN;
                }
                if (normal)
                {
                    return (Values[index] - Mins[index]) / (Maxs[index] - Mins[index]);
                }
                return Values[index];
            }
            set
            {
                if (index > Values.Count - 1 || index < 0)
                    throw new ArgumentOutOfRangeException(nameof(index));
                Values[index] = value;
            }
        }

        /// <summary>
        /// Добавление значения в список
        /// </summary>
        /// <param name="value">Значения</param>
        /// <param name="min">Допустимы минимум для него</param>
        /// <param name="max">Допустимый максимум для него</param>
        /// <returns>Возвращает индекс добавленного значения</returns>
        public int AddValue(double value, double min, double max)
        {
            Values.Add(value);
            Mins.Add(min);
            Maxs.Add(max);
            return Values.Count;
        }
    }
}
