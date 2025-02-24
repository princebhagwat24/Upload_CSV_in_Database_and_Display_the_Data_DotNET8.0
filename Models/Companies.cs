using System;

namespace CsvToDatabaseApi.Models
{
    public class Companies
    {
        public string OrganizationId { get; set; }
        public string Name { get; set; }
        public string Website { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public int Founded { get; set; }
        public string Industry { get; set; }
        public int NumberofEmployees { get; set; }
    }
}
