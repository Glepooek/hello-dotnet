namespace Stylet.Samples.UsingLabelledValue
{
    public class ShellViewModel
    {
        public BindableCollection<LabelledValue<EventType>> EnumValues { get; private set; }
        public LabelledValue<EventType> SelectedEnumValue { get; set; }

        public ShellViewModel()
        {
            this.EnumValues = new BindableCollection<LabelledValue<EventType>>
            {
                 LabelledValue.Create("FinishClass Value", EventType.FinishClass),
                 LabelledValue.Create("GroupPK Value", EventType.GroupPK),
                 LabelledValue.Create("NormalPK Value", EventType.NormalPK),
             };

            this.SelectedEnumValue = this.EnumValues[0];
        }
    }

    public enum EventType
    {
        GroupPK,
        NormalPK,
        FinishClass
    }
}
