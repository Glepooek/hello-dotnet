using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using Test.ListViewDemo.Models;

// https://blog.csdn.net/loveyou388i/article/details/77746593
// https://blog.csdn.net/DahlinSky/article/details/108636050

namespace Test.ListViewDemo.ViewModels
{
    internal class MainViewModel : ObservableObject
    {
        #region Properties

        private ObservableCollection<Student> mStudentList;
        public ObservableCollection<Student> StudentList
        {
            get { return mStudentList; }
            set { mStudentList = value; }
        }

        private Student mCurrentSelectedStudent;
        public Student CurrentSelectedStudent
        {
            get => mCurrentSelectedStudent;
            set => SetProperty(ref mCurrentSelectedStudent, value);
        }

        ICollectionView collectionView;
        /// <summary>
        /// 学生集合在页面中的代理
        /// </summary>
        public ICollectionView CollectionView
        {
            get { return collectionView; }
            set => SetProperty(ref collectionView, value);
        }

        private string filterText = string.Empty;
        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                SetProperty(ref filterText, value);
            }
        }

        #endregion

        #region Commands

        public RelayCommand RefreshCommand { get; set; }

        public void OnRefresh()
        {
            this.CollectionView.Refresh();
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {
            mStudentList = new ObservableCollection<Student>();
            RefreshCommand = new RelayCommand(OnRefresh);
            LoadStudents();
        }

        #endregion

        #region Methods

        private void LoadStudents()
        {
            Random random = new Random();
            for (var i = 0; i < 10000; i++)
            {
                StudentList.Add(new Student
                {
                    Id = i,
                    Name = $"stu{i}",
                    Address = $"北京市海淀区{i}号",
                    Email = $"7721{i}@qq.com",
                    Age = random.Next(100),
                    Sex = i % 2 == 0 ? "Female" : "male"
                });
            }

            CollectionView = new CollectionViewSource() { Source = StudentList }.View;
            CollectionView.Filter = new Predicate<object>(Filter);
            CollectionView.GroupDescriptions.Add(new PropertyGroupDescription() { PropertyName = "Sex" });
            CollectionView.SortDescriptions.Add(new SortDescription() { PropertyName = "Age", Direction = ListSortDirection.Descending });
            CurrentSelectedStudent = StudentList.FirstOrDefault(s => s.Id == 10);
        }

        private bool Filter(object obj)
        {
            bool result = true;
            if (string.IsNullOrWhiteSpace(filterText))
            {
                return result;
            }

            if (obj is Student student && student.Name.Contains(filterText))
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        #endregion
    }
}
