using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        public double Price
        {
            get { return price; }
            set
            {
                CalculateTotal(value, nightsCount, discount);
                if (value < 0)
                    throw new ArgumentException();

                price = value;
                Notify(nameof(Price));
                Notify(nameof(Total));
            }
        }

        private int nightsCount;
        public int NightsCount
        {
            get { return nightsCount; }
            set
            {
                CalculateTotal(price, value, discount);
                if (value <= 0)
                    throw new ArgumentException();

                nightsCount = value;
                Notify(nameof(NightsCount));
                Notify(nameof(Total));
            }
        }

        private double discount;
        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                CalculateTotal(price, NightsCount, value);
                if (total < 0)
                    throw new ArgumentException();

                Notify(nameof(Discount));
                Notify(nameof(Total));
            }
        }

        private double total;
        public double Total
        {
            get { return total; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException();
                total = value;
                discount = (1 - value / (price * nightsCount)) * 100;
                Notify(nameof(Total));
                Notify(nameof(Discount));
            }
        }

        private void CalculateTotal(double price, int nightsCount, double discount)
        {
            total = price * nightsCount * (1 - discount / 100);
        }
    }
}