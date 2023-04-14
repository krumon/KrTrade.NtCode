
using Microsoft.AspNetCore.Identity;

namespace KrTrade.WebApp.Services.Identity
{
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// The users first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// The users last name
        /// </summary>
        public string? LastName { get; set; }
    }
}
