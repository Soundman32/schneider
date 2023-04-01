namespace schneider
{
    using FluentAssertions;

    using Xunit;

    public partial class GameCoreShould
    {
        [Fact]
        public void Construct()
        {
            // Arrange
            const int expectedWidth = 1;
            const int expectedHeight = 2;
            const int expectedBombs = 3;

            // Act
            var sut = new GameCore(expectedWidth, expectedHeight, expectedBombs);

            // Assert

            sut.Width.Should().Be(expectedWidth);
            sut.Height.Should().Be(expectedHeight);
            sut.BombCount.Should().Be(expectedBombs);
        }

        [Fact]
        public void ThrowExceptionWhenTooHigh()
        {
            // Arrange

            // Act
            var result = Assert.Throws<ArgumentOutOfRangeException>(() => new GameCore(1, 100, 0));

            // Assert
            result.ParamName.Should().Be("height");
        }

        [Fact]
        public void ThrowExceptionWhenTooLow()
        {
            // Arrange

            // Act
            var result = Assert.Throws<ArgumentException>(() => new GameCore(10, 0, 0));

            // Assert
            result.ParamName.Should().Be("height");
        }

        [Fact]
        public void ThrowExceptionWhenTooNarrow()
        {
            // Arrange

            // Act
            var result = Assert.Throws<ArgumentException>(() => new GameCore(0, 10, 0));

            // Assert
            result.ParamName.Should().Be("width");
        }

        [Theory]
        [InlineData(2, 3, 1, 1)]
        [InlineData(10, 20, 5, 10)]
        public void InitialisePlayerPosition(int width, int height, int expectedPlayerX, int expectedPlayerY)
        {
            // Arrange
            var sut = new GameCore(width, height, 0);

            // Act
            sut.Start();

            // Assert
            sut.PlayerX.Should().Be(expectedPlayerX);
            sut.PlayerY.Should().Be(expectedPlayerY);
        }

        [Theory]
        [InlineData(10, 10, 10)]
        [InlineData(10, 10, 99)]
        public void InitialiseBombs(int width, int height, int bombs)
        {
            // Arrange
            var sut = new GameCore(width, height, bombs);

            // Act
            sut.Start();

            // Assert
            var bombCount = 0;
            for (int row = 0; row < height; row++)
            {
                for (int column = 0; column < width; column++)
                {
                    if (sut.Board[row][column] == 'B')
                    {
                        bombCount++;
                    }
                }
            }

            bombCount.Should().Be(bombs);
        }
    }
}
