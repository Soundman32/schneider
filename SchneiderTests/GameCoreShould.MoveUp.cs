namespace schneider
{
    using FluentAssertions;

    using Xunit;

    public partial class GameCoreShould
    {
        [Fact]
        public void MoveUpAndBeSafe()
        {
            // Arrange
            var sut = new GameCore(3, 3, 0);
            sut.Start();

            // Act
            var result = sut.Up();

            // Assert
            result.Should().BeTrue();
            sut.PlayerY.Should().Be(0);
        }

        [Fact]
        public void MoveUpAndNotBeOutsideBoard()
        {
            // Arrange
            var sut = new GameCore(3, 3, 0);
            sut.Start();
            sut.Up();

            // Act
            var result = sut.Up();

            // Assert
            result.Should().BeTrue();
            sut.PlayerY.Should().Be(0);
            sut.PlayerX.Should().Be(1);
        }

        [Fact]
        public void MoveUpOntoABomb()
        {
            // Arrange
            var sut = new GameCore(3, 3, 8);
            sut.Start();

            // Act
            var result = sut.Up();

            // Assert
            result.Should().BeFalse();
            sut.PlayerY.Should().Be(0);
            sut.PlayerX.Should().Be(1);
            sut.Board[sut.PlayerY][sut.PlayerX].Should().Be('*');
        }
    }
}
