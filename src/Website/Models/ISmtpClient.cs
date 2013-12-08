using System.Net.Mail;

namespace MusicCompany.Website.Models
{
	public interface ISmtpClient
	{
		void Send(MailMessage mailMessage);
	}
}