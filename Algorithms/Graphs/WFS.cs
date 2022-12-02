using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs
{
    public class WFS
    {
        private char[,] map;
        private Vertex[,] graph;
        private int playerX;
        private int playerY;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        private void BuildGraph(char[,] map, int width, int height)
        {
            this.map = map;
            graph = new Vertex[height, width];

            // Build the graph. Nodes are the characters. Edges go up, down, left, right.
            // Walls are not being added to the graph so we start at i,j = 1 and go until i,j = end-1
            // If walls are not put in as neighbors, they are still doing their job by not allowing
            // traversal past them.
            for (int i = 1; i < height - 1; i++)
            {
                for (int j = 1; j < width - 1; j++)
                {
                    if (map[i,j] == 'P')
                    {
                        playerX = i;
                        playerY = j;
                    }

                    // Gather all of the neighbors
                    List<Vertex> neighbors = new List<Vertex>();
                    AddNeighbors(neighbors, i, j + 1);
                    AddNeighbors(neighbors, i, j - 1);
                    AddNeighbors(neighbors, i + 1, j);
                    AddNeighbors(neighbors, i - 1, j);

                    // If the current vertex hasn't been created yet, do so. Otherwise update its neighbors. 
                    if (graph[i,j] == null)
                    {
                        Vertex v = new Vertex(map[i,j], false, neighbors);
                        graph[i, j] = v;
                    }
                    else
                    {
                        Vertex curr = graph[i, j];
                        curr.neighbors = neighbors;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="map"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public int GetMaxGold(char[,] map, int width, int height)
        {
            BuildGraph(map, width, height);
            
            return WhateverFirstSearch(graph[playerX,playerY]);
        }

        private int WhateverFirstSearch(Vertex start)
        {
            int gold = 0;
            Stack<Vertex> bag = new Stack<Vertex>();
            Vertex v;
            bool fear = false;
            bag.Push(start);

            while(bag.Count != 0)
            {
                v = bag.Pop();
                v.marked = true;

                if (v.val == 'G')
                {
                    gold++;
                }
                fear = false;
                foreach (Vertex u in v.neighbors)
                {
                    if (u.val == 'T')
                        fear = true;
                }
                foreach(Vertex u in v.neighbors)
                {
                    if (!fear && !u.marked)
                        bag.Push(u);
                }
            }
            return gold;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="neighbors"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void AddNeighbors(List<Vertex> neighbors, int x, int y)
        {
            if (map[x,y] == '#')
                return;
            else if(graph[x,y] == null)
            {
                graph[x, y] = new Vertex(map[x, y], false, new List<Vertex>());
                neighbors.Add(graph[x, y]);
            }
            else
                neighbors.Add(graph[x,y]);
        }

        /// <summary>
        /// 
        /// </summary>
        private class Vertex
        {
            public Char val;
            public bool marked;
            public List<Vertex> neighbors;

            public Vertex(char value, bool marked, List<Vertex> neighbors)
            {
                val = value;
                this.marked = marked;
                this.neighbors = neighbors;
            }
        }
            
    }
}
