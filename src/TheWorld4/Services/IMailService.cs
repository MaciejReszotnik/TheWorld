using System;

namespace TheWorld4.Services
{
	public interface IMailService
    {
        bool SendMail(string to, string from, string subject, string body);
    }
}