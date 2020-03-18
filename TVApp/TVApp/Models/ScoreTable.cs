using System;
using System.ComponentModel.DataAnnotations;

namespace TVApp.Models
{
    public class ScoreTable
    //any changes to data model -> Packet Manager Console: Add-Migration YourMigration Name and Update-Database
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ScoreTableId { get; set; }

        [Required]
        public float Result { get; set; }

        [Required]
        public float Score { get; set; }
    }
}
