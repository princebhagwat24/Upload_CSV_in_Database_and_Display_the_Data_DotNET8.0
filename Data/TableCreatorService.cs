//using CsvHelper;
//using CsvHelper.Configuration;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;

//namespace CsvToDatabaseApi.Data
//{
//    public class TableCreatorService
//    {
//        private readonly DatabaseContext _context;
//        private readonly ILogger<TableCreatorService> _logger;

//        public TableCreatorService(DatabaseContext context, ILogger<TableCreatorService> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        // Method to parse the CSV and insert data into the database table
//        public async Task InsertCsvDataIntoTableAsync(IFormFile file, string tableName, List<string> columns)
//        {
//            try
//            {
//                using (var reader = new StreamReader(file.OpenReadStream()))
//                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HeaderValidated = null, MissingFieldFound = null }))
//                {
//                    var records = csv.GetRecords<dynamic>().ToList(); // Read the CSV file into records

//                    // Insert each record into the database
//                    foreach (var record in records)
//                    {
//                        // Cast the dynamic record to an IDictionary
//                        var dictionaryRecord = (IDictionary<string, object>)record;

//                        var insertQuery = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) " +
//                                          $"VALUES ({string.Join(", ", columns.Select(c => $"'{dictionaryRecord[c]}'"))})";

//                        // Execute the insert query
//                        await _context.Database.ExecuteSqlRawAsync(insertQuery);
//                    }

//                    // Commit the changes (if needed, usually EF Core does this automatically with ExecuteSqlRawAsync)
//                    await _context.SaveChangesAsync();

//                    // Log the success message
//                    _logger.LogInformation($"Successfully inserted {records.Count} rows into {tableName}.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log any errors
//                _logger.LogError($"Error occurred while processing the file: {ex.Message}");
//            }
//        }
//        //public async Task InsertCsvDataIntoTableAsync(IFormFile file, string tableName, List<string> columns)
//        //{
//        //    try
//        //    {
//        //        using (var reader = new StreamReader(file.OpenReadStream()))
//        //        using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HeaderValidated = null, MissingFieldFound = null }))
//        //        {
//        //            var records = csv.GetRecords<dynamic>().ToList(); // Read the CSV file into records

//        //            // Insert each record into the database
//        //            foreach (var record in records)
//        //            {
//        //                // Cast the dynamic record to an IDictionary
//        //                var dictionaryRecord = (IDictionary<string, object>)record;

//        //                var insertQuery = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) " +
//        //                                  $"VALUES ({string.Join(", ", columns.Select(c => $"'{dictionaryRecord[c]}'"))})";

//        //                // Execute the insert query
//        //                await _context.Database.ExecuteSqlRawAsync(insertQuery);
//        //            }

//        //            // Commit the changes (if needed, usually EF Core does this automatically with ExecuteSqlRawAsync)
//        //            await _context.SaveChangesAsync();

//        //            // Log the success message
//        //            _logger.LogInformation($"Successfully inserted {records.Count} rows into {tableName}.");
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        // Log any errors
//        //        _logger.LogError($"Error occurred while processing the file: {ex.Message}");
//        //    }
//        //}


//    }
//}






//using CsvHelper;
//using CsvHelper.Configuration;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using System;

//namespace CsvToDatabaseApi.Data
//{
//    public class TableCreatorService
//    {
//        private readonly DatabaseContext _context;
//        private readonly ILogger<TableCreatorService> _logger;

//        public TableCreatorService(DatabaseContext context, ILogger<TableCreatorService> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        // Method to parse the CSV and insert data into the database table
//        public async Task InsertCsvDataIntoTableAsync(IFormFile file, string tableName, List<string> columns)
//        {
//            try
//            {
//                using (var reader = new StreamReader(file.OpenReadStream()))
//                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HeaderValidated = null, MissingFieldFound = null }))
//                {
//                    var rowCount = 0;
//                    const int batchSize = 1000; // Process records in batches of 1000

//                    // Read the CSV file row by row and process in batches
//                    while (csv.Read())
//                    {
//                        var record = csv.GetRecord<dynamic>(); // Process one record at a time
//                        rowCount++;

//                        // Cast the dynamic record to an IDictionary for proper indexing
//                        var dictionaryRecord = (IDictionary<string, object>)record;

//                        // Create the SQL insert query for this record
//                        var insertQuery = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) " +
//                                          $"VALUES ({string.Join(", ", columns.Select(c => $"'{dictionaryRecord[c]}'"))})";

//                        // Execute the insert query
//                        await _context.Database.ExecuteSqlRawAsync(insertQuery);

//                        // Log progress for every 1000 records
//                        if (rowCount % 1000 == 0)
//                        {
//                            _logger.LogInformation($"Processed {rowCount} rows.");
//                        }
//                    }

//                    // Commit the changes (usually handled automatically with ExecuteSqlRawAsync)
//                    await _context.SaveChangesAsync();

//                    // Log the total rows processed
//                    _logger.LogInformation($"Successfully inserted {rowCount} rows into {tableName}.");
//                }
//            }
//            catch (Exception ex)
//            {
//                // Log any errors
//                _logger.LogError($"Error occurred while processing the file: {ex.Message}");
//            }
//        }
//    }
//}




///////////////////////////////////////////////successfull/////////////////////////////////////////
//using CsvHelper;
//using CsvHelper.Configuration;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using System.Collections.Generic;
//using System.Globalization;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using System;

//namespace CsvToDatabaseApi.Data
//{
//    public class TableCreatorService
//    {
//        private readonly DatabaseContext _context;
//        private readonly ILogger<TableCreatorService> _logger;

//        public TableCreatorService(DatabaseContext context, ILogger<TableCreatorService> logger)
//        {
//            _context = context;
//            _logger = logger;
//        }

