using System.Collections.ObjectModel;
using System.IO;

namespace FileManager.Models
{
    public class RootDirectory : IWindowsFile
    {
        private string path;
        public string Name => "..";
        public string Path => path;
        public string Size => "";
        public string Icon => "/res/icons/root.png";
        public string Date => null;
        public RootDirectory(string path) => this.path = path;
        public bool Open(ref ObservableCollection<IWindowsFile> list)
        {
            return new WindowsDirectory(new DirectoryInfo(path)).Open(ref list);
        }
        public bool RequestRemove(ref ObservableCollection<IWindowsFile> list) => false;
        public bool Rename(string name) => false;
        public bool Remove() => false;
        public bool RequestRename() => false;
    }
}
