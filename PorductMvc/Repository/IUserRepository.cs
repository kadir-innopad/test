using PorductMvc.ViewModel;

namespace PorductMvc.Repository
{
    public interface IUserRepository
    {
        Task<UserViewModel> UserList();
    }
}
