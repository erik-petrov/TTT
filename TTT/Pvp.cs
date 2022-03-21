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
        bool _pvp;
        Label lbl;
        Grid g, g_;
        bool x = false;
        public Pvp(bool pvp)
        {
            this.BackgroundColor = Color.Red;
            _pvp = pvp;
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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                }
            };
            InitializeBoard();
            g.Children.Add(g_);
            Content = g;
            if (pvp) Game();
            else Bot();
        }
        private void Bot()
		{
            AI ai = new AI();
            AI.Move move = ai.findBestMove(DrawBoard());
            MakeMove(move);
        }
        private void Game()
		{
            lbl = new Label();
            char plr = x ? 'O' : 'X';
            lbl.Text = "Now playing: " + plr;
            lbl.FontSize = 96;
            lbl.VerticalOptions = LayoutOptions.Center;
            lbl.HorizontalOptions = LayoutOptions.Center;
        }
        private async void CheckWin()
        {
            char winner = x ? 'O' : 'X';
            lbl.Text = "Now playing: " + winner;
            if (IsWinCon())
            {
                if(await DisplayAlert($"Winner is {winner}", "Would you like to start another game?", "Yes", "No"))
                {
                    await Navigation.PopAsync();
                }
                return;
            }
			if (IsDraw())
			{
                if (await DisplayAlert($"Draw", "Would you like to start another game?", "Yes", "No"))
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
        private void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    img = new Image();
                    img.Source = ImageSource.FromFile("Nimetu.png");
                    img.ClassId = "_";
                    img.StyleId = "" + i + j;
                    g_.Children.Add(img, i, j);
                    var tap = new TapGestureRecognizer();
                    img.GestureRecognizers.Add(tap);
                    tap.Tapped += (object sender, EventArgs e) =>
                    {
                        Image img = sender as Image;
                        if (x)
                        {
                            img.Source = ImageSource.FromFile("krestik.png");
                            img.StyleId = "X";
                            img.ClassId = "x";
                            g_.BackgroundColor = Color.Blue;
                            g.BackgroundColor = Color.Blue;
                        }
                        else
                        {
                            img.Source = ImageSource.FromFile("nolik.png");
                            img.StyleId = "O";
                            img.ClassId = "o";
                            g_.BackgroundColor = Color.Red;
                            g.BackgroundColor = Color.Red;
                        }
                        Console.WriteLine(img.Id);
                        img.GestureRecognizers.Clear();
                        x = !x;
                        if (!_pvp && !x) Bot();
                        CheckWin();
                    };
                }
            }
        }
        private bool IsDraw()
		{
			for (int i = 0; i < g_.Children.Count; i++)
			{
				if (((Image)g_.Children[i]).ClassId == "_")
				{
                    return false;
				}
			}
            return true;
		}
        private string[,] DrawBoard()
        {
            string[,] board = new string[3,3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = ((Image)g_.Children[i+j]).ClassId;
                }
            }
            return board;
        }
        private void MakeMove(AI.Move move)
        {
            Image target = (Image)g_.Children[move.col + move.row];
            target.Source = ImageSource.FromFile("krestik.png");
            target.StyleId = "X";
            target.ClassId = "x";
            g_.BackgroundColor = Color.Blue;
            g.BackgroundColor = Color.Blue;
            target.GestureRecognizers.Clear();
            x = !x;
            CheckWin();
        }
    }//todo: board unplayable after win, bot???
}