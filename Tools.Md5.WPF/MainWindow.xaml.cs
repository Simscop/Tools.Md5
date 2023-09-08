using System.Diagnostics;
using System.Linq;
using Tools.Md5.Core;
using Tools.Md5.Core.Models;

namespace Tools.Md5.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : HandyControl.Controls.Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //this.FilesGrid.SelectedCellsChanged += FilesGrid_SelectedCellsChanged;
        }

        //private void FilesGrid_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
        //{
        //    if (this.DataContext is not Md5ViewModel vm) return;

        //    var select = FilesGrid.SelectedItems;
        //    if (select.Count == 0) return;

        //    vm.SelectedFiles.Clear();

        //    //foreach (var item in select) vm.SelectedFiles.Add((FileInfo)item);
        //}
    }
}
