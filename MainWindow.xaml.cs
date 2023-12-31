﻿using System.Windows;

namespace wpf_adatbazis
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        EmployeeService service = new EmployeeService();
        EmployeeWindow employeeWindow;
        Employee selectedEmployee = new Employee();
        public MainWindow()
        {
            InitializeComponent();
            Read();
        }

        private void Read()
        {
            employeeTable.ItemsSource = service.GetAll();
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            employeeWindow = new EmployeeWindow();
            employeeWindow.ShowDialog();
            Read();
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            selectedEmployee = employeeTable.SelectedItem! as Employee;
            if (selectedEmployee != null)
            {
                employeeWindow = new EmployeeWindow(selectedEmployee);
                employeeWindow.ShowDialog();
                Read();
            }
            else { MessageBox.Show("Válasszon egy dolgozót!"); return; }

        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = (Employee)employeeTable.SelectedItem;
            if (selectedEmployee == null) { MessageBox.Show("Válasszon egy dolgozót!"); return; }
            MessageBoxResult result = MessageBox.Show($"Biztos, hogy törli a dolgozót: {selectedEmployee.Name} ?", "Biztos?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                if (service.Delete(selectedEmployee))
                {
                    MessageBox.Show("A törlés sikerült!");
                }
                else { MessageBox.Show("Hiba történt a törlés során, a megadott elem nem található !"); }
            }
            Read();
        }
    }
}
