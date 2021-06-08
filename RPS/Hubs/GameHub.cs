using Microsoft.AspNetCore.SignalR;
using RPS.Constants;
using RPS.Enums;
using RPS.Extensions;
using RPS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Hubs
{
    public class GameHub : Hub
    {
        private readonly IDataService _dataService;
        private readonly IService _service;
        public GameHub(IDataService dataService, IService service)
        {
            _dataService = dataService;
            _service = service;
        }
        public async Task SelectPlay(string play)
        {
            var game = await _dataService.FindGameBySessionId(Context.ConnectionId);
          
            await _dataService.UpdatePlayForPlayer(game, Context.ConnectionId, (Play)Enum.Parse(typeof(Play), play));

            var playerSession = game.GetPlayerSession(Context.ConnectionId);
            var opponentSession = game.GetOpponentSession(playerSession);

            if (opponentSession.Play.HasValue)
            {
                
                var playerResult = _service.GetGameResult(
                    playerSession.Play.Value,
                    opponentSession.Play.Value).ToString();

                await Clients.Client(playerSession.Id)
                    .SendAsync(GameEventNames.GameEnd, playerResult);


                var opponentResult = _service.GetGameResult(
                    opponentSession.Play.Value,
                    playerSession.Play.Value).ToString();

                await Clients.Client(opponentSession.Id)
                    .SendAsync(GameEventNames.GameEnd, opponentResult);
            }
            else
            {
                await Clients.Caller.SendAsync(GameEventNames.WaitingForPlayerToPlay);
            }
        }
        public async Task JoinGame()
        {
            var existingGame = await _dataService.FindGameWaitingForPlayer();

            if (existingGame == null)
            {
                await _dataService.CreateGameWithPlayer(Context.ConnectionId);

                await Clients.Client(Context.ConnectionId).SendAsync(GameEventNames.WaitingForPlayerToJoin);
            }
            else
            {
                await _dataService.AddPlayerToGame(existingGame, Context.ConnectionId);

                await Clients.Client(existingGame.Player1.Id).SendAsync(GameEventNames.GameStart);
                await Clients.Client(existingGame.Player2.Id).SendAsync(GameEventNames.GameStart);
            }
        }
    }
}
