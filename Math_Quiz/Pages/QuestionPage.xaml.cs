using Math_Quiz.Data;
using Math_Quiz.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using System.Threading.Tasks;
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
    public sealed partial class QuestionPage : Page
    {
        #region Variables
        public static int index;
        public int Countdown { get; set; } = 3600;
        Options options;
        DispatcherTimer dt;
        public int CorrectAnswers;
        public QuestionsRepository QRepo;
        public Question CurrentQuestion;
        public List<Question> PracticeQs;
        public int Score;
        private bool isCorrect;
        private bool isChecked = false;
        #endregion

        public QuestionPage()
        {
            this.InitializeComponent();
        }

        #region Event handlers
        protected override void OnNavigatedTo(NavigationEventArgs args)
        {
            if (args.Parameter != null)
            {
                options = (Options)args.Parameter;
                txtQ.Text = options.NumberOfQuestions != -1 ? $"Question {++index} out of {options.NumberOfQuestions.ToString()}" : $"Question {++index}";
                return;
            }
            index++;
        }

        private void PageLoaded(object sender, RoutedEventArgs e)
        {
            isChecked = false;
            PracticeQs = new List<Question>();
            QRepo = new QuestionsRepository(options);
            index = 1;
            if (options.NumberOfQuestions != -1)
            {
                ChangeQuestion();
                Timer.Visibility = Visibility.Visible;
                dt = new DispatcherTimer();
                dt.Interval = TimeSpan.FromSeconds(1);
                dt.Tick += Dt_Tick;
                dt.Start();
            }
            else
            {
                ChangeQuestion();
            }
        }


        private async void Dt_Tick(object sender, object e)
        {
            if (Countdown != 0)
            {
                Countdown--;
                Timer.Text = Countdown / 60 + ":" + ((Countdown % 60) >= 10 ? (Countdown % 60).ToString() : "0" + Countdown % 60);
            }
            else
            {
                dt.Stop();
                await new MessageDialog("Time is done!").ShowAsync();
                this.Frame.Navigate(typeof(HubPage), CorrectAnswers);
            }
        }

        private async void Next(object sender, RoutedEventArgs e)
        {
            if (isChecked)
            {
                if (isCorrect)
                {
                    Score++;
                }
                if (options.NumberOfQuestions != -1)
                {
                    if (index < options.NumberOfQuestions)
                    {
                        txtQ.Text = options.NumberOfQuestions != -1 ? $"Question {++index} out of {options.NumberOfQuestions.ToString()}" : $"Question {++index}";
                        ChangeQuestion();
                        if (btnPrevious.Visibility == Visibility.Collapsed)
                        {
                            btnPrevious.Visibility = Visibility.Visible;
                        }
                    }
                    else
                    {
                        btnNext.Visibility = Visibility.Collapsed;
                        btnFinish.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    txtQ.Text = options.NumberOfQuestions != -1 ? $"Question {++index} out of {options.NumberOfQuestions.ToString()}" : $"Question {++index}";
                    if (btnPrevious.Visibility == Visibility.Collapsed)
                    {
                        btnPrevious.Visibility = Visibility.Visible;
                    }
                    ChangeQuestion(true);
                } 
            }
            else
            {
                await new MessageDialog("Please select a value").ShowAsync();
            }
        }

        private void Previous(object sender, RoutedEventArgs e)
        {
            if (Score > 0)
            {
                Score--; 
            }
            if (index > 2)
            {
                txtQ.Text = options.NumberOfQuestions != -1 ? $"Question {--index} out of {options.NumberOfQuestions.ToString()}" : $"Question {--index}";
            }
            else
            {
                txtQ.Text = options.NumberOfQuestions != -1 ? $"Question {--index} out of {options.NumberOfQuestions.ToString()}" : $"Question {--index}";
                btnPrevious.Visibility = Visibility.Collapsed;
            }
            if (btnFinish.Visibility == Visibility.Visible)
            {
                btnFinish.Visibility = Visibility.Collapsed;
                btnNext.Visibility = Visibility.Visible;
            }
            ChangeQuestion();
        }

        private async void Finish(object sender, RoutedEventArgs e)
        {
            if (isCorrect)
            {
                Score++;
            }
            await ShowMessageDialog("Are you sure you want to finish?");
            index = 1;
        }

        private async void Stop(object sender, RoutedEventArgs e)
        {
            if (isCorrect)
            {
                Score++;
            }
            await ShowMessageDialog("Are you sure you want to stop?");
            index = 1;
        }

        private void Checked(object sender, RoutedEventArgs e)
        {
            isCorrect = ((RadioButton)sender).Content.ToString() == CurrentQuestion.CorrectAnswer ? true : false;
            isChecked = true;
        }

        #endregion

        #region Methods
        private async Task ShowMessageDialog(string message)
        {
            MessageDialog msg = new MessageDialog("Are you sure you want to finish?");
            msg.Options = MessageDialogOptions.None;
            msg.Commands.Add(new UICommand("Yes", cmd =>
            {
                this.Frame.Navigate(typeof(HubPage), CorrectAnswers);
            }));
            msg.Commands.Add(new UICommand("No"));
            await msg.ShowAsync();

            await new MessageDialog($"You got {Score} out of the {index} questions right").ShowAsync();
        }

        private void ChangeQuestion(bool goingforward = false)
        {
            if (options.NumberOfQuestions != -1)
            {
                if (QRepo.questions.Count > index)
                {
                    CurrentQuestion = QRepo.questions[index]; 
                }
            }
            else
            {
                if (PracticeQs.Count < index)
                {
                    CurrentQuestion = QRepo.CreateQuestion(options);
                    PracticeQs.Add(CurrentQuestion);
                }
                else
                {
                    CurrentQuestion = PracticeQs[index - 1];
                }
            }

            Description.Text = CurrentQuestion.Description;
            A.Content = CurrentQuestion.Choices[0];
            B.Content = CurrentQuestion.Choices[1];
            C.Content = CurrentQuestion.Choices[2];
            D.Content = CurrentQuestion.Choices[3];
        } 
        #endregion
    }
}
