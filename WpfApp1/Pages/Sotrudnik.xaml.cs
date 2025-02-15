using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Navigation;
using WpfApp1.Models;

namespace WpfApp1.Pages
{
    public partial class Sotrudnik : Page
    {
        private Пр4_Агентсво_недвижимостиEntities db; // Контекст базы данных

        public Sotrudnik(Авторизация user, string role)
        {
            InitializeComponent();
            db = new Пр4_Агентсво_недвижимостиEntities(); // Инициализация контекста
            LoadData(); // Загрузка данных
        }

        private void LoadData()
        {
            // Загрузка данных о сотрудниках из базы данных
            var employees = db.Сотрудник.Select(c => new
            {
                c.Id_Сотрудник,
                c.Имя,
                c.Фамилия,
                c.Отчество,
                c.Контактные_данные,
                nazvanie = c.dolzhnost.nazvanie
            }).ToList();
            employeesDataGrid.ItemsSource = employees; // Привязка данных к ListView
        }

        private void adduser_Click(object sender, RoutedEventArgs e)
        {
            // Переход на страницу добавления сотрудника
            NavigationService.Navigate(new EditEmployeeForm());
        }

        private void OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Обработчик двойного клика по сотруднику
            if (employeesDataGrid.SelectedItem != null)
            {
                var selectedEmployee = employeesDataGrid.SelectedItem as dynamic;
                if (selectedEmployee != null)
                {
                    int employeeId = selectedEmployee.Id_Сотрудник; // Получение ID сотрудника
                    NavigationService.Navigate(new EditEmployeeForm(employeeId)); // Переход на страницу редактирования
                }
            }
        }

        private void PrintListButton_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = flowDocementReader.Document;
            if (doc == null)
            {
                MessageBox.Show("Документ не найден");
                return;

            }

            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {

                IDocumentPaginatorSource idpSource = doc;
                printDialog.PrintDocument(idpSource.DocumentPaginator, "Список сотрудников");
            }
            

        }

        private void employeesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void flowDocementReader_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Helpel.GetContext().ChangeTracker.Entries().ToList().ForEach(entry => entry.Reload());

                LoadData();

            }
        }
    }
}