using Interface.Dtos;
using Logic.Classes;

namespace Tests.Classes;

[TestClass]
public class EnemyTests
{
    private EnemyDto _enemyDto = null!;

    [TestInitialize]
    public void Setup()
    {
        _enemyDto = new EnemyDto
        {
            name = "Goblin",
            id = 1,
            sprite = "goblin_sprite.png"
        };
    }

    [TestMethod]
    public void constructor_ShouldInitializePropertiesCorrectly()
    {
        // Arrange & Act
        var enemy = new Enemy
        {
            name = _enemyDto.name,
            id = _enemyDto.id,
            sprite = _enemyDto.sprite
        };

        // Assert
        Assert.AreEqual(_enemyDto.name, enemy.name);
        Assert.AreEqual(_enemyDto.id, enemy.id);
        Assert.AreEqual(_enemyDto.sprite, enemy.sprite);
    }

    [TestMethod]
    public void ToDto_ShouldReturnCorrectEnemyDto()
    {
        // Arrange
        var enemy = new Enemy
        {
            name = _enemyDto.name,
            id = _enemyDto.id,
            sprite = _enemyDto.sprite
        };
        // Act
        var dto = enemy.ToDto();
        // Assert
        Assert.AreEqual(_enemyDto.name, dto.name);
        Assert.AreEqual(_enemyDto.id, dto.id);
        Assert.AreEqual(_enemyDto.sprite, dto.sprite);
    }
}