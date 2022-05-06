using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TestBot.BLL;
using TestBot.BLL.Mocks;
using System.Windows.Threading;
using TestBot.BLL.Telegram;
using System.Text.Json;
using System.IO;
using TestBot.BLL.GroupModels;
using TestBot.BLL.Questions;
using TestBot.BLL.Interfaces;
using TestBot.BLL.Interfaces.Implementations;
using TestBot.BLL.Models;

namespace TestBot.WRF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Group> Groups { get; private set; }

        public List<Group> SendGroup { get; private set; }

        public List<Test> Tests { get; private set; }

        private UserData _selectedUser; 
        private DispatcherTimer _timer;
        private const string _token = "5265334359:AAGJciyVQB0wg6YHbnIMmHSNBFMOQxZlrBs";
        private TelegramManager _telegramManager;

        public MainWindow()
        {
            _telegramManager = new TelegramManager(_token, OnMessage);
            InitializeComponent();


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void MainWindow1_Initialized(object sender, EventArgs e)
        {

            Groups = new List<Group>();
            Tests = new List<Test>();
            SendGroup = new List<Group>();
            Groups.Add(new Group("Другие"));
            Tests.Add(TestMock.GetMock(TestEnums.TestTelega));


            using (StreamReader reader = new StreamReader(@"D:\groups.json"))
            {
                string groupsJson = reader.ReadToEnd();
                if(groupsJson != "" || groupsJson != null)
                {
                    Groups.Clear();
                }
                List<GroupModel> groupModels = JsonSerializer.Deserialize<List<GroupModel>>(groupsJson)!;
                List<long> ids = new List<long>();
                foreach(var groupModel in groupModels)
                {
                    Groups.Add(new Group(groupModel.Name, groupModel.Users));
                    for (int i = 0; i < groupModel.Users.Count; i++)
                    {
                        ids.Add(groupModel.Users[i].ChatId);
                    }
                }
                _telegramManager.UpdateIds(ids);
            }
            //using (StreamReader reader = new StreamReader(@"C:\groups.json"))
            //{
            //    string groupsJson = reader.ReadToEnd();
            //    List<GroupModel> groupModels = JsonSerializer.Deserialize<List<GroupModel>>(groupsJson)!;
            //    List<long> ids = new List<long>();
            //    foreach (var groupModel in groupModels)
            //    {
            //        Groups.Add(new Group(groupModel.Name, groupModel.Users));
            //        for (int i = 0; i < groupModel.Users.Count; i++)
            //        {
            //            ids.Add(groupModel.Users[i].ChatId);
            //        }
            //    }
            //    _telegramManager.UpdateIds(ids);
            //}

            var data = LoadUserData();
            LoadTests();
            DataGridShowUsers.ItemsSource = data;
        }

        public void OnMessage(User newUser)
        {
            Groups[0].AddUser(newUser);
        }

        private void OnTick(object sender, EventArgs e)
        {
            var data = GetUsersInGroup();
            DataGridShowUsers.ItemsSource = data;
        }

        private void ComboBoxShowUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Groups != null)
            {
                var userData = GetUsersInGroup();
                DataGridShowUsers.ItemsSource = userData;
            }
        }

        private void ButtonSaveChangeUser_Click(object sender, RoutedEventArgs e)
        {
            List<UserData> data;
            string oldGroup = TextBoxSelectedUserGroup.Text;
            string oldName = TextBoxSelectedUserName.Text;
            var newGroup = ComboBoxNewUserGroup.SelectedItem;
            var newName = TextBoxNewUserName.Text;
            bool isDeleted = false;
            bool isAdded = false;

            if ((oldGroup != "" && oldName != "") && !(newName == "" && newGroup == null))
            {
                User user = new User(_selectedUser.Name, Convert.ToInt64(_selectedUser.ChatId));
                if (newGroup != null && newGroup.ToString() != oldGroup)
                {
                    for (int i = 0; i < Groups.Count; i++)
                    {
                        if (Groups[i].Name == oldGroup.ToString())
                        {
                            Groups[i].DeleteUser(user.ChatId);
                            isDeleted = true;
                        }
                        if (Groups[i].Name == newGroup.ToString())
                        {
                            Groups[i].AddUser(user);
                            isAdded = true;
                        }
                        if ((isAdded is true) && (isDeleted is true))
                        {
                            TextBoxSelectedUserGroup.Text = newGroup.ToString();
                            break;
                        }
                    }
                }


                if (newName != "")
                {
                    bool isChanged = false;
                    foreach (var group in Groups)
                    {
                        foreach (var changeableUser in group.Users)
                        {
                            if (changeableUser.Name == oldName)
                            {
                                changeableUser.ChangeName(newName);
                                TextBoxSelectedUserName.Text = newName;
                                isChanged = true;
                                break;
                            }
                        }
                        if (isChanged)
                        {
                            break;
                        }
                    }
                }

                TextBoxNewUserName.Text = "";
                ComboBoxNewUserGroup.SelectedIndex = -1;
                data = GetUsersInGroup();
                DataGridShowUsers.ItemsSource = data;
            }
            else
            {
                popupMisingArgs.IsOpen = true;
            }
        }

        private void DataGridShowUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridShowUsers.CurrentCell.Item is UserData)
            {
                _selectedUser = (UserData)(DataGridShowUsers.CurrentCell.Item);
                TextBoxSelectedUserName.Text = _selectedUser.Name;
                TextBoxSelectedUserGroup.Text = _selectedUser.Group;
            }
        }

        private void ButtonChangeGroupName_Click(object sender, RoutedEventArgs e)
        {
            string oldName;
            string newName;
            bool isSucces = false;
            if (ComboBoxOldGroupName.SelectedIndex != -1 && TextBoxNewNameGroup.Text != "")
            {
                oldName = ComboBoxOldGroupName.SelectedValue.ToString()!;
                newName = TextBoxNewNameGroup.Text;
                foreach(var group in Groups)
                {
                    if (group.Name == oldName)
                    {
                        group.ChangeName(newName);
                        isSucces = true;
                        break;
                    }
                }
            }
            else
            {
                popupMisingArguments.IsOpen = true;
            }

            if (isSucces)
            {
                UpdateComboBoxes();
                var data = GetUsersInGroup();
                DataGridShowUsers.ItemsSource = data;
                TextBoxNewNameGroup.Text = "";
            }
        }

        private void ButtonAddGroup_Click(object sender, RoutedEventArgs e)
        {

            string name = TextBoxNewGroupName.Text;
            if (name != "")
            {
                Groups.Add(new Group(name));
                ComboBoxShowUsers.Items.Add(name);
                ComboBoxNewUserGroup.Items.Add(name);
                ComboBoxDeleteGroup.Items.Add(name);
                ComboBoxOldGroupName.Items.Add(name);
                ComboBoxChooseGroup.Items.Add(name);
            }
            else
            {
                popupMisingAddNameArgument.IsOpen = true;
            }
        }

        private void ButtonDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxDeleteGroup.SelectedIndex != -1)
            {
                for (int i = 0; i < Groups.Count; i++)
                {
                    if (Groups[i].Name == ComboBoxDeleteGroup.SelectedValue.ToString()!)
                    {
                        var usersToDelete = Groups[i].Users;
                        for(int j = 0; j < usersToDelete.Count; j++)
                        {
                            Groups[0].AddUser(Groups[i].Users[j]);
                        }
                        Groups.RemoveAt(i);
                        break;
                    }
                }
                UpdateComboBoxes();
                var data = GetUsersInGroup();
                DataGridShowUsers.ItemsSource = data;
            }
            else
            {
                popupMisingDeletNameArgument.IsOpen = true;
            }
        }

        private void CheckBoxDeadLine_Click(object sender, RoutedEventArgs e)
        {
            if(CheckBoxDeadLine.IsChecked is true)
            {
                DatePickerDuration.Visibility = Visibility.Visible;
                TextBoxDuration.Visibility = Visibility.Hidden;
            }
            else
            {
                TextBoxDuration.Visibility = Visibility.Visible;
                DatePickerDuration.Visibility = Visibility.Hidden;
            }
        }

        private void ButtonStartTest_Click(object sender, RoutedEventArgs e)
        {
            TestController testController = TestController.GetTestController();
            if (ComboBoxTestsToSend.SelectedValue != null && SendGroup.Count != 0)
            {
                var chosenTest = ComboBoxTestsToSend.SelectedValue.ToString()!;
                foreach (var test in Tests)
                {
                    if (test.Name == chosenTest)
                    {
                        testController.SetTest(test);
                        testController.SetType(test.IsTest);
                        break;
                    }
                }
                testController.SetGroup(SendGroup);

                _telegramManager.Start();
            }
            else
            {
                popupMisingStartTestArgs.IsOpen = true;
            }
        }

        private void ButtonStopTest_Click(object sender, RoutedEventArgs e)
        {
            _telegramManager.Stop();
        }

        private void ButtonAddGroupToSend_Click(object sender, RoutedEventArgs e)
        {
            bool isContain = false;
            foreach(var group in SendGroup)
            {
                if (ComboBoxChooseGroup.SelectedValue != null && group.Name == ComboBoxChooseGroup.SelectedValue.ToString())
                {
                    isContain = true;
                }
            }
            if (ComboBoxChooseGroup.SelectedValue is null)
            {
                popupMisingTestGroup.IsOpen = true;
            }
            else if (isContain)
            {
                popupContainsTestGroup.IsOpen = true;
            }
            else
            {
                var selectedGroup = ComboBoxChooseGroup.SelectedValue.ToString()!;
                for (int i = 0; i < Groups.Count; i++)
                {
                    if(selectedGroup == Groups[i].Name)
                    {
                        SendGroup.Add(Groups[i]);
                    }
                }
            }
        }
        private void ButtonCreateTest_Click(object sender, RoutedEventArgs e)
        {
            var testName = TextBoxTestName.Text;
            if (TextBoxTestName.Text != "" && (TextBoxDuration.Text != "" || DatePickerDuration.SelectedDate != null))
            {
                ComboBoxTestNameEdit.Items.Add(testName);
                if (TextBoxDuration.IsVisible)
                {
                    if (RadioButtonPoll.IsChecked is true)
                    {
                        double testDuration = Convert.ToDouble(TextBoxDuration.Text);
                        Tests.Add(new Test(testName, SendGroup, testDuration, false));
                    }
                    else
                    {
                        double testDuration = Convert.ToDouble(TextBoxDuration.Text);
                        Tests.Add(new Test(testName, SendGroup, testDuration, true));
                    }
                }
                else
                {
                    if (RadioButtonPoll.IsChecked is true)
                    {
                        DateTime finishDate = (DateTime)(DatePickerDuration.SelectedDate);
                        Tests.Add(new Test(testName, SendGroup, finishDate, false));
                    }
                    else
                    {
                        DateTime finishDate = (DateTime)(DatePickerDuration.SelectedDate);
                        Tests.Add(new Test(testName, SendGroup, finishDate, true));
                    }
                }
            }
            else
            {
                popupMisingTestArgs.IsOpen = true;
            }

            ComboBoxTestNameSelect.Items.Add(TextBoxTestName.Text);
            ComboBoxTestsToSend.Items.Add(TextBoxTestName.Text);
        }

        private List<QuestionModel> LoadQuestionData()
        {
            List<QuestionModel> questionData = new List<QuestionModel>();

            foreach (var test in Tests)
            {
                if (ComboBoxTestNameSelect.SelectedValue.ToString() != null && test.Name == ComboBoxTestNameSelect.SelectedValue.ToString())
                {
                    for (int i = 0; i < test.Questions.Count; i++)
                    {
                        string[] tmp = test.Questions[i].GetType().ToString().Split(".");

                        questionData.Add(new QuestionModel(tmp[tmp.Count()-1], test.Questions[i].Description, i+1));
                    }
                    break;
                }
                else
                {

                }


            }

            return questionData;

        }

        private List<UserData> LoadUserData()
        {
            List<UserData> data = new List<UserData>();
            foreach (var group in Groups)
            {
                ComboBoxShowUsers.Items.Add(group.Name);
                ComboBoxNewUserGroup.Items.Add(group.Name);
                ComboBoxDeleteGroup.Items.Add(group.Name);
                ComboBoxOldGroupName.Items.Add(group.Name);
                ComboBoxChooseGroup.Items.Add(group.Name);
                foreach (var user in group.Users)
                {
                    if (group.Users.Count != 0)
                    {
                        
                    }
                }
            }
            ComboBoxDeleteGroup.Items.RemoveAt(0);
            return data;
        }

        private List<UserData> GetUsersInGroup()
        {
            List<UserData> data = new List<UserData>();
            bool allUsers = ComboBoxShowUsers.SelectedValue.ToString()!.Contains("Все пользователи");
            foreach (Group group in Groups)
            {
                if (ComboBoxShowUsers.SelectedValue.ToString()! == group.Name)
                {
                    foreach (var user in group.Users)
                    {
                        data.Add(new UserData()
                        {
                            Name = user.Name,
                            ChatId = $"{user.ChatId}",
                            Group = group.Name
                        });
                    }
                }
                else if (allUsers is true)
                {
                    data = GetAllUsers();
                }
            }
            return data;
        }

        private List<UserData> GetAllUsers()
        {
            List<UserData> data = new List<UserData>();
            foreach (var group in Groups)
            {
                foreach (var user in group.Users)
                {
                    if (group.Users.Count != 0)
                    {
                        data.Add(new UserData()
                        {
                            Name = user.Name,
                            ChatId = $"{user.ChatId}",
                            Group = group.Name
                        });
                    }
                }
            }
            return data;
        }

        private void LoadTests()
        {
            ComboBoxTestsToSend.Items.Clear();
            foreach (var test in Tests)
            {
                ComboBoxTestsToSend.Items.Add(test.Name);
                ComboBoxTestNameEdit.Items.Add(test.Name);
                ComboBoxTestNameSelect.Items.Add(test.Name);
            }
        }

        private void UpdateComboBoxes()
        {
            ComboBoxNewUserGroup.Items.Clear();
            ComboBoxDeleteGroup.Items.Clear();
            ComboBoxOldGroupName.Items.Clear();
            ComboBoxChooseGroup.Items.Clear();
            foreach (var group in Groups)
            {
                ComboBoxNewUserGroup.Items.Add(group.Name);
                ComboBoxDeleteGroup.Items.Add(group.Name);
                ComboBoxOldGroupName.Items.Add(group.Name);
                ComboBoxChooseGroup.Items.Add(group.Name);
            }
            for(int i = 2; i < ComboBoxShowUsers.Items.Count; i++)
            {
                ComboBoxShowUsers.Items.RemoveAt(2);
            }
            foreach(var group in Groups)
            {
                if (group.Name != "Другие")
                {
                    ComboBoxShowUsers.Items.Add(group.Name);
                }
            }
            ComboBoxDeleteGroup.Items.RemoveAt(0);
        }

        private void MainWindow1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string json = JsonSerializer.Serialize(Groups);
            using (StreamWriter writer = new StreamWriter(@"D:\groups.json", false))
            {
                writer.WriteAsync(json);
            }
        }

        private void ButtonClearGroups_Click(object sender, RoutedEventArgs e)
        {
            if(SendGroup != null)
            {
                SendGroup.Clear();
            }
        }

        private void ComboBoxTestNameSelect_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var questionData = LoadQuestionData();
            DataGridShowQuestions.ItemsSource = questionData; 
        }

        private void ButtonAddQuestion_Click(object sender, RoutedEventArgs e)
        {
            ButtonAddQuestion.IsEnabled = false;


            RadioButtonInputQuestion.Visibility = Visibility.Visible;
            RadioButtonOrderQuestion.Visibility = Visibility.Visible;
            RadioButtonOptionQuestion.Visibility = Visibility.Visible;
            QuestionTextLabel.Visibility = Visibility.Visible;
            TextBoxNewQuestionText.Visibility = Visibility.Visible;


        }

        private void SaveNewQuestionButton_Click(object sender, RoutedEventArgs e)
        {
            if (RadioButtonInputQuestion.IsChecked == true && (TextBoxNewQuestionText.Text.Trim() == "" || TextBoxCorrectAnswer.Text.Trim() == ""))
            {
                popupInvalidProperties.IsOpen = true;
            }
            else if (RadioButtonOptionQuestion.IsChecked == true && ComboBoxNewQuestionOptions.Items.Count == 0)
            {
                popupInvalidProperties.IsOpen = true;
            }
            else if (RadioButtonOrderQuestion.IsChecked == true && ComboBoxNewQuestionOptions.Items.Count == 0)
            {
                popupInvalidProperties.IsOpen = true;
            }
            else if (!string.IsNullOrEmpty(TextBoxNewQuestionText.Text) && TextBoxNewQuestionText.Text != " ")
            {
                CreateQuestion();
            

                UpdateDataGridShowQuestions();

                ButtonAddQuestion.IsEnabled = true;

                RadioButtonInputQuestion.IsChecked = false;
                RadioButtonOrderQuestion.IsChecked = false;
                RadioButtonOptionQuestion.IsChecked = false;

                RadioButtonInputQuestion.Visibility = Visibility.Hidden;
                RadioButtonOrderQuestion.Visibility = Visibility.Hidden;
                RadioButtonOptionQuestion.Visibility = Visibility.Hidden;

                QuestionTextLabel.Visibility = Visibility.Hidden;
                TextBoxNewQuestionText.Clear();
                TextBoxNewQuestionText.Visibility = Visibility.Hidden;
                ButtonSaveNewQuestion.Visibility = Visibility.Hidden;
                ComboBoxNewQuestionOptions.ItemsSource = new List<OptionTestModel>();
                ComboBoxNewQuestionOptions.Visibility = Visibility.Hidden;
                LabelCorrectAnser.Visibility = Visibility.Hidden;
                TextBoxCorrectAnswer.Clear();
                TextBoxCorrectAnswer.Visibility = Visibility.Hidden;
                CheckBoxCorrectAnswer.IsChecked = false;


                ButtonSaveNewQuestion.Margin = new Thickness(0, 339, 0, 0);
                TextBoxCorrectAnswer.Margin = new Thickness(77, 316, 0, 0);
            }
            else
            {
                popupInvalidProperties.IsOpen = true;
            }
        }

        private void RadioButtonInputQuestion_Checked(object sender, RoutedEventArgs e)             //Input
        {
            TextBoxCorrectAnswer.Visibility = Visibility.Visible;
            ButtonSaveNewQuestion.Visibility = Visibility.Visible;
            LabelCorrectAnser.Visibility = Visibility.Visible;

            ButtonSaveNewQuestion.Margin = new Thickness(0, 339, 0, 0);
            TextBoxCorrectAnswer.Margin = new Thickness(77, 316, 0, 0);


        }

        private void RadioButtonInputQuestion_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxCorrectAnswer.Visibility = Visibility.Hidden;
            LabelCorrectAnser.Visibility = Visibility.Hidden;

            ButtonSaveNewQuestion.Margin = new Thickness(0, 438, 0, 0);

        }

        private void RadioButtonOptionQuestion_Checked(object sender, RoutedEventArgs e)            //Option
        {
            ComboBoxNewQuestionOptions.Visibility = Visibility.Visible;
            TextBoxCorrectAnswer.Visibility = Visibility.Visible;
            LabelCorrectAnser.Visibility = Visibility.Visible;
            ButtonSaveNewQuestion.Visibility = Visibility.Visible;
            ButtonAddOption.Visibility = Visibility.Visible;
            CheckBoxCorrectAnswer.Visibility = Visibility.Visible;


            LabelCorrectAnser.Margin = new Thickness(13, 312, 0, 0);
            ButtonSaveNewQuestion.Margin = new Thickness(0, 414, 0, 0);
            TextBoxCorrectAnswer.Margin = new Thickness(57, 316, 0, 0);
        }

        private void RadioButtonOptionQuestion_Unchecked(object sender, RoutedEventArgs e)
        {
            ComboBoxNewQuestionOptions.Visibility = Visibility.Hidden;
            TextBoxCorrectAnswer.Visibility = Visibility.Visible;
            LabelCorrectAnser.Visibility = Visibility.Hidden;
            ButtonAddOption.Visibility = Visibility.Hidden;
            CheckBoxCorrectAnswer.Visibility = Visibility.Hidden;
            CheckBoxCorrectAnswer.IsChecked = false;




            LabelCorrectAnser.Margin = new Thickness(33, 312, 0, 0);
            TextBoxCorrectAnswer.Margin = new Thickness(77, 316, 0, 0);
        }

        private void RadioButtonOrderQuestion_Checked(object sender, RoutedEventArgs e)             //Order
        {
            ComboBoxNewQuestionOptions.Visibility = Visibility.Visible;
            ButtonSaveNewQuestion.Visibility = Visibility.Visible;
            TextBoxCorrectAnswer.Visibility = Visibility.Visible;
            LabelCorrectAnser.Visibility= Visibility.Visible;
            ButtonAddOption.Visibility = Visibility.Visible;

            ButtonSaveNewQuestion.Margin = new Thickness(0, 414, 0, 0);
            LabelCorrectAnser.Margin = new Thickness(13, 312, 0, 0);
            TextBoxCorrectAnswer.Margin = new Thickness(57, 316, 0, 0);
        }

        private void RadioButtonOrderQuestion_Unchecked(object sender, RoutedEventArgs e)
        {
            ButtonSaveNewQuestion.Margin = new Thickness(0, 438, 0, 0);

            ComboBoxNewQuestionOptions.Visibility = Visibility.Hidden;
            ButtonAddOption.Visibility = Visibility.Hidden;

        }

        private void ButtonAddOption_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxNewQuestionOptions.Items.Count == 3 )
            {
                ButtonAddOption.IsEnabled = false;
            }


            if (!string.IsNullOrEmpty(TextBoxCorrectAnswer.Text))
            {
                var optionsData = LoadOptionsData();
                ComboBoxNewQuestionOptions.ItemsSource = optionsData;

                TextBoxCorrectAnswer.Clear();
                CheckBoxCorrectAnswer.IsChecked = false;

            }

        }

        private List<OptionTestModel> LoadOptionsData()
        {
            var optionsData = OptionsOutput.GetOptionsOutput();


                if (!string.IsNullOrEmpty(TextBoxNewQuestionText.Text) && TextBoxNewQuestionText.Text.Trim() != "")
                {
                    int id = optionsData.Options.Count() + 1;
                    string name = TextBoxCorrectAnswer.Text;
                    string answer;

                    if (CheckBoxCorrectAnswer.IsChecked == true)
                    {
                        answer = "Верный";
                    }
                    else
                    {
                        answer = "Неверный";
                    }

                    if (name != "")
                    {
                        optionsData.AddOptions(new OptionTestModel(id, name, answer));
                    }
                }

            ComboBoxNewQuestionOptions.Items.Refresh();

            return optionsData.Options;
        }

        private Test GetTestByName(string testName)
        {
            Test test = null;

            foreach (var item in Tests)
            {
                if (item.Name == testName)
                {
                    test = item;
                }

            }
            return test;
        }

        private void CreateQuestion()
        {
            string questionDescription = TextBoxNewQuestionText.Text;
            string answer = TextBoxCorrectAnswer.Text;
            string testName = ComboBoxTestNameSelect.Text;
            List<string> correctAnswers = new List<string>();
            List<string> options = new List<string>();

            ITester tester;
            IKeyboardMaker keyboardMaker;
            AbstractQuestion currentQuestion;

            if (string.IsNullOrEmpty(questionDescription))
            {
                popupInvalidProperties.IsOpen = false;
            }
            else
            {
                if (RadioButtonInputQuestion.IsChecked == true)
                {
                    tester = new InputTester();
                    keyboardMaker = new InputKeyboardMaker();

                    correctAnswers.Add(answer);

                    currentQuestion = new InputQuestion(questionDescription, correctAnswers, tester, keyboardMaker);

                    GetTestByName(testName).Questions.Add(currentQuestion);
                }
                else if (RadioButtonOptionQuestion.IsChecked == true)
                {
                    tester = new OptionTester();

                    keyboardMaker = new OrderKeyboardMaker();

                    List<OptionTestModel> optionsData = LoadOptionsData();

                    foreach (var item in optionsData)
                    {
                        options.Add(item.Name);

                        if (item.Answer == "Верный")
                        {
                            correctAnswers.Add(item.Name);
                        }
                    }

                    optionsData.Clear();
                    currentQuestion = new OptionQuestion(questionDescription, options, correctAnswers, tester, keyboardMaker);

                    GetTestByName(testName).Questions.Add(currentQuestion);
                    ComboBoxNewQuestionOptions.Items.Refresh();

                }
                else if (RadioButtonOrderQuestion.IsChecked == true)
                {
                    tester = new OrderTester();
                    keyboardMaker = new OrderKeyboardMaker();

                    List<OptionTestModel> optionsData = LoadOptionsData();

                    foreach (var item in optionsData)
                    {
                        options.Add(item.Name);
                        correctAnswers.Add(item.Name);
                    }
                    optionsData.Clear();
                    currentQuestion = new OrderQuestion(questionDescription, options, correctAnswers, tester, keyboardMaker);

                    GetTestByName(testName).Questions.Add(currentQuestion);
                    ComboBoxNewQuestionOptions.Items.Refresh();

                }

            }

        }

        private void TextBoxCorrectAnswer_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxNewQuestionText.Text.Trim() != "" && TextBoxCorrectAnswer.Text.Trim() != "")
            {
                ButtonAddOption.IsEnabled = true;
            }
            else
            {
                ButtonAddOption.IsEnabled = false;

            }
        }

        private void UpdateDataGridShowQuestions()
        {
            var questionData = LoadQuestionData();
            DataGridShowQuestions.ItemsSource = questionData;
        }

        private void TextBoxNewQuestionText_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxNewQuestionText.Text.Trim() != "" && TextBoxCorrectAnswer.Text.Trim() != "")
            {
                ButtonAddOption.IsEnabled = true;
            }
            else
            {
                ButtonAddOption.IsEnabled = false;
            }
        }

        private void ButtonDeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            string testName = ComboBoxTestNameSelect.Text;
            dynamic row = DataGridShowQuestions.SelectedItem;
            string name = row.Description;

            foreach (var test in Tests)
            {
                if (test.Name == testName)
                {
                    test.DeleteQuestion(name);
                }
            }

            ButtonDeleteQuestion.IsEnabled = false;
            DataGridShowQuestions.ItemsSource = LoadQuestionData();

        }

        private void DataGridShowQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridShowQuestions.SelectedValue != null)
            {
                ButtonDeleteQuestion.IsEnabled = true;
            }
        }
    }
}
