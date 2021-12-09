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
                Location = drag.Value.Last
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
            if (block.Velocity.X != 0 || block.Velocity.Y != 0)
            {
                if (drag.HasValue && drag.Value.Dragged == i)
                {
                    Blocks[i] = block with { Velocity = Point.Zero };
                }
                else
                {
                    Blocks[i] = block with 
                    { 
                        Location = block.Location + block.Velocity * elapsed.TotalSeconds,
                        Velocity = Point.Zero
                    };
                }
            }
        }
    }

    public void AddBlock()
    {
        Blocks.Add(new Block(new Point(Width/2, Height/2), 100, 100, blockIX++));
    }

    public void DragStart(Point cursor)
    {
        if (drag.HasValue)
        {
            Drop();
        }

        for (var i = Blocks.Count-1; i >= 0; i--)
        {
            var block = Blocks[i];
            if (cursor.Within(block))
            {
                Blocks.RemoveAt(i);
                Blocks.Add(block);
                drag = new Drag(Blocks.Count-1, cursor);
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

    private const double pushSpeed = 200.0;
    private void Collide(Block a, Block b)
    {
        var ax = 0.0;
        var ay = 0.0;
        var bx = 0.0;
        var by = 0.0;

        if (a.X <= b.X)
        {
            ax = -pushSpeed;
            bx = pushSpeed;
        }
        else
        {
            ax = pushSpeed;
            bx = -pushSpeed;
        }

        if (a.Y <= b.Y)
        {
            ay = -pushSpeed;
            by = pushSpeed;
        }
        else
        {
            ay = pushSpeed;
            by = -pushSpeed;
        }

        a.Velocity += new Point(ax, ay);
        b.Velocity += new Point(bx, by);
    }
}