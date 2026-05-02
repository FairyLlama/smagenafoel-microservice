using AuthenticationService.Models;

namespace AuthenticationService.Enums;

public enum UserRoles
{
    Admin,
    AdultCostumer,
    Costumer
}

public class Roles
{
    public static Dictionary<UserRoles, string> UserRole = new()
{
    {
        UserRoles.Admin,
        "Admin"
    },
    {
        UserRoles.AdultCostumer,
        "Adult Costumer"
    },
    {
        UserRoles.Costumer,
        "Costumer"
    }
};
}