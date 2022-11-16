using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace WpfAppCurrency
{
    public class RateViewModel : PropertyChangedBase
    {
        protected RateModel curRate;
        protected DispatcherTimer timer;
        protected Visibility isProgressVisible;

        public RateViewModel()
        {
            curRate = new RateModel();

            timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 10);
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            isProgressVisible = Visibility.Hidden;

            curRate.PropertyChanged += (s, e) =>
            {
                OnPropertyChanged(e.PropertyName);
            };
        }

        public Visibility IsProgressVisible
        { 
            get { return isProgressVisible; } 
            set 
            { 
                isProgressVisible = value;
                OnPropertyChanged(nameof(isProgressVisible));
            }
        }

        private async void timer_Tick(object sender, EventArgs e)
        {
            await GetRate();
        }

        public async Task OnLoad(object sender, EventArgs e)
        {
            await GetRate();
        }

        public async Task GetRate()
        {
            isProgressVisible = Visibility.Visible;
            await curRate.GetRate();
            isProgressVisible = Visibility.Hidden;
        }

        public double ValueUSD
        {
            get { return curRate.ValueUSD; }
        }
        public double ValueEUR
        {
            get { return curRate.ValueEUR; }
        }
        public double ValueKZT
        {
            get { return curRate.ValueKZT; }
        }
    }
}
