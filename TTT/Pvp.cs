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
        bool x = false;
		public Pvp()
		{
            Grid g = new Grid
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
            Grid g_ = new Grid
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
            g_.BackgroundColor = Color.Red;
            g.BackgroundColor = Color.Red;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    img = new Image();
                    img.Source = ImageSource.FromFile("Nimetu.png");
                    g_.Children.Add(img, i, j);
                    var tap = new TapGestureRecognizer();
                    img.GestureRecognizers.Add(tap);
                    tap.Tapped += async (object sender, EventArgs e) =>
                    {
                        Image img = sender as Image;
                        if (x)
                        {
                            img.Source = ImageSource.FromFile("krestik.png");
                            img.GestureRecognizers.Clear();
                            x = !x;
                        }
                        else
                        {
                            img.Source = ImageSource.FromFile("nolik.png");
                            img.GestureRecognizers.Clear();
                            x = !x;
                        }
                    };
                }
            }
            g.Children.Add(g_);
            Content = g;
		}
	}
}