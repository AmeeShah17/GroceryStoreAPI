﻿namespace GroceryStoreAPI.Models
{
    public class BillModel
    {
        public int? BillID { get; set; }
        public string BillNumber { get; set; }
        public int OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public int CustomerID {  get; set; }
        public string? CustomerName { get; set; }
        public decimal TotalAmount {  get; set; }
        public int Discount {  get; set; }
        public decimal NetAmount {  get; set; }
        public DateTime? Created { get; set; }
        public DateTime? Modified { get; set; }
    }
}
