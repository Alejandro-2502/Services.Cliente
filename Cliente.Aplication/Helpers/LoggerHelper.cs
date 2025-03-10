using static Cliente.Application.Enums.LogerTypes;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.Helpers;

public class LoggerHelper
{
    public static string GetLeyendaMessages(RegisterLogCommand request)
    {
        var dateTimeLog = $"{DateTime.Now:yyyy - MM - dd HH: mm: ss}";
        var messages = request.logsRegister.Type switch
        {
            LogerType.Information => $"INFORMATION: {dateTimeLog} - {request.logsRegister.Messages}",
            LogerType.Error => $"ERROR: {dateTimeLog} - {request.logsRegister.Messages}",
            LogerType.Warning => $"WARNING: {dateTimeLog} - {request.logsRegister.Messages}",
            _ => throw new NotImplementedException(),
        };

        return messages;
    }
}
