// HITTER PROGRAM

// TonyTroeff implementation is used in the calculation of islands being hitted
// using Number of islands 
// expected input:  
//  ["1","1","1","1","0"],
//  ["1","1","0","1","0"],
//  ["1","1","0","0","0"],
//  ["0","0","0","0","0"]

using System;
using System.Collections.Generic;

class AlgorithmIMPL
{
    char[,] hpos;

    public AlgorithmIMPL()
    {
        hpos = new char[16, 16];
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                hpos[i, j] = '0';
            }
        }
    }

    public int NumIslands(char[,] grid)
    {
        bool[,] visited = new bool[grid.GetLength(0), grid.GetLength(1)];

        int ans = 0;
        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid.GetLength(1); j++)
            {
                if (visited[i, j]) continue;

                if (grid[i, j] == '1') { ans++; VisitAdjacent(i, j); }
            }
        }

        return ans;

        void VisitAdjacent(int row, int col)
        {
            if (row < 0 || row >= grid.GetLength(0) || col < 0 || col >= grid.GetLength(1)) return;
            if (visited[row, col] || grid[row, col] == '0') return;

            visited[row, col] = true;
            VisitAdjacent(row - 1, col);
            VisitAdjacent(row + 1, col);
            VisitAdjacent(row, col + 1);
            VisitAdjacent(row, col - 1);
        }
    }

    public char[,] IslandInterpreter(string hexHit)
    {
	    hexHit = hexHit.ToUpper();
	    int[] hxrec = new int[2];
	    Dictionary<char, int> mphex = new Dictionary<char, int>{
		    {'A', 10},
		    {'B', 11},
            {'C', 12},
            {'D', 13},
            {'E', 14},
            {'F', 15}
        };
        // hexHit = FF
        for(int i = 0; i<2 ; i++) {
            bool hxflag = false;
	        foreach (KeyValuePair<char, int> kvp in mphex){
                if(kvp.Key == hexHit[i]){
		 	        hxrec[i] += kvp.Value;
                    hxflag = true;
                    continue;
                }
            }
            if(!hxflag)
                hxrec[i] += Int16.Parse(hexHit[i].ToString());
        }

	    hpos[hxrec[1],hxrec[0]] = '1';
	    return hpos;
    }

    public void PrintGrid()
    {
        for (int i = 0; i < 16; i++)
        {
            for (int j = 0; j < 16; j++)
            {
                Console.Write(hpos[i, j]);
            }
            Console.WriteLine();
        }
    }

}
