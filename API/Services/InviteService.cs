using API.Data;
using Domain.Entities;

namespace API.Services
{
    public class InviteService
    {
        private readonly AppDbContext mmm_context;
        private readonly EmailService m_email;

        public InviteService(AppDbContext context, EmailService email)
        {
            mmm_context = context;
            m_email = email;
        }

        public async Task<Invite> CreateInviteAsync(string email)
        {
            var invite = new Invite
            {
                Email = email,
                Token = Guid.NewGuid().ToString(),
                IsUsed = false,
                CreatedAt = DateTime.UtcNow
            };

            mmm_context.Invites.Add(invite);
            await mmm_context.SaveChangesAsync();

            await m_email.SendInviteEmailAsync(email, invite.Token);

            return invite;
        }
    }
}
