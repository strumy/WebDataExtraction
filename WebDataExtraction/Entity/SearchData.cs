using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebDataExtraction.Models;

namespace WebDataExtraction.Entity
{
    public class SearchData
    {
        [Key]
        public int SearchDataId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Location { get; set; }
        public virtual ICollection<RestaurentData> RestaurentDatas { get; set; }
    }
}