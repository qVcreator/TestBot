namespace TestBot.BLL
{
    public class User
    {
        public string Name { get; private set; }
        public long ChatId { get; private set; }
        public int QueueQuestion { get; private set; } = 0;

        public User(string name, long chatId)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            Name = name;
            ChatId = chatId;
        }

        public void ChangeName(string newName)
        {
            if (newName == null)
            {
                throw new ArgumentNullException(nameof(newName));   
            }
            Name = newName;
        }

        public void UpdateQueue()
        {
            QueueQuestion++;
        }

        public override string ToString()
        {
            string str = $"[{Name}, {ChatId}] ";
            return str;
        }
    }
}