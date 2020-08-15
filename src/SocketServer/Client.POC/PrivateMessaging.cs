using System;

namespace SocketServer.POC.Models
{
    public class PrivateMessaging
    {
        public string From { get; set; }
        public string To { get; set; }
        public string Text { get; set; }
     
        public PrivateMessaging(string from, string to, string text)
        {
            From = from;
            To = to;
            Text = text;
        }
    }
}