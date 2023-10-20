using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IIG_School.Models
{
    public class Question : IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? QuestionName { get; set; }
        public virtual ICollection<Answer>? Answers { get; set; }
    }
}
