using Cliente.Application.Configurations;

namespace Cliente.Aplication.Configurations;

public class ConfigHelper
{
    public static ConfigSqlServer? ConfigSqlServer { get; set; }
    public static ConfiLoggerFile? ConfiLoggerFile { get; set; }
    public static ConfigFormatos? ConfigFormatos { get; set; }
}
