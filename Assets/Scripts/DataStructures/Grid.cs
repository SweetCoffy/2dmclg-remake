using System.Collections.Generic;
using UnityEngine;
namespace Game.DataStructures
{
    public struct GridItem<T>
    {
        public int x;
        public int y;
        public T value;
    }
    public class Grid<T>
    {
        protected T[,] grid;
        public virtual T this[int x, int y]
        {
            get => this.grid[x, y];
            set => this.grid[x, y] = value;
        }
        public virtual T this[Vector2Int pos]
        {
            get => this[pos.x, pos.y];
            set => this[pos.x, pos.y] = value;
        }
        public bool InGrid(Vector2Int position)
        {
            return position.x < grid.GetLength(0) && position.y < grid.GetLength(1) && position.x >= 0 && position.y >= 0;
        }
        public Vector2Int ToGrid(Vector2 position)
        {
            return Vector2Int.RoundToInt(position);
        }
        public Grid(int w, int h)
        {
            grid = new T[w, h];
        }
    }
}