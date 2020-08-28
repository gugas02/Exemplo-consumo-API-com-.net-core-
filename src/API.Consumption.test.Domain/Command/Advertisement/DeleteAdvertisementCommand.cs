using Flunt.Notifications;
using Flunt.Validations;
using System;

namespace API.Consumption.test.Domain.Command.Advertisement
{
    public class DeleteAdvertisementCommand : Notifiable, ICommand
    {
        public DeleteAdvertisementCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNull(Id, "Id", "O id não pode ser nulo")
            );
        }
    }
}
