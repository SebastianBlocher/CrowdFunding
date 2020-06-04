using crowdFunding.Core.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace crowdFunding.Core.Services.Options.Create
{
    public class CreateProjectOptions
    {       
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        //[JsonConverter(typeof(StringEnumConverter))]
        public Category Category { get; set; }        
        public decimal? AmountRequired { get; set; }
      
    }

    internal class StringEnumConverter
    {
    }
}

