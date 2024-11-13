using System;

namespace GameStore.Api.Entities;

public class Game
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public  int GenreId { get; set; }
    public Genre? GameGenre { get; set; }

    public decimal Type { get; set; }

    public DateOnly ReleaseDate { get; set; }
}
