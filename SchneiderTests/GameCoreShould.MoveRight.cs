namespace schneider
{
    using FluentAssertions;

    using Xunit;

    public partial class GameCoreShould
    {
        [Fact]
        public void MoveRightOntoABomb()
        {
            // Arrange
            const int width = 3;
            var sut = new GameCore(width, 3, 8);
            sut.Start();

            // Act
            var result = sut.Right();

            // Assert
            result.Should().BeFalse();
            sut.PlayerX.Should().Be(width-1);
            sut.PlayerY.Should().Be(1);
            sut.Board[sut.PlayerY][sut.PlayerX].Should().Be('*');
        }

        [Fact]
        public void MoveRightAndBeSafe()
        {
            // Arrange
            const int width = 3;
            var sut = new GameCore(width, 3, 0);
            sut.Start();

            // Act
            var result = sut.Right();

            // Assert
            result.Should().BeTrue();
            sut.PlayerX.Should().Be(width - 1);
            sut.PlayerY.Should().Be(1);
        }

        [Fact]
        public void MoveRightAndNotBeOutsideBoard()
        {
            // Arrange
            const int width = 3;
            var sut = new GameCore(width, 3, 0);
            sut.Start();
            sut.Right();

            // Act
            var result = sut.Right(); // Stay at same position

            // Assert
            result.Should().BeTrue();
            sut.PlayerX.Should().Be(width - 1);
            sut.PlayerY.Should().Be(1);
        }
    }
}
