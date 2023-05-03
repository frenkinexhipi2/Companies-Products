namespace Project.Data.DTOs.Company
{
    public class PutCompanyDTO
    {
        public string CompanyName { get; set; }
        public string Code { get; set; }
        public DateTime DateCreated { get; set; }
        public int ProductId { get; set; }
    }
}
