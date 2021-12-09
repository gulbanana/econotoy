using Econotoy.Geometry;

namespace Econotoy.State;

public struct Drag
{
    public int Dragged;
    public Point Offset;
    public Point Last;

    public Drag(int dragged, Point offset, Point start)
    {
        Dragged = dragged;
        Offset = offset;
        Last = start;
    }

    public Drag At(Point location) => new(Dragged, Offset, location);
}