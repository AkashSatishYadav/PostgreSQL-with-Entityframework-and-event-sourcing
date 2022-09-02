namespace PostgresMarten.Events
{
    public class UserAdded
    {
        public Guid UserId { get; set; }
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
