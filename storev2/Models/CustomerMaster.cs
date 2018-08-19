using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace storev2.Models
{
    [Table("Customer")]
    public class CustomerMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerId { get; set; }

        [Required(ErrorMessage ="Please Enter Customer Name")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Please Enter Customer Address")]
        public string CustomerAddress { get; set; }

        [Required(ErrorMessage = "Please Enter Mobile Number")]
        public string CustomerMobile { get; set; }

        [Required(ErrorMessage = "Please Enter Alternate Mobile Number")]
        public string CustomerAltMobile { get; set; }

        [Required(ErrorMessage = "Please Enter Occupation")]
        public string CustomerOccupation { get; set; }

        [Required(ErrorMessage = "Please Enter Office Number")]
        public string OfficeNo { get; set; }

        [Required(ErrorMessage = "Please Select Date")]
        public DateTime CustomerDob { get; set; }

        [Required(ErrorMessage = "Please Select Ann Date")]
        public Nullable< DateTime> CustomerAnnDate { get; set; }

        [Required(ErrorMessage = "Please Enter Reference")]
        public string Refrence { get; set; }

        [Required(ErrorMessage = "Please Enter Number")]
        public string CustomerNo { get; set; }

        [ForeignKey("AreaMaster")]
        public int AreaId { get; set; }

        [Required(ErrorMessage = "Please Enter Gurantor Name")]
        public string GurantorName { get; set; }

        [Required(ErrorMessage = "Please Enter Gurantor Number")]
        public string GurantorNo { get; set; }

        [Required(ErrorMessage = "Please Enter Contat No")]
        public string GurantorContatNo { get; set; }

        [Required(ErrorMessage = "Please Enter Pin")]
        public string Pin { get; set; }

        public virtual AreaMaster AreaMaster { get; set; }


    }
    public enum CustomerEnum
    {
        Index,
        CustomerName,
        AreaId,
        GurantorName
    }
}