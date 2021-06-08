using Microsoft.EntityFrameworkCore;
using RPS.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPS.Models
{
    public class Game
    {
        public Guid Id { get; set; }
        public GameSession Player1 { get; set; }
        public GameSession Player2 { get; set; }
    }

    [Owned]
    public class GameSession
    {
        public string Id { get; set; }
        public Play? Play { get; set; }
    }
}
