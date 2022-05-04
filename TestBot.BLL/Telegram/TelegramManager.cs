using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TestBot.BLL.Questions;

namespace TestBot.BLL.Telegram
{
    public class TelegramManager
    {
        private TelegramBotClient _client;
        private Action<User> _onMessage;
        private List<long> _usersId;
        private bool _isReceive;
        private Dictionary<long, UserTestData> TestingGroup;

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
                else if (botUpdates != null && botUpdates.Text != "/start" && TestingGroup != null)
                {
                    if (botUpdates.Text != null && TestingGroup[botUpdates.Chat.Id].Questions[TestingGroup[botUpdates.Chat.Id].QuestionNumber] is InputQuestion)
                    {
                        var currentQuestion = TestingGroup[botUpdates.Chat.Id].Questions[TestingGroup[botUpdates.Chat.Id].QuestionNumber];

                        if (currentQuestion._test.CheckInput(botUpdates.Text))
                        {
                            TestingGroup[botUpdates.Chat.Id].QuestionNumberIncrement();
                            currentQuestion = TestingGroup[botUpdates.Chat.Id].Questions[TestingGroup[botUpdates.Chat.Id].QuestionNumber];
                        }

                        await botClient.SendTextMessageAsync(botUpdates.Chat.Id, currentQuestion.Description,
                            replyMarkup: currentQuestion._keyboardMaker.GetKeyboard(currentQuestion.Options));
                    }
                }
                else if (update.CallbackQuery != null && TestingGroup != null)
                {
                    if (update.CallbackQuery != null && TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions[TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber] is OrderQuestion)
                    {
                        var currentQuestion = TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions[TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber];

                        if (update.CallbackQuery != null && update.CallbackQuery.Data != "Подтвердить" && currentQuestion._test.CheckInput(update.CallbackQuery.Data))
                        {
                            currentQuestion.UserAnswers.Add(update.CallbackQuery.Data);
                        }
                        else if (update.CallbackQuery != null && update.CallbackQuery.Data == "Подтвердить")
                        {
                            TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumberIncrement();
                            currentQuestion = TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions[TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber];

                            await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, currentQuestion.Description,
                                replyMarkup: currentQuestion._keyboardMaker.GetKeyboard(currentQuestion.Options));
                        }
                    }
                }
            }
        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void StartBot()
        {
            _client.StartReceiving(HandleResive, HandleError);
            _isReceive = true;
        }

        public void Start()
        {
            _isReceive = true;
            TestController testController = TestController.GetTestController();
            TestingGroup = testController.GetDictionary();
            SendFirstMessage();
        }

        public void Stop()
        {
            _isReceive = false;
        }

        public void SendFirstMessage()
        {
            foreach(var key in TestingGroup.Keys)
            {
                _client.SendTextMessageAsync(key, TestingGroup[key].Questions[0].Description) ;
            }
        }

        public void UpdateIds(List<long> usersId)
        {
            _usersId = usersId;
        }
    }
}