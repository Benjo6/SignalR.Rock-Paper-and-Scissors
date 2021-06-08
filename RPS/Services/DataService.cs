using Microsoft.EntityFrameworkCore;
using RPS.Data;
using RPS.Enums;
using RPS.Extensions;
using RPS.Interfaces;
using RPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Services
{
    public class DataService : IDataService
    {
        private readonly GameDbContext _gameDbContext;

        public DataService(GameDbContext gameDbContext)
        {
            _gameDbContext = gameDbContext;
        }

        public async Task<Game> FindGameWaitingForPlayer()
        {
            return await _gameDbContext.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .FirstOrDefaultAsync(g => g.Player2 == null);
        }

        public async Task<Game> CreateGameWithPlayer(string playerSessionId)
        {
            var game = new Game
            {
                Id = Guid.NewGuid(),
                Player1 = new GameSession
                {
                    Id = playerSessionId
                }
            };
            _gameDbContext.Games.Add(game);
            await _gameDbContext.SaveChangesAsync();
            return game;
        }
        
        public async Task<Game> AddPlayerToGame(Game game, string playerSessionId)
        {
            _gameDbContext.Attach(game);
            game.Player2 = new GameSession
            {
                Id = playerSessionId
            };
            await _gameDbContext.SaveChangesAsync();
            return game;
        }

        public async Task<Game> FindGameBySessionId(string playerSessionId)
        {
            return await _gameDbContext.Games
                .Include(g => g.Player1)
                .Include(g => g.Player2)
                .FirstAsync(g =>
                    g.Player1.Id == playerSessionId || g.Player2.Id == playerSessionId);
        }
        public async Task<Game> UpdatePlayForPlayer(Game game, string playerSessionId, Play play)
        {
            _gameDbContext.Attach(game);
            var player = game.GetPlayerSession(playerSessionId);
            player.Play = play;
            await _gameDbContext.SaveChangesAsync();
            return game;
        }

        
    }
}
