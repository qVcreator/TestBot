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

namespace TestBot.WRF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Group> Groups { get; private set; }
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
            Groups.Add(new Group("Другие"));

            var data = LoadUserData();
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

        private List<UserData> LoadUserData()
        {
            List<UserData> data = new List<UserData>();
            foreach (var group in Groups)
            {
                ComboBoxShowUsers.Items.Add(group.Name);
                ComboBoxNewUserGroup.Items.Add(group.Name);
                ComboBoxDeleteGroup.Items.Add(group.Name);
                ComboBoxOldGroupName.Items.Add(group.Name);
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

        private void UpdateComboBoxes()
        {
            ComboBoxNewUserGroup.Items.Clear();
            ComboBoxDeleteGroup.Items.Clear();
            ComboBoxOldGroupName.Items.Clear();
            foreach(var group in Groups)
            {
                ComboBoxNewUserGroup.Items.Add(group.Name);
                ComboBoxDeleteGroup.Items.Add(group.Name);
                ComboBoxOldGroupName.Items.Add(group.Name);
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

        private void ButtonStartTest_Click(object sender, RoutedEventArgs e)
        {
            _telegramManager.Start();
        }

        private void ButtonStopTest_Click(object sender, RoutedEventArgs e)
        {
            _telegramManager.Stop();
        }
    }
}
