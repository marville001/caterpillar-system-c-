
using CaterpillarControlSystem.App.Constants;

namespace CaterpillarControlSystem.App.Models;

public class Caterpillar
{
	public List<Segment> Segments { get; set; } = new List<Segment>();
	public int headIndex { get; set; }
	public int tailIndex { get; set; }
	public int spices { get; set; }

	public Caterpillar(int startX, int startY)
	{
		Segments.Add(new Segment(startX, startY, AppConstants.HEAD));
		Segments.Add(new Segment(startX, startY, AppConstants.TAIL));
		headIndex = 0;
		tailIndex = 1;
	}

	public Segment? GetSegment(int x, int y)
	{
		return Segments.FirstOrDefault(s => s.X == x && s.Y == y);
	}
}