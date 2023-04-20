namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record InstrumentUpdateDTO(Guid Id, string? Name = default, string? Description = default, float? Price = default,
            List<string>? Reviews = default, string? Color = default);


