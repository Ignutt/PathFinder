namespace NuclearGames
{
    public class WaveCell
    {
        public int x, y;

        public int weight = -1;

        public int cellType;

        public WaveCell(int x, int y, CellType cellType)
        {
            this.x = x;
            this.y = y;
            this.cellType = (int) cellType;

        }
    }
}