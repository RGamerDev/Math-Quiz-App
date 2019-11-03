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
    public sealed partial class QuestionPage : Page
    {
        public string QuestionNumber { get { return $"Question {Number}"; } set { QuestionNumber = value; } }
        public static int Number { get; set; }
        public int Countdown { get; set; } = 1800;
        Options Options;

        public QuestionPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            Options = (Options)args.Parameter;
            Number = Options.NumberOfQuestions;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            if (Options.NumberOfQuestions != -1)
            {
                DispatcherTimer dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick += Dt_Tick; 
            }
        }

        private void Dt_Tick(object sender, object e)
        {
            Countdown--;
            Timer.Text = Countdown / 60 + ":" + ((Countdown % 60) >= 10 ? (Countdown % 60).ToString() : "0" + Countdown % 60); 
        }

        private void Next(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(QuestionPage));
        }

        private void Previous(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }
    }
}
