using System;
using UnityEngine;

namespace NuclearGames
{
    public enum TypeDrawing
    {
        DrawFree,
        DrawBlocks,
        DrawStart,
        DrawEnd,
    }
    
    public class GameManager : MonoBehaviour
    {
        [Header("Type of drawing")] public TypeDrawing typeDrawing;

        public void SetType(string type)
        {
            typeDrawing = (TypeDrawing)Enum.Parse(typeof(TypeDrawing), type);
        }
    }
}
