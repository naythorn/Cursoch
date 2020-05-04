namespace Курсоч
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("result")]
    public partial class result
    {
        public int id { get; set; }

        [StringLength(50)]
        public string user_login { get; set; }

        public float? descriptor_EI { get; set; }

        public float? descriptor_SN { get; set; }

        public float? descriptor_TF { get; set; }

        public float? descriptor_JP { get; set; }

        [StringLength(50)]
        public string result_type { get; set; }

        public virtual user user { get; set; }
    }
}
