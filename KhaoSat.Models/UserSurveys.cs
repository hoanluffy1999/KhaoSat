using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhaoSat.Models
{
    [Table("UserSurveys")]
    public class UserSurveys
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("UserId")]
        public long UserId { get; set; }
        [Column("SurveyId")]
        public long SurveyId { get; set; }

    }
}
