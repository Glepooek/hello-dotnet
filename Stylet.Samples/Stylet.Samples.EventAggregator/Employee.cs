namespace Stylet.Samples.EventAggregator
{
    public class Employee : PropertyChangedBase
    {
        private string mName;
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name
        {
            get { return mName; }
            set { SetAndNotify(ref mName, value); }
        }

        private int mAge;
        /// <summary>
        /// 年龄
        /// </summary>
        public int Age
        {
            get { return mAge; }
            set { SetAndNotify(ref mAge, value); }
        }

        private string mSex;
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex
        {
            get { return mSex; }
            set { SetAndNotify(ref mSex, value); }
        }
    }
}
