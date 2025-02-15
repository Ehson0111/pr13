using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using WpfApp1.Models;
using WpfApp1.Validators;

namespace WpfApp1.Pages
{
    public partial class EditEmployeeForm : Page
    {
        private Пр4_Агентсво_недвижимостиEntities db;
        private int _employeeId;
        private bool _isNewEmployee; // Режим добавления или редактирования

        public EditEmployeeForm()
        {
            InitializeComponent();
            _isNewEmployee = true; // Режим добавления
            db = new Пр4_Агентсво_недвижимостиEntities();
            DataContext = new Сотрудник { Авторизация = new Авторизация() };
            cbDolzhnost.ItemsSource = db.dolzhnost.ToList(); // Загрузка должностей
            cbpol.ItemsSource = db.pol.ToList(); // Загрузка полов
        }

        public EditEmployeeForm(int employeeId)
        {
            InitializeComponent();
            _employeeId = employeeId;
            db = new Пр4_Агентсво_недвижимостиEntities();
            var employee = db.Сотрудник.Find(employeeId);
            if (employee == null)
            {
                MessageBox.Show("Сотрудник не найден!");
                return;
            }
            DataContext = employee;
            cbDolzhnost.ItemsSource = db.dolzhnost.ToList();
            cbpol.ItemsSource = db.pol.ToList();
        }

        // Обработчик сохранения данных
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var employee = DataContext as Сотрудник;
                if (employee == null)
                {
                    MessageBox.Show("Ошибка при создании или редактировании сотрудника!");
                    return;
                }

                var validator = new EmployeeValidator();
                var validationResults = validator.Validate(employee);
                string error = "Обязательно:";
                if (validationResults.Any())
                {
                    foreach (var result in validationResults)
                    {
                        error = error + "\n" + result.ToString();
                    }
                    if (error != "Обязательно:")
                    {
                        MessageBox.Show(error);
                    }
                    return;
                }

                if (_isNewEmployee)
                {
                    HashPassword hash = new HashPassword();
                    employee.Авторизация.password = hash.HashPassword1(pbPassword.Text);
                    db.Сотрудник.Add(employee);
                    db.SaveChanges();
                    MessageBox.Show("Новый сотрудник успешно добавлен!");

                }
                else
                {
                    var existingEmployee = db.Сотрудник.Find(_employeeId);
                    if (existingEmployee == null)
                    {
                        MessageBox.Show("Сотрудник не найден!");
                        return;
                    }

                    existingEmployee.Имя = txtFirstName.Text;
                    existingEmployee.Фамилия = txtLastName.Text;
                    existingEmployee.Отчество = txtMiddleName.Text;
                    existingEmployee.Контактные_данные = txtContactData.Text;
                    existingEmployee.Зарплата = Convert.ToInt32(txtSalary.Text);
                    existingEmployee.Дата_рождение = dpBirthday.SelectedDate.Value;
                    existingEmployee.id_dolzhnost = (int)cbDolzhnost.SelectedValue;
                    existingEmployee.id_pol = (int)cbpol.SelectedValue;

                    HashPassword hash = new HashPassword();
                    existingEmployee.Авторизация.password = hash.HashPassword1(pbPassword.Text);

                    db.SaveChanges();
                    MessageBox.Show("Изменения сохранены!");
                }

                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }

        // Очистка полей
        private void CLEAR_Click(object sender, RoutedEventArgs e)
        {
            txtFirstName.Clear();
            txtLastName.Clear();
            txtMiddleName.Clear();
            txtContactData.Clear();
            txtSalary.Clear();
            dpBirthday.SelectedDate = null;
            cbDolzhnost.SelectedIndex = -1;
            cbpol.SelectedIndex = -1;
            pbPassword.Clear();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Логика для кнопки "Добавить"
        }

        private void txtContactData_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Обработчик изменения текста
        }
    }
}