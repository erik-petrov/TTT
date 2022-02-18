using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace TTT
{
	public class Bot : ContentPage
	{
		public Bot()
		{
			Content = new StackLayout
			{
				Children = {
					new Label { Text = "Welcome to Xamarin.Forms!" }
				}
			};
		}
	}
}