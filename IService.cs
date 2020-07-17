using System.Threading.Tasks;

namespace BareBoneMembershipApi
{
    public interface IService   
    {   
        Task<Client> FetchClient(int clientId);
    }
}