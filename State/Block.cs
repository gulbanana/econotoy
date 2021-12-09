namespace Econotoy.State;

public record Block(int X, int Y, int Width, int Height, int Number)
{
    public int XVelocity { get; set; }
}