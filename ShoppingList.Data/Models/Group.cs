using System;
using System.Collections.Generic;

namespace ShoppingList.Data.Models
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<User> Users { get; set; }
    }
}