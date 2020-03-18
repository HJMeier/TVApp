using System;
using System.ComponentModel.DataAnnotations;

namespace TVApp.Models
{
    public class Discipline
    //any changes to data model -> Packet Manager Console: Add-Migration YourMigration Name and Update-Database
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Cathegory { get; set; }

        [Required]
        public int ScoreTableId { get; set; }
    }
}
