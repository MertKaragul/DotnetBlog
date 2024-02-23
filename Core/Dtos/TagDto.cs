using Core.Dtos.BlogDto;

namespace Core.Dtos
{
    public class TagDto {
        public int TagId { get; set; }
        public string? Name { get; set; }
        public IEnumerable<BlogDto.BlogDto>? Blogs { get; set; }
    }
}
