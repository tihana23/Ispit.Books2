using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ispit.Books2.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string UserId { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Users { get; set; } = new List<SelectListItem>();
        [NotMapped]
        public IEnumerable<SelectListItem> Authors { get; set; } = new List<SelectListItem>();
        [NotMapped]
        public IEnumerable<SelectListItem> Publishers { get; set; } = new List<SelectListItem>();
     
    }
}
