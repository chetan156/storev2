using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace storev2.Models
{
    [Table("Catagory")]
    public class CatagoryMaster
    {
        public CatagoryMaster()
        {
            this.PurchaseMasters = new HashSet<PurchaseMaster>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CatagoryId { get; set; }
        [Required(ErrorMessage ="Please enter Category Name")]
        public string CatagoryName { get; set; }

        [ForeignKey("CompanyMaster")]
        [Required(ErrorMessage = "Please Select Company")]
        public int CompanyId { get; set; }
        public virtual CompanyMaster CompanyMaster { get; set; }
        public virtual ICollection<PurchaseMaster> PurchaseMasters { get; set; }
    }

    public enum CatagoryEnum
    {
        Index,
        CatagoryId,
        CatagoryName,
        CompanyId
    }
}