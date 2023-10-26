using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace DeweyDirectory
{
    // class will be used to move the items of the list box in the IdentifyAreas.xaml.cs
    public class Controls
    {
        // method to swap indexs -- 1- 7
        public static void ChangeIndex(int move, ListBox listBox)

        {
            // first ensure the item is selected from the list box

            if (listBox.SelectedItem == null || listBox.SelectedIndex < 0)
            {
                return;
            }
            // target destination

            int changedIndex = listBox.SelectedIndex + move;
            // ensure new destination exists

            if (changedIndex < 0 || changedIndex >= listBox.Items.Count)
            {

                return;
            }

            // object selected

            object selected = listBox.SelectedItem;
            // insert into a new location
            listBox.Items.Remove(selected);
            listBox.Items.Insert(changedIndex, selected);
            listBox.SelectedIndex = changedIndex;
        }

        public static void GenerateRandomList(ListBox listBox)
        {
            // new list type of
            var list = new List<string>();
            Random random = new Random(); // to gen a random list every

            list = listBox.Items.Cast<string>().ToList();

            //shuffle the list of items

            int n = list.Count;
            while (n > 1)
            {
                int k = random.Next(n);
                n--; // decrements the value
                string value = list[k];
                list[k] = list[n]; //swapping
                list[n] = value;
            }

            listBox.Items.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                listBox.Items.Add(list[i]);
            }
        }
    }
}

