using CaterpillarControlSystem.App.Models;

namespace CaterpillarControlSystem.tests;

public class SegmentTests
{
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetPropertiesCorrectly()
    {
        const int expectedX = 5;
        const int expectedY = 10;
        const char expectedType = 'H';


        var segment = new Segment(expectedX, expectedY, expectedType);


        Assert.Equal(expectedX, segment.X);
        Assert.Equal(expectedY, segment.Y);
        Assert.Equal(expectedType, segment.Type);
    }

    [Theory]
    [InlineData(0, 0, 'A')]
    [InlineData(-1, -1, 'B')]
    [InlineData(int.MaxValue, int.MaxValue, 'Z')]
    [InlineData(int.MinValue, int.MinValue, '#')]
    public void Constructor_WithEdgeCaseParameters_ShouldSetPropertiesCorrectly(int x, int y, char type)
    {
        var segment = new Segment(x, y, type);


        Assert.Equal(x, segment.X);
        Assert.Equal(y, segment.Y);
        Assert.Equal(type, segment.Type);
    }

    [Theory]
    [InlineData('A')]
    [InlineData('Z')]
    [InlineData('0')]
    [InlineData('#')]
    public void Type_Setter_ShouldUpdateTypeProperty(char newType)
    {
        var segment = new Segment(0, 0, 'X');


        segment.Type = newType;


        Assert.Equal(newType, segment.Type);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void X_Setter_ShouldUpdateXProperty(int newX)
    {
        var segment = new Segment(0, 0, 'X');


        segment.X = newX;


        Assert.Equal(newX, segment.X);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(-1)]
    [InlineData(int.MaxValue)]
    [InlineData(int.MinValue)]
    public void Y_Setter_ShouldUpdateYProperty(int newY)
    {
        var segment = new Segment(0, 0, 'X');


        segment.Y = newY;


        Assert.Equal(newY, segment.Y);
    }
}