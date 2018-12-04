using FileManager.Models;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for Rename.xaml
    /// </summary>
    public partial class Rename : MetroWindow
    {
        private IWindowsFile file;
        public Rename(IWindowsFile file)
        {
            InitializeComponent();
            this.file = file;
            renameText.Text = file.Name;
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            file.Rename(renameText.Text);
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e) => Close();
    }
}
