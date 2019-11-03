﻿using Math_Quiz.Data;
using Math_Quiz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class RegisterPage : Page
    {
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        private void toLogin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.LoginPage));
        }

        private async void Register(object sender, RoutedEventArgs e)
        {
            await TryRegister();
        }

        private async System.Threading.Tasks.Task TryRegister()
        {
            Repository rep = new Repository();

            if (rep.canRegister(new Person(tbxusr.Text, tbxPass.Text)) && !string.IsNullOrEmpty(tbxusr.Text) && !string.IsNullOrEmpty(tbxPass.Text) && !string.IsNullOrEmpty(tbxconPass.Text))
            {
                if (tbxconPass.Text == tbxPass.Text)
                {
                    rep.Register(new Person(tbxusr.Text, tbxPass.Text));
                    await new MessageDialog("User has Registered").ShowAsync();
                    this.Frame.Navigate(typeof(Pages.LoginPage));
                }
                else
                {
                    await new MessageDialog("Passwords do not match").ShowAsync();
                    return;
                }
            }
            else
            {
                await new MessageDialog("Username or password is invalid").ShowAsync();
                return;
            }
        }

        private async void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await TryRegister();
            }
        }
    }
}
