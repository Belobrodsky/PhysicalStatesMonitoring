using System;
using System.Linq;
using System.Windows.Forms;

namespace MonitorForms.ArrayEditor
{
    /// <summary>
    /// Класс для редактирования значений в одномерных массивах.
    /// </summary>
    /// <remarks>Каждый массив записывается отдельной строкой.</remarks>
    internal class ArrayEditor : DataGridView
    {
        #region Свойства

        private int MaxLen
        {
            get
            {
                return Columns.Count;
            }
            set
            {
                for (int i = Columns.Count; i < value; i++)
                {
                    var index = Columns.Add(new DataGridViewTextBoxColumn());
                    Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
            }
        }

        /// <summary>
        ///     Получение/задание значения элемента массива по номеру массива и индексу элемента.
        /// </summary>
        /// <param name="i">Номер массива в списке.</param>
        /// <param name="index">Индекс элемента в массиве.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Если номер массива меньше нуля или больше количества массивов в редакторе.
        ///     <para>Если индекс элемента массива меньше нуля или больше количества элементов в массиве.</para>
        /// </exception>
        public new object this[int i, int index]
        {
            //TODO Получение значения через индексатор
            get
            {
                //CheckIndices(i, index);
                return Rows[i].Cells[index].Value;
            }
            //TODO Изменение значения через индексатор
            set
            {
                //CheckIndices(i, index);
                Rows[i].Cells[index].Value = value;
            }
        }

        /// <summary>
        ///     Получение массива по его номеру.
        /// </summary>
        /// <param name="i">Номер массива.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Если номер массива меньше нуля или больше количества массивов в
        ///     редакторе.
        /// </exception>
        public object[] this[int i]
        {
            //TODO Получение массива по его номеру
            get
            {
                return Rows[i].Cells.Cast<DataGridViewTextBoxCell>().Select(c => c.Value).ToArray();
            }
        }

        #endregion

        public ArrayEditor()
        {
            AutoGenerateColumns = true;
        }

        //TODO Добавление массива
        public void Add(string name, params object[] values)
        {
            dynamic ar = values[0];
            if (MaxLen < ar.Length)
                MaxLen = ar.Length;
            var index = Rows.Add();
            for (int i = 0; i < ar.Length; i++)
            {
                Rows[index].Cells[i].Value = ar[i];
                Rows[index].Cells[i].OwningColumn.ValueType = ar[i].GetType();
            }
            Rows[index].HeaderCell.Value = name;
        }

        #region Overrides of DataGridView

        protected override void OnColumnAdded(DataGridViewColumnEventArgs e)
        {
            base.OnColumnAdded(e);
            e.Column.HeaderText = e.Column.Index.ToString();
        }

        protected override void OnDataError(bool displayErrorDialogIfNoHandler, DataGridViewDataErrorEventArgs e)
        {
            MessageBox.Show("Неверное значение.", "Ошибка ввода", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        #endregion

        public void Clear()
        {
            Rows.Clear();
            Columns.Clear();
        }
    }
}