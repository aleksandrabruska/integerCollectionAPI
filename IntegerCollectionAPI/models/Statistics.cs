namespace IntegerCollectionAPI.models
{
    public class Statistics
    {
        public double mean { get; set; }
        public double median { get; set; }
        public Statistics(double mean_, double median_)
        {
            this.mean = mean_;
            this.median = median_;
        }
    }
}
