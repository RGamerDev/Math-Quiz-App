﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Math_Quiz.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.contentFrame.Navigate(typeof(HubPage));
            nav.SelectedItem = nav.MenuItems[0];
        }

        private void Navigate(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if ((string)((NavigationViewItem)args.SelectedItem).Tag == "Logout")
            {
                this.Frame.Navigate(typeof(LoginPage));
            }
            else
            {
                contentFrame.Navigate((string)((NavigationViewItem)args.SelectedItem).Tag == "Hub" ? typeof(HubPage) : typeof(SetupPage), ((NavigationViewItem)args.SelectedItem).Tag);
            }
        }

    }
}