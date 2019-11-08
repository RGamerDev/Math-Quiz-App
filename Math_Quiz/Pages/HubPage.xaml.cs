using Math_Quiz.Context;
using Math_Quiz.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Math_Quiz.Data;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Math_Quiz.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HubPage : Page
    {
        public string Title { get; set; } = $"Hello {SessionContext.username}!";
        public List<Result> Results { get; set; }

        public HubPage()
        {
            this.InitializeComponent();
            Results = new PeopleRepository().GetPerson(new Person(SessionContext.username, SessionContext.password)).Results;

            foreach (Result result in Results)
            {
                Grid grid = new Grid();
                for (int i = 0; i < 4; i++)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }

                List<TextBlock> textBlocks = new List<TextBlock>();
                textBlocks.Add(new TextBlock() { Text = result.Percentage, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 20 });
                textBlocks.Add(new TextBlock() { Text = result.Competency, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 20 });
                textBlocks.Add(new TextBlock() { Text = result.Grade, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 20 });
                textBlocks.Add(new TextBlock() { Text = result.Difficulty, HorizontalAlignment = HorizontalAlignment.Center, FontSize = 20 });

                for (int i = 0; i < 4; i++)
                {
                    grid.Children.Add(textBlocks[i]);
                    Grid.SetColumn(textBlocks[i], i);
                }

                List.Children.Add(grid);
            }
        }
    }
}
