using Microsoft.AspNetCore.Mvc.Filters;
using System.Globalization;

namespace Task2
{
    public class Logging : ActionFilterAttribute
    {
        private const string _logFilePath = @"AppData/log.txt";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string name = context.ActionDescriptor.DisplayName;
            if (!string.IsNullOrEmpty(name))
            {
                using StreamWriter streamWriter = new StreamWriter(_logFilePath, true);
                streamWriter.WriteLine($"Action method: {name} was called at: {DateTime.Now}");
            }
        }
    }
}
