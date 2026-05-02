using System;

namespace Domain.Models
{
    public class UserProfile : BaseEntity<int>
    {
        public int UserId { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string ZipCode { get; set; }

        // 🔗 Navigation
        public User User { get; set; }
    }
}