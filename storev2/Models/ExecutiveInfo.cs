using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace storev2.Models
{
    [Table("Executive")]
    public class ExecutiveInfo
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ExecutiveId { get; set; }
        [Required(ErrorMessage ="Please Enter Executive Name")]
        public string ExecutiveName { get; set; }
        [Required(ErrorMessage = "Please Enter Executive ContactNo ")]
        public string ExecutiveContactNo { get; set; }
        [Required(ErrorMessage = "Please Enter Executive Address ")]
        public string ExecutiveAddress { get; set; }
        [Required(ErrorMessage = "Please Enter Executive Alternate No ")]
        public string ExecutiveALtNo { get; set; }

    }
    public enum Executiveorder
    {
        Index,
        ExecutiveName,
        ExecutiveContactNo,
        ExecutiveAddress,
        ExecutiveALtNo
    }
}