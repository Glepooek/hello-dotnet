namespace _02_MultipleInterfaces.Interfaces
{
    interface IMammal
    {
        int LegNum { get; set; }
        HairType HairType { get; set; }
    }

    public enum HairType
    {
        Fur,
        Hair
    }
}
