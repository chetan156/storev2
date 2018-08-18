using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace storev2.Models
{
    [Table("Area")]
    public class AreaMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaId { get; set; }
        [Required(ErrorMessage ="Please Enter Area Name")]
        public string AreaName { get; set; }
    }

    public enum Area
    {
        Index,
        AreaId,
        AreaName
    }
}