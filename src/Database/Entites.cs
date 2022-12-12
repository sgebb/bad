using System.ComponentModel.DataAnnotations.Schema;

namespace Bad.Database
{
    public record StringEntity(string Value)
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
    }
    public record NumberEntity(int Value)
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
    }
}
