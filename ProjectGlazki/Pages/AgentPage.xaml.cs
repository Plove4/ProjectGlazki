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
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        public AgentPage()
        {
            InitializeComponent();

            ListAgent.ItemsSource = DBcontext.Context.Agent.ToList();

            var AgentType = DBcontext.Context.AgentType.ToList();
            AgentType.Insert(0, new AgentType { Title = "Все типы" });
            CmbType.ItemsSource = AgentType;
            CmbType.SelectedIndex = 0;

            CmbSort.Items.Insert(0, "Без сортировки");
            CmbSort.Items.Insert(1, "Наименование");
            CmbSort.Items.Insert(2, "Приоритет агента");
            CmbSort.SelectedIndex = 0;
        }

        private void SortChange()
        {
            var sortItem = DBcontext.Context.Agent.ToList();
            if(string.IsNullOrWhiteSpace(TxtSearch.Text) == false)
            {
                sortItem = sortItem.Where(sort => sort.Title.ToLower().Contains(TxtSearch.Text.ToLower()) || sort.Email.ToLower().Contains(TxtSearch.Text.ToLower()) || sort.Phone.ToLower().Contains(TxtSearch.Text.ToLower())).ToList();
            }
            if(CmbType.SelectedIndex > 0)
            {
                sortItem = sortItem.Where(sort => sort.AgentTypeID == CmbType.SelectedIndex).ToList();
            }
            switch(CmbSort.SelectedIndex)
            {
                case 1:
                {
                        if (Chbfiltr.IsChecked == true)
                        {
                            sortItem = sortItem.OrderByDescending(sort => sort.Title).ToList();
                        }
                        else
                        {
                            sortItem = sortItem.OrderBy(sort => sort.Title).ToList();
                        }
                        break;
                }
                case 2:
                    {
                        if (Chbfiltr.IsChecked == true)
                        {
                            sortItem = sortItem.OrderByDescending(sort => sort.Priority).ToList();
                        }
                        else
                        {
                            sortItem = sortItem.OrderBy(sort => sort.Priority).ToList();
                        }
                        break;
                    }
            }
            ListAgent.ItemsSource = sortItem;
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SortChange();
        }

        private void CmbType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortChange();
        }

        private void CmbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortChange();
        }

        private void Chbfiltr_Checked(object sender, RoutedEventArgs e)
        {
            SortChange();
        }

        private void Chbfiltr_Unchecked(object sender, RoutedEventArgs e)
        {
            SortChange();
        }

        private void Add_btn_Click(object sender, RoutedEventArgs e)
        {
            FrameManager.frmMain.Navigate(new AddAgentPage(null));
            ListAgent.ItemsSource = DBcontext.Context.Agent.ToList();
        }

        private void Edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var item = ListAgent.SelectedItem;
            FrameManager.frmMain.Navigate(new AddAgentPage(item as Agent));
            ListAgent.ItemsSource = DBcontext.Context.Agent.ToList();
        }

        private void Delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var delet = ListAgent.SelectedItem as Agent;
            if (MessageBox.Show($"Вы хотите удалить продукт №{delet.ID} ?", "Удаление данных", MessageBoxButton.YesNo,
                MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    DBcontext.Context.Agent.Remove(delet);
                    DBcontext.Context.SaveChanges();
                    MessageBox.Show("Данные удалены");
                    ListAgent.ItemsSource = DBcontext.Context.Agent.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ListAgent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Edit_btn.Visibility = Visibility.Visible;
            Delete_btn.Visibility = Visibility.Visible;
        }
    }
}
