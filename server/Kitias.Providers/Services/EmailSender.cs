using Kitias.Providers.Models;
using Microsoft.Extensions.Configuration;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace Kitias.Providers.Services
{
	/// <summary>
	/// Service to send email confirmation to user
	/// </summary>
	public class EmailSender
	{
		private readonly IConfiguration _config;

		/// <summary>
		/// Constructor to take nessasary services
		/// </summary>
		/// <param name="config">Config</param>
		public EmailSender(IConfiguration config) => _config = config;

		/// <summary>
		/// Send email method
		/// </summary>
		/// <param name="model">Email sender settings</param>
		/// <returns>Asynchorys</returns>
		public async Task SendEmailAsync(EmailRequestModel model)
		{
			var client = new SendGridClient(_config["SendGrid:Key"]);
			var message = new SendGridMessage
			{
				From = new()
				{
					Email = "boris.sizov.2001@mail.ru",
					Name = _config["SendGrid:Name"]
				},
				Subject = model.Subject,
				PlainTextContent = model.Message,
				HtmlContent = model.Message
			};

			message.AddTo(model.To);
			message.SetClickTracking(false, false);
			await client.SendEmailAsync(message);
		}
	}
}
