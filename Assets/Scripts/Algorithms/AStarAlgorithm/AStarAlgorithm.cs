using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace NuclearGames
{
    public class AStarAlgorithm : MonoBehaviour
    {
        private List<List<AStarCell>> _table = new List<List<AStarCell>>();
        private Field _field;

        private AStarCell _activeCell;

        public void Solve(List<List<Cell>> cells, Field field)
        {
            ReworkTable(cells);

            _field = field;
            int startX = _field.startCell.x, startY = _field.startCell.y;
            int targetX = _field.endCell.x, targetY = _field.endCell.y;
            _activeCell = new AStarCell(startX, startY);
            List<AStarCell> way = new List<AStarCell>();

            int step = 1;
            bool solved = false;
            while (!solved)
            {
                int minWeight = 1000;
                AStarCell nextActiveCell = new AStarCell(0, 0);
                
                if (_activeCell.y - 1 >= 0)
                {
                    if (_table[_activeCell.y - 1][_activeCell.x].cellType != 1 && !_table[_activeCell.y - 1][_activeCell.x].used)
                    {
                        _table[_activeCell.y - 1][_activeCell.x].weight =
                            10 + DistanceBetween(_activeCell.x, _activeCell.y - 1, targetX, targetY) * 10;
                        
                        if (_activeCell.x == targetX && _activeCell.y - 1 == targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x, _activeCell.y - 1, targetX, targetY) * 10))
                        {
                            minWeight = 10 + DistanceBetween(_activeCell.x, _activeCell.y - 1, targetX, targetY) * 10;
                            nextActiveCell = _table[_activeCell.y - 1][_activeCell.x];
                        }
                    }
                }
                if (_activeCell.y - 1 >= 0 && _activeCell.x + 1 < _table[0].Count)
                {
                    if (_table[_activeCell.y - 1][_activeCell.x + 1].cellType != 1 && !_table[_activeCell.y - 1][_activeCell.x + 1].used)
                    {
                        _table[_activeCell.y - 1][_activeCell.x + 1].weight =
                            10 + DistanceBetween(_activeCell.x + 1, _activeCell.y - 1, targetX, targetY) * 10;
                        
                        if (_activeCell.x + 1 == targetX && _activeCell.y - 1== targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x + 1, _activeCell.y - 1, targetX, targetY) *
                            10))
                        {
                            minWeight = 10 + DistanceBetween(_activeCell.x + 1, _activeCell.y - 1, targetX, targetY) *
                                10;
                            nextActiveCell = _table[_activeCell.y - 1][_activeCell.x + 1];
                        }
                    }
                }
                if (_activeCell.x + 1 < _table[0].Count)
                {
                    if (_table[_activeCell.y][_activeCell.x + 1].cellType != 1 && !_table[_activeCell.y][_activeCell.x + 1].used)
                    {
                        _table[_activeCell.y][_activeCell.x + 1].weight =
                            10 + DistanceBetween(_activeCell.x + 1, _activeCell.y, targetX, targetY) * 10;
                        
                        if (_activeCell.x + 1 == targetX && _activeCell.y == targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x + 1, _activeCell.y, targetX, targetY) * 10))
                        {
                            minWeight = 10 + DistanceBetween(_activeCell.x + 1, _activeCell.y, targetX, targetY) * 10;
                            nextActiveCell = _table[_activeCell.y][_activeCell.x + 1];
                        }
                    }
                }
                if (_activeCell.y + 1 < _table.Count && _activeCell.x + 1 < _table[0].Count)
                {
                    if (_table[_activeCell.y + 1][_activeCell.x + 1].cellType != 1 && !_table[_activeCell.y + 1][_activeCell.x + 1].used)
                    {
                        _table[_activeCell.y + 1][_activeCell.x + 1].weight =
                            10 + DistanceBetween(_activeCell.x + 1, _activeCell.y + 1, targetX, targetY) * 10;
                        
                        if (_activeCell.x + 1 == targetX && _activeCell.y + 1 == targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x + 1, _activeCell.y + 1, targetX, targetY) *
                            10))
                        {
                            minWeight = 10 + DistanceBetween(_activeCell.x + 1, _activeCell.y + 1, targetX, targetY) *
                                10;
                            nextActiveCell = _table[_activeCell.y + 1][_activeCell.x + 1];
                        }
                    }
                }
                if (_activeCell.y + 1 < _table.Count)
                {
                    if (_table[_activeCell.y + 1][_activeCell.x].cellType != 1 && !_table[_activeCell.y + 1][_activeCell.x].used)
                    {
                        _table[_activeCell.y + 1][_activeCell.x].weight =
                            10 + DistanceBetween(_activeCell.x, _activeCell.y + 1, targetX, targetY) * 10;
                        
                        if (_activeCell.x == targetX && _activeCell.y + 1 == targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x, _activeCell.y + 1, targetX, targetY) * 10))
                        {
                            minWeight = 10 + DistanceBetween(_activeCell.x, _activeCell.y + 1, targetX, targetY) * 10;
                            nextActiveCell = _table[_activeCell.y + 1][_activeCell.x];
                        }
                    }
                }
                if (_activeCell.y + 1 < _table.Count && _activeCell.x - 1 >= 0)
                {
                    if (_table[_activeCell.y + 1][_activeCell.x - 1].cellType != 1 && !_table[_activeCell.y + 1][_activeCell.x - 1].used)
                    {
                        _table[_activeCell.y + 1][_activeCell.x - 1].weight =
                            10 + DistanceBetween(_activeCell.x - 1, _activeCell.y + 1, targetX, targetY) * 10;
                        
                        if (_activeCell.x - 1 == targetX && _activeCell.y + 1 == targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x - 1, _activeCell.y + 1, targetX, targetY) *
                            10))
                        {
                            minWeight = 10 + DistanceBetween(_activeCell.x - 1, _activeCell.y + 1, targetX, targetY) *
                                10;
                            nextActiveCell = _table[_activeCell.y + 1][_activeCell.x - 1];
                        }
                    }
                }
                if (_activeCell.x - 1 >= 0)
                {
                    if (_table[_activeCell.y][_activeCell.x - 1].cellType != 1 && !_table[_activeCell.y][_activeCell.x - 1].used)
                    {
                        _table[_activeCell.y][_activeCell.x - 1].weight =
                            10 + DistanceBetween(_activeCell.x - 1, _activeCell.y, targetX, targetY) * 10;
                        
                        if (_activeCell.x - 1 == targetX && _activeCell.y == targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x - 1, _activeCell.y, targetX, targetY) * 10))
                        {
                            //print(_activeCell.x + " " + _activeCell.y);
                            minWeight = 10 + DistanceBetween(_activeCell.x - 1, _activeCell.y, targetX, targetY) * 10;
                            nextActiveCell = _table[_activeCell.y][_activeCell.x - 1];
                        }
                    }
                }
                if (_activeCell.y - 1 >= 0 && _activeCell.x - 1 >= 0)
                {
                    if (_table[_activeCell.y - 1][_activeCell.x - 1].cellType != 1 && !_table[_activeCell.y - 1][_activeCell.x - 1].used)
                    {
                        _table[_activeCell.y - 1][_activeCell.x - 1].weight =
                            10 + DistanceBetween(_activeCell.x - 1, _activeCell.y - 1, targetX, targetY) * 10;
                        
                        if (_activeCell.x - 1 == targetX && _activeCell.y - 1 == targetY) solved = true;
                        
                        if (minWeight > (10 + DistanceBetween(_activeCell.x - 1, _activeCell.y - 1, targetX, targetY) *
                            10))
                        {
                            minWeight = 10 + DistanceBetween(_activeCell.x - 1, _activeCell.y - 1, targetX, targetY) *
                                10;
                            nextActiveCell = _table[_activeCell.y - 1][_activeCell.x - 1];
                        }
                    }
                }

                _table[_activeCell.y][_activeCell.x].used = true;
                //print("-------" + step + "------");
                if (step == 20)
                {
                    solved = true;
                    //print(_activeCell.x + " " + _activeCell.y);
                }
                _activeCell = nextActiveCell;
                way.Add(_activeCell);
                step++;
            }

            for (int i = 0; i < way.Count - 1; i++)
            {
                _field.SetPathCell(way[i].x, way[i].y);
            }
        }

        public int DistanceBetween(int x1, int y1, int x2, int y2)
        {
            bool solved = false;
            List<AStarCell> _activeCells = new List<AStarCell>();
            _activeCells.Add(new AStarCell(x1, y1, CellType.Start));
            int step = 1;
            while (!solved)
            {
                List<AStarCell> newList = new List<AStarCell>();
                for (int i = 0; i < _activeCells.Count; i++)
                {
                    if (_activeCells[i].y - 1 >= 0)
                    {
                        newList.Add(_table[_activeCells[i].y - 1][_activeCells[i].x]);
                        if ((_activeCells[i].y - 1) == y2 && _activeCells[i].x == x2) solved = true;
                    }
                    if (_activeCells[i].x + 1 < _table[0].Count)
                    {
                        newList.Add(_table[_activeCells[i].y][_activeCells[i].x + 1]);
                        if (_activeCells[i].y == y2 && (_activeCells[i].x + 1) == x2) solved = true;
                    }
                    if (_activeCells[i].y + 1 < _table.Count)
                    {
                        newList.Add(_table[_activeCells[i].y + 1][_activeCells[i].x]);
                        if ((_activeCells[i].y + 1) == y2 && _activeCells[i].x == x2) solved = true;
                    }

                    if (_activeCells[i].x - 1 >= 0)
                    {
                        newList.Add(_table[_activeCells[i].y][_activeCells[i].x - 1]);
                        if ((_activeCells[i].y) == y2 && (_activeCells[i].x - 1) == x2) solved = true;
                    }
                }

                if (step == 500) solved = true;
                
                step++;
                _activeCells = newList;
            }

            return step - 1;
        }

        public void ReworkTable(List<List<Cell>> table)
        {
            for (int i = 0; i < table.Count; i++)
            {
                _table.Add(new List<AStarCell>());
                for (int j = 0; j < table[i].Count; j++)
                {
                    AStarCell newWaveCell = new AStarCell(table[i][j].x, table[i][j].y, table[i][j].cellType == CellType.End ? CellType.Free : table[i][j].cellType);
                    _table[i].Add(newWaveCell);
                }
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
    }
}
