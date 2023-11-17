using System;
using System.Collections.Generic;

namespace Entities.Entity
{
    public partial class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string EmailAddress { get; set; } = null!;
        public string Password { get; set; } = null!;
        public bool IsConfirm { get; set; }
        public bool IsTwoFactor { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? TokenStartDate { get; set; }
        public DateTime? TokenExpiredDate { get; set; }
        public bool IsActive { get; set; }
    }
}
