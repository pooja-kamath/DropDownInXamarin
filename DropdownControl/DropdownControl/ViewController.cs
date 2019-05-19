using System;
using CoreGraphics;
using Foundation;
using UIKit;

namespace DropdownControl
{
    public partial class ViewController : UIViewController ,IUITableViewDataSource,
        IUITableViewDelegate, IUITextFieldDelegate
    {
        /// <summary>
        ///     The drop down table view.
        /// </summary>
        private UITableView _dropDownTableView;

        /// <summary>
        ///     The drop down view.
        /// </summary>
        private UIView _dropDownView;


        private readonly string[] _optionsList = new string[]
       {
            "Option 1", "Option 2", "Option 3",
            "Option 4", "Option 5"
       };

        protected ViewController(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            // Perform any additional setup after loading the view, typically from a nib.
            dropTextField.Delegate = this;
            CreateDropDownView(new CGRect(dropTextField.Frame.X, dropTextField.Frame.Y,
                dropTextField.Frame.Width, 43 * _optionsList.Length));
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.
        }


        [Export("tableView:numberOfRowsInSection:")]
        public nint RowsInSection(UITableView tableView, nint section)
        {
            return _optionsList.Length;
        }


        /// <summary>
        ///     Gets the cell.
        /// </summary>
        /// <returns>The cell.</returns>
        /// <param name="tableView">Table view.</param>
        /// <param name="indexPath">Index path.</param>
        [Export("tableView:cellForRowAtIndexPath:")]
        public UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell("DropDownCell");
            cell.TextLabel.Text = _optionsList[indexPath.Row];
            return cell;
        }

        /// <summary>
        ///     Gets the view for footer.
        /// </summary>
        /// <returns>The view for footer.</returns>
        /// <param name="tableView">Table view.</param>
        /// <param name="section">Section.</param>
        [Export("tableView:viewForFooterInSection:")]
        public UIView GetViewForFooter(UITableView tableView, nint section)
        {
            UIView footerView = new UIView(new CGRect(0, 0, 0, 0));
            return footerView;
        }

        /// <summary>
        ///     Gets the height for footer.
        /// </summary>
        /// <returns>The height for footer.</returns>
        /// <param name="tableView">Table view.</param>
        /// <param name="section">Section.</param>
        [Export("tableView:heightForFooterInSection:")]
        public nfloat GetHeightForFooter(UITableView tableView, nint section)
        {
            return 1;
        }

        private void CreateDropDownView(CGRect frameForDropDown)
        {
            _dropDownView = new UIView(frameForDropDown);
            _dropDownTableView = new UITableView(new CGRect(0, 0, frameForDropDown.Width, frameForDropDown.Height));
            _dropDownTableView.RegisterClassForCellReuse(typeof(UITableViewCell), "DropDownCell");
            _dropDownTableView.DataSource = this;
            _dropDownTableView.Delegate = this;
            _dropDownView.AddSubview(_dropDownTableView); 
            AddShadowToDropDown();

        }

        private void AddShadowToDropDown()
        {
            var shadowPath = UIBezierPath.FromRect(_dropDownView.Bounds);
            _dropDownView.Layer.MasksToBounds = false;
            _dropDownView.Layer.ShadowColor = UIColor.Black.CGColor;
            _dropDownView.Layer.ShadowOffset = new CGSize(width: 0, height: 0.5);
            _dropDownView.Layer.ShadowOpacity = 0.2f;
            _dropDownView.Layer.ShadowPath = shadowPath.CGPath;
            _dropDownTableView.ClipsToBounds = true;
        }

        public override void ViewDidLayoutSubviews()
        {
            base.ViewDidLayoutSubviews();

        }

        [Export("tableView:didSelectRowAtIndexPath:")]
        public void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            dropTextField.Text = _optionsList[indexPath.Row];
            _dropDownView.RemoveFromSuperview();
        }

        /// <summary>
        ///     Should begin editing.
        ///     Show the drop down view.
        /// </summary>
        /// <returns><c>true</c>, should show keyboard, <c>false</c> otherwise.</returns>
        /// <param name="textField">Text field.</param>
        [Export("textFieldShouldBeginEditing:")]
        public bool ShouldBeginEditing(UITextField textField)
        {
            View.AddSubview(_dropDownView);
            UIApplication.SharedApplication.KeyWindow.BringSubviewToFront(_dropDownTableView);
            return false;
        }
    }
}
