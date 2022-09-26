namespace EcomzExercise.Data.Models.View_Models
{
    public class AddressVM
    {
    }

    public class AddAddressVM
    {
        public int AddressTypeId { get; set; }
        public int AddressStreetNumber { get; set; }
        public string AddressStreetName { get; set; }
        public int AddressZipPostal { get; set; }
        public int CityId { get; set; }
        public int CustomerId { get; set; }

    }

    public class ListCountryVM
    {
        public string CountryName { get; set; }
        public string CountryInitials { get; set; }
    }

    public class ListCityVM
    {
        public string CityName { get; set; }
        public int CountryId { get; set; }
    }

    public class ListAddressTypeVM
    {
        public string AddressTypeName { get; set; }
    }
}
