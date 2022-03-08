using Android.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TTT
{
	public partial class MainPage : ContentPage
	{
		protected MediaPlayer player;
		public void StartPlayer(String filePath)
		{
			if (player == null)
			{
				player = new MediaPlayer();
			}
			else
			{
				player.Reset();
				player.SetDataSource(filePath);
				player.Prepare();
				player.Start();
			}
		}
		public MainPage()
		{
			StartPlayer("../../Assets/song.mp3");
			Label lbl = new Label
			{
				Text = "Swipe to play(Right - pvp, Left - bot)",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.Center,
				FontSize = 36,
			};
			SwipeGestureRecognizer swipeLeft = new SwipeGestureRecognizer();
			swipeLeft.Direction = SwipeDirection.Left;
			SwipeGestureRecognizer swipeRight = new SwipeGestureRecognizer();
			swipeRight.Direction = SwipeDirection.Right;
			swipeLeft.Swiped += SwipeLeft_Swiped;
			swipeRight.Swiped += SwipeRight_Swiped;
			StackLayout st = new StackLayout
			{
				Children = { lbl }
			};
			lbl.GestureRecognizers.Add(swipeRight);
			lbl.GestureRecognizers.Add(swipeLeft);
			Content = st;
		}
		async void SwipeRight_Swiped(object sender, SwipedEventArgs e)
		{
			//await Navigation.PushAsync(new Bot());
			await Navigation.PushAsync(new Pvp(true));
		}
		async void SwipeLeft_Swiped(object sender, SwipedEventArgs e)
		{
			await Navigation.PushAsync(new Pvp(false));
		}
	}
}
