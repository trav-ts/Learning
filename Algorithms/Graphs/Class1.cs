using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WFS
    {
        private char[][] map;

        public int WhateverFirstSearch(char[][] map, int width, int height)
        {
            this.map = map;
            Dictionary<char, Vertex> graph = new Dictionary<char, Vertex>();

            int gold = 0;

            // Build the graph. Nodes are the characters. Edges go up, down, left, right.
            // Walls are not being added to the graph so we start at i,j = 1 and go until i,j = end-1
            // If walls are not put in as neighbors, they are still doing their job by not allowing
            // traversal past them.
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    List<char> neighbors = new List<char>();
                    AddNodeToGraph(neighbors, i, j + 1);
                    AddNodeToGraph(neighbors, i, j - 1);
                    AddNodeToGraph(neighbors, i + 1, j);
                    AddNodeToGraph(neighbors, i - 1, j);
                    Vertex v = new Vertex(map[i][j], false, neighbors);
                    graph.Add(map[i][j], v);
                }
            }

            return 0;
        }

        private void AddNodeToGraph(List<char> neighbors, int x, int y)
        {
            if (map[x][y] == '#')
                return;
            else
                neighbors.Add(map[x][y]);
        }

        private class Vertex
        {
            Char val;
            bool marked;
            List<char> neighbors;

            public Vertex(char value, bool marked, List<char> neighbors)
            {
                val = value;
                this.marked = marked;
                this.neighbors = neighbors;
            }
        }
            
    }
}
