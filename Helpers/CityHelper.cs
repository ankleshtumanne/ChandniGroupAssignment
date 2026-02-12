namespace B2BManagement.Helpers{
    public class CityHelper
    {
        public static string GetDestinationCode(string city)
        {
            return city.ToLower() switch
            {
                "mumbai" => "BOM",
                "delhi" => "DEL",
                "bangalore" => "BLR",
                "hyderabad" => "HYD",
                _ => city.ToUpper()
            };
        }
    }}