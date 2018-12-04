using FileManager.Models;
using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileManager.ViewModels
{
    class MainVM : INotifyPropertyChanged
    {
        public DirectoryInfo CurrentCatalog { get; set; }
        private string selectedDrive;
        public string SelectedDrive
        {
            get { return selectedDrive; }
            set
            {
                selectedDrive = value;
                CurrentCatalog = new DirectoryInfo(value);
                new WindowsDirectory(new DirectoryInfo(value)).Open(ref leftFiles);
                OnPropertyChanged();
            }
        }
        public ObservableCollection<string> Drives { get; set; }
        private ObservableCollection<IWindowsFile> leftFiles;
        public ObservableCollection<IWindowsFile> LeftFiles
        {
            get { return leftFiles; }
            set
            {
                leftFiles = value;
                OnPropertyChanged();
            }
        }
        private IWindowsFile leftSelectedFile;
        public IWindowsFile LeftSelectedFile {
            get { return leftSelectedFile; }
            set
            {
                leftSelectedFile = value;
                OnPropertyChanged();
            }
        }
        public MainVM()
        {
            LeftFiles = new ObservableCollection<IWindowsFile>();
            Drives = new ObservableCollection<string>(Environment.GetLogicalDrives());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        private RelayCommand openFileCommand;
        public RelayCommand OpenFileCommand
        {
            get
            {
                return openFileCommand ??
                  (openFileCommand = new RelayCommand(obj =>
                  {
                      if (LeftSelectedFile != null)
                      {
                          CurrentCatalog = new DirectoryInfo(LeftSelectedFile.Path);
                          LeftSelectedFile.Open(ref leftFiles);
                      }
                  }));
            }
        }
        private RelayCommand renameFileCommand;
        public RelayCommand RenameFileCommand
        {
            get
            {
                return renameFileCommand ??
                  (renameFileCommand = new RelayCommand(obj =>
                  {
                      if(LeftSelectedFile != null)
                        LeftSelectedFile.RequestRename();
                  }));
            }
        }
        private RelayCommand deleteFileCommand;
        public RelayCommand DeleteFileCommand
        {
            get
            {
                return deleteFileCommand ??
                  (deleteFileCommand = new RelayCommand(obj =>
                  {
                      if(LeftSelectedFile != null)
                        LeftSelectedFile.RequestRemove(ref leftFiles);
                  }));
            }
        }
        private RelayCommand desktopCommand;
        public RelayCommand DesktopCommand
        {
            get
            {
                return desktopCommand ??
                  (desktopCommand = new RelayCommand(obj =>
                  {
                      CurrentCatalog = new DirectoryInfo(
                          Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));
                      new WindowsDirectory(CurrentCatalog).Open(ref leftFiles);
                  }));
            }
        }
        private RelayCommand myDocumentsCommand;
        public RelayCommand MyDocumentsCommand
        {
            get
            {
                return myDocumentsCommand ??
                  (myDocumentsCommand = new RelayCommand(obj =>
                  {
                      CurrentCatalog = new DirectoryInfo(
                          Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
                      new WindowsDirectory(CurrentCatalog)
                          .Open(ref leftFiles);
                  }));
            }
        }
        private RelayCommand systemCommand;
        public RelayCommand SystemCommand
        {
            get
            {
                return systemCommand ??
                  (systemCommand = new RelayCommand(obj =>
                  {
                      CurrentCatalog = new DirectoryInfo(
                          Environment.GetFolderPath(Environment.SpecialFolder.System));
                      new WindowsDirectory(CurrentCatalog)
                          .Open(ref leftFiles);
                  }));
            }
        }
        private RelayCommand newFolderCommand;
        public RelayCommand NewFolderCommand
        {
            get
            {
                return newFolderCommand ??
                  (newFolderCommand = new RelayCommand(obj =>
                  {
                      if(CurrentCatalog != null)
                      {
                          var dir = CurrentCatalog.CreateSubdirectory("Новая папка");
                          LeftFiles.Add(new WindowsDirectory(dir));
                      }
                  }));
            }
        }
    }
}
