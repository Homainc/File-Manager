using FileManager.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace FileManager
{
    /// <summary>
    /// Interaction logic for ConfirmWindow.xaml
    /// </summary>
    public partial class ConfirmWindow : MetroWindow
    {
        private ObservableCollection<IWindowsFile> l;
        private IWindowsFile wf;
        public ConfirmWindow(IWindowsFile wf, ref ObservableCollection<IWindowsFile> list)
        {
            InitializeComponent();
            l = list;
            this.wf = wf;
            delName.Text = $"Вы действительно хотите удалить \"{wf.Name}\"?";
        }

        private void Yes_Click(object sender, RoutedEventArgs e)
        {
            l.Remove(wf);
            wf.Remove();
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
