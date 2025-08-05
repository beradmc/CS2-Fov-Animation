using MySqlConnector;
using Fov.Models;

namespace Fov.Services;

public class DatabaseService
{
    private static MySqlConnectionStringBuilder? _connectionBuilder;

    public static void InitializeConnection(Config config)
    {
        _connectionBuilder = new MySqlConnectionStringBuilder
        {
            Server = config.DatabaseHost,
            Database = config.DatabaseName,
            UserID = config.DatabaseUser,
            Password = config.DatabasePassword,
            Port = 3306,
            Pooling = true,
            MaximumPoolSize = 10,
            MinimumPoolSize = 2
        };
    }

    private static string GetConnectionString()
    {
        return _connectionBuilder?.ConnectionString ?? throw new InvalidOperationException("Database connection not initialized");
    }

    public static void InitializeDatabase()
    {
        try
        {
            using var connection = new MySqlConnection(GetConnectionString());
            connection.Open();
            using var command = new MySqlCommand(@"
                CREATE TABLE IF NOT EXISTS player_fov (
                    steam_id BIGINT UNSIGNED PRIMARY KEY,
                    fov INT NOT NULL,
                    last_updated TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
                )", connection);
            command.ExecuteNonQuery();
        }
        catch (Exception)
        {
        }
    }

    public static void SavePlayerFov(ulong steamId, int fov)
    {
        try
        {
            using var connection = new MySqlConnection(GetConnectionString());
            connection.Open();
            using var command = new MySqlCommand(@"
                INSERT INTO player_fov (steam_id, fov)
                VALUES (@steamId, @fov)
                ON DUPLICATE KEY UPDATE fov = @fov", connection);
            command.Parameters.AddWithValue("@steamId", steamId);
            command.Parameters.AddWithValue("@fov", fov);
            command.ExecuteNonQuery();
        }
        catch (Exception)
        {
        }
    }

    public static int? LoadPlayerFov(ulong steamId)
    {
        try
        {
            using var connection = new MySqlConnection(GetConnectionString());
            connection.Open();
            using var command = new MySqlCommand("SELECT fov FROM player_fov WHERE steam_id = @steamId", connection);
            command.Parameters.AddWithValue("@steamId", steamId);
            var result = command.ExecuteScalar();
            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            return null;
        }
        catch (Exception)
        {
            return null;
        }
    }
} 