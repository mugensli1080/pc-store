namespace pc_store.Controllers.Resources
{
    public class SpecificationPhotoResource : KeyValuePairResource
    {
        public string Thumbnail { get; set; }
        public int Index { get; set; }
        public int ProductId { get; set; }
    }
}