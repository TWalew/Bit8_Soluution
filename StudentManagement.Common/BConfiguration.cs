using Microsoft.Extensions.Configuration;

namespace StudentManagement.Common
{
    public class BConfiguration : IBConfiguration
    {
        public string ConnectionString { get; set; }

        public BConfiguration(IConfiguration configuration)
        {
            ConnectionString = configuration.GetValue<string>("ConnectionString");
        }        
    }
}