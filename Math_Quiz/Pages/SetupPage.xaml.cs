using Math_Quiz.Models;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Math_Quiz.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SetupPage : Page
    {
        public string Option { get; set; }

        public SetupPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            Option = (string)args.Parameter;

            if (Option == "Test") NumQ.Visibility = Visibility.Visible;
        }

        private void Begin(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(
                typeof(QuestionPage), 
                new Options() 
                { 
                    Grade = (int)Grade.Value, 
                    Difficulty = (int)Dif.Value, 
                    NumberOfQuestions = NumQ.Visibility == Visibility.Visible ? (int)NumQ.Value : -1 
                });
        }
    }
}
