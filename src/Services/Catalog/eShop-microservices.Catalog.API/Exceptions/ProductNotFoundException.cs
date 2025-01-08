namespace eShop_microservices.Catalog.API.Exceptions {
    [Serializable]
    internal class ProductNotFoundException : Exception {
   
        public ProductNotFoundException() : base("Product not found") {
        }
    }
}