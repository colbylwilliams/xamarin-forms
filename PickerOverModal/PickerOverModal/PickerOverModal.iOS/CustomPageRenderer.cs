using PickerOverModal.iOS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace PickerOverModal.iOS
{
	public class CustomPageRenderer : PageRenderer
	{
		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);


			var page = e.NewElement as MainPage;

			if (page != null) {
				
				MessagingCenter.Subscribe(page, "KillActionSheet", (MainPage sender) => {

					var actionSheet = ViewController.PresentedViewController;

					if (actionSheet != null) ViewController.DismissViewController(true, null);

				});
			}
		}
	}
}