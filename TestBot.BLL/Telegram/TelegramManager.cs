using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TestBot.BLL.Telegram
{
    public class TelegramManager
    {
        private TelegramBotClient _client;
        private Action<User> _onMessage;
        private List<long> _usersId;
        private bool _isReceive;

        public TelegramManager(string token, Action<User> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            _usersId = new List<long>();
            _isReceive = false;
        }

        private async Task HandleResive(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (_isReceive == true)
            {
                var botUpdates = update.Message;
                if (botUpdates != null && botUpdates.Text == "/start" && !(_usersId.Contains(botUpdates.Chat.Id)))
                {
                    _usersId.Add(botUpdates.Chat.Id);
                    User newUser = new User(botUpdates.Chat.FirstName!, botUpdates.Chat.Id);
                    _onMessage(newUser);
                }
            }
        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        public void Start()
        {
            _client.StartReceiving(HandleResive, HandleError);
            _isReceive = true;
        }

        public void Stop()
        {
            _isReceive = false;
        }
    }
}