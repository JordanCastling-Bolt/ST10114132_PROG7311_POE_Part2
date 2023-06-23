using PROG_Part2.Models;

namespace PROG_Part2.Controllers.IController
{
    public interface IEmployees
    {
        //check if employee id or farmer id exists in the database
        Employee CheckLogin(int username, string password);

    }
}
