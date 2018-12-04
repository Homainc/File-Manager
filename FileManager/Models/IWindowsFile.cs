using FileManager.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Models
{
    public interface IWindowsFile
    {
        string Name { get; }
        string Path { get; }
        string Size { get; }
        string Icon { get; }
        string Date { get; }
        bool Open(ref ObservableCollection<IWindowsFile> list);
        bool RequestRemove(ref ObservableCollection<IWindowsFile> list);
        bool Rename(string name);
        bool Remove();
        bool RequestRename();
    }
}
