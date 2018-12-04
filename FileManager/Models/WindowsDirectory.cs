using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FileManager.Models
{
    public class WindowsDirectory : IWindowsFile, INotifyPropertyChanged
    {
        private DirectoryInfo di;
        public string Name => di.Name;
        public string Path => di.FullName;
        public string Icon => "res/icons/dir_icon.png";
        public string Size => "Папка";
        public string Date => di.LastWriteTime.ToString();

        public WindowsDirectory(DirectoryInfo di)
        {
            this.di = di;
        }

        public bool Open(ref ObservableCollection<IWindowsFile> list)
        {
            list.Clear();
            if (di.Parent != null)
                list.Add(new RootDirectory(di.Parent.FullName));
            foreach (IWindowsFile dir in di.GetDirectories().Select(x => new WindowsDirectory(x)))
            {
                list.Add(dir);
            }
            foreach (IWindowsFile file in di.GetFiles().Select(x => new WindowsFile(x)))
            {
                list.Add(file);
            }
            return true;
        }

        public bool RequestRemove(ref ObservableCollection<IWindowsFile> list)
        {
            var main = Application.Current.MainWindow;
            var win = new ConfirmWindow(this, ref list);
            win.Owner = main;
            win.Show();
            win.Focus();
            return true;
        }

        public bool Remove()
        {
            di.Delete(true);
            return true;
        }

        public bool RequestRename()
        {
            var main = Application.Current.MainWindow;
            var win = new Rename(this);
            win.Show();
            win.Focus();
            return true;
        }

        public bool Rename(string name)
        {
            di.MoveTo(di.FullName.Replace(di.Name, name));
            OnPropertyChanged("Name");
            return true;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
