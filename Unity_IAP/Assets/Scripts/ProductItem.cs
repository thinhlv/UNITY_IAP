using UnityEngine;
using System.Collections;
using UnityEngine.Purchasing;
using System.Collections.Generic;

public class ProductItem {
    string ProductID { get; set; }
    string StoreName { get; set; }

    public ProductItem(string productId, string storeName)
    {
        this.ProductID = productId;
        this.StoreName = storeName;
    }
}