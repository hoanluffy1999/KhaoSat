using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhaoSat.Models
{
    [Table("Questions")]
    public class Questions
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("SurveyId")]
        public long SurveyId { get; set; }
        [Column("Description")]
        public string Description { get; set; }
        [Column("Type")]
        public byte Type { get; set; }
        [Column("Other")]
        public bool? Other { get; set; }
        public DateTime? CreatedDate { get; set; }
        [Column("UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
        [Column("CreatedBy")]
        public long? CreatedBy { get; set; }
        [Column("UpdatedBy")]
        public long? UpdatedBy { get; set; }
        [NotMapped]
        public List<Answer> Answers { get; set; }
       
    }
}
