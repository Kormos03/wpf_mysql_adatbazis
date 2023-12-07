using System.Windows;
namespace wpf_adatbazis
{
    /// <summary>
    /// Interaction logic for employeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        Employee employee = new Employee();
        EmployeeService service;
        public EmployeeWindow()
        {
            InitializeComponent();
            service = new EmployeeService();
        }

        public bool validateEmp(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(nameTextBox.Text)) { MessageBox.Show("A név nem lehet üres"); return false; }
            else if (string.IsNullOrWhiteSpace(ageTextBox.Text) || (!int.TryParse(ageTextBox.Text, out int agenumb) || agenumb < 18 || agenumb > 150)) { MessageBox.Show("A kor nem lehet üres és vagy számnak kell lennie 18-150ig"); return false; }
            else if (string.IsNullOrWhiteSpace(genderComboBox.Text)) { MessageBox.Show("A nemi identitásnak kell, hogy legyen értéke"); return false; }
            else if (string.IsNullOrWhiteSpace(salaryTextBox.Text) || (!int.TryParse(salaryTextBox.Text, out int temp))) { MessageBox.Show("A fizetés nem lehet üres és számnak kell lennie!"); return false; }
            return true;
        }

        private void submit_Click(object sender, RoutedEventArgs e)
        {
            employee.Name = nameTextBox.Text;
            employee.Salary = int.Parse(salaryTextBox.Text);
            employee.Gender = genderComboBox.Text;
            employee.Age = int.Parse(ageTextBox.Text);
            if (validateEmp(employee))
            {
                if (service.CreateEmp(employee))
                {
                    MessageBox.Show("Sikeres volt a felvétel!");
                    return;
                }
                else { MessageBox.Show("Hiba történt a felvétel során! 21"); return; }
            }
            MessageBox.Show("Hiba történt a felvétel során 11");

        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
