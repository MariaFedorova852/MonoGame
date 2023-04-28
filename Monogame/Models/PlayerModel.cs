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
        public int size => 192;

        public int speed => 400;

        public int spriteSize => 48;

        int ICreature.idleFrames => 6;

        int ICreature.runFrames => 6;

        int ICreature.attackFrames => 4;

        int ICreature.deathFrames => 4;
    }
}
