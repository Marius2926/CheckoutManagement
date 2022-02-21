using SharedKernel;

namespace CkeckoutManagement.Core.ValueObjects
{
    public class BasketValue : ValueObject
    {
        public BasketValue(double totalNet, double totalGross, bool paysVAT)
        {
            TotalNet = totalNet;
            TotalGross = totalGross;
            PaysVAT = paysVAT;
        }

        public double TotalNet { get; private set; }
        public double TotalGross { get; private set; }
        public bool PaysVAT { get; private set; }

        public void AddedNewItem(double price)
        {
            TotalNet += price;
            if (PaysVAT == true)
            {
                TotalGross = TotalNet * 1.1;
            }
            else
            {
                TotalGross = TotalNet;
            }
        }
        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TotalNet;
            yield return TotalGross;
            yield return PaysVAT;
        }
    }
}
