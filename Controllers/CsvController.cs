//using CsvToDatabaseApi.Data;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CsvToDatabaseApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CsvController : ControllerBase
//    {
//        private readonly TableCreatorService _tableCreatorService;

//        public CsvController(TableCreatorService tableCreatorService)
//        {
//            _tableCreatorService = tableCreatorService;
//        }

//        [HttpPost("upload")]
//        public async Task<IActionResult> UploadCsv(IFormFile file, [FromQuery] string tableName)
//        {
//            // Define the column names that exist in the table (this needs to be defined manually based on your table schema)
//            var columns = new List<string>
//            {
//                "OrganizationId",
//                "Name",
//                "Website",
//                "Country",
//                "Description",
//                "Founded",
//                "Industry",
//                "NumberofEmployees"
//            };

//            // Call the service to insert the CSV data into the table
//            await _tableCreatorService.InsertCsvDataIntoTableAsync(file, tableName, columns);

//            return Ok("CSV data inserted successfully.");
//        }
//    }
//}



/////////////////////////////////////////////////////////////////Successfull/////////////////////////////////////////////////////////////////
//using CsvToDatabaseApi.Data;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CsvToDatabaseApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CsvController : ControllerBase
//    {
//        private readonly TableCreatorService _tableCreatorService;

//        public CsvController(TableCreatorService tableCreatorService)
//        {
//            _tableCreatorService = tableCreatorService;
//        }

//        [HttpPost("upload")]
//        public async Task<IActionResult> UploadCsv(IFormFile file, [FromQuery] string tableName)
//        {
//            // Define the column names that exist in the table (this needs to be defined manually based on your table schema)
//            var columns = new List<string>
//            {
//                "OrganizationId",
//                "Name",
//                "Website",
//                "Country",
//                "Description",
//                "Founded",
//                "Industry",
//                "NumberofEmployees"
//            };

//            // Call the service to insert the CSV data into the table
//            await _tableCreatorService.InsertCsvDataIntoTableAsync(file, tableName, columns);

//            return Ok("CSV data inserted successfully.");
//        }
//    }
//}




///////////////////////////////////////////////////////////////Pagination/////////////////////////////////////////////////////////////
//using CsvToDatabaseApi.Data;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Threading.Tasks;

//namespace CsvToDatabaseApi.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class CsvController : ControllerBase
//    {
//        private readonly TableCreatorService _tableCreatorService;
//        private readonly DatabaseContext _context;

//        public CsvController(TableCreatorService tableCreatorService, DatabaseContext context)
//        {
//            _tableCreatorService = tableCreatorService;
//            _context = context;
//        }

//        // POST: api/csv/upload
//        [HttpPost("upload")]
//        public async Task<IActionResult> UploadCsv(IFormFile file, [FromQuery] string tableName)
//        {
//            // Define the column names that exist in the table (this needs to be defined manually based on your table schema)
//            var columns = new List<string>
//            {
//                "OrganizationId",
//                "Name",
//                "Website",
//                "Country",
//                "Description",
//                "Founded",
//                "Industry",
//                "NumberofEmployees"
//            };

//            // Call the service to insert the CSV data into the table
//            await _tableCreatorService.InsertCsvDataIntoTableAsync(file, tableName, columns);

//            return Ok("CSV data inserted successfully.");
//        }

//        // GET: api/csv/Companies
//        [HttpGet("Companies")]
//        public async Task<IActionResult> GetCompanies([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
//        {
//            // Ensure default values are set if not provided
//            if (pageNumber < 1) pageNumber = 1;
//            if (pageSize < 1) pageSize = 10;

//            // Calculate the total number of companies for pagination info
//            var totalCompanies = await _context.Companies.CountAsync();

//            // Get the paginated result
//            var companies = await _context.Companies
//                .Skip((pageNumber - 1) * pageSize) // Skip previous pages
//                .Take(pageSize) // Take a specific number of results (pageSize)
//                .ToListAsync();

//            // Return the paginated result
//            return Ok(new
//            {
//                TotalCompanies = totalCompanies,
//                PageNumber = pageNumber,
//                PageSize = pageSize,
//                Companies = companies
//            });
//        }
//    }
//}



////////////////////////////////////////////////////////////////Lazy Loading//////////////////////////////////////////////////////////////////
using CsvToDatabaseApi.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CsvToDatabaseApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CsvController : ControllerBase
    {
        private readonly TableCreatorService _tableCreatorService;
        private readonly DatabaseContext _context;

        public CsvController(TableCreatorService tableCreatorService, DatabaseContext context)
        {
            _tableCreatorService = tableCreatorService;
            _context = context;
        }

        // POST: api/csv/upload
        [HttpPost("upload")]
        public async Task<IActionResult> UploadCsv(IFormFile file, [FromQuery] string tableName)
        {
            // Define the column names that exist in the table (this needs to be defined manually based on your table schema)
            var columns = new List<string>
            {
                "OrganizationId",
                "Name",
                "Website",
                "Country",
                "Description",
                "Founded",
                "Industry",
                "NumberofEmployees"
            };

            // Call the service to insert the CSV data into the table
            await _tableCreatorService.InsertCsvDataIntoTableAsync(file, tableName, columns);

            return Ok("CSV data inserted successfully.");
        }

        // GET: api/csv/Companies
        [HttpGet("Companies")]
        public async Task<IActionResult> GetCompanies([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            // Fetch only the required fields for performance reasons (projection)
            var companies = await _context.Companies
                .Skip((pageNumber - 1) * pageSize) // Pagination: Skip records based on page number and size
                .Take(pageSize) // Limit the number of records to the page size
                .Select(c => new // Lazy loading with only required fields
                {
                    c.OrganizationId,
                    c.Name,
                    c.Website,
                    c.Country,
                    c.Description,
                    c.Founded,
                    c.Industry,
                    c.NumberofEmployees
                })
                .ToListAsync();

            return Ok(companies);
        }
    }
}
