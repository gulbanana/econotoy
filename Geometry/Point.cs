using Econotoy.State;

namespace Econotoy.Geometry;

public struct Point
{
    public static Point Zero = new(0, 0);
    
    public int X;
    public int Y;

    public Point(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Point(double x, double y)
    {
        X = (int)Math.Floor(x);
        Y = (int)Math.Floor(y);
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
}