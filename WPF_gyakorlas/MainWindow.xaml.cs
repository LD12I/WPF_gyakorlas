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


            CreateButton.Click += (s,e) => {
                on();
                Add.Visibility = Visibility.Visible;
                Delete.Visibility = Visibility.Collapsed;
                DeleteAll.Visibility = Visibility.Collapsed;
                UpdatePerson.Visibility = Visibility.Collapsed;
                Title.Text = "Modify: Add";

                CreateNameTB.Visibility = Visibility.Visible;
                EditTBName.Visibility = Visibility.Collapsed;
                CreateAgeTB.Visibility = Visibility.Visible;
            };


            DeleteButton.Click += (s, e) => {
                on();
                Add.Visibility = Visibility.Collapsed;
                Delete.Visibility = Visibility.Visible;
                DeleteAll.Visibility = Visibility.Collapsed;
                UpdatePerson.Visibility = Visibility.Collapsed;
                Title.Text = "Modify: Delete";
                CreateNameTB.Visibility = Visibility.Visible;
                EditTBName.Visibility = Visibility.Collapsed;
                CreateAgeTB.Visibility = Visibility.Collapsed;
            };

            EditButton.Click += (s, e) => {
                on();
                Add.Visibility = Visibility.Collapsed;
                Delete.Visibility = Visibility.Collapsed;
                DeleteAll.Visibility = Visibility.Collapsed;
                UpdatePerson.Visibility = Visibility.Visible;
                Title.Text = "Modify: update";
                CreateNameTB.Visibility = Visibility.Visible;
                EditTBName.Visibility = Visibility.Visible;
                CreateAgeTB.Visibility = Visibility.Visible;
            };



            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    doff();
                }
            };
        }


        private async void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                await sc.CreatePerson(CreateNameTB.Text, Convert.ToInt32(CreateAgeTB.Text));
                CreateNameTB.Text = "";
                CreateAgeTB.Text = "";
                UpdateAges();
                UpdateNames();
            }
            catch (Exception err)
            {
                MessageBox.Show("hiba, adj meg nevet, és kort");
            }
            doff();

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
            try
            {
                await sc.DeletePersonAsync(CreateNameTB.Text);
                CreateNameTB.Text = "";
                CreateAgeTB.Text = "";
                UpdateAges();
                UpdateNames();
            }
            catch (Exception err)
            {
                MessageBox.Show("hiba, adj meg nevet");
            }
            doff();
        }

        private async void DeleteAll_Click(object sender, RoutedEventArgs e)
        {
            await sc.DeleteAllPersonsAsync();
            CreateNameTB.Text = "";
            CreateAgeTB.Text = "";
            UpdateAges();
            UpdateNames();
        }

        private async void UpdatePerson_Click(object sender, RoutedEventArgs e)
        {
            await sc.UpdatePerson(EditTBName.Text, CreateNameTB.Text, CreateAgeTB.Text);
            CreateNameTB.Text = "";
            CreateAgeTB.Text = "";
            UpdateAges();
            UpdateNames();
            doff();
        }



        void on()
        {
            modifyREC.Visibility = Visibility.Visible;
            ModidyTable.Visibility = Visibility.Visible;
        }

        void doff() 
        {
            modifyREC.Visibility = Visibility.Hidden;
            ModidyTable.Visibility = Visibility.Hidden;
        }

        private void EditTBName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (names.Count > 0)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    if (EditTBName.Text == names[i])
                    {
                        CreateNameTB.Text = names[i];
                        CreateAgeTB.Text = ages[i].ToString();
                    }
                }
            }
        }
    }
}
