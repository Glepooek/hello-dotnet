namespace UsingObservableObject
{
    public class PersonDetailViewModel : BindableBase
    {
        private Person _selectedPerson;
        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set 
            {
                _selectedPerson = value;
                RaisePropertyChanged();
            }
        }

        public PersonDetailViewModel()
        {

        }
    }
}
