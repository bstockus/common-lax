using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lax.Services.Directory {

    public interface IDirectoryServices {

        Task<DirectoryServicesUser> GetUserByObjectSid(string objectSid);

        Task<IEnumerable<DirectoryServicesUser>> GetAllUsers();

    }

}