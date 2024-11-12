using System;
using GameStore.Api.Dtos;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    private static readonly List<GameDto> games = [
    new GameDto(
        Id: 1,
        Name: "Fifa",
        Genre: "Sport",
        Type: 2007,
        ReleaseDate : new DateOnly(2006, 09, 27)
    ),
        new GameDto(
        Id: 2,
        Name: "Mortal Combact",
        Genre: "Fight",
        Type: 4,
        ReleaseDate : new DateOnly(1999,11,01)
    ),
        new GameDto(
        Id: 3,
        Name: "Apex Legends",
        Genre: "Battle Royal",
        Type: 2007,
        ReleaseDate : new DateOnly(2022,12,09)
    ),
];
    public static RouteGroupBuilder mapGamesEndpoints(this WebApplication app)
    {
        const string GetGame = "GetGame";
        //groups
        var group = app.MapGroup("games")
            .WithParameterValidation();
        // get games
        group.MapGet("/", () => games);

        // get game by Id
        group.MapGet("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            return game == null ? Results.NotFound() : Results.Ok(game);
        }).WithName(GetGame);
        // post game
        group.MapPost("/", (CreateGameDto newGame) =>
        {
            GameDto game = new(
                Id: games.Count + 1,
                Name: newGame.Name,
                Type: newGame.Type,
                ReleaseDate: newGame.ReleaseDate,
                Genre: newGame.Genre
            );
            games.Add(game);
            return Results.CreatedAtRoute(GetGame, new { id = game.Id }, game);
        });
        // update game
        group.MapPut("/{id}", (int id, UpdateGameDto newGame) =>
        {
            // find the game in games
            int i = games.FindIndex(game => game.Id == id);
            if (i == -1)
                return Results.NotFound();
            games[i] = new GameDto(
                Id: id,
                Name: newGame.Name,
                Type: newGame.Type,
                Genre: newGame.Genre,
                ReleaseDate: newGame.ReleaseDate
            );
            return Results.NoContent();
        });

        group.MapDelete("/{id}", (int id) =>
        {
            GameDto? game = games.Find(game => game.Id == id);
            if (game != null)
                games.Remove(game);
            return Results.NoContent();
        });
        return group;
    }
}
