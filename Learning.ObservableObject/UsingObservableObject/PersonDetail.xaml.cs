using System.Windows.Controls;

namespace UsingObservableObject
{
    /// <summary>
    /// Interaction logic for PersonDetail
    /// </summary>
    public partial class PersonDetail : UserControl
    {
        public PersonDetail()
        {
            InitializeComponent();

			this.DataContext = new PersonDetailViewModel();
			RegionContext.GetObservableContext(this).PropertyChanged += PersonDetail_PropertyChanged;
		}

        private void PersonDetail_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var context = (ObservableObject<object>)sender;
            var selectedPerson = (Person)context.Value;
            (DataContext as PersonDetailViewModel).SelectedPerson = selectedPerson;
        }
    }
}
