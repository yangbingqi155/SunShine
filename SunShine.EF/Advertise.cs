namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Advertise")]
    public partial class Advertise
    {
        [Key]
        [StringLength(50)]
        public string idadvertise { get; set; }

        [StringLength(500)]
        public string path { get; set; }

        public int? sortno { get; set; }
    }
}
