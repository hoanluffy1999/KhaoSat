using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhaoSat.Models
{
    [Table("UserAnswers")]
    public class UserAnswers
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("UserSurveyId")]
        public long UserSurveyId { get; set; }
        [Column("QuestionId")]
        public long QuestionId { get; set; }
        [Column("AnswerId")]
        public long AnswerId { get; set; }
    }
}
