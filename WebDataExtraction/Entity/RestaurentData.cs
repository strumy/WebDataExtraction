using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebDataExtraction.Entity
{
    public class RestaurentData
    {
        [Key]
        public int RestaurentId { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        public string Address { get; set; }
        public string Zipcode { get; set; }
        public int SearchDataId { get; set; }
        [ForeignKey("SearchDataId")]
        public virtual SearchData  SearchData { get; set; }
    }
}