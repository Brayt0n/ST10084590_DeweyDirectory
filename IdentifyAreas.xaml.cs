using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
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
    /// Interaction logic for IdentifyAreas.xaml
    /// </summary>
    public partial class IdentifyAreas : Window
    {
        #region Variables
        // varaibles to be used throughout the class
        public static int questCount = 4; // question count
        public static bool callQuest = true;
        public static int maxCorrect; // maximum value of questions correct
        Random rnd = new Random(); // random object initialized
        IDictionary<string, string> baseQuestions = new Dictionary<string, string>();
        IDictionary<string, string> usedQuestions = new Dictionary<string, string>();
        // timer for flashing output
        DispatcherTimer flashTimer = new DispatcherTimer();
        private bool isGray;
        #endregion

        #region Startup
        public IdentifyAreas()
        {
            InitializeComponent();
            // set cursor
            SetCursor();
            // when class loads
            // housekeeping() before start
            StartupMeth();
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

        public void StartupMeth()
        {
            questionsCol.IsEnabled = false; // set to false
            answersCol.IsEnabled = false; // set to false
            submitBtn.IsEnabled = false; // disable the button
            submitBtn.Opacity = 0.2; // dim the button
            arrowUpBtn.IsEnabled = false; // disable the arrow buttons
            arrowDownBtn.IsEnabled = false;

            // set arrow button opacities
            arrowUpBtn.Opacity = 0.2;
            arrowDownBtn.Opacity = 0.2;
        }

        public void ResetDesign()
        {
            // re-enable newGame button
            newGameBtn.IsEnabled = true;
            newGameBtn.Opacity = 1; // revert to original opacity
            submitBtn.IsEnabled = false; // disable the button
            submitBtn.Opacity = 0.2; // dim the button
            arrowUpBtn.IsEnabled = false; // disable the arrow buttons
            arrowDownBtn.IsEnabled = false;

            // set arrou button opacities
            arrowUpBtn.Opacity = 0.2;
            arrowDownBtn.Opacity = 0.2;
        }

        private void loadDefaults()
        {
            // housekeeping()
            baseQuestions.Clear();  
            usedQuestions.Clear();

            //list of possible questions
            baseQuestions.Add("000-099", "GENERAL WORKS");
            baseQuestions.Add("100-199", "PHILOSOPHY AND PSYCOLOGY");
            baseQuestions.Add("200-299", "RELIGION");
            baseQuestions.Add("300-399", "SOCIAL SCIENCES");
            baseQuestions.Add("400-499", "LANGUAGE");
            baseQuestions.Add("500-599", "NATURAL SCIENCES");
            baseQuestions.Add("600-699", "TECHNOLOGY");
            baseQuestions.Add("700-799", "THE ARTS");
            baseQuestions.Add("800-899", "LITERATURE AND RHETORIC");
            baseQuestions.Add("900-999", "HISTORY, BIOGRAPHY AND GEOGRAPHY");
            
        }
        #endregion

        #region Tree and Calculation of Score
        internal static void FindChildren<T>(List<T> results, DependencyObject startNode) where T : DependencyObject
        {
            int count = VisualTreeHelper.GetChildrenCount(startNode);

            for (int i = 0; i < count; i++)
            {
                DependencyObject current = VisualTreeHelper.GetChild(startNode, i);
                if ((current.GetType()).Equals(typeof(T)) || (current.GetType()).GetTypeInfo().IsSubclassOf(typeof(T)))
                {
                    T asType = (T)current;
                    results.Add(asType);
                }

                FindChildren<T>(results, current);
            }
        }

        // method to calculate score
        private int CalcScore()
        {
            int score = 0;
            List<ListBoxItem> list = new List<ListBoxItem>();
            FindChildren(list, answersCol);
            for (int i = 0; i < questCount; i++)
            {

                string callNumber;
                string description;
                if (!callQuest)
                {
                    callNumber = questionsCol.Items[i].ToString();
                    description = answersCol.Items[i].ToString();
                }
                else // flip them around
                {
                    callNumber = answersCol.Items[i].ToString();
                    description = questionsCol.Items[i].ToString();
                }
                if (usedQuestions[callNumber] == description)
                {
                    // green color to notify user on correct choice
                    list[i].Background = new SolidColorBrush(Colors.Green);

                    //add onto the score
                    score++;
                }
                else
                {
                    // red color to notify user on incorrect choice
                    list[i].Background = new SolidColorBrush(Colors.Red);
                }

            }
            for (int i = questCount; i < answersCol.Items.Count; i++)
            {
                // gray color to represent other choices existent outside of the 4 chosen ones
                list[i].Background = new SolidColorBrush(Colors.Gray);
            }

            // return the user score
            return score;
        }
        #endregion

        #region Button Logic
        private void arrowUpBtn_Click(object sender, RoutedEventArgs e)
        {
            // up btn --> changes index to one up
            Controls.ChangeIndex(-1, answersCol);
        }

        private void arrowDownBtn_Click(object sender, RoutedEventArgs e)
        {
            // down btn --> changes index to one down
            Controls.ChangeIndex(1, answersCol);
        }

        private void newGameBtn_Click(object sender, RoutedEventArgs e)
        {
            // set current window's opacity lower --> on user click
            this.Opacity = 0.2;

            // open new game prompt window
            NewGame ng = new NewGame();
            ng.identifyAreasWindow = this;

            // show the window as a dialog this time
            bool? result = ng.ShowDialog();

            // the result is determined on DialogResult in the NewGame.xaml.cs
            if (result == true) 
            {
                // housekeeping() before code
                // stop the flashy timer
                flashTimer.Stop();
                numberCorrectText.Foreground = Brushes.Gray;
                correctText.Foreground = Brushes.Gray;

                newGameBtn.IsEnabled = false; // set to false -- prevents errors
                newGameBtn.Opacity = 0.2;

                // re-enable the listboces
                questionsCol.IsEnabled = true;
                answersCol.IsEnabled = true;

                // re-enable the submit button
                submitBtn.IsEnabled = true;
                submitBtn.Opacity = 1;

                // initialize a new game
                // set scores and texts
                Global.noCorrect = 0;
                maxCorrect = 0;
                correctText.Text = "0/4";

                // call methods
                loadDefaults();
                repopulateLists();

                // re-enable buttons
                arrowUpBtn.IsEnabled = true;
                arrowDownBtn.IsEnabled = true;

                // reset opacities back to normal
                arrowUpBtn.Opacity = 1;
                arrowDownBtn.Opacity = 1;
            }
            else // User clicked "No" or closed the dialog
            {
                return;
            }
        }

        private void submitBtn_Click(object sender, RoutedEventArgs e)
        {
            // Submit btn
            // submits answers to be checked 
            // pull the method -- setup the textblocks

            // play button sound
            // pulls straight from debug folder
            SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\completedTask.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
            player.Play();

            int score = CalcScore();

            arrowUpBtn.IsEnabled = false;
            arrowDownBtn.IsEnabled = false;

            // any other btns or features you want to disable st this point

            // load the defaults
            loadDefaults();
            Global.noCorrect = Global.noCorrect + score; // add to the score
            maxCorrect = maxCorrect + questCount; //checks that you cant execute    

            // badge gamification feature
            // show window --> set opacity first
            this.Opacity = 0.2; // sets opacity for current window

            Achievement a = new Achievement(); // instantiate obj
            a.identifyAreasWindowOpened = this; // logic in the other class to revert this window to normal opacity when the other is closed
            a.Show();

            // display text in a flashy way
            FlashyOutput(); // starts the timer
            correctText.Text = Global.noCorrect + "/" + maxCorrect;

            //housekeeping()
            ResetDesign();
        }

        private void homeBtn_Click(object sender, RoutedEventArgs e)
        {
            // method to close the current window and return home
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();

            // close this window
            this.Close();
        }
        #endregion

        #region Ditctionary
        //Dictionary --> kvp --> method
        private void getKVP(out string call, out string desc)
        {
            KeyValuePair<string, string> kvp;
            int index = rnd.Next(baseQuestions.Count());
            kvp = baseQuestions.ElementAt(index);
            usedQuestions.Add(kvp);
            baseQuestions.Remove(kvp);
            call = kvp.Key;
            desc = kvp.Value;
        }

        private void repopulateLists()
        {
            questionsCol.Items.Clear();
            answersCol.Items.Clear();
            //Alternate between call numbers and descriptions
            if (callQuest)
            {

                //Generate 4 callNo + 4 desc
                for (int i = 0; i < 4; i++)
                {
                    getKVP(out string callNo, out string desc);
                    questionsCol.Items.Add(callNo);
                    answersCol.Items.Add(desc);
                }
                //Generate 3 more desc
                for (int i = 0; i < 3; i++)
                {
                    getKVP(out _, out string desc);
                    answersCol.Items.Add(desc);
                }
                //prep alt
                callQuest = false;

            }
            else
            {
                //Generate 4 desc + 4 callNos
                for (int i = 0; i < 4; i++)
                {
                    getKVP(out string callNo, out string desc);
                    questionsCol.Items.Add(desc);
                    answersCol.Items.Add(callNo);
                }

                //Generate 3 more callNos
                for (int i = 0; i < 3; i++)
                {
                    getKVP(out string callNo, out _);
                    answersCol.Items.Add(callNo);
                }
                //prep alt
                callQuest = true;

            }
            //TODO randomize lists
            Controls.GenerateRandomList(questionsCol);
            Controls.GenerateRandomList(answersCol);
        }
        #endregion

        #region Misc
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
            // Toggle the background color between gray and white
            Brush textColor = isGray ? Brushes.Gray : Brushes.White;
            numberCorrectText.Foreground = textColor;
            correctText.Foreground = textColor;

            // Toggle the flag for the next tick
            isGray = !isGray;
        }

        private void Badge()
        {
            
        }
        #endregion
    }
}
