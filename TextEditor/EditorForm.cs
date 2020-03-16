using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextEditor
{
    public interface IEditorForm
    {
        string FilePath { get; }
        string Content { get; set; }
        void SetSymbolCount(int count);
        event EventHandler FileOpenClick;
        event EventHandler FileSaveClick;
        event EventHandler ContentChanged;
    }

    public partial class EditorForm : Form, IEditorForm
    {
        public EditorForm()
        {
            InitializeComponent();
            butOpenFile.Click += ButOpenFile_Click;
            butSaveFile.Click += ButSaveFile_Click;
            fldContent.TextChanged += FldContent_TextChanged;
            butSelectFile.Click += ButSelectFile_Click;
            numFont.ValueChanged += NumFont_ValueChanged;
        }

        #region Проброс события с формы к презентеру 
        private void FldContent_TextChanged(object sender, EventArgs e)
        {
            ContentChanged?.Invoke(this, EventArgs.Empty);
        }

        private void ButSaveFile_Click(object sender, EventArgs e)
        {
            FileSaveClick?.Invoke(this, EventArgs.Empty);
        }

        private void ButOpenFile_Click(object sender, EventArgs e)
        {
            FileOpenClick?.Invoke(this, EventArgs.Empty);
        }
        #endregion

        public string FilePath { get => fldFilePath.Text; }

        public string Content { get => fldContent.Text; set => fldContent.Text = value; }

        public void SetSymbolCount(int count)
        {
            lblSymbolCount.Text = count.ToString();
        }

        public event EventHandler FileOpenClick;
        public event EventHandler FileSaveClick;
        public event EventHandler ContentChanged;

        #region Клики на самой форме
        private void ButSelectFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Текстовые файлы|*.txt|Все файлы|*.*";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                fldFilePath.Text = dialog.FileName;
                FileOpenClick?.Invoke(this, EventArgs.Empty);
            }
        }

        private void NumFont_ValueChanged(object sender, EventArgs e)
        {
            fldContent.Font = new Font("Arial", (int)numFont.Value);
        }
        #endregion
    }
}
