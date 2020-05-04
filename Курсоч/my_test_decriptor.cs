namespace Курсоч
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class my_test_decriptor
    {
        public int id { get; set; }

        [StringLength(50)]
        public string test_name { get; set; }

        [StringLength(50)]
        public string name_descriptor { get; set; }

        public string description { get; set; }

        [Column(TypeName = "image")]
        public byte[] pic { get; set; }

        public virtual my_test my_test { get; set; }
    }
}
