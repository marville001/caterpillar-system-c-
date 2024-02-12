using CaterpillarControlSystem.App.Constants;
using CaterpillarControlSystem.App.Models;
using CaterpillarControlSystem.App.Services;

namespace CaterpillarControlSystem.App.Controllers;

public class GecaController
{
	private int _startX;
	private int _startY;
	private int _lastX;
	private int _lastY;
	private int _size;
	public Caterpillar _caterpillar;

	private List<int[]> _boosters = new List<int[]>{
		new int[] { 1, 1 },
		new int[] { 2, 2 },
		new int[] { 3, 3 },
		new int[] { 4, 4 },
		new int[] { 5, 5 }
	};
	private List<int[]> _spices = new List<int[]>{
		new int[] { 6, 6 },
		new int[] { 7, 7 },
		new int[] { 8, 8 },
		new int[] { 9, 9 },
		new int[] { 10, 10 }
	};
	private List<int[]> _obstacles = new List<int[]>{
		new int[] { 11, 11 },
		new int[] { 12, 12 },
		new int[] { 13, 13 },
		new int[] { 14, 14 },
		new int[] { 15, 15 }
	};

	private readonly LoggingService _logger = new LoggingService();
	public GecaController(int startX, int startY, int size)
	{
		_startX = startX;
		_startY = startY;
		_size = size;
		_caterpillar = new Caterpillar(_startX, _startY);
	}

	public void Run()
	{
		_logger.Log("Game started");

		while (true)
		{
			PaintRadarBoard();

			var command = Console.ReadLine();

			try
			{
				if (!string.IsNullOrEmpty(command))
				{
					HandleCommand(command.ToUpper());
				}

			}
			catch (Exception ex)
			{
				Console.WriteLine("Invalid command");
				_logger.Log(ex.Message, "ERROR");
			}
		}
	}

	public void PaintRadarBoard()
	{
		Console.Clear();
		DisplayRadarImage();
		DisplayMenu();
	}

	public void DisplayMenu()
	{
		Console.WriteLine("Enter command (U/D/L/R/G/S/Q):");
		Console.WriteLine("U - Move Up");
		Console.WriteLine("D - Move Down");
		Console.WriteLine("L - Move Left");
		Console.WriteLine("R - Move Right");
		Console.WriteLine("G - Grow Caterpillar");
		Console.WriteLine("S - Shrink Caterpillar");
		Console.WriteLine("Q - Quit");
		Console.WriteLine("Spices: " + _caterpillar.spices);
	}

	public void HandleCommand(string command)
	{
		switch (command)
		{
			case "U":
			case "D":
			case "L":
			case "R":
				MoveCaterpillar(command[0]);
				_logger.Log($"Caterpillar moved {command}");
				break;
			case "G":
				GrowingCaterpillar();
				_logger.Log("Caterpillar grew");
				break;
			case "S":
				ShrinkingCaterpillar();
				break;
			case "Q":
				_logger.Log("Game ended");
				Environment.Exit(0);
				break;
			default:
				_logger.Log("Invalid command", "ERROR");
				Console.WriteLine("Invalid command");
				break;
		}
	}

	#region Caterpillar Operations

	public void MoveCaterpillar(char direction, int index = -1, int x = 0, int y = 0)
	{
		if (!IsValidateDirection(direction))
		{
			throw new Exception("Invalid direction provided");
		}

		if (index != -1)
		{
			if (index == _caterpillar.Segments.Count)
			{
				_lastX = x;
				_lastY = y;

				return;
			}

			var segment = _caterpillar.Segments[index];

			var nextX = segment.X;
			var nextY = segment.Y;

			segment.X = x;
			segment.Y = y;

			MoveCaterpillar(direction, index + 1, nextX, nextY);
		}
		else
		{
			var head = _caterpillar.Segments.First();
			var headX = head.X;
			var headY = head.Y;

			var nextX = head.X;
			var nextY = head.Y;

			switch (direction)
			{
				case AppConstants.UP:
					headY--;
					break;
				case AppConstants.DOWN:
					headY++;
					break;
				case AppConstants.LEFT:
					headX--;
					break;
				case AppConstants.RIGHT:
					headX++;
					break;
			}

			head.X = headX;
			head.Y = headY;

			if (!IsAdjacent(head, _caterpillar.Segments[1]))
			{
				MoveCaterpillar(direction, _caterpillar.headIndex + 1, nextX, nextY);
			}
		}
	}

