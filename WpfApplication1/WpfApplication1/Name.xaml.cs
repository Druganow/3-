using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Логика взаимодействия для Name.xaml
    /// </summary>
    public partial class Name : Window
    {
        public Name()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            nameNewFile = NameTB.Text;
            Close();
        }
     

        
        public string nameNewFile
        {
            get; set;
        }
    }
}

