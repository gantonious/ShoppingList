using System;

namespace ShoppingList.Data.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string ProfileUrl { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}