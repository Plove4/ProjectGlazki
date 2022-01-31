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
using ProjectGlazki.Entities;
using ProjectGlazki.Utilities;

namespace ProjectGlazki.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddAgentPage.xaml
    /// </summary>
    public partial class AddAgentPage : Page
    {
        private Agent currentAgent;
        public AddAgentPage(Agent agent)
        {
            InitializeComponent();
            currentAgent = agent ?? new Agent();
            CmbType.ItemsSource = DBcontext.Context.AgentType.ToList();
            DataContext = currentAgent;
        }

        private void Save_btn_Click(object sender, RoutedEventArgs e)
        {
            StringBuilder builder = new StringBuilder();
            if (string.IsNullOrWhiteSpace(currentAgent.Title))
                builder.AppendLine("Укажите наименование");
            if (string.IsNullOrWhiteSpace(currentAgent.INN))
                builder.AppendLine("Укажите ИНН");
            if (string.IsNullOrWhiteSpace(currentAgent.Phone))
                builder.AppendLine("Укажите номер телефона");
            if (string.IsNullOrWhiteSpace(currentAgent.Priority.ToString()))
                builder.AppendLine("Укажите приоритет");
            if (string.IsNullOrWhiteSpace(currentAgent.Logo))
                currentAgent.Logo = "";
            if (currentAgent.AgentType == null)
                builder.AppendLine("Выбирите тип агента");


            if(builder.Length > 0)
            {
                MessageBox.Show(builder.ToString());
                return;
            }

            if(currentAgent.ID == 0)
            {
                DBcontext.Context.Agent.Add(currentAgent);
            }

            try
            {
                DBcontext.Context.SaveChanges();
                MessageBox.Show("Данные сохранены");
                FrameManager.frmMain.Navigate(new AgentPage());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Cancel_btn_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.frmMain.GoBack();
        }
    }
}
