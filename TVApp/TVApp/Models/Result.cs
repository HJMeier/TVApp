using System;
using System.ComponentModel.DataAnnotations;

namespace TVApp.Models
{
    public class Result
    //any changes to data model -> Packet Manager Console: Add-Migration YourMigration Name and Update-Database
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ParticipantId { get; set; }

        [Required]
        public string Discipline { get; set; }

        [Required]
        public float DisciplineResult { get; set; }

        [Required]
        public float DisciplineScore { get; set; }
    }
}
