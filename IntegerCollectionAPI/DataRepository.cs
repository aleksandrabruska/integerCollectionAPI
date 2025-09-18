namespace IntegerCollectionAPI
{

    //repository class that stores integer collection
    //and allows basic operations
    public class DataRepository : IDataRepository
    {
        private static List<int> numbers = new List<int>();

        private static List<int> numbersSortedAsc = new List<int>();    //for search and mean optimization
        private bool sortedAscUpToDate = false;                         //could be done for descending sort as well

        public List<int> GetNumbers() { return new List<int>(numbers); }
        public bool FindNumber(int value) {
            if (sortedAscUpToDate)
            {
                return BinarySearch(value, new List<int>(numbersSortedAsc));
            }
            else
            {
                return SequentialSearch(value, new List<int>(numbers));
            }
        }

        public void AddNumbers(List<int> numbersToAdd) 
        {   numbers = numbers.Concat(numbersToAdd).ToList(); 
            sortedAscUpToDate = false; 
        }

        public List<int> GetSorted(bool asc)
        {
            if (asc && sortedAscUpToDate)
            {
                return numbersSortedAsc;
            }
            List<int> result = QuickSort(new List<int>(numbers), 0, numbers.Count-1, asc);
            if (asc)
            {
                SetSorted(result);
            }
            return result;
        }


        public double Median()
        {
            List<int> numbersSorted;

            if (sortedAscUpToDate)
            {
                numbersSorted = numbersSortedAsc;
            }
            else
            {
                numbersSorted = QuickSort(new List<int>(numbers), 0, numbers.Count-1, true);
            }
            int count = numbersSorted.Count();

            if (count % 2 == 0)
            {
                return (numbersSorted[count / 2 - 1] + numbersSorted[count / 2]) / 2.0;
            }
            else
            {
                int index = (count - 1) / 2;
                return numbersSorted[index];
            }
        }

        public double Mean()
        {
            return (double)numbers.Sum() / numbers.Count;
        }


        private void SetSorted(List<int> numbersSortedAsc_)
        {
            sortedAscUpToDate = true;
            numbersSortedAsc = numbersSortedAsc_;
        }

        private bool SequentialSearch(int value, List<int> numbers)
        {
            bool found = false;
            foreach (int i in numbers)
            {
                if (i == value)
                {
                    found = true; 
                    break;
                }
            }
            return found;
        }

        private bool BinarySearch(int value, List<int> numbersSorted)
        {
            int low = 0;
            int high = numbersSorted.Count - 1;
            while (low <= high)
            {
                int mid = low + (high - low) / 2;
                if (numbersSorted[mid] == value)
                {
                    return true;
                }
                else if (numbersSorted[mid] < value)
                {
                    low = mid + 1;
                }
                else
                {
                    high = mid - 1;
                }
            }
            return false;
        }



        private List<int> QuickSort(List<int> numbers, int low, int high, bool asc)
        {
            if (low < high)
            {
                int pivotIdx = QuickSortPartition(numbers, low, high, asc);
                QuickSort(numbers, low, pivotIdx - 1, asc);
                QuickSort(numbers, pivotIdx + 1, high, asc);

            }
            return numbers;
        }

        private int QuickSortPartition(List<int> numbers, int low, int high, bool asc)
        {
            int pivot = numbers[high];
            int i = low - 1;
            int temp;
            for (int j = low; j < high; j++)
            {
                bool swap = false;
                if (asc)
                {
                    swap = numbers[j] <= pivot;
                }
                else
                {
                    swap = numbers[j] >= pivot;
                }

                if (swap)
                {
                    i++;
                    temp = numbers[i];
                    numbers[i] = numbers[j];
                    numbers[j] = temp;
                }
            }

            temp = numbers[high];
            numbers[high] = numbers[i + 1];
            numbers[i + 1] = temp;
            return i + 1;


        }


    }
}
