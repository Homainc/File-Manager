using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FileManager.Models
{
    public class WindowsFile : IWindowsFile, INotifyPropertyChanged
    {
        private FileInfo fi;
        public string Name => fi.Name;
        public string Path => fi.FullName;
        public string Size => fi.Length.ToString();
        public string Icon => "/res/icons/unknown_file.png";
        public string Date => fi.LastWriteTime.ToString();

        public WindowsFile(FileInfo fi)
        {
            this.fi = fi;
        }

        public bool Open(ref ObservableCollection<IWindowsFile> list)
        {
            var proc = new Process();
            proc.StartInfo.FileName = fi.FullName;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
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
            fi.Delete();
            return true;
        }

        public bool Rename(string name)
        {
            fi.MoveTo(fi.FullName.Replace(fi.Name, name));
            OnPropertyChanged("Name");
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
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
