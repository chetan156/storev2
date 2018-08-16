using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace storev2.Models
{
    [Table("ModelMaster")]
    public class ModelMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ModelId { get; set; }
        [Required(ErrorMessage ="Please Enter Model Name")]
        public string ModelName { get; set; }
        [ForeignKey("CompanyMaster")]
        [Required(ErrorMessage ="Please Select Company Name")]
        public int CompanyId { get; set; }
        [ForeignKey("CategoryMaster")]
        [Required(ErrorMessage = "Please Select Category Name")]
        public int CategoryId { get; set; }

        public virtual CompanyMaster CompanyMaster { get; set; }
        public virtual CategoryMaster CategoryMaster { get; set; }
    }
    public enum Model
    {
        Index,
        ModelId,
        ModelName,
        CompanyId,
        CategoryId
    }
}