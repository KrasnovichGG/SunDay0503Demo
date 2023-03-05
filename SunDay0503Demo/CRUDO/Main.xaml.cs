using SunDay0503Demo.DB;
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

namespace SunDay0503Demo
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class Main : Window
    {
        public Main()
        {
            InitializeComponent();
            LstItems.ItemsSource = App.DBLopushok.Product.ToList();
            FeelCombobox();
            CmbBoxFiltr.ItemsSource = App.DBLopushok.ProductType.ToList();
        }
        private void Sort()
        {
            switch (CmbBoxSort.SelectedItem)
            { 
                case "От А до Я":
                LstItems.ItemsSource = null;
                LstItems.ItemsSource = App.DBLopushok.Product.OrderBy(x => x.Title).ToList();
                break;
                case "От Я до А":
                LstItems.ItemsSource = null;
                LstItems.ItemsSource = App.DBLopushok.Product.OrderByDescending(x => x.Title).ToList();
                break;
                default:
                LstItems.ItemsSource = null;
                LstItems.ItemsSource = App.DBLopushok.Product.ToList();
                break;

            }
        }
        private void FeelCombobox()
        {
            CmbBoxSort.Items.Add("От А до Я");
            CmbBoxSort.Items.Add("От Я до А");
        }
        private void TxPoisk_TextChanged(object sender, TextChangedEventArgs e)
        {
            var poisk = App.DBLopushok.Product.ToList();
            poisk = poisk.Where(x => x.Title.ToLower().Contains(TxPoisk.Text.ToLower())).ToList();
            LstItems.ItemsSource = poisk.ToList();
        }

        private void CmbBoxSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void BtnX_Click(object sender, RoutedEventArgs e)
        {
            //CmbBoxFiltr.SelectedIndex = -1;
            CmbBoxSort.SelectedIndex = -1;
            TxPoisk.Text = " ";
            LstItems.ItemsSource = App.DBLopushok.Product.ToList();
        }

        private void CmbBoxFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedType = CmbBoxFiltr.SelectedItem;
            var typeProduct = ((ProductType)selectedType).ID;
            LstItems.ItemsSource = App.DBLopushok.Product.Where(x => x.ProductTypeID == typeProduct).ToList();
        }
    }
}
