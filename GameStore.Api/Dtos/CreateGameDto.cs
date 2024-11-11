namespace GameStore.Api.Dtos;

public record class CreateGameDto(
    string Name,
    string Genre,
    decimal Type,
    DateOnly ReleaseDate
);
