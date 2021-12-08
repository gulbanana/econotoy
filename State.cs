namespace Econotoy;

public class State
{
    public int Width { get; set; }
    public int Height { get; set; }
    private double time;
    public string Fill { get; set; } = "rgb(0, 0, 0)";
    
    public void Tick(TimeSpan elapsed)
    {
        time = time + elapsed.TotalSeconds;
        Fill = $"rgb(0, 0, {(time - Math.Floor(time))*255})";
    }
}