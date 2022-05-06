using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TestBot.BLL.Questions;
using Excel = Microsoft.Office.Interop.Excel;

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
                    ProccessingStartMessage(botUpdates);
                }
                else if (botUpdates != null && botUpdates.Text != "/start" && TestingGroup != null &&
                     TestingGroup[botUpdates.Chat.Id].IsTest == false &&
                     TestingGroup[botUpdates.Chat.Id].QuestionNumber < TestingGroup[botUpdates.Chat.Id].Questions.Count)
                {
                    ProccessingInputQuestion(botUpdates, botClient);
                }
                else if (update.CallbackQuery != null && update.CallbackQuery.Message != null && TestingGroup != null &&
                    TestingGroup[update.CallbackQuery.Message.Chat.Id].IsTest == false &&
                    TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber < TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions.Count)
                {
                    ProccessingOrderQuestion(update, botClient);
                }
                else if (botUpdates != null && botUpdates.Text != "/start" && TestingGroup != null &&
                     TestingGroup[botUpdates.Chat.Id].IsTest == true &&
                     TestingGroup[botUpdates.Chat.Id].QuestionNumber < TestingGroup[botUpdates.Chat.Id].Questions.Count)
                {
                    ProccessingTestInputQuestion(botUpdates, botClient);
                }
                else if (update.CallbackQuery != null && update.CallbackQuery.Message != null && TestingGroup != null &&
                    TestingGroup[update.CallbackQuery.Message.Chat.Id].IsTest == true &&
                    TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber < TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions.Count)
                {
                    ProccessingTestOrderQuestion(update, botClient);
                }
                else if (update.CallbackQuery != null && update.CallbackQuery.Message != null && TestingGroup != null &&
                   TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber >= TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions.Count)
                {
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Тест окончен!");
                }
                else if(botUpdates != null && botUpdates.Text != "/start" && TestingGroup != null &&
                    TestingGroup[botUpdates.Chat.Id].QuestionNumber >= TestingGroup[botUpdates.Chat.Id].Questions.Count)
                {
                    await botClient.SendTextMessageAsync(botUpdates.Chat.Id, "Тест окончен!");
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
            TestController testController = TestController.GetTestController();
            TestingGroup = testController.GetDictionary();
            SendFirstMessage();
        }

        public void Stop()
        {
            _isReceive = false;
            TestController testController = TestController.GetTestController();
            SaveReport(TestingGroup, testController);
        }

        public async void SendFirstMessage()
        {
            foreach(var key in TestingGroup.Keys)
            {
                await _client.SendTextMessageAsync(key, TestingGroup[key].Questions[0].Description,
                    replyMarkup: TestingGroup[key].Questions[0]._keyboardMaker.GetKeyboard(TestingGroup[key].Questions[0].Options)) ;
            }
        }

        public void UpdateIds(List<long> usersId)
        {
            _usersId = usersId;
        }

        public async void ProccessingInputQuestion(Message botUpdates, ITelegramBotClient botClient)
        {
            var currentQuestion = TestingGroup[botUpdates.Chat.Id].Questions[TestingGroup[botUpdates.Chat.Id].QuestionNumber];
            if (botUpdates.Text != null && currentQuestion is InputQuestion)
            {
                if (currentQuestion._test.CheckInput(botUpdates.Text))
                {
                    currentQuestion.UserAnswers.Add(botUpdates.Text);
                    TestingGroup[botUpdates.Chat.Id].QuestionNumberIncrement();

                    if (TestingGroup[botUpdates.Chat.Id].QuestionNumber < TestingGroup[botUpdates.Chat.Id].Questions.Count)
                    {
                        currentQuestion = TestingGroup[botUpdates.Chat.Id].Questions[TestingGroup[botUpdates.Chat.Id].QuestionNumber];
                    }
                }
                if (TestingGroup[botUpdates.Chat.Id].QuestionNumber < TestingGroup[botUpdates.Chat.Id].Questions.Count)
                {
                    await botClient.SendTextMessageAsync(botUpdates.Chat.Id, currentQuestion.Description,
                    replyMarkup: currentQuestion._keyboardMaker.GetKeyboard(currentQuestion.Options));
                }
            }
        }

        public async void ProccessingTestInputQuestion(Message botUpdates, ITelegramBotClient botClient)
        {
            var currentQuestion = TestingGroup[botUpdates.Chat.Id].Questions[TestingGroup[botUpdates.Chat.Id].QuestionNumber];
            if (botUpdates.Text != null && currentQuestion is InputQuestion)
            {
                if (currentQuestion._test.CheckInput(botUpdates.Text))
                {
                    currentQuestion.UserAnswers.Add(botUpdates.Text);
                    TestingGroup[botUpdates.Chat.Id].QuestionNumberIncrement();
                    if (currentQuestion._test.CheckAnswer(new List<string>() { botUpdates.Text }, currentQuestion.CorrectAnswers))
                    {
                        await botClient.SendTextMessageAsync(botUpdates.Chat.Id, "✅");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(botUpdates.Chat.Id, "❌");
                    }
                }
                if (TestingGroup[botUpdates.Chat.Id].QuestionNumber < TestingGroup[botUpdates.Chat.Id].Questions.Count)
                {
                    currentQuestion = TestingGroup[botUpdates.Chat.Id].Questions[TestingGroup[botUpdates.Chat.Id].QuestionNumber];

                    await botClient.SendTextMessageAsync(botUpdates.Chat.Id, currentQuestion.Description,
                        replyMarkup: currentQuestion._keyboardMaker.GetKeyboard(currentQuestion.Options));
                }
            }
        }

        public async void ProccessingOrderQuestion(Update update, ITelegramBotClient botClient)
        {
            var currentQuestion = TestingGroup[update.CallbackQuery!.Message!.Chat.Id].Questions[TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber];
            if (update.CallbackQuery != null &&
                TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber < TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions.Count &&
                (currentQuestion is OrderQuestion || currentQuestion is OptionQuestion))
            {
                if (update.CallbackQuery.Data != null && update.CallbackQuery.Data != "Подтвердить" && currentQuestion._test.CheckInput(update.CallbackQuery.Data))
                {
                    currentQuestion.UserAnswers.Add(update.CallbackQuery.Data);
                }
                else if (update.CallbackQuery != null && update.CallbackQuery.Data == "Подтвердить")
                {
                    TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumberIncrement();

                    if (TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber < TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions.Count)
                    {
                        currentQuestion = TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions[TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber];

                        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, currentQuestion.Description,
                            replyMarkup: currentQuestion._keyboardMaker.GetKeyboard(currentQuestion.Options));
                    }
                }
            }
        }

        public async void ProccessingTestOrderQuestion(Update update, ITelegramBotClient botClient)
        {
            var currentQuestion = TestingGroup[update.CallbackQuery!.Message!.Chat.Id].Questions[TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber];
            var currentUserTestData = TestingGroup[update.CallbackQuery!.Message!.Chat.Id];
            if (update.CallbackQuery != null &&
                TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber < TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions.Count &&
                (currentQuestion is OrderQuestion || currentQuestion is OptionQuestion))
            {
                if (update.CallbackQuery.Data != null && update.CallbackQuery.Data != "Подтвердить" && currentQuestion._test.CheckInput(update.CallbackQuery.Data))
                { 
                    currentUserTestData.UserAnswers[currentQuestion].Add(update.CallbackQuery.Data);
                }
                else if (update.CallbackQuery != null && update.CallbackQuery.Data == "Подтвердить")
                {

                    if (currentQuestion._test.CheckAnswer(currentUserTestData.UserAnswers[currentQuestion], currentQuestion.CorrectAnswers))
                    {
                        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "✅");
                    }
                    else
                    {
                        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "❌");
                    }

                    TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumberIncrement();

                    if (TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber < TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions.Count)
                    {
                        currentQuestion = TestingGroup[update.CallbackQuery.Message.Chat.Id].Questions[TestingGroup[update.CallbackQuery.Message.Chat.Id].QuestionNumber];

                        await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, currentQuestion.Description,
                            replyMarkup: currentQuestion._keyboardMaker.GetKeyboard(currentQuestion.Options));
                    }
                }
            }
        }

        public void ProccessingStartMessage(Message botUpdates)
        {
            _usersId.Add(botUpdates.Chat.Id);
            User newUser = new User(botUpdates.Chat.FirstName!, botUpdates.Chat.Id);
            _onMessage(newUser);
        }

        public void SaveReport(Dictionary<long, UserTestData> testingGroup, TestController testController)
        {
            string json = JsonSerializer.Serialize(testingGroup);
            using (StreamWriter writer = new StreamWriter($@"D:\{testController.UsersTest.Name}.json", false))
            {
                writer.WriteAsync(json);
            }
        }
    }
}