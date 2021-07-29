using UnityEngine;

namespace NuclearGames
{
    public enum CellType
    {
        Free,
        Block,
        Start,
        End,
        Path
    }
    
    public class Cell : MonoBehaviour
    {
        [Header("Cell position")] 
        public int x, y;

        [Header("Cell type")]
        public CellType cellType;
        
        [Header("Type cells colors")] 
        public Color freeCellColor = Color.white;
        public Color blockCellColor = Color.black;
        public Color startCellColor = Color.yellow;
        public Color endCellColor = Color.red;
        public Color pathCellColor = Color.green;

        [Header("Field")] 
        public Field field;

        private GameManager _gameManager;
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnMouseDown()
        {
            switch (_gameManager.typeDrawing)
            {
                case TypeDrawing.DrawFree:
                    SetToFree();
                    break;
                case TypeDrawing.DrawBlocks:
                    SetToBlock();
                    field.SetBlockCell(x, y);
                    break;
                case TypeDrawing.DrawEnd:
                    SetToEnd();
                    field.SetEndCell(x, y);
                    break;
                case TypeDrawing.DrawStart:
                    SetToStart();
                    field.SetStartCell(x, y);
                    break;
            }
        }

        public void SetToFree()
        {
            cellType = CellType.Free;
            _spriteRenderer.color = freeCellColor;
        }

        public void SetToPath()
        {
            cellType = CellType.Path;
            _spriteRenderer.color = pathCellColor;
        }

        private void SetToBlock()
        {
            cellType = CellType.Block;
            _spriteRenderer.color = blockCellColor;
        }

        private void SetToStart()
        {
            cellType = CellType.Start;
            _spriteRenderer.color = startCellColor;
        }

        private void SetToEnd()
        {
            cellType = CellType.End;
            _spriteRenderer.color = endCellColor;
        }
    }
}
