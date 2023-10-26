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

namespace DeweyDirectory
{
    /// <summary>
    /// Interaction logic for Failed.xaml
    /// </summary>
    public partial class Failed : Window
    {
        public ReplaceBook ReplaceBookWindow { get; set; }

        public Failed()
        {
            InitializeComponent();
            // set cursor
            SetCursor();

            // subscribe the close method to the Window_Closed method
            Closed += Window_Closed;
        }

        // method to set cursor
        private void SetCursor()
        {
            // variable to set cursor
            Cursor Sword;

            string cursorDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName + "\\Cursors";
            Sword = new Cursor($"{cursorDirectory}\\cursor.cur");
            this.Cursor = Sword;
        }
        private void retryBtn_Click(object sender, RoutedEventArgs e)
        {
            // close the window and reset opacity of the Replace Books opacity back to normal

            this.Close();
        }

        // method to check if the window is still open and will revert the opacity to normal
        private void Window_Closed(object sender, EventArgs e)
        {
            if (ReplaceBookWindow != null)
            {
                // restore the ReplaceBook window to the original opacity
                ReplaceBookWindow.Opacity = 1.0;
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            // return the user to the mainscreen
            MainWindow mw = new MainWindow();
            mw.Show();

            // close current window
            this.Close();

            if (ReplaceBookWindow != null)
            {
                // close the ReplaceBook window
                ReplaceBookWindow.Close();
            }
        }
    }
}
