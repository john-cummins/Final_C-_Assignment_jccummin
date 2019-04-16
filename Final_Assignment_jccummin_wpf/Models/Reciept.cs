namespace Final_Assignment_jccummin_wpf.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Reciept")]
    public partial class Reciept
    {
        public int RecieptID { get; set; }

        public decimal? Expense { get; set; }

        public decimal? Credit { get; set; }

        public decimal? Balance { get; set; }

        public decimal? TotalCash { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DayOfPurchase { get; set; }
    }
}
