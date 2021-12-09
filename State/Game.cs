using Econotoy.Geometry;

namespace Econotoy.State;

public class Game
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Block> Blocks { get; } = new();
    private int blockIX = 1;
    private Drag? drag;

    public void Tick(TimeSpan elapsed)
    {
        if (drag.HasValue)
        {
            Blocks[drag.Value.Dragged] = Blocks[drag.Value.Dragged] with
            {
                X = drag.Value.Last.X,
                Y = drag.Value.Last.Y
            };
        }

        for (var i = 0; i < Blocks.Count; i++)
        {
            var a = Blocks[i];
            for (var j = i+1; j < Blocks.Count; j++)
            {
                var b = Blocks[j];
                if (!object.ReferenceEquals(a, b) && Intersects(a, b))
                {
                    Collide(a, b);
                }
            }
        }

        for (var i = 0; i < Blocks.Count; i++)
        {
            var block = Blocks[i];
            if (block.XVelocity != 0)
            {
                if (drag.HasValue && drag.Value.Dragged == i)
                {
                    Blocks[i] = block with { XVelocity = 0 };
                }
                else
                {
                    Blocks[i] = block with { X = block.X + block.XVelocity, XVelocity = 0 };
                }
            }
        }
    }

    public void AddBlock()
    {
        Blocks.Add(new Block(Width/2, Height/2, 100, 100, blockIX++));
    }

    public void DragStart(Point cursor)
    {
        if (drag.HasValue)
        {
            Drop();
        }

        for (var i = Blocks.Count-1; i >= 0; i--)
        {
            if (cursor.Within(Blocks[i]))
            {
                drag = new Drag(i, cursor);
                return;
            }
        }
    }

    public void DragUpdate(Point cursor)
    {
        if (drag.HasValue)
        {
            drag = drag.Value.At(cursor);
        }
    }

    public void DragEnd(Point cursor)
    {
        if (drag.HasValue)
        {
            drag = drag.Value.At(cursor);
            Drop();
        } 
    }

    private void Drop()
    {
        drag = null;
    }

    private bool Intersects(Block a, Block b)
    {
        return (Math.Abs(a.X - b.X) * 2 < (a.Width + b.Width)) &&
               (Math.Abs(a.Y - b.Y) * 2 < (a.Height + b.Height));
    }

    private void Collide(Block a, Block b)
    {
        if (a.X <= b.X) // a is left
        {
            a.XVelocity-=2;
            b.XVelocity+=2;
        }
        else
        {
            a.XVelocity+=2;
            b.XVelocity-=2;
        }
    }
}