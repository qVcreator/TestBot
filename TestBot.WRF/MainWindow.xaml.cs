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

namespace TestBot.WRF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Group> Groups { get; private set; }
        private UserData _selectedUser;
        private bool _isRefresh;


        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow1_Initialized(object sender, EventArgs e)
        {
            Groups = new List<Group>();
            Groups.Add(GroupMock.GetMock(GroupEnums.group1));
            Groups.Add(GroupMock.GetMock(GroupEnums.group2));
            Groups.Add(GroupMock.GetMock(GroupEnums.group3));

            var userData = LoadUserData();
            DataGridShowUsers.ItemsSource = userData;
            _isRefresh = false;
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

            data = GetUsersInGroup();
            DataGridShowUsers.ItemsSource = data;
        }

        private List<UserData> LoadUserData()
        {
            List<UserData> data = new List<UserData>();
            foreach (var group in Groups)
            {
                ComboBoxShowUsers.Items.Add(group.Name);
                ComboBoxNewUserGroup.Items.Add(group.Name);
                ComboBoxDeleteGroup.Items.Add(group.Name);
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

        private void DataGridShowUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataGridShowUsers.CurrentCell.Item is UserData)
            {
                _selectedUser = (UserData)(DataGridShowUsers.CurrentCell.Item);
                TextBoxSelectedUserName.Text = _selectedUser.Name;
                TextBoxSelectedUserGroup.Text = _selectedUser.Group;
            }
        }
    }
}
