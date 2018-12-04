using FileManager.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace FileManager
{
    public partial class OperationWindow : MetroWindow
    {
        private DirectoryInfo di;
        public OperationWindow(DirectoryInfo di)
        {
            InitializeComponent();
            this.di = di;
            ExecuteOperation();
        }
        public async void ExecuteOperation()
        {
            var tick = 100.0/(di.GetDirectories().Count() + di.GetFiles().Count());
            double progress = 0;
            ProgressBar.Value = progress;
            await Task.Run(() => {
                foreach (DirectoryInfo d in di.GetDirectories())
                {
                    progress += tick;
                    Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                    {
                        ItemText.Text = d.FullName;
                        ProgressBar.Value = progress;
                    }));
                    d.Delete(true);
                }
                foreach (FileInfo f in di.GetFiles())
                {
                    progress += tick;
                    Dispatcher.Invoke(DispatcherPriority.Background, new Action(() =>
                    {
                        ItemText.Text = f.FullName;
                        ProgressBar.Value = progress;
                    }));
                    f.Delete();
                }
            });
            Close();
        }
    }
}
