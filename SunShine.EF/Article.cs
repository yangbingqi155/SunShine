namespace SunShine.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Article")]
    public partial class Article
    {
        [Key]
        [StringLength(50)]
        public string idarticle { get; set; }

        [Required]
        [StringLength(100)]
        public string title { get; set; }

        [StringLength(500)]
        public string img { get; set; }

        [Column(TypeName = "text")]
        public string content { get; set; }

        [Required]
        [StringLength(50)]
        public string idcategory { get; set; }

        public int? follow { get; set; }

        public DateTime? cretime { get; set; }
    }
}
