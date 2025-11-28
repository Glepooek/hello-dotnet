using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace UsingObservableObject
{
    public class PersonViewModel
    {
        public ObservableCollection<Person> PersonList
        {
            get;
            set;
        }

        public PersonViewModel()
        {
            CreatePeople();
        }

        private void CreatePeople()
        {
            PersonList = new ObservableCollection<Person>();
            for (int i = 0; i < 10; i++)
            {
                PersonList.Add(new Person()
                {
                    FirstName = String.Format("First {0}", i),
                    LastName = String.Format("Last {0}", i),
                });
            }
        }
    }
}
