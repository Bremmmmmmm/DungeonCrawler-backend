using Interface.Dtos;
using Interface.Interfaces.Dal;
using Npgsql; // <-- changed from Microsoft.Data.SqlClient

namespace DataAccess.Database;

public class ItemDal(string connectionString) : IItemDal
{
    public async Task<IEnumerable<ItemDto>> GetItemsByArea(int areaId)
    {
        var items = new List<ItemDto>();

        await using var connection = new NpgsqlConnection(connectionString); // <-- changed
        try
        {
            await connection.OpenAsync();

            var query = "SELECT * FROM Items WHERE @areaId = ANY(area_numbers);";

            await using var command = new NpgsqlCommand(query, connection); // <-- changed
            command.Parameters.AddWithValue("@areaId", areaId);

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                items.Add(new ItemDto
                {
                    name = reader.GetString(reader.GetOrdinal("item_name")),
                    displayName = reader.GetString(reader.GetOrdinal("display_name")),
                    dropChance = reader.GetInt32(reader.GetOrdinal("drop_chance")),
                });
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new ArgumentException("Something went wrong while getting items: " + e.Message);
        }
        finally
        {
            await connection.CloseAsync();
        }

        return items;
    }
}