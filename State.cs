namespace Econotoy;

public class State
{
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Block> Blocks { get; } = new();
    private int blockIX = 1;
    
    public void Tick(TimeSpan elapsed)
    {
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
                Blocks[i] = block with { X = block.X + block.XVelocity, XVelocity = 0 };
            }
        }
    }

    public void AddBlock()
    {
        Blocks.Add(new Block(Width/2, Height/2, 100, 100, blockIX++));
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