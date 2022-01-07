using System.Drawing;
public class Squares
{
    public int NumberOfAdjacentMines { get; set; }
    public bool IsMine { get; set; }
    public bool IsFlagged { get; set; }
    // show if square is left-clicked and opened
    public bool IsUncovered { get; set; }
    public Point Location { get ; set ; }    
}