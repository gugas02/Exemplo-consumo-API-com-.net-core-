using API.Consumption.test.Domain.Command;
using API.Consumption.test.Domain.Command.Advertisement;
using API.Consumption.test.Domain.Entity;
using API.Consumption.test.Domain.Repositories;
using API.Consumption.test.Domain.Shared;
using Flunt.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.Domain.Handlers;

namespace API.Consumption.test.Domain.Handlers
{
    public class AdvertisementHandler : Notifiable,
        IHandler<EditAdvertisementCommand>,
        IHandler<CreateAdvertisementCommand>,
        IHandler<DeleteAdvertisementCommand>
    {
        private readonly IAdvertisementRepository _repository;

        public AdvertisementHandler(IAdvertisementRepository repository)
        {
            _repository = repository;
        }

        public async Task<ICommandResult> HandleAsync(EditAdvertisementCommand command)
        {
            if (command == null)
                return new GenericCommandResult(false, "Comando inválido",
                    NotificationHelpers.BuildNotifications(new Notification("body", "O corpo da requisição não pode ser nulo verifique as propriedades enviadas")));

            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Comando inválido", command.Notifications);

            var advertisement = await _repository.Get(command.Id);
            if (advertisement == null)
                return new GenericCommandResult(false, "Comando inválido",
                    NotificationHelpers.BuildNotifications(new Notification("body", "Anúncio não encontrado")));

            advertisement.Edit(command);
            advertisement.Validate();
            if (advertisement.Invalid)
                return new GenericCommandResult(false, "Comando inválido", advertisement.Notifications);

            await _repository.Update(advertisement);

            return new GenericCommandResult(
                    true,
                    "Anúncio editado com sucesso com sucesso",
                    new { });
        }

        public async Task<ICommandResult> HandleAsync(CreateAdvertisementCommand command)
        {
            if (command == null)
                return new GenericCommandResult(false, "Comando inválido",
                    NotificationHelpers.BuildNotifications(new Notification("body", "O corpo da requisição não pode ser nulo verifique as propriedades enviadas")));

            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Comando inválido", command.Notifications);

            var user = new Advertisement(command);
            user.Validate();
            if (user.Invalid)
                return new GenericCommandResult(false, "Comando inválido", user.Notifications);

            await _repository.Insert(user);
            return new GenericCommandResult(true, "Anúncio Cadastrado com sucesso", null);
        }

        public async Task<ICommandResult> HandleAsync(DeleteAdvertisementCommand command)
        {
            if (command == null)
                return new GenericCommandResult(false, "Comando inválido",
                    NotificationHelpers.BuildNotifications(new Notification("body", "O corpo da requisição não pode ser nulo verifique as propriedades enviadas")));

            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Comando inválido", command.Notifications);

            var advertisement = await _repository.Get(command.Id);
            if (advertisement == null)
                return new GenericCommandResult(false, "Comando inválido",
                    NotificationHelpers.BuildNotifications(new Notification("body", "Anúncio não encontrado")));
            
            await _repository.Delete(advertisement);

            return new GenericCommandResult(
                    true,
                    "Anúncio deletado com sucesso com sucesso",
                    new { });
        }
    }
}
