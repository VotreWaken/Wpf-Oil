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
using System.Windows.Threading;
namespace WpfApp14
{
    public partial class MainWindow : Window
    {
        Dictionary<string, int> mydict = new Dictionary<string, int>();
        Dictionary<string, int> Food = new Dictionary<string, int>();
        bool status;
        double totalMoneyPerDay;
        private DispatcherTimer timer1;
        public MainWindow()
        {
            InitializeComponent();
            mydict.Add("92", 10);
            mydict.Add("95", 12);
            mydict.Add("97", 15);

            Food.Add("Хот-Дог", 4);
            Food.Add("Гамбургер", 6);
            Food.Add("Картопля-Фрi", 3);
            Food.Add("Кока-Кола", 5);

            CBHotDog.Content = Food.ElementAt(0).Key;
            CBHamburger.Content = Food.ElementAt(1).Key;
            CBPotato.Content = Food.ElementAt(2).Key;
            CBCola.Content = Food.ElementAt(3).Key;
            TBHotDogPrice.Text = Food.ElementAt(0).Value.ToString();
            TBHamburgerPrice.Text = Food.ElementAt(1).Value.ToString();
            TBPotatoPrice.Text = Food.ElementAt(2).Value.ToString();
            TBColaPrice.Text = Food.ElementAt(3).Value.ToString();
            CBFuel.Items.Add(mydict.ElementAt(0).Key);
            CBFuel.Items.Add(mydict.ElementAt(1).Key);
            CBFuel.Items.Add(mydict.ElementAt(2).Key);

            CBFuel.SelectedIndex = 0;

            Timer_Start();
        }
        private void Timer_Start()
        {
            timer1 = new DispatcherTimer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = new TimeSpan(0, 0, 0, 0, 10000);
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (status == true)
            {
                MessageBoxResult result = MessageBox.Show("Очитити форму?", "Запит", MessageBoxButton.OKCancel);
                if (result == MessageBoxResult.OK)
                {
                    TBCount.Text = 0.ToString();
                    TBSum.Text = 0.ToString();
                    FuelPrice.Content = 0.ToString();
                    CafePrice.Content = 0.ToString();
                    TotalPrice.Content = 0.ToString();
                    TBHotDogCount.Text = 0.ToString();
                    TBHamburgerCount.Text = 0.ToString();
                    TBPotatoCount.Text = 0.ToString();
                    TBColaCount.Text = 0.ToString();
                    status = false;
                    timer1.Stop();
                }
                else if (result == MessageBoxResult.Cancel)
                {
                    timer1.Start();
                    status = true;
                }
            }
        }

        private double CalculatePrice()
        {
            try
            {
                double toOpata = 0;
                if (RBCount.IsChecked == true)
                {
                    toOpata = double.Parse(TBPrice.Text) * double.Parse(TBCount.Text);
                }
                else if (RBSum.IsChecked == true)
                {
                    toOpata = double.Parse(TBSum.Text);
                    TBCount.Text = (double.Parse(TBSum.Text) / double.Parse(TBPrice.Text)).ToString("N2");
                }
                return toOpata;
            }
            catch (Exception a)
            {
                return 0;
            }
        }

        private void CBFuel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CBFuel.SelectedIndex == 0)
            {
                TBPrice.Text = mydict.ElementAt(0).Value.ToString();
            }
            else if (CBFuel.SelectedIndex == 1)
            {
                TBPrice.Text = mydict.ElementAt(1).Value.ToString();
            }
            else
                TBPrice.Text = mydict.ElementAt(2).Value.ToString();
        }

        private void TBCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            FuelPrice.Content = CalculatePrice().ToString("N2");
        }

        private void RBCount_Click(object sender, RoutedEventArgs e)
        {
            if (RBCount.IsChecked == true)
            {
                TBCount.IsReadOnly = false;
                TBSum.IsReadOnly = true;
                TBSum.Text = string.Empty;
            }
            else if (RBSum.IsChecked == true)
            {
                TBCount.IsReadOnly = true;
                TBSum.IsReadOnly = false;
                TBCount.Text = string.Empty;
            }
        }

        int CalculateSum()
        {
            try
            {
                if (TBHotDogCount.Text == string.Empty)
                    TBHotDogCount.Text = 0.ToString();
                if (TBHamburgerCount.Text == string.Empty)
                    TBHamburgerCount.Text = 0.ToString();
                if (TBPotatoCount.Text == string.Empty)
                    TBPotatoCount.Text = 0.ToString();
                if (TBColaCount.Text == string.Empty)
                    TBColaCount.Text = 0.ToString();
                return
                    int.Parse(TBHotDogCount.Text) * int.Parse(TBHotDogPrice.Text) +
                    int.Parse(TBHamburgerCount.Text) * int.Parse(TBHamburgerPrice.Text) +
                    int.Parse(TBPotatoCount.Text) * int.Parse(TBPotatoPrice.Text) +
                    int.Parse(TBColaCount.Text) * int.Parse(TBColaPrice.Text);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            TotalPrice.Content = (double.Parse(CafePrice.Content.ToString()) + double.Parse(FuelPrice.Content.ToString())).ToString();
            totalMoneyPerDay += (double.Parse(TotalPrice.Content.ToString()));
            status = true;
            timer1.Start();
        }

        private void CBHotDog_Click(object sender, RoutedEventArgs e)
        {
            if (CBHotDog.IsChecked == true)
            {
                TBHotDogCount.IsEnabled = true;
            }
            else
            {
                TBHotDogCount.IsEnabled = false;
            }
            if (CBHamburger.IsChecked == true)
            {
                TBHamburgerCount.IsEnabled = true;
            }
            else
            {
                TBHamburgerCount.IsEnabled = false;
            }
            if (CBPotato.IsChecked == true)
            {
                TBPotatoCount.IsEnabled = true;
            }
            else
            {
                TBPotatoCount.IsEnabled = false;
            }
            if (CBCola.IsChecked == true)
            {
                TBColaCount.IsEnabled = true;
            }
            else
            {
                TBColaCount.IsEnabled = false;
            }
        }
        private void TBColaCount_TextChanged(object sender, TextChangedEventArgs e)
        {
            CafePrice.Content = CalculateSum().ToString();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            MessageBox.Show($"Виторг за день склав: {totalMoneyPerDay}", "Виторг");
        }

        private void ChangeDarkTheme(object sender, RoutedEventArgs e)
        {
            MainPart.Background = new SolidColorBrush(Colors.DimGray);
            DownPart.Background = new SolidColorBrush(Colors.DimGray);
            GridMain.Background = new SolidColorBrush(Colors.DimGray);
        }

        private void ChangeLightTheme(object sender, RoutedEventArgs e)
        {
            MainPart.Background = new SolidColorBrush(Colors.White);
            DownPart.Background = new SolidColorBrush(Colors.White);
            GridMain.Background = new SolidColorBrush(Colors.White);
        }
    }
}
