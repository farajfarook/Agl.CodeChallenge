namespace AglTest.Infrastructure.Data
{
    public class DataOptions
    {
        public PeopleDataOptions People { get; set; }
        
        public class PeopleDataOptions
        {
            public string Uri { get; set; }
        }
    }
}