	private static bool IsAdjacent(Segment seg1, Segment seg2)
	{
		return Math.Abs(seg1.X - seg2.X) <= 1 && Math.Abs(seg1.Y - seg2.Y) <= 1;
	}

	public void ShrinkingCaterpillar()
	{
		if (_caterpillar.Segments.Count > 2)
		{
			_caterpillar.Segments.RemoveAt(_caterpillar.Segments.Count - 1);

			var tail = _caterpillar.Segments.Last();
			tail.Type = AppConstants.TAIL;
		}
	}

	public void GrowingCaterpillar()
	{
		Console.Clear();

		if (_caterpillar.Segments.Count < AppConstants.MAX_SEGMENTS)
		{
			var tail = _caterpillar.Segments.Last();
			tail.Type = AppConstants.BODY;

			_caterpillar.Segments.Add(new Segment(_lastX, _lastY, AppConstants.TAIL));
		}
	}

	#endregion

	public void DisplayRadarImage()
	{
		for (int i = 0; i < _size; i++)
		{
			for (int j = 0; j < _size; j++)
			{
				var segment = _caterpillar.GetSegment(j, i);
				var isBooster = _boosters.Exists(b => b[0] == j && b[1] == i);
				var isSpice = _spices.Exists(s => s[0] == j && s[1] == i);
				var isObstacle = _obstacles.Exists(o => o[0] == j && o[1] == i);

				if (segment != null)
				{
					if (isBooster)
					{
						var boosterIndex = _boosters.FindIndex(b => b[0] == j && b[1] == i);
						GrowingCaterpillar();
						_boosters.RemoveAt(boosterIndex);
						Console.Clear();
						j = i = 0;
						break;
					}
					else if (isSpice)
					{
						_caterpillar.spices++;
						var spiceIndex = _spices.FindIndex(s => s[0] == j && s[1] == i);
						_spices.RemoveAt(spiceIndex);
					}
					else if (isObstacle)
					{
						_logger.Log("Caterpillar hit an obstacle", "ERROR");
						Console.WriteLine("\nCaterpillar hit an obstacle!");
						Environment.Exit(0);
					}
					Console.Write(segment.Type);
				}
				else
				{
					if (isBooster)
					{
						Console.Write(AppConstants.BOOSTER);
					}
					else if (isSpice)
					{
						Console.Write(AppConstants.SPICE);
					}
					else if (isObstacle)
					{
						Console.Write(AppConstants.OBSTACLE);
					}
					else
					{
						Console.Write(AppConstants.EMPTYLAND);
					}
				}

			}
			Console.WriteLine();
		}

	}

	private bool IsValidateDirection(char direction)
	{
		if (!new char[]
			{
				AppConstants.UP,
				AppConstants.DOWN,
				AppConstants.LEFT,
				AppConstants.RIGHT
			}.Contains(direction))
		{
			return false;
		}

		if (
			(direction == AppConstants.UP && _caterpillar.Segments.First().Y == 0) ||
			(direction == AppConstants.DOWN && _caterpillar.Segments.First().Y == _size - 1) ||
			(direction == AppConstants.LEFT && _caterpillar.Segments.First().X == 0) ||
			(direction == AppConstants.RIGHT && _caterpillar.Segments.First().X == _size - 1)
		)
		{
			_logger.Log("Caterpillar hit the wall", "ERROR");
			return false;
		}


		return true;
	}
}
