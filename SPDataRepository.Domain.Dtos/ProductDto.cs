namespace SPDataRepository.Domain.Dtos
{
    public sealed record ProductDto : DtoBase
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

    }
}
