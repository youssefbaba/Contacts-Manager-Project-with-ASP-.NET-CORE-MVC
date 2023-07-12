namespace Assignement_3
{
    public class DataSource
    {
        public Dictionary<int, string> GetData()
        {
            return new Dictionary<int, string>()
            {
                { 1, "United States"},
                { 2, "Canada"},
                { 3, "Japan"},
                { 4, "United Kingdom"},
                { 5, "Spain"}
            };
        }
    }
}
