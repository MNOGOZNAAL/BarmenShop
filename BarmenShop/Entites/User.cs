using Microsoft.AspNetCore.Identity;

namespace BarmenShop.Entites
{
    public class User : IdentityUser
    {
        public DateTime RegDate { get; set; }
        public List<Order> Orders { get; set; }
    }
}
