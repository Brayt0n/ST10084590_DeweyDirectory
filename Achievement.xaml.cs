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
using System.Windows.Threading;

namespace DeweyDirectory
{
    /// <summary>
    /// Interaction logic for Achievement.xaml
    /// </summary>
    public partial class Achievement : Window
    {
        // varaibles
        // created a BitmapImage --> this is for the chesr
        BitmapImage bitmapImage = new BitmapImage();
        BitmapImage badgeImage = new BitmapImage(); // bitmap image for the badge
        // obj instantiation
        public IdentifyAreas identifyAreasWindowOpened { get; set; }
        // timer for flashing output
        DispatcherTimer flashTimer = new DispatcherTimer();
        private bool isCurrentColor;

        public Achievement()
        {
            InitializeComponent();
            // set cursor
            SetCursor();

            // subscribe the close method to the Window_Closed method
            Closed += Window_Closed;
            // allows the button to flash
            FlashyOutput();
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

        private void openChestBtn_Click(object sender, RoutedEventArgs e)
        {
            // change the image of the chest when button is clicked
            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri("Assets/pixelChestOpened.png", UriKind.RelativeOrAbsolute);
            bitmapImage.EndInit();

            chestImage.Source = bitmapImage;

            // pulls straight from debug folder
            // plays chest opening sound
            SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\openChest.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
            player.Play();

            // disable the button to prevent errors
            openChestBtn.IsEnabled = false;
            flashTimer.Stop();

            // switch case to determine what badge to return to the user
            switch (Global.noCorrect)
            {
                case 0:
                    // call the fail badge
                    FailBadge();
                    break;
                case 1:
                    // call the 1st badge
                    FirstBadge();
                    break;
                case 2:
                    // call the 2nd badge
                    SecondBadge();
                    break;
                case 3:
                    // call the 3rd badge
                    ThirdBadge();
                    break;
                case 4:
                    // call the 4th badge
                    FourthBadge();
                    break;
            }

            // refert button back to its original colors
            openChestBtn.Background = Brushes.Transparent;
            openChestBtn.Foreground = Brushes.White;
        }

        private void close_btn_Click(object sender, RoutedEventArgs e)
        {
            // close the achievement pop up
            this.Close();
        }

        // method to check if the window is still open and will revert the opacity to normal
        private void Window_Closed(object sender, EventArgs e)
        {
            if (identifyAreasWindowOpened != null)
            {
                // restore the ReplaceBook window to the original opacity
                identifyAreasWindowOpened.Opacity = 1.0;
            }
        }

        private void FlashyOutput()
        {
            // a new timer was initialized to prevent a clash between the other timer used in the code
            flashTimer = new DispatcherTimer();
            // flashes every 500 milliseconds
            flashTimer.Interval = TimeSpan.FromMilliseconds(500);
            // subscribe to the Colour_Tick method
            flashTimer.Tick += Colour_Tick;

            // Start the timer
            flashTimer.Start();
        }

        private void Colour_Tick(object sender, EventArgs e)
        {
            // toggle the background color between transparent and white
            Brush buttonColor = isCurrentColor ? Brushes.Transparent : Brushes.White;
            openChestBtn.Background = buttonColor;

            // toggle the foreground color between black and white
            Brush textColor = isCurrentColor ? Brushes.White : Brushes.Black;
            openChestBtn.Foreground = textColor;

            // toggle the flag for the next tick
            isCurrentColor = !isCurrentColor;
        }

        #region Badges
        // badges belwo
        private void FailBadge()
        {
            // set the image for the badge
            badgeImage.BeginInit();
            badgeImage.UriSource = new Uri("Assets/junkBadge.jpg", UriKind.RelativeOrAbsolute);
            badgeImage.EndInit();

            badge.Source = badgeImage;
        }

        private void FirstBadge()
        {
            // set the image for the badge
            badgeImage.BeginInit();
            badgeImage.UriSource = new Uri("Assets/pixelSword.jpg", UriKind.RelativeOrAbsolute);
            badgeImage.EndInit();

            badge.Source = badgeImage;
        }

        private void SecondBadge()
        {
            // set the image for the badge
            badgeImage.BeginInit();
            badgeImage.UriSource = new Uri("Assets/pixelGoblet.jpg", UriKind.RelativeOrAbsolute);
            badgeImage.EndInit();

            badge.Source = badgeImage;
        }

        private void ThirdBadge()
        {
            // set the image for the badge
            badgeImage.BeginInit();
            badgeImage.UriSource = new Uri("Assets/pixelNecklace.jpg", UriKind.RelativeOrAbsolute);
            badgeImage.EndInit();

            badge.Source = badgeImage;
        }

        private void FourthBadge()
        {
            // set the image for the badge
            badgeImage.BeginInit();
            badgeImage.UriSource = new Uri("Assets/pixelCrown.jpg", UriKind.RelativeOrAbsolute);
            badgeImage.EndInit();

            badge.Source = badgeImage;
        }
        #endregion
    }
}
