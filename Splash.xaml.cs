﻿using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace DeweyDirectory
{
    /// <summary>
    /// Interaction logic for Splash.xaml
    /// </summary>
    public partial class Splash : Window
    {
        public Splash()
        {
            InitializeComponent();

            // method call
            LoadTime();
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

        void timer_Elapsed(object sender, EventArgs e)
        {
            timer.Stop();

            // instantiate objects for both classes
            ReplaceBook rb = new ReplaceBook();
            IdentifyAreas ia = new IdentifyAreas();

            // check logic to determine which screen to navigate to
            if (Global.isReplaceSelected)
            {
                // will display replace books screen
                rb.Show();
            } else
            {
                // will display identify areas screen
                ia.Show();
            }

            this.Close();
        }
    }
}
