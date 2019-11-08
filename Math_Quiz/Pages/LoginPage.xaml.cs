using System;
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
using Microsoft.Identity.Client;
using System.Diagnostics;
using System.Threading.Tasks;
using Windows.UI.Popups;
using System.Data.SqlClient;
using System.Data;
using Math_Quiz.Models;
using Math_Quiz.Context;
using Math_Quiz.Data;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Math_Quiz.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void Login(object sender, RoutedEventArgs e)
        {
            await TryLogin();
        }

        private async Task TryLogin()
        {
            Person p = new Person(tbxusr.Text, tbxPass.Text);
            PeopleRepository rep = new PeopleRepository();
            if (rep.canLogin(p))
            {
                SessionContext.username = tbxusr.Text;
                SessionContext.password = tbxPass.Text;
                SessionContext.Results = p.Results;
                await new MessageDialog("User has successfully logged in").ShowAsync();
                this.Frame.Navigate(typeof(Pages.MainPage));
            }
            else
            {
                await new MessageDialog("Username or password is invalid/non-existent").ShowAsync();
            }
        }

        private void toRegister(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.RegisterPage));
        }

        private async void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                await TryLogin();
            }
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=ANIMUS;Initial Catalog=MathApp;Integrated Security=True"))
            {
                con.Open();
                if (con.State == ConnectionState.Open)
                {
                    await new MessageDialog($"Connection is {con.State.ToString()}").ShowAsync();
                }
            }
        }
    }
}
