using System;
using Test.SqliteNetDemo.SQLite;

namespace Test.SqliteNetDemo.Models
{
    [Table("Stocks")]
    public class Stock
    {
        [PrimaryKey]
        [Column("local_id")]
        [MaxLength(32)]
        public string LocalId { get; set; }

        [Column("symbol")]
        public string Symbol { get; set; }

        [Column("version")]
        public int Version { get; set; }

        [Column("created_time")]
        public DateTime CreatedTime { get; set; }

        [Column("updated_time")]
        public DateTime UpdatedTime { get; set; }

        /// <summary>
        /// 删除状态，0正常，1删除
        /// </summary>
        [Column("status")]
        public uint Status { get; set; }
    }
}
