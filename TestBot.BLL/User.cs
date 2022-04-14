namespace TestBot.BLL
{
    public class User
    {
        public string Name { get; private set; }
        public long ChatId { get; private set; }

        public User(string name, long chatId)
        {
            Name = name;
            ChatId = chatId;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }
    }
}