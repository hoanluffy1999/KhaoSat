using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace KhaoSat.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        [Column("Id")]
        public long Id { get; set; }
        [Column("FullName")]
        public string FullName { get; set; }
        [Column("Sex")]
        public bool Sex { get; set; }
        [Column("Job")]
        public string Job { get; set; }
        [Column("Place")]
        public string Place { get; set; }
        [Column("Email")]
        public string Emai { get; set; }
        [Column("Time")]
        public DateTime? Time { get; set; }
        [NotMapped]
        public Surveys Surveys { get; set; }
        [NotMapped]
        public List<UserAnswers> ListAnswers { get; set; }
    }
}
