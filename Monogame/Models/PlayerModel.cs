using Monogame.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Models
{
    public class PlayerModel : ICreature
    {
        public int Size => 192;

        public int Speed => 350;

        public int SpriteSize => 48;

        int ICreature.IdleFrames => 6;

        int ICreature.RunFrames => 6;

        int ICreature.AttackFrames => 4;

        int ICreature.DeathFrames => 3;
    }
}
