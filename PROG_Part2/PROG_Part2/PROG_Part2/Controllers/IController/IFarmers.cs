using PROG_Part2.Models;

namespace PROG_Part2.Controllers.IController
{
    public interface IFarmers
    {
        //check if employee exists in the database
        Farmer CheckLogin(int username, string password);
    }
}
