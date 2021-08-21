using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // start not UI thread
            Thread thread = new Thread(UpdateTextNonUiThread);
            thread.Start();
        }

        private void UpdateTextNonUiThread()
        {
            // Try from non UI thread
            var b = showDialog("Bla bla bla");
            // dispatch TextBox update to UI thread
            Dispatcher.Invoke(() => this.TextBox1.Text = b ? "YES ASDASD" : "NOOOOOOOOOOOOOOOOOO");
        }

        public bool showDialog(string text)
        {
            if (!Dispatcher.CheckAccess())
            {
                // We're not in the UI thread, ask the dispatcher to call this same method in the UI thread
                var onUiThread = Dispatcher.BeginInvoke(new Func<bool>(() => { return showDialog(text); }));
                // wait for UI thread result
                while (onUiThread.Status == DispatcherOperationStatus.Pending || onUiThread.Status == DispatcherOperationStatus.Executing)
                {
                    Thread.Sleep(100);//💩 active wait...
                }
                if (onUiThread.Status != DispatcherOperationStatus.Completed)
                {
                    throw new Exception("showDialog crashed");
                }
                // pass it back to non ui thread
                return (bool)onUiThread.Result;
            }

            // were on UI Thread, show popup
            var r = MessageBox.Show(text, "Title", MessageBoxButton.YesNo);
            return r == MessageBoxResult.Yes;
        }
    }
}
