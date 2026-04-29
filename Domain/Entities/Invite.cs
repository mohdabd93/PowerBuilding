using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Invite
    {
        public int Id { get; set; }

        public string Email { get; set; } = "";

        public string Token { get; set; } = "";

        public bool IsUsed { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string Link { get; set; } = "";
        public DateTime ExpiresAt { get; set; }
    }
}
