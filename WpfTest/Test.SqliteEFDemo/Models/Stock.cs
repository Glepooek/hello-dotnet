using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Test.SqliteEFDemo.Models
{
    public class Stock
    {
        [Key]
        [MaxLength(32)]
        public string LocalId { get; set; }

        public string Symbol { get; set; }

        public int Version { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 删除状态，0正常，1删除
        /// </summary>
        public int Status { get; set; }
    }
}
