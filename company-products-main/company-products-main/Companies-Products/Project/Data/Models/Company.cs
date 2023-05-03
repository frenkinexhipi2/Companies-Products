using Project.Data.Base;

namespace Project.Data.Models
{
    public class Company : BaseClass
    {
        public string CompanyName { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }

        //Add a reference to Subject table
        public int ProductId { get; set; }
    }
}