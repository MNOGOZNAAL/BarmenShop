using BarmenShop.Enums;

namespace BarmenShop.Models.Responces
{
    public class AuthenticationResult
    {
        public List<string> ErrorMessages { get; set; }
        public RegStatusCodes StatusCodes { get; set; }
    }
}
