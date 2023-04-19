namespace MobyLabWebProgramming.Core.Entities
{
    public class Categorie : BaseEntity
    {
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public ICollection<Subcategorie> Subcategorii { get; set; } = default!;
    }
}
