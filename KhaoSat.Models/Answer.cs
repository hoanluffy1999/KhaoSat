using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhaoSat.Models
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("QuestionId")]
        public long QuestionId { get; set; }
        [Column("Text")]
        public string Text { get; set; }
        public bool? RightAnswer { get; set; }
        [NotMapped]
        public double TiLeChon { get; set; }
    }
}
