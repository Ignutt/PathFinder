using System.Collections.Generic;
using System.IO;

namespace NuclearGames
{
    public class WaveAlgorithm
    {
        private List<List<WaveCell>> _table = new List<List<WaveCell>>();

        private List<WaveCell> _activeCells = new List<WaveCell>();

        private Field _field;

        public WaveAlgorithm(List<List<Cell>> table, Field field)
        {
            ReworkTable(table);
            _field = field;

            _activeCells.Add(new WaveCell(_field.startCell.x, _field.startCell.y, CellType.Start));
            _table[_field.startCell.y][_field.startCell.x].weight = 9;
        }

        public void Solve()
        {
            bool solved = false;
            int step = 1;
            int targetX = _field.endCell.x, targetY = _field.endCell.y;
            while (!solved)
            {
                List<WaveCell> newList = new List<WaveCell>();

                for (int i = 0; i < _activeCells.Count; i++)
                {
                    if (_activeCells[i].y - 1 >= 0)
                        if (_table[_activeCells[i].y - 1][_activeCells[i].x].cellType != 1 &&
                            _table[_activeCells[i].y - 1][_activeCells[i].x].weight == -1)
                        {
                            _table[_activeCells[i].y - 1][_activeCells[i].x].weight = step;
                            newList.Add(_table[_activeCells[i].y - 1][_activeCells[i].x]);
                            if ((_activeCells[i].y - 1) == targetY && _activeCells[i].x == targetX) solved = true;
                        }

                    if (_activeCells[i].x + 1 < _table[0].Count)
                        if (_table[_activeCells[i].y][_activeCells[i].x + 1].cellType != 1 &&
                            _table[_activeCells[i].y][_activeCells[i].x + 1].weight == -1)
                        {
                            _table[_activeCells[i].y][_activeCells[i].x + 1].weight = step;
                            newList.Add(_table[_activeCells[i].y][_activeCells[i].x + 1]);
                            if (_activeCells[i].y == targetY && (_activeCells[i].x + 1)== targetX) solved = true;
                        }

                    if (_activeCells[i].y + 1 < _table.Count)
                        if (_table[_activeCells[i].y + 1][_activeCells[i].x].cellType != 1 &&
                            _table[_activeCells[i].y + 1][_activeCells[i].x].weight == -1)
                        {
                            _table[_activeCells[i].y + 1][_activeCells[i].x].weight = step;
                            newList.Add(_table[_activeCells[i].y + 1][_activeCells[i].x]);
                            if ((_activeCells[i].y + 1) == targetY && _activeCells[i].x == targetX) solved = true;
                        }

                    if (_activeCells[i].x - 1 >= 0)
                        if (_table[_activeCells[i].y][_activeCells[i].x - 1].cellType != 1 &&
                            _table[_activeCells[i].y][_activeCells[i].x - 1].weight == -1)
                        {
                            _table[_activeCells[i].y][_activeCells[i].x - 1].weight = step;
                            newList.Add(_table[_activeCells[i].y][_activeCells[i].x - 1]);
                            if ((_activeCells[i].y) == targetY && (_activeCells[i].x - 1) == targetX) solved = true;
                        }
                }

                _activeCells = newList;
                step++;

                // unifinity
                if (step == 70)
                {
                    solved = true;
                    step = 0;
                    _field.CantFindPath();
                }
            }

            step -= 2;
            int lastX = _field.endCell.x;
            int lastY = _field.endCell.y;
            while (step > 0)
            {
                if (lastY - 1 >= 0)
                        if (_table[lastY - 1][lastX].cellType != 1 &&
                            _table[lastY - 1][lastX].weight == step)
                        {
                            _field.SetPathCell(lastX, lastY - 1);
                            lastY -= 1;
                        }

                if (lastX + 1 < _table[0].Count)
                    if (_table[lastY][lastX + 1].cellType != 1 &&
                        _table[lastY][lastX + 1].weight == step)
                    {
                        _field.SetPathCell(lastX + 1, lastY);
                        lastX += 1;
                    }

                if (lastY + 1 < _table.Count)
                    if (_table[lastY + 1][lastX].cellType != 1 &&
                        _table[lastY + 1][lastX].weight == step)
                    {
                        _field.SetPathCell(lastX, lastY + 1);
                        lastY += 1;
                    }

                if (lastX - 1 >= 0)
                    if (_table[lastY][lastX - 1].cellType != 1 &&
                        _table[lastY][lastX - 1].weight == step)
                    {
                        _field.SetPathCell(lastX - 1, lastY);
                        lastX -= 1;
                    }

                step--;

            }
            
            PrintCoordinates();
        }

        private void PrintCoordinates()
        {
            var  fileName = "Coordinates.txt";
            var sr = File.CreateText(fileName);
            for (int i = 0; i < _table.Count; i++)
            {
                for (int j = 0; j < _table[0].Count; j++)
                {
                    sr.Write(_table[i][j].weight + " ");
                }
                sr.WriteLine();
            }
            sr.Close();
        }
        
        public void ReworkTable(List<List<Cell>> table)
        {
            for (int i = 0; i < table.Count; i++)
            {
                _table.Add(new List<WaveCell>());
                for (int j = 0; j < table[i].Count; j++)
                {
                    WaveCell newWaveCell = new WaveCell(table[i][j].x, table[i][j].y, table[i][j].cellType);
                    _table[i].Add(newWaveCell);
                }
            }
        }
        
    }
}
