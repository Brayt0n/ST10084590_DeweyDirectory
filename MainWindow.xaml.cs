using DeweyDirectory.Properties;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DeweyDirectory
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // public media variable
        private MediaPlayer mp = new MediaPlayer();

        public MainWindow()
        {
            InitializeComponent();

            // setup cursor
            SetCursor();

            // get file path of theme
            mp.Open(new Uri(string.Format("{0}\\deweyNewTheme.mp3", AppDomain.CurrentDomain.BaseDirectory)));
            // loop theme when over
            mp.MediaEnded += new EventHandler(Media_Ended);

            if ((bool)Settings.Default["music"] == true) 
            { 
                mp.Play();
            }
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

        private void Media_Ended(object sender, EventArgs e)
        {
            if ((bool)Settings.Default["music"] == true)
            {
                mp.Position = TimeSpan.Zero;
                mp.Play();
            }
        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            // exit protocall for app
            Application.Current.Shutdown();
        }

        private void bookSortBtn_Click(object sender, RoutedEventArgs e)
        {
            // button to direct user to the Replace Books feature -- after splash screen
            // play button sound
            // pulls straight from debug folder
            SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\newSelectSound.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
            player.Play();
            
            // create new object
            Splash s = new Splash();
            // show splash screen
            s.Show();

            // set opacity after splash appears
            this.Opacity = 0.2;

            // call load time method -- need this window to close after the next closes
            LoadTime();

            // stop dewey theme
            mp.Stop();

            // disable window on click
            this.IsEnabled = false;

            // new feature added --> need to add different logic for splash
            Global.isReplaceSelected = true; // set to true

        }

        // timer method with variable
        DispatcherTimer timer = null;
        void LoadTime()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += new EventHandler(timer_Elapsed);
            timer.Start();
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            // button to direct user to the Identify Areas feature -- after splash screen
            // play button sound
            // pulls straight from debug folder
            SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\newSelectSound.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
            player.Play();

            // create new object
            Splash s = new Splash();
            // show splash screen
            s.Show();

            // set opacity after splash appears
            this.Opacity = 0.2;

            // call load time method -- need this window to close after the next closes
            LoadTime();

            // stop dewey theme
            mp.Stop();

            // disable window on click
            this.IsEnabled = false;

            // set the identify areas selected to true --> so set other variable as false
            Global.isReplaceSelected = false; 
        }

        void timer_Elapsed(object sender, EventArgs e)
        {
            timer.Stop();

            // close this window
            this.Close();
        }
    }
}
