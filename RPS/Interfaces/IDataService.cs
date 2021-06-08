using RPS.Enums;
using RPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Interfaces
{
    public interface IDataService
    {
        //Find a game which has a Player1Session but no Player2Session
        Task<Game> FindGameWaitingForPlayer();

        //Create a new game and add a PlayerSession with the provided id to it
        Task<Game> CreateGameWithPlayer(string playerSessionId);

        //Set Player2Session of the provided game using the provided playerSessionId
        Task<Game> AddPlayerToGame(Game game, string playerSessionId);

        //Find the game which the player is a part of 
        Task<Game> FindGameBySessionId(string playerSessionId);

        //Updates what choice the player made (Rock, Paper or Scissors) during the game
        Task<Game> UpdatePlayForPlayer(Game game, string playerSessionId, Play play);
    }
}
