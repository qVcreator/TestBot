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
            List<Group> groups = new List<Group>();
            groups.Add(GroupMock.GetMock(GroupEnums.group1));
            groups.Add(GroupMock.GetMock(GroupEnums.group2));
            groups.Add(GroupMock.GetMock(GroupEnums.group3));

            foreach (var item in groups)
            {
                ComboBoxShowUsers.Items.Add(item.Name);
                foreach (var user in item.Users)
                {
                    if (item.Users.Count != 0)
                    {
                        var data = new ItemData { Name = user.Name, ChatId = $"{user.ChatId}", Group = item.Name };
                        DataGridShowUsers.Items.Add(data);
                    
                    }
                }
            }
        }
    }
}
