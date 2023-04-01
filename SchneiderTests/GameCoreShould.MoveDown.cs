namespace schneider
{
    using FluentAssertions;

    using Xunit;

    public partial class GameCoreShould
    {
        [Fact]
        public void MoveDownAndBeSafe()
        {
            // Arrange
            const int height = 3;
            var sut = new GameCore(3, height, 0);
            sut.Start();

            // Act
            var result = sut.Down();

            // Assert
            result.Should().BeTrue();
            sut.PlayerY.Should().Be(height - 1);
            sut.PlayerX.Should().Be(1);
        }

        [Fact]
        public void MoveDownAndNotBeOutsideBoard()
        {
            // Arrange
            const int height = 3;
            var sut = new GameCore(3, height, 0);
            sut.Start();
            sut.Down();

            // Act
            var result = sut.Down(); // Stay at same row

            // Assert
            result.Should().BeTrue();
            sut.PlayerY.Should().Be(height - 1);
            sut.PlayerX.Should().Be(1);
        }

        [Fact]
        public void MoveDownOntoABomb()
        {
            // Arrange
            const int height = 3;
            var sut = new GameCore(3, height, 8);
            sut.Start();

            // Act
            var result = sut.Down();

            // Assert
            result.Should().BeFalse();
            sut.PlayerY.Should().Be(height - 1);
            sut.PlayerX.Should().Be(1);
            sut.Board[sut.PlayerY][sut.PlayerX].Should().Be('*');
        }
    }
}
