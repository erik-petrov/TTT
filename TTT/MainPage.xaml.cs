using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TTT
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			Label lbl = new Label
			{
				Text = "Swipe right for bot play and left for pvp",
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
			await Navigation.PushAsync(new Bot());
		}
		async void SwipeLeft_Swiped(object sender, SwipedEventArgs e)
		{
			await Navigation.PushAsync(new Pvp());
		}
	}
}
