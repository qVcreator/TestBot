namespace TestBot.BLL
{
    public class User
    {
        public string Name { get; private set; }
        public string ChatId { get; private set; }

        public void ChangeName(string newName)
        {
            Name = newName;
        }
    }
}