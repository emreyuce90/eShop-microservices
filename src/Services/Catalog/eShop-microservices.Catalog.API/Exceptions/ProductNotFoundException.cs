using BuildingBlocks.Exceptions;

namespace eShop_microservices.Catalog.API.Exceptions {
    [Serializable]
    internal class ProductNotFoundException : NotFoundException {
        public ProductNotFoundException(string message) : base(message) {
        }

        public ProductNotFoundException(string name,object key) : base(name,key) {
        }
    }
}