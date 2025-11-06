namespace Interface.Dtos;

public class ItemDto
{
    public string name { get; set; }
    public string displayName { get; set; }
    public int dropChance { get; set; }
    public int[] areaNumbers { get; set; }
    public string sprite { get; set; }
}