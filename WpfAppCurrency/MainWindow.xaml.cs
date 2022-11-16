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

namespace WpfAppCurrency
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected RateViewModel viewModel;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = viewModel = new RateViewModel();
        }
        
        private async void  OnLoad (object sender, RoutedEventArgs e)
        {
            await viewModel.OnLoad(sender, e);
        }

        Point constPosition;
        bool move = false;

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            constPosition = e.GetPosition(this);

            move = true;

            this.CaptureMouse();
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (move == true)
            {
                double deltaX = e.GetPosition (this).X - constPosition.X;
                double deltaY = e.GetPosition (this).Y - constPosition.Y;

                this.Left += deltaX;
                this.Top += deltaY;
            }
        }

        private void Window_MouseUp(object sender, MouseButtonEventArgs e)
        {
            move = false;
            this.ReleaseMouseCapture();
        }
    }
}
