namespace Project_63131631.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	[Table("ProductTypes")]
	public partial class ProductType_63131631
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProductType_63131631()
        {
            Products = new HashSet<Product_63131631>();
        }

        [Key]
        public int typeID { get; set; }

        public int? cateID { get; set; }

        [StringLength(100)]
        public string typeName { get; set; }

        public virtual Category_63131631 Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Product_63131631> Products { get; set; }
    }
}
