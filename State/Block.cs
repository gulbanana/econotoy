using Econotoy.Geometry;
namespace Econotoy.State;

public record Block(Point Location, int Width, int Height, int Number)
{
    public int X => (int)Math.Floor(Location.X);
    public int Y => (int)Math.Floor(Location.Y);
    public Point Velocity { get; set; }
}