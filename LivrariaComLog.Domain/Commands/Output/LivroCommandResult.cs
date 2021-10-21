using LivrariaComLog.Infra.Interfaces.Commands;

namespace LivrariaComLog.Domain.Commands.Output
{
    public class LivroCommandResult : ICommandResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public LivroCommandResult(bool success, string message, object data)
        {
            Success = success;
            Message = message;
            Data = data;
        }
    }
}