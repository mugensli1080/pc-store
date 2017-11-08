namespace pc_store.Controllers.Resources
{
    public class ProductPhotoResource : KeyValuePairResource
    {
        public string Thumbnail { get; set; }
        public int ProductId { get; set; }

        public bool Activated { get; set; }
    }
}