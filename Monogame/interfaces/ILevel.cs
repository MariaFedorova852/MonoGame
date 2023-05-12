using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monogame.interfaces
{
    public interface ILevel
    {
        int[,] map { get; }
        int mapWidth { get; }
        int mapHeight { get; }
        List<IObject> objects { get; }
        List<IEntity> enemys { get; }
        IEnumerable<IObject> enemysAndObjects { get; }
    }
}
