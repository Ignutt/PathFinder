using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NuclearGames
{
    public class Field : MonoBehaviour
    {
        [Header("Start point of table")] 
        public Transform startPoint;

        [Header("Width and Height")] 
        [Range(1, 16)]
        public int width;

        [Range(1, 8)] 
        public int height;

        [Header("Cell prefab")] 
        public Cell cell;

        [Header("Offset")] 
        public Vector2 offset = new Vector2(0.5f, 0.5f);
        
        [HideInInspector]
        public Cell startCell;
        [HideInInspector]
        public Cell endCell;

        [Header("UI Slider")] 
        public Slider widthSlider;
        public Slider heightSlider;

        private List<List<Cell>> _cells = new List<List<Cell>>();
        private List<Cell> _pathCells = new List<Cell>();
        private List<Cell> _blocksCells = new List<Cell>();

        private bool waveAlg = true;

        private void Start()
        {
            if (heightSlider) heightSlider.value = height;
            if (widthSlider) widthSlider.value = width;
        }

        public void CreateField()
        {
            RemoveField();
            
            float posX = startPoint.position.x;
            float posY = startPoint.position.y;

            for (int i = 0; i < height; i++)
            {
                _cells.Add(new List<Cell>());
                for (int j = 0; j < width; j++)
                {
                    _cells[i].Add(Instantiate(cell));
                    _cells[i][j].transform.SetParent(transform);
                    _cells[i][j].transform.position = new Vector3(posX, posY);
                    _cells[i][j].x = j;
                    _cells[i][j].y = i;
                    _cells[i][j].field = this;

                    posX += _cells[i][j].GetComponent<SpriteRenderer>().bounds.size.x + offset.x;
                }

                posX = startPoint.position.x;
                posY -= _cells[i][0].GetComponent<SpriteRenderer>().bounds.size.y + offset.y;
            }
        }

        public void ChangeAlgorithmToWave()
        {
            waveAlg = true;
        }
        
        public void ChangeAlgorithmToAStar()
        {
            waveAlg = false;
        }

        public void RemoveField()
        {
            for (int i = 0; i < _cells.Count; i++) for (int j = 0; j < _cells[i].Count; j++) Destroy(_cells[i][j].gameObject);
            _cells.Clear();
        }
        
        public void OnWidthChanged()
        {
            width = (int) widthSlider.value;
        }

        public void OnHeightChanged()
        {
            height = (int) heightSlider.value;
        }
        
        public void FindPath()
        {
            if (!startCell) return;
            if (!endCell) return;
            ClearPath();
            if (waveAlg)
            {
                WaveAlgorithm wave = new WaveAlgorithm(_cells, this);
                wave.Solve();
            }
            else
            {
                AStarAlgorithm v = new AStarAlgorithm();
                v.Solve(_cells, this);
            }
        }
        
        public void ClearField()
        {
            if (startCell) startCell.SetToFree();
            if (endCell) endCell.SetToFree();
            for (int i = 0; i < _pathCells.Count; i++) _pathCells[i].SetToFree();
            for (int i = 0; i < _blocksCells.Count; i++) _blocksCells[i].SetToFree();
            _pathCells.Clear();
            _blocksCells.Clear();
        }

        public void ClearPath()
        {
            for (int i = 0; i < _pathCells.Count; i++) _pathCells[i].SetToFree();
            _pathCells.Clear();
        }

        public void SetPathCell(int x, int y)
        {
            _cells[y][x].SetToPath();
            _pathCells.Add(_cells[y][x]);
        }

        public void SetBlockCell(int x, int y)
        {
            _blocksCells.Add(_cells[y][x]);
        }
        
        public void SetStartCell(int x, int y)
        {
            if (startCell != null)
                startCell.SetToFree();
            
            startCell = _cells[y][x];
        }
        
        public void SetEndCell(int x, int y)
        {
            if (endCell != null)
                endCell.SetToFree();
            
            endCell = _cells[y][x];
        }

        public void CantFindPath()
        {
            print("Durak!");
        }
    }
}