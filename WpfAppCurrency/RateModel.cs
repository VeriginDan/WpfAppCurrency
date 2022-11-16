using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace WpfAppCurrency
{
    public class RateModel : PropertyChangedBase
    {
        protected double val;

        public double ValueUSD
        {   get { return val; } 
            set 
            { 
                val = value;
                OnPropertyChanged(nameof(ValueUSD));
            } 
        }
        public double ValueEUR
        {
            get { return val; }
            set
            {
                val = value;
                OnPropertyChanged(nameof(ValueEUR));
            }
        }
        public double ValueKZT
        {
            get { return val; }
            set
            {
                val = value;
                OnPropertyChanged(nameof(ValueKZT));
            }
        }
        public async Task GetRate()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    var xml = await client.DownloadStringTaskAsync(new Uri("https://www.cbr-xml-daily.ru/daily.xml"));
                    XDocument xdoc = XDocument.Parse(xml);
                    var e1 = xdoc.Element("ValCurs").Elements("Valute"); //R01335 - kzt R01239 - eur
                    string USD = e1.Where(x => x.Attribute("ID").Value == "R01235").Select(x => x.Element("Value").Value).FirstOrDefault();
                    string EUR = e1.Where(x => x.Attribute("ID").Value == "R01239").Select(x => x.Element("Value").Value).FirstOrDefault();
                    string KZT = e1.Where(x => x.Attribute("ID").Value == "R01335").Select(x => x.Element("Value").Value).FirstOrDefault();
                    if (!string.IsNullOrWhiteSpace(USD))
                    {
                        ValueUSD = Convert.ToDouble(USD);
                        ValueEUR = Convert.ToDouble(EUR);
                        ValueKZT = Convert.ToDouble(KZT);
                        return;
                    }
                }

                ValueUSD = 0;
                ValueEUR = 0;
                ValueKZT = 0;
            }

            catch (Exception)
            {

            }
        }
    }
    
}
