using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.interfaces
{
    public interface ICreature
    {
        int idleFrames { get; }
        int runFrames { get; }
        int attackFrames { get; }
        int deathFrames { get; }
        int size { get; }
        int spriteSize { get; }
        int speed { get; }
    }
}