//        // Method to parse the CSV and insert data into the database table
//        public async Task InsertCsvDataIntoTableAsync(IFormFile file, string tableName, List<string> columns)
//        {
//            try
//            {
//                using (var reader = new StreamReader(file.OpenReadStream()))
//                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HeaderValidated = null, MissingFieldFound = null }))
//                {
//                    var rowCount = 0;
//                    const int batchSize = 1000;

//                    while (csv.Read())
//                    {
//                        try
//                        {
//                            var record = csv.GetRecord<dynamic>();
//                            rowCount++;

//                            var dictionaryRecord = (IDictionary<string, object>)record;

//                            // Construct SQL insert query
//                            var insertQuery = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) " +
//                                              $"VALUES ({string.Join(", ", columns.Select(c => $"'{dictionaryRecord[c]}'"))})";

//                            // Log the SQL query being executed
//                            _logger.LogInformation($"Executing SQL: {insertQuery}");

//                            // Execute the insert query
//                            await _context.Database.ExecuteSqlRawAsync(insertQuery);

//                            if (rowCount % 1000 == 0)
//                            {
//                                _logger.LogInformation($"Processed {rowCount} rows.");
//                            }
//                        }
//                        catch (Exception rowEx)
//                        {
//                            _logger.LogError($"Error processing row {rowCount}: {rowEx.Message}");
//                        }
//                    }

//                    // Commit the changes
//                    await _context.SaveChangesAsync();
//                    _logger.LogInformation($"Successfully inserted {rowCount} rows into {tableName}.");
//                }
//            }
//            catch (Exception ex)
//            {
//                _logger.LogError($"Error occurred while processing the file: {ex.Message}");
//            }
//        }


//        // Method to validate a record, you can add more validation checks as needed
//        private bool IsValidRecord(IDictionary<string, object> record, List<string> columns)
//        {
//            foreach (var column in columns)
//            {
//                if (!record.ContainsKey(column) || record[column] == null || string.IsNullOrWhiteSpace(record[column].ToString()))
//                {
//                    return false; // Invalid record if any column is missing or null
//                }

//                // Example: check if "Founded" column can be parsed to DateTime
//                if (column == "Founded" && !DateTime.TryParse(record[column].ToString(), out _))
//                {
//                    return false; // Invalid record if the "Founded" column is not a valid date
//                }
//            }

//            return true; // Valid record if all checks pass
//        }
//    }
//}





using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using CsvToDatabaseApi.Models; // Reference the Models namespace
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System;

namespace CsvToDatabaseApi.Data
{
    public class TableCreatorService
    {
        private readonly DatabaseContext _context;
        private readonly ILogger<TableCreatorService> _logger;

        public TableCreatorService(DatabaseContext context, ILogger<TableCreatorService> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Method to parse the CSV and insert data into the database table
        public async Task InsertCsvDataIntoTableAsync(IFormFile file, string tableName, List<string> columns)
        {
            try
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { HeaderValidated = null, MissingFieldFound = null }))
                {
                    var rowCount = 0;
                    const int batchSize = 1000; // Process records in batches of 1000
                    var skippedRows = 0;

                    // Read the CSV file row by row and process in batches
                    while (csv.Read())
                    {
                        try
                        {
                            var record = csv.GetRecord<dynamic>(); // Process one record at a time
                            rowCount++;

                            // Cast the dynamic record to an IDictionary for proper indexing
                            var dictionaryRecord = (IDictionary<string, object>)record;

                            // Validate the data for required columns (e.g., check if a column is null or empty)
                            if (IsValidRecord(dictionaryRecord, columns))
                            {
                                // Create the SQL insert query for this record
                                var insertQuery = $"INSERT INTO {tableName} ({string.Join(", ", columns)}) " +
                                                  $"VALUES ({string.Join(", ", columns.Select(c => $"'{dictionaryRecord[c]}'"))})";

                                // Execute the insert query
                                await _context.Database.ExecuteSqlRawAsync(insertQuery);
                            }
                            else
                            {
                                // Log the invalid row
                                _logger.LogWarning($"Skipping invalid row at index {rowCount}: {string.Join(", ", dictionaryRecord.Values)}");
                                skippedRows++;
                            }

                            // Log progress for every 1000 records
                            if (rowCount % 1000 == 0)
                            {
                                _logger.LogInformation($"Processed {rowCount} rows.");
                            }
                        }
                        catch (Exception rowEx)
                        {
                            // Log any exceptions encountered while processing a specific row
                            _logger.LogError($"Error processing row {rowCount}: {rowEx.Message}");
                            skippedRows++;
                        }
                    }

                    // Commit the changes (usually handled automatically with ExecuteSqlRawAsync)
                    await _context.SaveChangesAsync();

                    // Log the total rows processed and skipped
                    _logger.LogInformation($"Successfully inserted {rowCount - skippedRows} rows into {tableName}. Skipped {skippedRows} invalid rows.");
                }
            }
            catch (Exception ex)
            {
                // Log any errors encountered during the entire file processing
                _logger.LogError($"Error occurred while processing the file: {ex.Message}");
            }
        }

        // Method to validate a record, you can add more validation checks as needed
        private bool IsValidRecord(IDictionary<string, object> record, List<string> columns)
        {
            foreach (var column in columns)
            {
                if (!record.ContainsKey(column) || record[column] == null || string.IsNullOrWhiteSpace(record[column].ToString()))
                {
                    return false; // Invalid record if any column is missing or null
                }

                // Example: check if "Founded" column can be parsed to DateTime
                if (column == "Founded" && !DateTime.TryParse(record[column].ToString(), out _))
                {
                    return false; // Invalid record if the "Founded" column is not a valid date
                }
            }

            return true; // Valid record if all checks pass
        }
    }
}

