using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfDispatcherDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public bool showDialog(string text)
        {
            //if (!Dispatcher.CheckAccess())
            //{
            //    // We're not in the UI thread, ask the dispatcher to call this same method in the UI thread, then exit
            //    Dispatcher.BeginInvoke(new Action(showDialog(text)));
            //    return;
            //}

            var r = MessageBox.Show(text, "Title", MessageBoxButton.YesNo);

            return r == MessageBoxResult.Yes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Thread thread = new Thread(UpdateText);
            thread.Start();
        }

        private void UpdateText()
        {
            this.TextBox1.Text = showDialog("Bla bla bla") ? "YES ASDASD" : "NOOOOOOOOOOOOOOOOOO";
        }
    }
}
