using XFUsingCSharp.Models;
using XFUsingCSharp.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace XFUsingCSharp.Views
{
    public class MainPage : ContentPage
    {
        //define elements
        Image xamagonImage;
        Editor noteEditor;
        Label textLabel;
        Button saveButton, deleteButton;

        public MainPage()
        {
            BackgroundColor = Color.PowderBlue;

            BindingContext = new MainPageViewModel();

            //initialise elements
            xamagonImage = new Image
            {
                Source = "xamagon.png"
            };

            noteEditor = new Editor
            {
                Placeholder = "Enter note here",
                BackgroundColor = Color.White,
                Margin = new Thickness(10)
            };

            noteEditor.SetBinding(Editor.TextProperty, nameof(MainPageViewModel.NoteText));

            saveButton = new Button
            {
                Text = "Save",
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                Margin = new Thickness(10)
            };

            //saveButton.Clicked += saveButton_Clicked;
            saveButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.SaveNoteCommand));

            deleteButton = new Button
            {
                Text = "Delete",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                Margin = new Thickness(10)
            };

            //deleteButton.Clicked += deleteButton_Clicked;
            deleteButton.SetBinding(Button.CommandProperty, nameof(MainPageViewModel.DeleteNotesCommand));

            var collectionView = new CollectionView
            {
                ItemTemplate = new NotesTemplate()
            };

            collectionView.SetBinding(CollectionView.ItemsSourceProperty, nameof(MainPageViewModel.Notes));
            //collectionView.SetBinding(CollectionView.SelectedItemProperty, nameof(MainPageViewModel.SelectedNote));
            //collectionView.SetBinding(CollectionView.SelectionChangedCommandProperty, nameof(MainPageViewModel.NoteSelectedCommand));

            textLabel = new Label
            {
                FontSize = 20,
                Margin = new Thickness(10)
            };

            //use grid for layout
            var grid = new Grid
            {
                Margin = new Thickness(20, 40),
                ColumnDefinitions =
                {
                    //GridUnitType.Star means keep these 2 cols proportional (equal width)
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) }
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2.5, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1.0, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(2.0, GridUnitType.Star) }
                }
            };

            //add elements to grid
            grid.Children.Add(xamagonImage, 0, 0);
            Grid.SetColumnSpan(xamagonImage, 2);

            grid.Children.Add(noteEditor, 0, 1);
            Grid.SetColumnSpan(noteEditor, 2);

            grid.Children.Add(saveButton, 0, 2);
            grid.Children.Add(deleteButton, 1, 2);

            grid.Children.Add(collectionView, 0, 3);
            Grid.SetColumnSpan(collectionView, 2);

            Content = grid;
        }

        class NotesTemplate : DataTemplate
        {
            public NotesTemplate() : base(LoadTemplate)
            {

            }

            static StackLayout LoadTemplate()
            {
                var textLabel = new Label();
                textLabel.SetBinding(Label.TextProperty, nameof(Note.Text));

                var frame = new Frame
                {
                    VerticalOptions = LayoutOptions.Center,
                    Content = textLabel
                };

                return new StackLayout
                {
                    Children = { frame },
                    Padding = new Thickness(10)
                };
            }
        }
    }
}
