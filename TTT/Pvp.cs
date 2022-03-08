using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TTT
{
    public class Pvp : ContentPage
    {
        Image img;

        Grid g, g_;
        bool x = false;
        public Pvp()
        {
            g = new Grid
            {
                RowSpacing = 0,
                ColumnSpacing = 0,
                RowDefinitions =
                {
                    new RowDefinition{Height = new GridLength(2, GridUnitType.Star) },
                    new RowDefinition{Height = new GridLength(1, GridUnitType.Star)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width = GridLength.Auto },
                }
            };
            g_ = new Grid
            {
                RowSpacing = 0,
                RowDefinitions =
                {
                    new RowDefinition{Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition{Height = new GridLength(1, GridUnitType.Star)},
                    new RowDefinition{Height = new GridLength(1, GridUnitType.Star)}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition{Width= new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width= new GridLength(1, GridUnitType.Star)},
                    new ColumnDefinition{Width= new GridLength(1, GridUnitType.Star)},
                }
            };
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    img = new Image();
                    img.Source = ImageSource.FromFile("Nimetu.png");
                    img.StyleId = ""+i+j;
                    g_.Children.Add(img, i, j);
                    var tap = new TapGestureRecognizer();
                    img.GestureRecognizers.Add(tap);
                    tap.Tapped += async (object sender, EventArgs e) =>
                    {
                        Image img = sender as Image;
                        if (x)
                        {
                            img.Source = ImageSource.FromFile("krestik.png");
                            img.StyleId = "X";
                            g_.BackgroundColor = Color.Blue;
                            g.BackgroundColor = Color.Blue;
                        }
                        else
                        {
                            img.Source = ImageSource.FromFile("nolik.png");
                            img.StyleId = "Y";
                            g_.BackgroundColor = Color.Red;
                            g.BackgroundColor = Color.Red;
                        }
                        Console.WriteLine(img.Id);
                        img.GestureRecognizers.Clear();
                        x = !x;
                        CheckWin();
                    };
                }
            }
            Label lbl = new Label();
            lbl.Text = "";
            lbl.FontSize = 96;
            lbl.VerticalOptions = LayoutOptions.Center;
            lbl.HorizontalOptions = LayoutOptions.Center;
            g.Children.Add(g_);
            g.Children.Add(lbl, 0, 1);
            Content = g;
            
        }
        private async void CheckWin()
        {
            char winner = x ? 'X' : 'O';
            if (IsWinCon())
            {
                if(await DisplayAlert($"Winner is {winner}", "Would you like to start another game?", "Yes", "No"))
                {
                    await Navigation.PopAsync();
                }
            }
        }
        private bool IsWinCon()
        {
            return
                ((Image)g_.Children[0]).StyleId == ((Image)g_.Children[1]).StyleId && ((Image)g_.Children[1]).StyleId == ((Image)g_.Children[2]).StyleId ||
                ((Image)g_.Children[3]).StyleId == ((Image)g_.Children[4]).StyleId && ((Image)g_.Children[4]).StyleId == ((Image)g_.Children[5]).StyleId ||
                ((Image)g_.Children[6]).StyleId == ((Image)g_.Children[7]).StyleId && ((Image)g_.Children[7]).StyleId == ((Image)g_.Children[8]).StyleId ||
                ((Image)g_.Children[0]).StyleId == ((Image)g_.Children[3]).StyleId && ((Image)g_.Children[3]).StyleId == ((Image)g_.Children[6]).StyleId ||
                ((Image)g_.Children[1]).StyleId == ((Image)g_.Children[4]).StyleId && ((Image)g_.Children[4]).StyleId == ((Image)g_.Children[7]).StyleId ||
                ((Image)g_.Children[2]).StyleId == ((Image)g_.Children[5]).StyleId && ((Image)g_.Children[5]).StyleId == ((Image)g_.Children[8]).StyleId ||
                ((Image)g_.Children[0]).StyleId == ((Image)g_.Children[4]).StyleId && ((Image)g_.Children[4]).StyleId == ((Image)g_.Children[8]).StyleId ||
                ((Image)g_.Children[2]).StyleId == ((Image)g_.Children[4]).StyleId && ((Image)g_.Children[4]).StyleId == ((Image)g_.Children[6]).StyleId;
        }
    }
}