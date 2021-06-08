using RPS.Enums;
using RPS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Services
{
    public class Service:IService
    {
        public GameResult GetGameResult(Play playerChoice, Play opponentChoice)
        {
            return _possibleGameStates
                .Single(gs => gs.player == playerChoice && gs.opponent == opponentChoice)
                .result;
        }

        private readonly List<(Play player, Play opponent, GameResult result)> _possibleGameStates =
            new List<(Play player, Play opponent, GameResult result)>
            {
            (Play.Rock, Play.Rock, GameResult.Draw),
            (Play.Rock, Play.Paper, GameResult.Lose),
            (Play.Rock, Play.Scissors, GameResult.Win),
            (Play.Paper, Play.Rock, GameResult.Win),
            (Play.Paper, Play.Paper, GameResult.Draw),
            (Play.Paper, Play.Scissors, GameResult.Lose),
            (Play.Scissors, Play.Rock, GameResult.Lose),
            (Play.Scissors, Play.Paper, GameResult.Win),
            (Play.Scissors, Play.Scissors, GameResult.Draw),
            };
    }
}
