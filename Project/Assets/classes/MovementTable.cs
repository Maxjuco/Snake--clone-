using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.classes
{
    /*tableau de */
    class MovementTable
    {
        //the position of the cell in the scene 
        public Vector2[,] positionsTable;

        //the size of the table (square) (base = 17)
        public int size;
        //cell size (square cellSize x cellSize)
        public float cellSize;

        //to check if the cell is empty; (E = empty and F = full)
        public char[,] containTable;

        public Vector2 actualHeadPosition;

        //create a Singleton instance 
        public static MovementTable instance;

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogWarning("Two instance of MovementTable are in the scene !");
                return;
            }

            instance = this;
        }

        public MovementTable(int size, float cellSize)
        {
            this.size = size;
            this.cellSize = cellSize;
            this.positionsTable = new Vector2[size, size];
            this.containTable = new char[size, size];
            for(int y = 0; y<size; y++)
            {
                for (int x = 0; x<size; x++)
                {
                    positionsTable[x, y] = new Vector2(cellSize*x, cellSize*y);
                    containTable[x, y] = 'E';

                }
            }
        }

        public bool isEmpty(int x, int y)
        {
                return containTable[x, y] == 'E';
        }

        public Vector3 getPositionHead(ref int x, ref int y)
        {
            int rightX = x;
            int rightY = y;

            //out of bound horizontal : 
            if (x > size - 1)
            {
                //come back at the wester side of the board :
                rightX = 0;
                x = 0;
            }
            if (x < 0)
            {
                //come back at the easter side of the board :
                rightX = size-1;
                x = size - 1;
            }

            //out of bound vertical : 
            if (y > size - 1)
            {
                //come back at the southern side of the board :
                rightY = 0;
                y = 0;
            }
            if (y < 0)
            {
                //come back at the northern side of the board :
                rightY = size - 1;
                y = size - 1;
            }

            Vector3 pos = positionsTable[rightX, rightY];
            return new Vector3(positionsTable[rightX, rightY].x, positionsTable[rightX, rightY].y, 0);
            //return new Vector3((float) 5.2, (float)5.2, 0);

            /*TODO implement the attibut to put as full a cell and empty the last one;*/
        }



        public Vector3 getPosition(int x,  int y)
        {
            int rightX = x;
            int rightY = y;

            //out of bound horizontal : 
            if (x > size - 1)
            {
                //come back at the wester side of the board :
                rightX = 0;
                
            }
            if (x < 0)
            {
                //come back at the easter side of the board :
                rightX = size - 1;
                
            }

            //out of bound vertical : 
            if (y > size - 1)
            {
                //come back at the southern side of the board :
                rightY = 0;
                
            }
            if (y < 0)
            {
                //come back at the northern side of the board :
                rightY = size - 1;
                
            }

            return new Vector3(positionsTable[rightX, rightY].x, positionsTable[rightX, rightY].y, 0);

            /*TODO implement the attibut to put as full a cell and empty the last one;*/
        }


        /*function to change the state of a cell (empty/full)*/
        public void SetFull(bool isFull, int X, int Y)
        {
            //if we are filling the cell : 
            if (isFull)
            {
                containTable[X, Y] = 'F';
            }
            //if we set the cell empty : 
            else
            {
                containTable[X, Y] = 'E';
            }
            
        }
    }
}
