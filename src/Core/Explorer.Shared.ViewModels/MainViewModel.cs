using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Explorer.Shared.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Public Properties
        public ObservableCollection<FileEntityViewModel> DirectoriesAndFiles { get; set; } = new ObservableCollection<FileEntityViewModel>();
        public FileEntityViewModel SelectedFileEntity { get; set; }
        public string FilePath { get; set; }
        #endregion

        #region Commands
        public ICommand OpenCommand { get; }
        #endregion

        #region CommandsMethods
        private void Open(object parameter)
        {
            if(parameter is DirectoryViewModel dirViewModel)
            {
                FilePath = dirViewModel.FullName;
                DirectoriesAndFiles.Clear();

                var dirInfo = new DirectoryInfo(FilePath);
                foreach(var dir in dirInfo.GetDirectories())
                {
                    DirectoriesAndFiles.Add(new DirectoryViewModel(dir));
                }
                foreach (var fileInfo in dirInfo.GetFiles())
                {
                    DirectoriesAndFiles.Add(new FileViewModel(fileInfo));
                }

            }
        }
        #endregion

        #region Contructor
        public MainViewModel()
        {
            OpenCommand = new DelegateCommand(Open);
            foreach(var logicalDrive in Directory.GetLogicalDrives())
            {
                DirectoriesAndFiles.Add(new DirectoryViewModel(logicalDrive));
            }
        }
        #endregion

    }
}
