//using CsvHelper;
//using CsvHelper.Configuration;
//using System.Globalization;
//using System.IO;

//namespace CsvToDatabaseApi.Services
//{
//    public class CsvService
//    {
//        public IEnumerable<dynamic> ParseCsv(string filePath)
//        {
//            // Use the CsvConfiguration to correctly set HasHeaderRecord
//            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
//            {
//                HasHeaderRecord = true  // Set it here in the configuration
//            };

//            using (var reader = new StreamReader(filePath))
//            using (var csv = new CsvReader(reader, config)) // Pass the configuration to the CsvReader
//            {
//                var records = csv.GetRecords<dynamic>().ToList();
//                return records;
//            }
//        }
//    }
//}



using CsvHelper;
using CsvHelper.Configuration;
using CsvToDatabaseApi.Models; // Add the Models namespace if you're planning to use strongly-typed data
using System.Globalization;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging; // For logging (optional but useful)
using System;

namespace CsvToDatabaseApi.Services
{
    public class CsvService
    {
        private readonly ILogger<CsvService> _logger;

        public CsvService(ILogger<CsvService> logger)
        {
            _logger = logger;
        }

        public IEnumerable<Companies> ParseCsv(string filePath)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Ensure headers are read
                    MissingFieldFound = null // Optional: prevents exceptions if a field is missing
                };

                using (var reader = new StreamReader(filePath))
                using (var csv = new CsvReader(reader, config))
                {
                    // Read records into strongly-typed objects, this assumes Organization model
                    var records = csv.GetRecords<Companies>().ToList();
                    return records;
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError($"Error occurred while parsing the CSV file: {ex.Message}");
                return Enumerable.Empty<Companies>(); // Return empty list in case of error
            }
        }

        // Optionally, add support for IFormFile (useful in web API scenario)
        public IEnumerable<Companies> ParseCsvFromFile(IFormFile file)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    HasHeaderRecord = true, // Ensure headers are read
                    MissingFieldFound = null // Optional: prevents exceptions if a field is missing
                };

                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, config))
                {
                    // Read records into strongly-typed objects
                    var records = csv.GetRecords<Companies>().ToList();
                    return records;
                }
            }
            catch (Exception ex)
            {
                // Log the error
                _logger.LogError($"Error occurred while parsing the CSV file: {ex.Message}");
                return Enumerable.Empty<Companies>(); // Return empty list in case of error
            }
        }
    }
}
