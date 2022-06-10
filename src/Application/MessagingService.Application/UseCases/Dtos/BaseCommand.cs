using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MessagingService.Application.UseCases.Dtos
{
    public abstract class BaseCommand
    {
        public BaseCommand()
        {
            ValidationErrorList = new List<string>();
        }
        [JsonIgnore]
        public List<string> ValidationErrorList { get; set; }

        public void SetValidationErrorList(IEnumerable<string> errorMessages)
        {
            ValidationErrorList.AddRange(errorMessages);
        }
    }
}