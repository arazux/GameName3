using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameName3.Pathing.FlowField
{
    public class HeatMap
    {
        public static float Diagonal = (float)Math.Sqrt(2);
        public static int HEATMAP_RADIUS = 32;
        public static Position TopLeftOffset = new Position(HEATMAP_RADIUS + 1, HEATMAP_RADIUS + 1);

        #region Public Members

        #endregion
        #region Private Members
        // Current origin (Map middle used as anchor)
        Position Origin;
        // Reference to the map
        GameMap Map;
        // HeatMap internal grids
        float[,] Grid;
        bool[,] GridVisits;
        // Queue for the wavefront traversal
        Queue<Tuple<Position, float>> GridQueue;
        #endregion

        #region Public Methods

        public HeatMap(GameMap map)
        {
            Map = map;
            Grid = new float[HEATMAP_RADIUS*2 + 1, HEATMAP_RADIUS*2 + 1];
            GridVisits = new bool[HEATMAP_RADIUS*2 + 1, HEATMAP_RADIUS*2 + 1];
            GridQueue = new Queue<Tuple<Position, float>>();
        }

        public bool Rebuild(Position origin)
        {
            Origin = origin;
            // Initialize the grids
            Array.Clear(Grid, 0, Grid.Length);
            Array.Clear(GridVisits, 0, GridVisits.Length);
            // If the destination is unreachable we immediately quit
            // Possibly try to find a walkable destination nearby and path to it?
            if (!Map[origin].walkable)
                return false;

            // Create a queue of tiles to process
            GridQueue.Clear();
            GridQueue.Enqueue(new Tuple<Position, float>(Origin, 0));
            while (GridQueue.Count > 0)
            {
                var tuple = GridQueue.Dequeue();
                var pos = tuple.Item1;
                if (WasVisited(pos))
                    continue;
                var cost = tuple.Item2;
                var tile = Map[pos];

                // Process all neighbours of this tile, adding to queue as needed.
                ProcessTile(tile.North, cost + 1);
                ProcessTile(tile.South, cost + 1);
                ProcessTile(tile.West, cost + 1);
                ProcessTile(tile.East, cost + 1);
                ProcessTile(tile.NorthWest, cost + Diagonal);
                ProcessTile(tile.NorthEast, cost + Diagonal);
                ProcessTile(tile.SouthWest, cost + Diagonal);
                ProcessTile(tile.SouthEast, cost + Diagonal);

                // Mark the current tile as visited:
                Visit(pos);
            }

            return true;
        }

        #endregion

        #region Private Methods
        private void Visit(Position pos)
        {

            GridVisits[pos.x, pos.y] = true;
        }

        private bool WasVisited(Position pos)
        {
            return GridVisits[pos.x, pos.y];
        }

        // Takes a tile and either marks it as non-viable,
        // or assigns it the total cost and queues it up.
        private void ProcessTile(Position pos, float cost)
        {
            if (WasVisited(pos))
                return;

            if (!Map[pos].walkable)
            {
                Grid[pos.x, pos.y] = -1;
            }
            else
            {
                GridQueue.Enqueue(new Tuple<Position, float>(pos, cost));
                Grid[pos.x, pos.y] = cost;
            }
        }
        #endregion
    }
}
