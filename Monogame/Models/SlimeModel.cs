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
        public int size => 128;

        public int speed => 100;

        public int spriteSize => 32;

        int ICreature.idleFrames => 4;

        int ICreature.runFrames => 6;

        int ICreature.attackFrames => 7;

        int ICreature.deathFrames => 6;
    }
}
