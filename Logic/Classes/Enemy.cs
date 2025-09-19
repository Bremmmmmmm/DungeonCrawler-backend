using Interface.Dtos;

namespace Logic.Classes;

public class Enemy
{
    public string name { get; set; }
    public int id { get; set; }
    public string sprite { get; set; }

    public EnemyDto ToDto()
    {
        return new EnemyDto
        {
            name = name,
            id = id,
            sprite = sprite
        };
    }
}