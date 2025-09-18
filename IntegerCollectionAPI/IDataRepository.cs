using System.Diagnostics;

namespace IntegerCollectionAPI
{
    public interface IDataRepository
    {
        public List<int> GetNumbers();
        public bool FindNumber(int value);
        public void AddNumbers(List<int> numbersToAdd);
        public List<int> GetSorted(bool asc);
        public double Median();
        public double Mean();


    }
}
