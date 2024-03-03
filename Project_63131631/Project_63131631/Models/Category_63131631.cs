namespace Project_63131631.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
	[Table("Categories")]
	public partial class Category_63131631
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category_63131631()
        {
            ProductTypes = new HashSet<ProductType_63131631>();
        }

        [Key]
        public int cateID { get; set; }

        [StringLength(100)]
        public string cateName { get; set; }

        public string catePhoto { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductType_63131631> ProductTypes { get; set; }
    }
}
