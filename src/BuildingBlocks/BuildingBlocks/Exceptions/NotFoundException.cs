namespace BuildingBlocks.Exceptions {
    public class NotFoundException : Exception {
        public NotFoundException(string message) : base(message) {

        }

        public NotFoundException(string name, object key) : base($"The entity {name} key ({key}) was not found") {

        }
    }
}
