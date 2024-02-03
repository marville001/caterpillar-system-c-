using Xunit;
using CaterpillarControlSystem.App.Controllers;
using CaterpillarControlSystem.App.Constants;
using CaterpillarControlSystem.App.Models;
using CaterpillarControlSystem.App.Services;

namespace CaterpillarControlSystem.tests;

public class GecaControlerTests
{


	[Theory]
	[InlineData(AppConstants.UP)]
	public void HandleCommand_ValidUpCommand_MovesCaterpillarUp(char direction)
	{
		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		gecaController.MoveCaterpillar(direction);
		Assert.Equal(AppConstants.START_Y-1, gecaController._caterpillar.Segments.First().Y);
		Assert.Equal(AppConstants.START_X, gecaController._caterpillar.Segments.First().X);
	}

	[Theory]
	[InlineData(AppConstants.DOWN)]
	public void HandleCommand_ValidUpCommand_MovesCaterpillarDown(char direction)
	{
		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		gecaController.MoveCaterpillar(direction);
		Assert.Equal(AppConstants.START_Y+1, gecaController._caterpillar.Segments.First().Y);
		Assert.Equal(AppConstants.START_X, gecaController._caterpillar.Segments.First().X);
	}

	[Theory]
	[InlineData(AppConstants.RIGHT)]
	public void HandleCommand_ValidUpCommand_MovesCaterpillarRight(char direction)
	{
		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		gecaController.MoveCaterpillar(direction);
		Assert.Equal(AppConstants.START_X+1, gecaController._caterpillar.Segments.First().X);
		Assert.Equal(AppConstants.START_Y, gecaController._caterpillar.Segments.First().Y);
	}

	[Theory]
	[InlineData(AppConstants.LEFT)]
	public void HandleCommand_ValidUpCommand_MovesCaterpillarLeft(char direction)
	{
		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		gecaController.MoveCaterpillar(direction);
		Assert.Equal(AppConstants.START_X-1, gecaController._caterpillar.Segments.First().X);
		Assert.Equal(AppConstants.START_Y, gecaController._caterpillar.Segments.First().Y);
	}

	[Fact]
	public void TestMove_InvalidDirection_ThrowsArgumentException()
	{
		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		Assert.Throws<Exception>(() => gecaController.MoveCaterpillar('N'));
	}

	[Fact]
	public void TestGrow_CaterpillarGrows()
	{
		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		gecaController.GrowingCaterpillar();
		Assert.Equal(3, gecaController._caterpillar.Segments.Count);
	}

	[Fact]
	public void TestGrow_CaterpillarAtMaxSize_DoesNotGrow()
	{

		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		for (int i = 0; i < AppConstants.MAX_SEGMENTS + 1; i++)
		{
			gecaController.GrowingCaterpillar();
		}

		Assert.Equal(AppConstants.MAX_SEGMENTS, gecaController._caterpillar.Segments.Count);
	}

	[Fact]
	public void TestShrink_CaterpillarShrinks()
	{
		var gecaController = new GecaController(AppConstants.START_X, AppConstants.START_Y, AppConstants.AREA_SIZE);
		gecaController.GrowingCaterpillar();
		gecaController.ShrinkingCaterpillar();
		Assert.Equal(2, gecaController._caterpillar.Segments.Count);
	}

}