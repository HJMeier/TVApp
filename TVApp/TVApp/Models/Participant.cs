using System;
using System.ComponentModel.DataAnnotations;

namespace TVApp.Models
{
    public class Participant
    //any changes to data model -> Packet Manager Console: Add-Migration YourMigration Name and Update-Database
    {
        [Key]
        public int ParticipantId { get; set; }

        [Required]
        public string PreName { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int YearOfBirth { get; set; }

        [Required]
        public string Club { get; set; }

        [Required]
        public string Cathegory { get; set; }
    }
}
