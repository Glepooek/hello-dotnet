using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.SqliteEFDemo.Models
{
    public class Valuation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int StockId { get; set; }

        public DateTime Time { get; set; }

        public decimal Price { get; set; }
    }
}
