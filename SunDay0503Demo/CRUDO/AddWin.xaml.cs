using Microsoft.Win32;
using SunDay0503Demo.DB;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace SunDay0503Demo.CRUDO
{
    /// <summary>
    /// Логика взаимодействия для AddWin.xaml
    /// </summary>
    public partial class AddWin : Window
    {
        public AddWin()
        {
            InitializeComponent();
            CmbRroduct.ItemsSource = App.DBLopushok.ProductType.ToList();
        }
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private void BtnSelectImage_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "Image files|*.bmp;*.jpg;*.png|All files|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFileDialog.FileName);
                image.EndInit();
                ImageBym.Source = image;
            }
        }

        private void BtnAddToDB_Click(object sender, RoutedEventArgs e)
        {
            if (TxArticle.Text == "" || TxDes.Text == "" || TxMinCostAgent.Text == ""
               || TxProdPers.Text == "" || TxProdShop.Text == "" || TtxBoxTitle.Text == "" || CmbRroduct.SelectedIndex == -1)
            {
                MessageBox.Show("Не оставляйте незаполенные поля, или же информация не сохранится", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            Product product = new Product();
            {
                product.Image = openFileDialog.FileName;
                product.Title = TtxBoxTitle.Text;
                product.ArticleNumber = TxArticle.Text;
                product.MinCostForAgent = Convert.ToDecimal(TxMinCostAgent.Text);
                product.ProductionPersonCount = Convert.ToInt32(TxProdPers.Text);
                product.ProductionWorkshopNumber = Convert.ToInt32(TxProdShop.Text);
                var idType = CmbRroduct.SelectedItem;
                product.ProductTypeID = ((ProductType)idType).ID;
            }
            App.DBLopushok.Product.Add(product); 
            App.DBLopushok.SaveChanges();
            MessageBox.Show("Добавлено");
            this.Close();
        }

        private void TxArticle_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Вводить только цифры!,допустимое значение 6 цифр", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void TxMinCostAgent_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Вводить только цифры!,допустимое значение 3 цифры", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }

        private void TxProdShop_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Char.IsDigit(e.Text, 0))
            {
                MessageBox.Show("Вводить только цифры!,допустимое значение 2 цифры", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
                e.Handled = true;
            }
        }
    }
}
