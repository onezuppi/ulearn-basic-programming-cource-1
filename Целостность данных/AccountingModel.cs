using System;

namespace HotelAccounting
{
    public class AccountingModel : ModelBase
    {
        private double price;
        private int nightsCount;
        private double discount;
        private double total;

        public AccountingModel()
        {
            nightsCount = 1;
        }
        
        public double Price
        {
            get => price;
            set
            {
                if (value < 0)
                    throw new ArgumentException();
                price = value;
                SetTotal(currentTotal);
                Notify(nameof(Price));
            }
        }

        public int NightsCount
        {
            get => nightsCount;
            set
            {
                if (value < 1)
                    throw new ArgumentException();
                nightsCount = value;
                SetTotal(currentTotal);
                Notify(nameof(NightsCount));
            }
        }

        public double Discount
        {
            get => discount;
            set
            {
                SetDiscount(value);
                if (Math.Abs(discount - currentDiscount) > 1e-6)
                    SetTotal(currentTotal);
            }
        }

        public double Total
        {
            get => total;
            set
            {
                SetTotal(value);
                if (Math.Abs(currentTotal - Total) > 1e-6)
                    SetDiscount(currentDiscount);
            }
        }

        private void SetDiscount(double value)
        {
            discount = value;
            Notify(nameof(Discount));
        }
        
        private void SetTotal(double value)
        {
            if (value < 0)
                throw new ArgumentException();
            total = value;
            Notify(nameof(Total));
        }
        
        private double currentDiscount => (1 - Total / (Price * NightsCount)) * 100;
        private double currentTotal => Price * NightsCount * (1 - Discount / 100);
    }
}