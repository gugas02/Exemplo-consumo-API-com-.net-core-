using Flunt.Notifications;
using Flunt.Validations;

namespace API.Consumption.test.Domain.Command.Advertisement
{
    public class EditAdvertisementCommand : Notifiable, ICommand
    {
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public int Year { get; set; }
        public int Km { get; set; }
        public string Observation { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract()
                .IsNotNull(Id, "Id", "O id não pode ser nulo")
                .IsNotNullOrEmpty(Brand, "Brand", "A marca não pode ser nulo ou vazio")
                .IsLowerOrEqualsThan(Brand.Length, 45, "Brand", "A marca não pode ter mais que 45 caracteres")
                .IsNotNullOrEmpty(Model, "Model", "O modelo não pode ser nulo ou vazio")
                .IsLowerOrEqualsThan(Model.Length, 45, "Brand", "O modelo não pode ter mais que 45 caracteres")
                .IsNotNullOrEmpty(Version, "Version", "A versão não pode ser nulo ou vazio")
                .IsLowerOrEqualsThan(Version.Length, 45, "Version", "O versão não pode ter mais que 45 caracteres")
                .IsNotNull(Year, "Year", "O ano não pode ser nula")
                .IsNotNull(Km, "Km", "A quilometragem não pode ser nula")
                .IsNotNullOrEmpty(Observation, "Observation", "A observação não pode ser nulo ou vazio")
            );
        }
    }
}
