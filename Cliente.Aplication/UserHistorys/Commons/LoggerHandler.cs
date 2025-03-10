using Cliente.Aplication.Configurations;
using Cliente.Application.Common.Logger;
using Cliente.Application.Helpers;
using MediatR;
using static Cliente.Application.UserHistorys.Commons.LoggerHandler;

namespace Cliente.Application.UserHistorys.Commons;
public class LoggerHandler() : IRequestHandler<RegisterLogCommand, bool>
{
    public record RegisterLogCommand(LogsRegister logsRegister) : IRequest<bool>;
    public async Task<bool> Handle(RegisterLogCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var _logFilePath = ConfigHelper.ConfiLoggerFile!.LoggerFile;

            if (!File.Exists(_logFilePath)) 
                File.Create(_logFilePath).Dispose();
           
            var resultHelper = LoggerHelper.GetLeyendaMessages(request);

            File.AppendAllText(_logFilePath, resultHelper + Environment.NewLine);

            if (_logFilePath.Any())
                return true;

            return false;
        }
        catch (Exception ex)
        {
            throw new NotImplementedException();
        }
    }
}
