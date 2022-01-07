using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
public class Map
{

    public List<Squares> SquaresWithMine { get; private set; }
    public Squares[,] AllSquares { get; set; }
    public int MineNumber { get; set; }

    public Map(int squaresHorizontal, int squaresVertical, int mineNumber)
    {
        MineNumber = mineNumber;
        AllSquares = new Squares[squaresVertical, squaresHorizontal];
        SquaresWithMine = new List<Squares>(mineNumber);


        for (int i = 0; i < AllSquares.GetLength(0); i++)
        {
            for (int j = 0; j < AllSquares.GetLength(1); j++)
            {
                AllSquares[i, j] = new Squares()
                {
                    Location = new Point(j, i),
                    IsFlagged = false,
                    IsMine = false,
                    IsUncovered = false,
                    NumberOfAdjacentMines = 0
                };                   
            }
        }
    }

    public Squares Square(int x, int y)
    {
        return AllSquares[y, x];
    }

    // list of non-clicked and no mine squares For showing left squares
    // after all mines are opened
    public IEnumerable<Squares> NotOpenedSquares()
    {
        IEnumerable<Squares> notOpenedSquares;

        notOpenedSquares = AllSquares
                             .Cast<Squares>()
                               .Where(item => !item.IsMine & !item.IsUncovered);

        return notOpenedSquares;
    }

    // create a mine-free region 3X3 or 2X2 at corners for first click event
    IEnumerable<Squares> MineFreeRegion(Point firstClickedSquare)
    {
        int x = firstClickedSquare.X;
        int y = firstClickedSquare.Y;

        List<Squares> neighborhoods = NeighborhoodCells(firstClickedSquare).ToList();
        neighborhoods.Add(AllSquares[y, x]); 

        return neighborhoods;
    }

    // getting list of adjacent neighborhood squares 
    IEnumerable<Squares> NeighborhoodCells(Point square)
    {
        var adjacentCells = new List<Squares>(); 
        int currentTop;
        int currentLeft;

        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                if (i != 0 | j != 0)
                {
                    currentLeft = square.X + j;
                    currentTop = square.Y + i;

                    if (currentLeft > -1 & currentLeft < AllSquares.GetLength(1))
                    {
                        if (currentTop > -1 & currentTop < AllSquares.GetLength(0))
                        {
                            Squares neighborhood = AllSquares[currentTop, currentLeft];
                            adjacentCells.Add(neighborhood); 
                        }
                    }
                }
            }
        }

        return adjacentCells;
    }

    // getting mine list in the order on being close to first clicked mine
    public IEnumerable<Squares> MinesFromCloseToAway(Point clicked)
    {
        IEnumerable<Squares> orderedMines;
        orderedMines = SquaresWithMine
                       .OrderBy(item => MineDistanceToExplosion(item.Location, clicked));

        return orderedMines;
    }

    // calculate mines distance to first clicked mine
    int MineDistanceToExplosion(Point mine, Point explosion)
    {
        int x = mine.X - explosion.X;
        int y = mine.Y - explosion.Y;
        int distance = x * x + y * y;

        return distance;
    }

    // if a square that has no mine neighborhood is clicked, then it and its adjacent cells
    // will be added to list for opening all once
    public void OpenSquaresRecursively(IList<Squares> squares, Squares clicked)
    {
        clicked.IsUncovered = true;
        squares.Add(clicked);
        IEnumerable<Squares> nghbrhds = NeighborhoodCells(clicked.Location);

        foreach (Squares neighborhoodSquare in nghbrhds)
        {
            if (neighborhoodSquare.IsUncovered | neighborhoodSquare.IsFlagged)
            {
                continue;
            }

            if (neighborhoodSquare.NumberOfAdjacentMines == 0)
            {
                OpenSquaresRecursively(squares, neighborhoodSquare);                    
            }
            else
            {
                neighborhoodSquare.IsUncovered = true;
                squares.Add(neighborhoodSquare);
            }
        }
    }

    public void OpenSquare(Squares square)
    {
        square.IsUncovered = true;
    }

    public void ChangeFlagState(Squares clicked)
    {
        clicked.IsFlagged = !clicked.IsFlagged;
    }

    // when first click is made, a mine free region that include first clicked square
    // in the middle is created and those cells is removed from the all cell list 
    // for placing mines in squares those left. after that this in line list is shuffled
    // with creating random numbers 
    public void LocateMinesRandomly(Point firstClickedSquare)
    {
        Random random = new Random();
        IEnumerable<Squares> mineFreeRegion = MineFreeRegion(firstClickedSquare);

        AllSquares
          .Cast<Squares>()
              .Where(point => !mineFreeRegion.Any(square => square.Location == point.Location))
                .OrderBy(item => random.Next())
                   .Take(MineNumber)
                      .ToList()
                         .ForEach(item =>
                         {
                             Squares mine = AllSquares[item.Location.Y, item.Location.X];
                             mine.IsMine = true;
                             SquaresWithMine.Add(mine);
                         });
    }

    // calculate number of adjacent mines that a no-mine square has 
    public void FindMinesAdjacent()
    {
        SquaresWithMine
          .SelectMany(item
            => NeighborhoodCells(item.Location))
              .ToList()
                .ForEach(item => item.NumberOfAdjacentMines++);
    }



}