using System;

namespace Test.Reflections
{
    public class TestReflections
    {
        #region Filds

        private string mName = string.Empty;
        public string sex;
        public Action<string> mAction;
        public Action<string> mAction2;

        #endregion

        #region Properties

        public string Name
        {
            get { return mName; }
            set { mName = value; }
        }

        public int Age { get; set; }

        #endregion

        #region Events

        public event Action<string> TestHandler;

        #endregion

        #region Constructor

        public TestReflections(string name)
        {
            mName = name;
            mAction = InvokeAction;
        }

        private TestReflections()
        {

        }

        #endregion

        #region Methods

        public void Show()
        {
            Console.WriteLine($"build object: {mName}");
        }

        private void Confirm()
        {
            Console.WriteLine($"this is a private method");
        }

        public void InvokeAction(string msg)
        {
            Console.WriteLine($"InvokeAction: {msg}");
        }

        public void TriggerEvent()
        {
            TestHandler?.Invoke("trigger event");
        }

        #endregion
    }
}
