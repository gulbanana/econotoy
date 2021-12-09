using Econotoy.Geometry;
namespace Econotoy.State;

public record Block(int X, int Y, int Width, int Height, int Number)
{
    public Point Velocity { get; set; }
}