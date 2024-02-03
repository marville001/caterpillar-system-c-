
namespace CaterpillarControlSystem.App.Models;

public class Segment
{
	public int X { get; set; }
	public int Y { get; set; }
	public char Type { get; set; }

	public Segment(int x, int y, char type)
	{
		X = x;
		Y = y;
		Type = type;
	}
}