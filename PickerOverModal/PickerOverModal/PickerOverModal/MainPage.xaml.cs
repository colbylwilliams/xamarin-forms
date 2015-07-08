using System;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace PickerOverModal
{
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent();
		}

		protected override void OnAppearing ()
		{
			base.OnAppearing();

			ThePicker.Focused += HandlePickerFocused;
			TheButton.Clicked += HandleButtonClicked;

			MessagingCenter.Subscribe<MainPage>(this, "ShowModal", async  mainPage => {
				await Task.Delay(3000);
				DismissActionSheetAndPushModal();
			});

			MessagingCenter.Subscribe<ModalPage>(this, "CloseModal", modalPage => Navigation.PopModalAsync());
		}


		protected override void OnDisappearing ()
		{
			ThePicker.Focused -= HandlePickerFocused;
			TheButton.Clicked -= HandleButtonClicked;

			MessagingCenter.Unsubscribe<MainPage>(this, "ShowModal");
			MessagingCenter.Unsubscribe<MainPage>(this, "CloseModal");

			base.OnDisappearing();
		}


		void HandleButtonClicked (object sender, EventArgs eventArgs)
		{
			MessagingCenter.Send(this, "ShowModal");

			Device.BeginInvokeOnMainThread(() => 
				Device.OnPlatform(
				Android: async () => Title = await DisplayCustomAndroidActionSheet("The Action Sheet", "Cancel", null, "Main - One", "Main - Two"), 
				Default: async () => Title = await DisplayActionSheet("The Action Sheet", "Cancel", null, "Main - One", "Main - Two")
			));
		}


		void HandlePickerFocused (object sender, FocusEventArgs focusEventArgs)
		{
			MessagingCenter.Send(this, "ShowModal");
		}


		public Task<string> DisplayCustomAndroidActionSheet (string title, string cancel, string destruction, params string[] buttons)
		{
			var args = new CustomAndroidActionSheetArguments (title, cancel, destruction, buttons);
			MessagingCenter.Send(this, "DisplayCustomAndroidActionSheet", args);
			return args.Result.Task;
		}


		void DismissActionSheetAndPushModal ()
		{
			MessagingCenter.Send(this, "KillActionSheet");

			Device.BeginInvokeOnMainThread(() => Navigation.PushModalAsync(new ModalPage ()));
		}
	}
}
