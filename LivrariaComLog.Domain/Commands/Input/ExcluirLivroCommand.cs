using System.Text.Json.Serialization;
using Flunt.Notifications;
using LivrariaComLog.Infra.Interfaces.Commands;

namespace LivrariaComLog.Domain.Commands.Input
{
    public class ExcluirLivroCommand : Notifiable, ICommandPadrao
    {
        [JsonIgnore] public long Id { get; set; }

        public bool ValidarCommand()
        {
            if (string.IsNullOrWhiteSpace(Id.ToString()))
                AddNotification("Id", "ID é um campo obrigatório");
            if (Id < 0)
                AddNotification("Id", "Id deve ser maior que zero");

            return Valid;
        }
    }
}