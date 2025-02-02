﻿namespace GroceryStoreAPI.Models
{
    public class UserModel
    {
        public int? UserID { get; set; }
        public string UserName { get; set; }
        public string Email {  get; set; }
        public string Password { get; set; }
        public bool IsActive {  get; set; }
        public DateTime? Created {  get; set; }
        public DateTime? Modified { get; set; }
    }
}
