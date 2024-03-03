namespace Project_63131631.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	[Table("Producers")]

	public partial class Producer_63131631
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Producer_63131631()
        {
            Products = new HashSet<Product_63131631>();
        }

        [Key]
        public int pdcID { get; set; }

        [StringLength(100)]
        public string pdcName { get; set; }

        [StringLength(20)]
        public string pdcPhone { get; set; }

        [StringLength(50)]
        public string pdcEmail { get; set; }

        public string pdcAddress { get; set; }

        public string pdcPhoto { get; set; }

        public string pdcInfo { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_63131631> Products { get; set; }
    }
}
