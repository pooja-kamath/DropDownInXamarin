// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace DropdownControl
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		UIKit.UITextField dropTextField { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (dropTextField != null) {
				dropTextField.Dispose ();
				dropTextField = null;
			}
		}
	}
}
