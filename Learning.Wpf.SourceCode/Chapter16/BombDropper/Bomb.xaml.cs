using System.Windows.Controls;

namespace BombDropper
{
	public partial class Bomb : UserControl
	{
		public Bomb()
		{
			InitializeComponent();
		}

		public bool IsFalling
		{
			get;
			set;
		}
	}
}
