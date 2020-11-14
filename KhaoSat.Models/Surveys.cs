using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhaoSat.Models
{
    [Table("Surveys")]
    public class Surveys
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("Title")]
        public string Title { get; set;}
        [Column("Description")]
        public string Description { get; set; }
        [Column("Status")]
        public byte? Status { get; set; }
        [Column("CreatedDate")]
        public DateTime? CreatedDate { get; set; }
        [Column("UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("CreatedBy")]
        public long? CreatedBy { get; set; }
        [Column("UpdatedBy")]
        public long? UpdatedBy { get; set; }
        [NotMapped]
        public List<Questions> Questions { get; set; }
  
    }
    public class SurveyViewModel : Surveys
    {
        public List<Users> Users { get; set; }
        public List<UserAnswers> UserAnswers { get; set; }
    }
}
