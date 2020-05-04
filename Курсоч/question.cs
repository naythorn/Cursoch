namespace Курсоч
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("question")]
    public partial class question
    {
        public int id { get; set; }

        public int? question_number { get; set; }

        [StringLength(50)]
        public string test_name { get; set; }

        public string text { get; set; }

        public virtual my_test my_test { get; set; }
    }
}
