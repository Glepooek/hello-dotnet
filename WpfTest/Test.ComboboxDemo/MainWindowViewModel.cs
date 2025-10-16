using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using Test.ComboboxDemo.Models;

namespace Test.ComboboxDemo
{
    public class MainWindowViewModel : ObservableObject
    {
        #region Properties

        private Sex mSex = Sex.None;
        public Sex Sex
        {
            get => mSex = Sex.None;
            set => SetProperty(ref mSex, value, nameof(Sex));
        }

        private ObservableCollection<Actor> mActorList;
        public ObservableCollection<Actor> ActorList
        {
            get => mActorList;
            set => SetProperty(ref mActorList, value, nameof(ActorList));
        }

        private Actor mSelectedActor;
        public Actor SelectedActor
        {
            get => mSelectedActor;
            set => SetProperty(ref mSelectedActor, value, nameof(SelectedActor));
        }

        #endregion

        #region Constructor

        public MainWindowViewModel()
        {
            mActorList = new ObservableCollection<Actor>()
            {
                new Actor(){ Name= "Hedy Lamarr", BorthDate="1914", HeaderImg="/Test.ComboboxDemo;component/Images/1.jpg" , Description="beautiful woman"},
                new Actor(){ Name= "Ingird Bergman", BorthDate="1915", HeaderImg="/Test.ComboboxDemo;component/Images/2.png" , Description="beautiful woman"},
                new Actor(){ Name= "Joan Lamarr", BorthDate="1904", HeaderImg="/Test.ComboboxDemo;component/Images/45-5.png" , Description="beautiful woman"},
                new Actor(){ Name= "Hedy Crawford", BorthDate="1924", HeaderImg="/Test.ComboboxDemo;component/Images/神木麗0.jpg" , Description="beautiful woman"},
                new Actor(){ Name= "Marlene Dietrich", BorthDate="1914", HeaderImg="/Test.ComboboxDemo;component/Images/神木麗1.jpg" , Description="beautiful woman"},
            };
            mSelectedActor = mActorList.FirstOrDefault();
        }

        #endregion
    }
}
