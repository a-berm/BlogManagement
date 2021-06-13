using System.ComponentModel.DataAnnotations;

namespace BlogManagement.Domain.Dto
{
    public class PostInputDto
    {
        [Required]
        [Range(1, long.MaxValue, ErrorMessage = "userId must be greater than 0")]
        public long UserId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Title { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Content { get; set; }
    }
}
