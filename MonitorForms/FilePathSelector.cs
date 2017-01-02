using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace MonitorForms
{
    [Description("Контрол для выбора и отображения пути к файлу.")]
    public partial class FilePathSelector : UserControl
    {
        public FilePathSelector()
        {
            InitializeComponent();
            Caption = Name;
        }

        /// <summary>Путь к файлу</summary>
        [Description("Путь к файлу.")]
        public string FilePath
        {
            get { return pathTextBox.Text; }
            set { pathTextBox.Text = value; }
        }

        /// <summary>Диалог выбора файла</summary>
        [Description("Диалог выбора файла.")]
        public SaveFileDialog FileDialog { get; set; }

        [Description("Заголовок поля.")]
        public string Caption
        {
            get { return label1.Text; }
            set { label1.Text = value; }
        }

        private void pathTextBox_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(pathTextBox, pathTextBox.Text);
            var w = TextRenderer.MeasureText(FilePath, Font).Width;
            //Показывать подсказку только если текст выходит за рамки поля
            toolTip1.Active = w > pathTextBox.ClientSize.Width;
        }

        //Вызов диалога выбора файла. Если диалог не назначен, то создаётся по умолчанию.
        private void browseButton_Click(object sender, EventArgs e)
        {
            if (FileDialog == null)
            {
                using (var dialog = new SaveFileDialog())
                {
                    dialog.FileName = FilePath;
                    dialog.OverwritePrompt = false;
                    if (dialog.ShowDialog(this) != DialogResult.OK)
                        return;
                    pathTextBox.Text = dialog.FileName;
                }
            }
            else
            {
                FileDialog.FileName = FilePath;
                var op = FileDialog.OverwritePrompt;
                if (File.Exists(FilePath))
                {
                    FileDialog.InitialDirectory = Path.GetDirectoryName(FilePath);
                }
                if (FileDialog.ShowDialog(this) != DialogResult.OK)
                {
                    FileDialog.OverwritePrompt = op;
                    return;
                }
                FileDialog.OverwritePrompt = op;
                pathTextBox.Text = FileDialog.FileName;
            }
        }
    }
}
