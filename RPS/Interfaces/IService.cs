using RPS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Interfaces
{
    public interface IService
    {
        GameResult GetGameResult(Play playerChoice, Play opponentChoice);
    }
}
