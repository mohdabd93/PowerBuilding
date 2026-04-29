using System.Net;
using System.Net.Mail;

namespace API.Services
{
    public class EmailService
    {
        private readonly IConfiguration m_config;

        public EmailService(IConfiguration config)
        {
            m_config = config;
        }

        public async Task SendInviteEmailAsync(string toEmail, string token)
        {
            var smtpHost = m_config["Smtp:Host"];
            var smtpPort = int.Parse(m_config["Smtp:Port"]!);
            var smtpUser = m_config["Smtp:User"];
            var smtpPass = m_config["Smtp:Pass"];

            var encodedToken = Uri.EscapeDataString(token);

            var inviteLink =
                $"https://localhost:7133/register?token={encodedToken}";

            using var message = new MailMessage
            {
                From = new MailAddress(smtpUser),
                Subject = "Power Building App - Invitation",
                Body = $@"
            <h2>You are invited!</h2>
            <p>Click below to register:</p>
            <a href='{inviteLink}'>Register Now</a>
            <p>This invite may expire or be single-use.</p>
        ",
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            using var smtp = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
        public async Task SendResetPasswordAsync(string toEmail, string token)
        {
            var smtpHost = m_config["Smtp:Host"];
            var smtpPort = int.Parse(m_config["Smtp:Port"]!);
            var smtpUser = m_config["Smtp:User"];
            var smtpPass = m_config["Smtp:Pass"];

            var encodedToken = Uri.EscapeDataString(token);

            var resetLink =
                $"https://localhost:7133/reset-password?token={encodedToken}&email={toEmail}";

            using var message = new MailMessage
            {
                From = new MailAddress(smtpUser),
                Subject = "Power Building App - Password Reset",
                Body = $@"
            <h2>Password Reset Request</h2>
            <p>Click the link below to reset your password:</p>
            <a href='{resetLink}'>Reset Password</a>
            <p>This link will expire soon.</p>
        ",
                IsBodyHtml = true
            };

            message.To.Add(toEmail);

            using var smtp = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            await smtp.SendMailAsync(message);
        }
    }
}
