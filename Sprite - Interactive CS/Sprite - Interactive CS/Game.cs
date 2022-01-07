using System.Collections.Generic;
using System.Drawing;
public enum Clicks
{
DefaultClick = 0,
LeftClick = 1,
RightClick = 2
}

public enum Actions
{
DoNothing = 0,
PutFlag,
RemoveFlag,
ExplodeAllMines,
OpenSquare,
OpenSquaresRecursively
}

public enum GameStatus
{
Default = 0,
NotFinished,
Won,
Lost
}
public class Game
{
bool firstClick;
bool clickedToMine;
public int WinningScore { get; private set; }
public int CurrentScore { get; private set; }
public Squares[,] Squares { get; private set; }
Map map;

public Game(int squaresHorizontal, int squaresVertical, int mineNumber)
{
    CurrentScore = 0;
    WinningScore = squaresHorizontal * squaresVertical - mineNumber;
    firstClick = false;
    clickedToMine = false;
    map = new Map(squaresHorizontal, squaresVertical, mineNumber);
    Squares = map.AllSquares;
}

public int NumberOfNotOpenedSafetySquare()
{
    return WinningScore - CurrentScore;
}

public Squares Square(int x, int y)
{
    return map.AllSquares[y, x];
}


public GameStatus GameSituation()
{
    if (CurrentScore == WinningScore)
    {
        return GameStatus.Won;
    }
    else if (clickedToMine)
    {
        return GameStatus.Lost;
    }
    else
    {
        return GameStatus.NotFinished;
    }
}

public Actions ClickSquare(Clicks mouseClick, Squares clicked)
{
    // running once when map is first time clicked by left click during game 
    if (!firstClick & mouseClick == Clicks.LeftClick)
    {
        StartGame(clicked.Location);
        firstClick = !firstClick;
    }

    if (mouseClick == Clicks.RightClick)
    {
        Actions result;

        // if a square ic left-clicked before then right click has no effect
        if (clicked.IsUncovered)
        {
            return Actions.DoNothing;
        }

        // if square has flag on it then it will be removed
        // else flag will be placed on it
        if (clicked.IsFlagged)
        {
            result = Actions.RemoveFlag;
        }
        else 
        {
            result = Actions.PutFlag;
        }


        ChangeFlagState( clicked);

        return result;

    }

    if (mouseClick == Clicks.LeftClick)
    {
        // if a square that has flag on it received left-click 
        // there will be no effect
        if (clicked.IsFlagged)
        {
            return Actions.DoNothing;
        }

        if (clicked.IsMine)
        {
            clickedToMine = true;
            return Actions.ExplodeAllMines;
        }
        // if a square that has mine neighborhood is clicked, 
        // a number of mines will be wrote on it 
        if (clicked.NumberOfAdjacentMines > 0)
        {
            OpenSquare(clicked);
            return Actions.OpenSquare;
        }
        // if a square that has no mine neighborhood is clicked, 
        // then its neighborhodo and itself will be opened at once
        else
        {
            return Actions.OpenSquaresRecursively;
        }
    }

    return Actions.DoNothing;
}


public IEnumerable<Squares> NotOpenedSquare()
{
    IEnumerable<Squares> notOpenedSquares = map.NotOpenedSquares();

    return notOpenedSquares;
}

// getting list of mines for showing where each all of them
//  if a mine is clicked by the user
public IEnumerable<Squares> MinesToShow()
{
    IEnumerable<Squares> minesToShow = map.SquaresWithMine;

    return minesToShow;
}

// getting list of in line mines for exploding all if a mine is clicked by the user
public IEnumerable<Squares> MinesToExplode(Squares clicked)
{
    IEnumerable<Squares> minesToExplode = map.MinesFromCloseToAway(clicked.Location);

    return minesToExplode; 
}

// if a square that has no mine neighborhood is clicked then 
// its all neighborhoods and itself is added a list for opening all in once
// and number of those added to score
public IEnumerable<Squares> SquaresWillBeOpened(Squares clicked)
{           
    var squaresWillBeOpened = new List<Squares>();
    map.OpenSquaresRecursively(squaresWillBeOpened, clicked);
    CurrentScore += squaresWillBeOpened.Count;

    return squaresWillBeOpened;
}

public void StartGame(Point firstClickedSquare)
{
    map.LocateMinesRandomly(firstClickedSquare);
    map.FindMinesAdjacent();
}

public void OpenSquare(Squares square)
{
    map.OpenSquare(square);
    CurrentScore++;
}

void ChangeFlagState(Squares clicked)
{
    map.ChangeFlagState(clicked);
}
}
