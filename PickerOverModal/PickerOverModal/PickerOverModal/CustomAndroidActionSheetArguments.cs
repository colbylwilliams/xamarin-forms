using System.Collections.Generic;
using System.Threading.Tasks;

namespace PickerOverModal
{
	public class CustomAndroidActionSheetArguments
	{
		public string Title { get; private set; }

		public string Cancel { get; private set; }

		public string Destruction { get; private set; }

		public IEnumerable<string> Buttons { get; private set; }

		public TaskCompletionSource<string> Result { get; private set; }

		public CustomAndroidActionSheetArguments (string title, string cancel, string destruction, IEnumerable<string> buttons)
		{
			Title = title;
			Cancel = cancel;
			Destruction = destruction;
			Buttons = buttons;
			Result = new TaskCompletionSource<string> ();
		}

		public void SetResult (string result)
		{
			Result.TrySetResult(result);
		}
	}
}