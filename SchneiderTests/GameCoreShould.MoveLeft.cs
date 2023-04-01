namespace schneider
{
    using FluentAssertions;

    using Xunit;

    public partial class GameCoreShould
    {
        [Fact]
        public void MoveLeftAndBeSafe()
        {
            // Arrange
            var sut = new GameCore(3, 3, 0);
            sut.Start();

            // Act
            var result = sut.Left();

            // Assert
            result.Should().BeTrue();
            sut.PlayerX.Should().Be(0);
            sut.PlayerY.Should().Be(1);
        }

        [Fact]
        public void MoveLeftAndNotBeOutsideBoard()
        {
            // Arrange
            var sut = new GameCore(3, 3, 0);
            sut.Start();
            sut.Left();

            // Act
            var result = sut.Left(); // Stay at same position

            // Assert
            result.Should().BeTrue();
            sut.PlayerX.Should().Be(0);
        }

        [Fact]
        public void MoveLeftOntoABomb()
        {
            // Arrange
            var sut = new GameCore(3, 3, 8);
            sut.Start();

            // Act
            var result = sut.Left();

            // Assert
            result.Should().BeFalse();
            sut.PlayerX.Should().Be(0);
            sut.PlayerY.Should().Be(1);
            sut.Board[sut.PlayerY][sut.PlayerX].Should().Be('*');
        }
    }
}
