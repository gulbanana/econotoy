using Econotoy.State;

namespace Econotoy.Geometry;

public struct Point
{
    public static Point Zero = new(0, 0);
    
    public double X { get; }
    public double Y { get; }
    public double Length => Math.Sqrt(X*X + Y*Y);

    public Point(double x, double y)
    {
        X = x;
        Y = y;
    }

    public bool Within(Block block)
    {
        return X >= block.X && 
               Y >= block.Y && 
               X <= (block.X + block.Width) &&
               Y <= (block.Y + block.Height);
    }

    public static Point operator +(Point a, Point b)
    {
        return new Point(a.X+b.X, a.Y+b.Y);
    }

    public static Point operator -(Point a, Point b)
    {
        return new Point(a.X-b.X, a.Y-b.Y);
    }

    public static Point operator *(Point a, double b)
    {
        return new Point(a.X*b, a.Y*b);
    }

    public static Point operator /(Point a, double b)
    {
        return new Point(a.X/b, a.Y/b);
    }

    public override string ToString()
    {
        return $"Point {{ X = {X}, Y = {Y}, Length = {Length} }}";
    }
}