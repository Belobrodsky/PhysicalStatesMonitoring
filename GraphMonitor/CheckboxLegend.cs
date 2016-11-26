using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms.VisualStyles;

namespace GraphMonitor
{
    class CheckboxLegend : LegendItem
    {
        private static string _checkboxCheckedPath;
        private static string _checkboxUncheckedPath;
        public CheckboxLegend()
        {
            if (!File.Exists(_checkboxCheckedPath) || !File.Exists(_checkboxUncheckedPath))
                DrawBitmaps();
            AddCells();
        }

        private void AddCells()
        {
            var imageCell = new LegendCell() { CellType = LegendCellType.Image, Image = _checkboxUncheckedPath };
            var seriesCell = new LegendCell() { CellType = LegendCellType.Text, Text = SeriesName };
        }

        private void DrawBitmaps()
        {
            _checkboxCheckedPath = Path.Combine(Path.GetTempPath(), "checkboxChecked.bmp");
            _checkboxUncheckedPath = Path.Combine(Path.GetTempPath(), "checkboxUnchecked.bmp");
            var sizeChecked = CheckBoxRenderer.GetGlyphSize(Graphics.FromHwnd(IntPtr.Zero), CheckBoxState.CheckedNormal);
            var sizeUnchecked = CheckBoxRenderer.GetGlyphSize(Graphics.FromHwnd(IntPtr.Zero), CheckBoxState.UncheckedNormal);
            using (Bitmap checkedBmp = new Bitmap(sizeChecked.Width, sizeChecked.Height), unCheckedBmp = new Bitmap(sizeUnchecked.Width, sizeUnchecked.Height))
            {
                using (Graphics g = Graphics.FromImage(checkedBmp), g1 = Graphics.FromImage(unCheckedBmp))
                {
                    CheckBoxRenderer.DrawCheckBox(g, new Point(), CheckBoxState.CheckedNormal);
                    CheckBoxRenderer.DrawCheckBox(g1, new Point(), CheckBoxState.UncheckedNormal);
                }
                checkedBmp.Save(_checkboxCheckedPath, ImageFormat.Bmp);
                unCheckedBmp.Save(_checkboxUncheckedPath, ImageFormat.Bmp);
            }
        }
    }
}
