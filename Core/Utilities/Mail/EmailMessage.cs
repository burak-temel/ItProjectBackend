using System.Collections.Generic;

namespace Core.Utilities.Mail
{
    public class EmailMessage
    {
        public string ToAddress { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
    }
}
