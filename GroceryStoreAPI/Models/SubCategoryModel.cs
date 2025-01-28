﻿namespace GroceryStoreAPI.Models
{
    public class SubCategoryModel
    {
        public int? SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryImage {  get; set; }
        public string CategoryName { get; set; }
        public int CategoryID { get; set; }
        public DateTime Created { get; set; }
        public DateTime Modified { get; set; }
    }

    public class SubCategoryDropDownModel
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
    }
}
