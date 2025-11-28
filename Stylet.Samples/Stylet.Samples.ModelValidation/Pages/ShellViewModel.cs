namespace Stylet.Samples.ModelValidation.Pages
{
    public class ShellViewModel : Conductor<IScreen>
    {
        public ShellViewModel(UserViewModel userViewModel)
        {
            this.DisplayName = "Stylet.Samples.ModelValidation";

            this.ActiveItem = userViewModel;
        }

        protected override void OnInitialActivate()
        {
            base.OnInitialActivate();
            // 
        }
    }
}
