using static Cliente.Application.Enums.LogerTypes;

namespace Cliente.Application.Common.Logger;

public class LogsRegister
{
    public LogerType Type { get; set; }
    public string Messages { get; set; }
}
