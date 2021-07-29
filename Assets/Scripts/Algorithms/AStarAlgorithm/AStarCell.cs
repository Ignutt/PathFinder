using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NuclearGames
{
    public class AStarCell
    {
        public int x, y;

        public int weight = -1;
        public bool used = false;

        public int cellType;

        public AStarCell(int x, int y, CellType cellType = CellType.Path)
        {
            this.x = x;
            this.y = y;
            this.cellType = (int) cellType;

        }
    }
}
