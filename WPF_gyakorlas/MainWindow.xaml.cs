using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

namespace WPF_gyakorlas
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> names = new List<string>();
        List<int> ages = new List<int>();
        ServerController sc = new ServerController();


        public MainWindow()
        {
            InitializeComponent();
            UpdateAges();
            UpdateNames();
        }

        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            await sc.CreatePerson(CreateNameTB.Text, Convert.ToInt32(CreateAgeTB.Text));
            UpdateAges();
            UpdateNames();
        }


        private async void UpdateNames()
        {
            names.Clear();
            NameSp.Children.Clear();
            names = await sc.getAllNames();

            if (names.Count > 0)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    NameSp.Children.Add(new TextBlock() { Text = names[i], FontSize = 20, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center });
                }
            }

        }


        private async void UpdateAges()
        {
            ages.Clear();
            AgeSp.Children.Clear();
            ages = await sc.getAllages();

            if (ages.Count > 0)
            {
                for (int i = 0; i < ages.Count; i++)
                {
                    AgeSp.Children.Add(new TextBlock() { Text = ages[i].ToString(), FontSize = 20, Foreground = Brushes.White, HorizontalAlignment = HorizontalAlignment.Center });
                }
            }

        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            await sc.DeletePersonAsync(CreateNameTB.Text);
            UpdateAges();
            UpdateNames();
        }
    }
}
