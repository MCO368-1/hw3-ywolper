using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife
{
    class Board
    {
        private Cell[,] CELLS;

        public Board()
        {
            //Default size to fit all 5 scenarios
            const int size = 20;

            CELLS = new Cell[size, size];

            //Initiate board Cells
            fillBoard(size);
        }

        private void fillBoard(int size)
        {

            for (var i = 0; i < size; i++)
                for (var j = 0; j < size; j++)
                    CELLS[i, j] = new Cell();
        }

        public Board SetBoard(BeginningLayout layout)
        {
            switch (layout)
            {
                case BeginningLayout.Blinker:
                    SetBlinker();
                    return this;

                case BeginningLayout.Toad:
                    SetToad();
                    return this;

                case BeginningLayout.Beacon:
                    SetBeacon();
                    return this;

                case BeginningLayout.Pulsar:
                    SetPulsar();
                    return this;

                case BeginningLayout.Pentadecathlon:
                    SetPentadecathlon();
                    return this;

                default: throw new InvalidEnumArgumentException();
            }
        }


        private void SetBlinker()
        {
            CELLS[9, 9].IsAlive = true;
            CELLS[9, 10].IsAlive = true;
            CELLS[9, 11].IsAlive = true;
        }

        private void SetToad()
        {
            CELLS[9, 9].IsAlive = true;
            CELLS[9, 10].IsAlive = true;
            CELLS[9, 11].IsAlive = true;
            CELLS[10, 8].IsAlive = true;
            CELLS[10, 9].IsAlive = true;
            CELLS[10, 10].IsAlive = true;
        }

        private void SetBeacon()
        {
            CELLS[9, 8].IsAlive = true;
            CELLS[9, 9].IsAlive = true;
            CELLS[10, 8].IsAlive = true;
            CELLS[10, 9].IsAlive = true;
            CELLS[11, 10].IsAlive = true;
            CELLS[11, 11].IsAlive = true;
            CELLS[12, 10].IsAlive = true;
            CELLS[12, 11].IsAlive = true;
        }

        private void SetPulsar()
        {
            CELLS[4, 5].IsAlive = true;
            CELLS[4, 6].IsAlive = true;
            CELLS[4, 7].IsAlive = true;
            CELLS[4, 11].IsAlive = true;
            CELLS[4, 12].IsAlive = true;
            CELLS[4, 13].IsAlive = true;
            CELLS[6, 3].IsAlive = true;
            CELLS[6, 8].IsAlive = true;
            CELLS[6, 10].IsAlive = true;
            CELLS[6, 15].IsAlive = true;
            CELLS[7, 3].IsAlive = true;
            CELLS[7, 8].IsAlive = true;
            CELLS[7, 10].IsAlive = true;
            CELLS[7, 15].IsAlive = true;
            CELLS[8, 3].IsAlive = true;
            CELLS[8, 8].IsAlive = true;
            CELLS[8, 10].IsAlive = true;
            CELLS[8, 15].IsAlive = true;
            CELLS[9, 5].IsAlive = true;
            CELLS[9, 6].IsAlive = true;
            CELLS[9, 7].IsAlive = true;
            CELLS[9, 11].IsAlive = true;
            CELLS[9, 12].IsAlive = true;
            CELLS[9, 13].IsAlive = true;
            CELLS[11, 5].IsAlive = true;
            CELLS[11, 6].IsAlive = true;
            CELLS[11, 7].IsAlive = true;
            CELLS[11, 11].IsAlive = true;
            CELLS[11, 12].IsAlive = true;
            CELLS[11, 13].IsAlive = true;
            CELLS[12, 3].IsAlive = true;
            CELLS[12, 8].IsAlive = true;
            CELLS[12, 10].IsAlive = true;
            CELLS[12, 15].IsAlive = true;
            CELLS[13, 3].IsAlive = true;
            CELLS[13, 8].IsAlive = true;
            CELLS[13, 10].IsAlive = true;
            CELLS[13, 15].IsAlive = true;
            CELLS[14, 3].IsAlive = true;
            CELLS[14, 8].IsAlive = true;
            CELLS[14, 10].IsAlive = true;
            CELLS[14, 15].IsAlive = true;
            CELLS[16, 5].IsAlive = true;
            CELLS[16, 6].IsAlive = true;
            CELLS[16, 7].IsAlive = true;
            CELLS[16, 11].IsAlive = true;
            CELLS[16, 12].IsAlive = true;
            CELLS[16, 13].IsAlive = true;
        }

        private void SetPentadecathlon()
        {
            CELLS[8, 7].IsAlive = true;
            CELLS[8, 12].IsAlive = true;
            CELLS[9, 5].IsAlive = true;
            CELLS[9, 6].IsAlive = true;
            CELLS[9, 8].IsAlive = true;
            CELLS[9, 9].IsAlive = true;
            CELLS[9, 10].IsAlive = true;
            CELLS[9, 11].IsAlive = true;
            CELLS[9, 13].IsAlive = true;
            CELLS[9, 14].IsAlive = true;
            CELLS[10, 7].IsAlive = true;
            CELLS[10, 12].IsAlive = true;
        }

        public Board NextState()
        {
            var newBoard = new Board();
            newBoard.fillBoard(20);

            var nineCellSum = 0;
            for (var i = 0; i < CELLS.GetLength(0); i++)
                for (var j = 0; j < CELLS.GetLength(0); j++)
                {
                    nineCellSum = GetNineCellSum(i, j);
                    if (nineCellSum == 3)
                    {
                        newBoard.CELLS[i, j].IsAlive = true;
                    }
                    else if (nineCellSum == 4)
                    {
                        newBoard.CELLS[i,j] = this.CELLS[i, j];
                    }
                    else
                    {  
                        newBoard.CELLS[i, j].IsAlive = false;
                    }
                }
            this.CELLS = newBoard.CELLS;
            return newBoard;
        }

        private int GetNineCellSum(int row, int column)
        {
            var count = 0;
            var totalRows = CELLS.GetLength(0);
            var totalColumns = CELLS.GetLength(1);

            for (var i = row - 1; i <= row + 1; i++)
                for (var j = column - 1; j <= column + 1; j++)
                {
                    var rowIndex = (i + totalRows) % totalRows;
                    var columnIndex = (j + totalColumns) % totalColumns;

                    if (CELLS[rowIndex, columnIndex].IsAlive)
                        count++;
                }
            return count;
        }

        public override string ToString()
        {
            var returnString = new StringBuilder();

            for (var i = 0; i < CELLS.GetLength(0); i++)
            {
                for (var j = 0; j < CELLS.GetLength(0); j++)
                {
                    returnString.Append(CELLS[i, j].IsAlive ? 'O' : '-').Append(" ");
                }
                returnString.Append("\n");
            }
            return returnString.ToString();
        }
    }

    class Cell
    {
        public Cell(bool alive = false)
        {
            IsAlive = alive;
        }

        public bool IsAlive { get; set; }
    }
}

enum BeginningLayout
{
    Blinker = 0,
    Toad = 1,
    Beacon = 2,
    Pulsar = 3,
    Pentadecathlon = 4
}
