namespace IntegerCollectionAPI.models
{
    public class SearchResult 
    {
        public int value { get; set; }
        public bool found { get; set; } 
        public SearchResult(int value_, bool found_)
        {
            value = value_;
            found = found_;
        }
    }
}
