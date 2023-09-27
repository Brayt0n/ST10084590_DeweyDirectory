using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Media;
using System.Reflection.Emit;
using System.Security.Cryptography.X509Certificates;
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
    /// Interaction logic for ReplaceBook.xaml
    /// </summary>
    public partial class ReplaceBook : Window
    {
        #region Global Variables
        // global variables/declarations
        DoublyLinkedList<string> list = new DoublyLinkedList<string>();
        // variable to be used in timer
        private int timeLeft = 60;
        // create dispatcher timer
        DispatcherTimer timer = new DispatcherTimer(); // use system.threading
        // create dispatch timer for the font flash
        DispatcherTimer flashTimer = new DispatcherTimer();
        // bool variable to check if text is gray
        private bool isGray;
        #endregion

        #region Start Up
        public ReplaceBook()
        {
            InitializeComponent();

            // when the project loads
            // then the list gets pupulated by methods below
            // on start up, disable the lisbox and the checkBtn
            OpacityStartUp();
        }

        // method to invoke the class that generates the random call numbers
        public void GenerateNum()
        {
            // random number generator
            var generator = new RandomGenerator();

            // initialize a for loop to generate 10 different call numbers
            for (int i = 0; i < 10; i++)
            {
                list.Add(generator.RandomDewey());
            }
        }

        public void OpacityStartUp()
        {
            unsortedLb.IsEnabled = false; // set to false
            checkBtn.IsEnabled = false; // set to false
            checkBtn.Opacity = 0.2; // dim the button
        }
        #endregion

        #region Window Events
        // method to send to the listbox
        public void updatedListBox()
        {
            unsortedLb.Items.Clear();
            foreach (var item in list)
            {
                unsortedLb.Items.Add(item);
            }
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

        #region Timer
        private void startBtn_Click(object sender, RoutedEventArgs e)
        {
            // play button sound
            // pulls straight from debug folder
            SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\startEffect.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
            player.Play();

            // call methods
            GenerateNum();// generate call num
            updatedListBox();// update listbox

            // set score text back to default
            scoreText.Text = "0pts";
            // clear the sorted listbox
            sortedLb.Items.Clear();

            // end the flash timer and set text back to gray
            flashTimer.Stop();
            playerScoreText.Foreground = Brushes.Gray;
            scoreText.Foreground = Brushes.Gray;

            // timer load activity
            timer.Interval = TimeSpan.FromSeconds(1); // uses seconds

            // create the tick method --> will be generated below
            timer.Tick += Tick;
            timer.Start();
        }

        private void Tick(object sender, EventArgs e)
        {
            if (timeLeft > 0)
            {
                // re-enable the listbox and checkBtn here
                unsortedLb.IsEnabled = true;
                checkBtn.IsEnabled = true;
                checkBtn.Opacity = 1; // set the button opacity back to normal

                timeLeft--; // it is a count down, therefore we decrement

                timerText.Text = timeLeft.ToString();

                // gray out button while timer ticks
                startBtn.IsEnabled = false;
                // set opacity for stying
                startBtn.Opacity = 0.2;
            }
            else 
            {
                // play failed tone 
                // play button sound
                // pulls straight from debug folder
                SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\failTone.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
                player.Play();

                // end the timer and display message
                timer.Stop();

                // reset timer to initial value
                ResetTimer();

                // open failed window
                Failed f = new Failed();
                f.ReplaceBookWindow = this;
                f.Show();

                // set current window's opacity lower
                this.Opacity = 0.2;

                // renable the start button
                startBtn.IsEnabled = true;
                // set opacity back to normal
                startBtn.Opacity = 1;

                // disable the listbox here and checkBtn here
                OpacityStartUp();

                list.Clear();// clear the list first
            }
        }

        public void ResetTimer()
        {
            // unsubsrcibe timer.Tick from Tick()
            timer.Tick -= Tick;
            // reset time back to initial value
            timeLeft = 60;
            // update textbox
            timerText.Text = timeLeft.ToString();
        }
        #endregion

        #region Doubly Linked List Class
        public class DoublyLinkedList<T> : IEnumerable<T>
        {

            public class Node
            {
                public T Value { get; set; }
                public Node Next { get; set; }
                public Node Previous { get; set; }
            }
            public Node head; // outside of the class as its not needed to be defined multiple times
            public Node tail;

            // method to add values
            public void Add(T Value)
            {
                Node newNode = new Node { Value = Value };
                if (head == null) // if head is null then a new list is created
                {
                    head = newNode;
                    tail = newNode;
                }
                else // if not then a tail is added
                {
                    tail.Next = newNode;
                    newNode.Previous = tail;
                    tail = newNode;
                }
            }

            public IEnumerator<T> GetEnumerator() // <T> --> list of t -- allows for different datatypes
            {
                Node current = head;
                while (current != null)
                {
                    yield return current.Value;
                    current = current.Next;
                }
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return ((IEnumerable<T>)this).GetEnumerator(); // checks compatability
            }

            public int IndexOf(T value) // 
            {
                Node current = head;
                int index = 0;
                while (current != null)
                {
                    // it needs to compare the 2 values
                    if (EqualityComparer<T>.Default.Equals(current.Value, value)) // compares current value to the node you are moving it to
                        return index;
                    index++;
                    current = current.Next;

                    // once the position --> is item is sorted
                    // take away that slot as avail
                }
                return index - 1;
            }

            // method to move node
            public void Move(int sourceIndex, int destinationIndex)
            {
                //checks while you move things around in this list?
                if (sourceIndex < 0 || sourceIndex >= Count || destinationIndex < 0 || destinationIndex >= Count)
                    return;

                Node sourceNode = GetNodeAtIndex(sourceIndex); // will add the method in a bit
                Node destNode = GetNodeAtIndex(destinationIndex); // will add the method in a bit

                T temp = sourceNode.Value;
                sourceNode.Value = destNode.Value; // flipping them around
                destNode.Value = temp;  // flipping them around
            }

            // method to clear the node
            public void Clear()
            {
                Node current = head;

                while (current != null)
                {
                    Node temp = current;
                    current = current.Next;
                    temp = null; // Ensure the node is garbage collected
                }

                head = null;
                tail = null;
            }


            private Node GetNodeAtIndex(int index)
            {
                // right click and choose generate method above
                // remove the throws exception
                Node current = head;
                for (int i = 0; i < index && current != null; i++)
                {
                    current = current.Next;
                }

                return current;
            }

            // get index count
            public int Count
            {
                get
                {
                    int count = 0;
                    Node current = head;
                    while (current != null)
                    {
                        count++;
                        current = current.Next;
                    }
                    return count;
                }
            }

            // method to sort the sorted listbox
            /*
             * the algorithm we used for the sorting below is the Bubble Sort algorithm
             *      - as we compared the nodes in their current order
             *      - then we swopped them if they were in the wrong order
             */
            public void Sort()
            {
                // list for temp storage
                List<Node> nodeList = new List<Node>();
                Node current = head;

                // checks current node status
                while (current != null)
                {
                    nodeList.Add(current);
                    current = current.Next;
                }

                // this is where the bubble sort takes place
                int n = nodeList.Count;
                for (int i = 0; i < n - 1; i++)
                {
                    for (int j = 0; j < n - i - 1; j++)
                    {
                        int xNumeric = ExtractNumericValue(nodeList[j].Value);
                        int yNumeric = ExtractNumericValue(nodeList[j + 1].Value);

                        if (xNumeric > yNumeric)
                        {
                            // swap the nodes --> if they were in the wromg order
                            Node temp = nodeList[j];
                            nodeList[j] = nodeList[j + 1];
                            nodeList[j + 1] = temp;
                        }
                    }
                }

                head = nodeList.First();
                tail = nodeList.Last();

                for (int i = 0; i < nodeList.Count; i++)
                {
                    nodeList[i].Previous = (i > 0) ? nodeList[i - 1] : null;
                    nodeList[i].Next = (i < nodeList.Count - 1) ? nodeList[i + 1] : null;
                }
            }

            private int ExtractNumericValue(T value)
            {
                // convert the value to a string and extract the first three numeric characters
                string stringValue = value.ToString();
                string numericPart = string.Concat(stringValue.TakeWhile(char.IsDigit));

                if (int.TryParse(numericPart, out int result))
                    return result;

                return int.MaxValue; // return a high value if parsing fails
            }
        }
        #endregion

        #region Call Numbers Generator Class
        // create a class to randomly generate the call numbers
        public class RandomGenerator
        {
            // instantiation of the random number generator
            private readonly Random _random = new Random();

            // a number is genrated within a range set by min and max
            public int RandomNumber(int min, int max)
            {
                return _random.Next(min, max);
            }

            // a random string is generated with a fixed size
            public string RandomString(int size, bool lowerCase = false)
            {
                var builder = new StringBuilder(size);


                // char is a single Unicode character
                char offset = lowerCase ? 'a' : 'A';
                const int lettersOffset = 26; // 26 represents the number of letters in the alphabet

                for (var i = 0; i < size; i++)
                {
                    var @char = (char)_random.Next(offset, offset + lettersOffset);
                    builder.Append(@char);
                }

                return lowerCase ? builder.ToString().ToLower() : builder.ToString();
            }

            // generates the random call number whilst using the above methods to parse arguments
            public string RandomDewey()
            {
                var deweyBuilder = new StringBuilder();

                /*
                 * Format for the call number
                 *      - Must include 3 numbers
                 *      - Then a period
                 *      - Then 2 other numbers
                 *      - A space
                 *      - Then finally a code consisting of 3 uppercase letters
                 *      - E.g. according to POE: 005.73 JAM
                 */

                // following the format above
                // first 3 numbers
                deweyBuilder.Append(RandomNumber(100, 999)); // min 100; max 999

                // append period
                deweyBuilder.Append('.');

                // append the next 2 numbers
                deweyBuilder.Append(RandomNumber(0, 9));
                deweyBuilder.Append(RandomNumber(0, 9));

                // append the space
                deweyBuilder.Append(' ');

                // finally, append the book code
                deweyBuilder.Append(RandomString(3));

                return deweyBuilder.ToString();
            }

        }
        #endregion

        #region Drag and Drop
        private void ListBoxItem_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ListBoxItem draggedItem = sender as ListBoxItem; // allows uou to drag drop
                if (draggedItem != null)
                {
                    // allow the drag drop to happen
                    DragDrop.DoDragDrop(draggedItem, draggedItem.Content,
                        DragDropEffects.Move);
                }
            }
        }

        private void ListBoxItem_Drop(object sender, DragEventArgs e)
        {
            ListBoxItem targetItem = sender as ListBoxItem;
            if (targetItem != null)
            {
                int targetIndex = unsortedLb.Items.IndexOf(targetItem.Content);
                int draggedIndex = list.IndexOf((string)e.Data.GetData(typeof(string)));

                if (targetIndex >= 0 && draggedIndex >= 0)
                {
                    list.Move(draggedIndex, targetIndex);
                    updatedListBox();
                }
            }
        }
        #endregion

        #region Score User
        private void checkBtn_Click(object sender, RoutedEventArgs e)
        {
            // play button sound
            // pulls straight from debug folder
            SoundPlayer player = new SoundPlayer(new Uri(string.Format("{0}\\Sounds\\successTone.wav", AppDomain.CurrentDomain.BaseDirectory)).ToString());
            player.Play();

            // stop the timer
            timer.Stop();

            UpdateSortedListBox();// udate sorted lb
            Score();// issue user score
            FlashyOutput();

            // re-enable the start button
            startBtn.IsEnabled = true;
            //set start button opacity back to normal
            startBtn.Opacity = 1;
            startBtn.Content = "RETRY";
            // clear the doubly linked list
            list.Clear();

            // return to startup state
            OpacityStartUp();

            // reset timer
            ResetTimer();
        }

        private void UpdateSortedListBox()
        {
            // sort the doubly linked list
            list.Sort();

            // clear the second ListBox
            sortedLb.Items.Clear();

            foreach (var item in list)
            {
                sortedLb.Items.Add(item);
            }
        }

        private void Score()
        {
            // clear any previous scores
            int score = 0;

            // iterate through the items in both list boxes and compare
            for (int i = 0; i < unsortedLb.Items.Count; i++)
            {
                if (i < unsortedLb.Items.Count && unsortedLb.Items[i].ToString() == sortedLb.Items[i].ToString())
                {
                    score++;
                }
            }

            // calculate the score out of 1000 --> each point is worth 100 points
            double userScore = (double)score / unsortedLb.Items.Count * 1000;


            // display the user's score in the scoreText textbox
            scoreText.Text = $"{userScore}pts";
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
            // Toggle the background color between gray and white
            Brush textColor = isGray ? Brushes.Gray : Brushes.White;
            playerScoreText.Foreground = textColor;
            scoreText.Foreground = textColor;

            // Toggle the flag for the next tick
            isGray = !isGray;
        }
        #endregion
    }
}

