using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
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
    /// Interaction logic for NewGame.xaml
    /// </summary>
    public partial class NewGame : Window
    {
        // obj instantiation
        public IdentifyAreas identifyAreasWindow { get; set; }

        public NewGame()
        {
            InitializeComponent();
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

        private void yesBtn_Click(object sender, RoutedEventArgs e)
        {
            // will compile logic if the user has selected yes to start a new game
            // set dialogresult to true --> as the user selected the option of a new game
            // play button sound
            // pulls straight from debug folder
            SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\startSound.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
            player.Play();

            DialogResult = true;

            // close this window prompt
            this.Close();
        }

        private void noBtn_Click(object sender, RoutedEventArgs e)
        {
            // set dialogresult to false
            DialogResult = false;

            // close this window
            this.Close();
        }

        // method to check if the window is still open and will revert the opacity to normal
        private void Window_Closed(object sender, EventArgs e)
        {
            if (identifyAreasWindow != null)
            {
                // restore the ReplaceBook window to the original opacity
                identifyAreasWindow.Opacity = 1.0;
            }
        }
    }
}
