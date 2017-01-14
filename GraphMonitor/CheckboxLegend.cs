using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;
using ContentAlignment = System.Drawing.ContentAlignment;

namespace GraphMonitor
{
    /// <summary>
    ///     Класс для отображения названия графика с чекбоксом
    /// </summary>
    internal class CheckboxLegend : LegendItem
    {
        #region Свойства

        //Путь к изображению отмеченного чекбокса
        private static string _checkboxCheckedPath;
        //Путь к изображению неотмеченного чекбокса
        private static string _checkboxUncheckedPath;
        //Ассоциированный график
        private readonly Series _series;

        #endregion

        public CheckboxLegend(Series series)
        {
            _series = series;
            _series.IsVisibleInLegend = false;
            ImageStyle = LegendImageStyle.Line;
            Color = _series.Color;
            if (!File.Exists(_checkboxCheckedPath)
                || !File.Exists(_checkboxUncheckedPath))
                DrawBitmaps();
            AddCells();
        }

        /// <summary>Событие при выборе легенды в списке</summary>
        public event EventHandler<LegendSelectedEventArgs> LegendSelected;

        /// <summary>Добавление ячеек с нужными элементами</summary>
        private void AddCells()
        {
            //Изображение чекбокса
            var imageCell = new LegendCell
                            {
                                CellType = LegendCellType.Image,
                                Image = _series.Enabled ? _checkboxCheckedPath : _checkboxUncheckedPath
                            };
            //Название графика
            var seriesCell = new LegendCell(LegendCellType.Text, _series.Name);
            seriesCell.Alignment = ContentAlignment.MiddleLeft;

            //Цвет графика
            var typeCell = new LegendCell
                           {
                               CellType = LegendCellType.SeriesSymbol
                           };
            Cells.Add(imageCell);
            Cells.Add(typeCell);
            Cells.Add(seriesCell);
        }

        /// <summary> Клик по описанию графика </summary>
        public void Click(LegendCell cell = null)
        {
            if (cell == null)
                return;
            switch (cell.CellType)
            {
                case LegendCellType.Text:
                case LegendCellType.SeriesSymbol:
                    //Прямоугольник выделения
                    foreach (var item in Legend.CustomItems)
                    {
                        if (!(item is CheckboxLegend))
                            continue;

                        item.Cells[1].BackColor = Color.Transparent;
                        item.Cells[2].BackColor = Color.Transparent;
                    }
                    Cells[1].BackColor = Color.FromArgb(50, Color.Blue);
                    Cells[2].BackColor = Color.FromArgb(50, Color.Blue);
                    OnSelectedLegendChanged(new LegendSelectedEventArgs(_series));
                    break;
                case LegendCellType.Image:
                    _series.Enabled = !_series.Enabled;
                    Cells[0].Image = _series.Enabled ? _checkboxCheckedPath : _checkboxUncheckedPath;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>Рисование чекбоксов</summary>
        private static void DrawBitmaps()
        {
            //Пути к файлам во временной папке
            _checkboxCheckedPath = Path.Combine(Path.GetTempPath(), "checkboxChecked.bmp");
            _checkboxUncheckedPath = Path.Combine(Path.GetTempPath(), "checkboxUnchecked.bmp");
            //Размеры изображений чекбоксов для разных состояний
            var sizeChecked = CheckBoxRenderer.GetGlyphSize(Graphics.FromHwnd(IntPtr.Zero), CheckBoxState.CheckedNormal);
            var sizeUnchecked = CheckBoxRenderer.GetGlyphSize(
                Graphics.FromHwnd(IntPtr.Zero), CheckBoxState.UncheckedNormal);

            //Рисование изображений чекбоксов
            using (
                Bitmap checkedBmp = new Bitmap(sizeChecked.Width, sizeChecked.Height),
                       unCheckedBmp = new Bitmap(sizeUnchecked.Width, sizeUnchecked.Height))
            {
                using (Graphics g = Graphics.FromImage(checkedBmp), g1 = Graphics.FromImage(unCheckedBmp))
                {
                    CheckBoxRenderer.DrawCheckBox(g, new Point(), CheckBoxState.CheckedNormal);
                    CheckBoxRenderer.DrawCheckBox(g1, new Point(), CheckBoxState.UncheckedNormal);
                }
                //Сохранение во временную папку.
                if (!File.Exists(_checkboxCheckedPath))
                    checkedBmp.Save(_checkboxCheckedPath, ImageFormat.Bmp);
                if (!File.Exists(_checkboxUncheckedPath))
                    unCheckedBmp.Save(_checkboxUncheckedPath, ImageFormat.Bmp);
            }
        }

        /// <summary>
        ///     Метод вызова события <see cref="LegendSelected" />
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnSelectedLegendChanged(LegendSelectedEventArgs e)
        {
            EventHandler<LegendSelectedEventArgs> handler = LegendSelected;
            if (handler != null)
                handler(this, e);
        }
    }
}