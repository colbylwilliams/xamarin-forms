using Xamarin.Forms;

namespace PickerOverModal
{
	public partial class ModalPage : ContentPage
	{
		public ModalPage()
		{
			InitializeComponent();

			TheButton.Clicked += (sender, args) =>
			{
				MessagingCenter.Send(this, "CloseModal");
			};
		}
	}
}