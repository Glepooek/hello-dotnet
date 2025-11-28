namespace Stylet.Samples.HelloDialog
{
    public class DialogViewModel : Screen
    {
        private string mName;
        public string Name
        {
            get { return mName; }
            set { SetAndNotify(ref mName, value); }
        }


        public DialogViewModel()
        {
            this.DisplayName = "I'm Dialog1";
        }

        public void Close()
        {
            this.RequestClose(null);
        }

        public void Save()
        {
            this.RequestClose(true);
        }

    }
}
