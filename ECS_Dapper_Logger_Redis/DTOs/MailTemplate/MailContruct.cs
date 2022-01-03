using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace ECS_Dapper_Logger_Redis.DTOs.MailTemplate
{
    public class MailContruct
    {
        public string Fullname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }

        public string GetTemplate()
        {
            string url = @"http://localhost:3000/confirm-account/" + Token;
            return $"<h1>Dear Mr/Mrs {Fullname}</h1> " +
                $"<p>We registed your account. Please confirm it</p>" +
                $"<p>Your Email : {Email}</p>" +
                $"<p>Your Password : {Password}</p>" +
                $"<p>Have a nice day.</p>" +
                $"<a href='{HtmlEncoder.Default.Encode(url)}'>Click here to confirm your account</a>";
        }


    }
}
