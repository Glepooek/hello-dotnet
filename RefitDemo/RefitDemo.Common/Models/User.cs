namespace RefitDemo.Common.Models
{
    public sealed class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public override string ToString()
        {
            return $"User {Id}: {Name} ({Email})";
        }
    }
}
