using System;
using TextEditor.BL;

namespace TextEditor
{
    public class MainPresenter
    {
        private readonly IEditorForm _view;
        private readonly IFileManager _manager;
        private readonly IMessageService _message;
        private string _currentFilePath;

        public MainPresenter(IEditorForm view, IFileManager manager, IMessageService message)
        {
            _view = view;
            _manager = manager;
            _message = message;

            _view.SetSymbolCount(0);

            _view.ContentChanged += _view_ContentChanged;
            _view.FileOpenClick += _view_FileOpenClick;
            _view.FileSaveClick += _view_FileSaveClick;
        }

        private void _view_FileSaveClick(object sender, EventArgs e)
        {
            if(_view.FilePath != string.Empty)
            {
                try
                {
                    string content = _view.Content;
                    _manager.SaveContent(_currentFilePath, content);
                    _message.ShowMessage("Файл сохранен!");
                }
                catch (Exception ex)
                {
                    _message.ShowError(ex.Message);
                }
            }
            _message.ShowMessage("Сначала выберить путь к файлу");
        }

        private void _view_FileOpenClick(object sender, EventArgs e)
        {
            try
            {
                string filePath = _view.FilePath;
                bool exist = _manager.IsExist(filePath);
                if (!exist)
                {
                    _message.ShowExclamation("Файл не существует");
                    return;
                }
                _currentFilePath = filePath;

                string content = _manager.GetContent(_currentFilePath);
                int count = _manager.GetSymbolsCount(content);
                _view.Content = content;
                _view.SetSymbolCount(count);
            }
            catch(Exception ex)
            {
                _message.ShowError(ex.Message);
            }
        }

        private void _view_ContentChanged(object sender, EventArgs e)
        {
            string content = _view.Content;
            int count = _manager.GetSymbolsCount(content);
            _view.SetSymbolCount(count);
        }
    }
}
