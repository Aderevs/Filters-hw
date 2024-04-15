using Microsoft.AspNetCore.Mvc.Filters;

namespace Task1
{
    public class CountUsers : ActionFilterAttribute
    {
        private HashSet<string> _uniqueUsers = new HashSet<string>();
        private static readonly object _lock = new object();
        private const string _logFilePath = @"AppData/unique_users.txt";
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var ipAddress = context.HttpContext.Connection.RemoteIpAddress.ToString();
            bool ifUserUnique = CheckIfUserIsUnique(ipAddress);
            if (ifUserUnique)
            {
                LogUniqueUsersToFile(ipAddress);
            }
        }
        private bool CheckIfUserIsUnique(string userIp)
        {
            lock( _lock )
            {
                if (!_uniqueUsers.Contains(userIp))
                {
                    _uniqueUsers.Add(userIp);
                    return true;
                }
                else 
                {
                    return false;
                }
            }
        }

        private void LogUniqueUsersToFile(string userIp)
        {
            using (StreamWriter writer = File.AppendText(_logFilePath))
            {
                writer.WriteLine($"Unique User IP: {userIp}, Timestamp: {DateTime.Now}");
            }
        }

    }
}
