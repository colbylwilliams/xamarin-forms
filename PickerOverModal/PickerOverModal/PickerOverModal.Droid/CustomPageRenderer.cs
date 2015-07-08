using System;
using System.Linq;

using Android.App;

using PickerOverModal.Droid;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Page), typeof(CustomPageRenderer))]
namespace PickerOverModal.Droid
{
	public class CustomPageRenderer : PageRenderer
	{
		AlertDialog actionSheet;

		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);

			var page = e.NewElement as MainPage;

			if (page != null) {

				MessagingCenter.Subscribe(page, "KillActionSheet", (MainPage sender) => {
				
					if (actionSheet != null) actionSheet.Dismiss();
				
				});


				MessagingCenter.Subscribe(page, "DisplayCustomAndroidActionSheet", (MainPage sender, CustomAndroidActionSheetArguments args) => {

					var builder = new AlertDialog.Builder (Forms.Context);

					builder.SetTitle(args.Title);

					var items = args.Buttons.ToArray();

					builder.SetItems(items, (sender2, args2) => args.Result.TrySetResult(items[args2.Which]));

					if (args.Cancel != null)
						builder.SetPositiveButton(args.Cancel, delegate {
							args.Result.TrySetResult(args.Cancel);
						});


					if (args.Destruction != null)
						builder.SetNegativeButton(args.Destruction, delegate {
							args.Result.TrySetResult(args.Destruction);
						});


					actionSheet = builder.Create();

					builder.Dispose();

					actionSheet.SetCanceledOnTouchOutside(true);

					actionSheet.CancelEvent += (sender3, ee) => args.SetResult(null);

					actionSheet.Show();
				});
			}
		}
	}
}