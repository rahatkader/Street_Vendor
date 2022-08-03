using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Street_Vendors.Models
{
    public class Item
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int ItemPrice { get; set; }
        public string ItemImage { get; set; }
        public int ItemRating { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
}