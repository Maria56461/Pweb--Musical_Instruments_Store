
namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record CategorieUpdateDTO(Guid Id, string? Name = default, string? Description = default);
