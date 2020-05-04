namespace Курсоч
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class my_test
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public my_test()
        {
            my_test_decriptor = new HashSet<my_test_decriptor>();
            questions = new HashSet<question>();
        }

        [Key]
        [StringLength(50)]
        public string test_name { get; set; }

        [StringLength(50)]
        public string host_login { get; set; }

        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<my_test_decriptor> my_test_decriptor { get; set; }

        public virtual user user { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<question> questions { get; set; }
    }
}
