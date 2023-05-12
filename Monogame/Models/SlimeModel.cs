using Monogame.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.Models
{
    public class SlimeModel : ICreature
    {
        public int Size => 128;

        public int Speed => 300;

        public int SpriteSize => 32;

        int ICreature.IdleFrames => 4;

        int ICreature.RunFrames => 6;

        int ICreature.AttackFrames => 7;

        int ICreature.DeathFrames => 6;
    }
}
