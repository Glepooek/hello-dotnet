using System.Windows.Forms;

namespace XBAP
{
	public partial class UserNameWinForm : Form
	{
		public UserNameWinForm()
		{
			InitializeComponent();
		}

		public string UserName
		{
			get { return txtName.Text; }
		}
	}
}