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
        }
        
        private void ComboBoxShowUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var userData = GetUsersInGroup();
            DataGridShowUsers.ItemsSource = userData;
        }
        private void ButtonSaveChangeUser_Click(object sender, RoutedEventArgs e)
        {
            List<UserData> data;
            var oldGroup = DataGridShowUsers.SelectedCells[2];
            var newGroup = ComboBoxChangeUserGroup.SelectedItem;
            User selectedUser = new User(DataGridShowUsers.SelectedCells[0].ToString()!,
                Convert.ToInt64(DataGridShowUsers.SelectedCells[1].ToString()));

            DataGridShowUsers.SelectedCells[2] = (DataGridCellInfo)(newGroup);

            for(int i = 0; i< Groups.Count; i++)
            {
                if(Groups[i].Name == oldGroup.ToString())
                {
                    Groups[i].DeleteUser(selectedUser.ChatId);
                }
                if (Groups[i].Name == newGroup.ToString())
                {
                    Groups[i].AddUser(selectedUser);
                }
            }

            data = GetUsersInGroup();
            DataGridShowUsers.ItemsSource = data;

        }
        private void DataGridShowUsers_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            List<UserData> data;
            var oldGroup = DataGridShowUsers.SelectedCells[2];
            var newGroup = ComboBoxChangeUserGroup.SelectedItem;
            User selectedUser = new User(DataGridShowUsers.SelectedCells[0].ToString()!,
                Convert.ToInt64(DataGridShowUsers.SelectedCells[1].ToString()));

            DataGridShowUsers.SelectedCells[2] = (DataGridCellInfo)(newGroup);

            for (int i = 0; i < Groups.Count; i++)
            {
                if (Groups[i].Name == oldGroup.ToString())
                {
                    Groups[i].DeleteUser(selectedUser.ChatId);
                }
                if (Groups[i].Name == newGroup.ToString())
                {
                    Groups[i].AddUser(selectedUser);
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
                ComboBoxChangeUserGroup.Items.Add(group.Name);
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

    }
}
