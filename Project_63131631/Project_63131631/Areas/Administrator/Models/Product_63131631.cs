namespace Project_63131631.Areas.Administrator.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	[Table("Products")]
	public partial class Product_63131631
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product_63131631()
        {
            OrderDetails = new HashSet<OrderDetail_63131631>();
        }

        [Key]
        [StringLength(50)]
        public string proID { get; set; }

        public int? pdcID { get; set; }

        public int? typeID { get; set; }

        [StringLength(200)]
        public string proName { get; set; }

        [StringLength(10)]
        public string proGuarantee { get; set; }

        [StringLength(10)]
        public string proPrice { get; set; }

        public int? proDiscount { get; set; }

        public string proPhoto { get; set; }

        [StringLength(50)]
        public string proUpdateDate { get; set; }

        public string proDescription { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OrderDetail_63131631> OrderDetails { get; set; }

        public virtual Producer_63131631 Producer { get; set; }

        public virtual ProductType_63131631 ProductType { get; set; }

        public virtual Rate_63131631 Rate { get; set; }
    }
}
