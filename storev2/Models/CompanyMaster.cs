﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace storev2.Models
{
    [Table("Company")]
    public class CompanyMaster
    {
        public CompanyMaster()
        {
            this.CatagoryMasters = new HashSet<CatagoryMaster>();
            this.ModelMasters = new HashSet<ModelMaster>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CompanyId { get; set; }
        [Required(ErrorMessage ="Please Enter Company Name")]
        public string CompanyName { get; set; }

        public virtual ICollection<CatagoryMaster> CatagoryMasters { get; set; }
        public virtual ICollection<ModelMaster> ModelMasters { get; set; }
    }
    public enum Company
    {
        Index,
        CompanyId,
        CompanyName
    }
}