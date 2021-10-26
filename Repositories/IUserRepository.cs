using System.Collections.Generic;
using TrackingVoucher_v02.Models;

namespace TrackingVoucher_v02.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAll();

        User GetById(int id);

        User GetByGmail(string gmail);

        User GetByFacebook(string facebook);

        bool Create(User entity);

        bool Update(User entity);

        bool Delete(User entity);

        bool ExistedId(int id);

        bool ExistedGmail(string gmail);

        bool ExistedFacebook(string facebook);
    }
}