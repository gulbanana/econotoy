using Econotoy.Geometry;

namespace Econotoy.State;

public struct Drag
{
    public int Dragged;
    public Point Last;

    public Drag(int dragged, Point start)
    {
        Dragged = dragged;
        Last = start;
    }

    public Drag At(Point location) => new(Dragged, location);
}