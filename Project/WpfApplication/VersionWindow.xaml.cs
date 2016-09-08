using MahApps.Metro.Controls;
using System.Windows;

namespace WpfApplication
{
    public partial class VersionWindow : MetroWindow
    {
        public VersionWindow()
        {
            InitializeComponent();
        }

        void ClickClose(object sender, RoutedEventArgs e)
            => Close();
    }
}
