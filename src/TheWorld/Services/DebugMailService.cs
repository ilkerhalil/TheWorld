using System.Diagnostics;

namespace TheWorld.Services
{
    public class DebugMailService : IMailService
    {
        public bool SendMail(string to, string @from, string subect, string body)
        {
            Debug.WriteLine($"Sending mail: To: {to}, Subject: {subect}");
            return true;
        }
    }
}