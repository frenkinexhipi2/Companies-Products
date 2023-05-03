using Project.Data.Base;

namespace Project.Data.Models
{
    public class Product : BaseClass
    {
        //public string ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Type { get; set; }

        //Define Reference with Company table
        public List<Company> Products { get; set; }
    }
}
