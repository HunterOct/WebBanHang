namespace Project_63131631.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Administrator")]
    public partial class Administrator_63131631
    {
        [Key]
        [StringLength(500)]
        public string adAcc { get; set; }

        [StringLength(500)]
        public string adPass { get; set; }
    }
}
