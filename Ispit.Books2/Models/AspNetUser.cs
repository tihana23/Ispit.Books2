
using Microsoft.AspNetCore.Identity;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace Ispit.Books2.Models
    {
        public class AspNetUser : IdentityUser
        {
            [StringLength(50)]
            public string? FirstName { get; set; }
            [StringLength(50)]
            public string? LastName { get; set; }
            [StringLength(150)]
            public string? Address { get; set; }

            [ForeignKey("UserId")]
            public virtual ICollection<Book> Books { get; set; }

        }
    }
