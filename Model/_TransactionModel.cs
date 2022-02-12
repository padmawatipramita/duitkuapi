using Binus.WS.Pattern.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace DuitKu.API.Model
{
    [DatabaseName("DuitKu")]
    [Table("trTransaction")]
    public class _TransactionModel : BaseModel
    {
        [Column("TransactionID")]
        [Key]
        public int TransactionID { get; set; }
        [Column("Balance")]
        public int Balance { get; set; }
        [Column("Date")]
        public DateTime Date { get; set; }
        [Column("Notes")]
        public string Notes { get; set; }
        [Column("TransactionType")]
        public string TransactionType { get; set; }
        [Column("UserID")]
        public int UserID { get; set; }
    }
}