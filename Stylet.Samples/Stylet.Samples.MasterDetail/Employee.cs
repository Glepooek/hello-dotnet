namespace Stylet.Samples.MasterDetail
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
    }
}
