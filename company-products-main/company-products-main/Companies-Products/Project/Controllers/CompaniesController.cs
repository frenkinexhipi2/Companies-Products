using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Data.DTOs.Company;
using Project.Data.Models;

namespace Project.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompaniesController : ControllerBase
    {
        private AppDbContext _appDbContext;
        public CompaniesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        //Krijo nje API endpoint per te marre te dhenat nga DB
        [HttpGet("GetAllCompanies")]
        public IActionResult GetAllCompanies()
        {
            var allCompanies = _appDbContext.Companies.ToList();
            return Ok(allCompanies);
        }

        //Krijo nje API endpoint per te marre te dhenat nga DB
        [HttpGet("GetCompanyById/{id}")]
        public IActionResult GetCompanyById(int id)
        {
            var Company = _appDbContext.Companies.FirstOrDefault(x => x.Id == id);
            return Ok($"Produkti me id = {id} u kthye me sukses!");
        }

        [HttpPost("AddCompany")]
        public IActionResult AddCompany([FromBody] PostCompanyDTO payload)
        {
            //1. Krijo nje objekt produkt me te dhenat e marra nga payload
            Company newCompany = new Company()
            {
                CompanyName = payload.CompanyName,
                Code = payload.Code,
                DateCreated = payload.DateCreated,

                ProductId = payload.ProductId
            };


            _appDbContext.Companies.Add(newCompany);
            _appDbContext.SaveChanges();

            return Ok("Kompania u krijua me sukses!");
        }

        [HttpPut("UpdateCompany")]
        public IActionResult UpdateCompany([FromBody] PutCompanyDTO payload, int id)
        {
            //1. Duke perdour ID marrim te dhenat nga databaza
            var company = _appDbContext.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null)
                return NotFound();

            //2. Perditesojme Komanine e DB me te dhenat e payload-it
            company.CompanyName = payload.CompanyName;
            company.Code = payload.Code;
            company.DateCreated = payload.DateCreated;

            //Add Refrence to Product Id
            company.ProductId = payload.ProductId;

            //3. Ruhen te dhenat ne database
            _appDbContext.Companies.Update(company);
            _appDbContext.SaveChanges();

            return Ok("Kompania u modifikua me sukses!");
        }

        [HttpDelete("DeleteCompany/{id}")]
        public IActionResult DeleteCompany(int id)
        {
            //1. Duke perdour ID marrim te dhenat nga databaza
            var company = _appDbContext.Companies.FirstOrDefault(x => x.Id == id);
            if (company == null)
                return NotFound();

            //2. Fshijme Kompanine nga DB
            _appDbContext.Companies.Remove(company);
            _appDbContext.SaveChanges();

            return Ok($"Kompania me id = {id} u fshi me sukses!");
        }
    }
}