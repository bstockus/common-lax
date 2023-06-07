using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace Lax.Services.Directory {

    public class DirectoryServices : IDirectoryServices {

        private readonly DirectoryServicesOptions _directoryServicesOptions;

        public DirectoryServices(DirectoryServicesOptions directoryServicesOptions) =>
            _directoryServicesOptions = directoryServicesOptions;

        public async Task<IEnumerable<DirectoryServicesUser>> GetAllUsers() => await Task.Run(() => {
            return _directoryServicesOptions.LdapConnectionStrings.Select(ldapConnectionString => {
                using var searcher = new DirectorySearcher(new DirectoryEntry(ldapConnectionString)) {
                    Filter = $"(&(objectClass=person)(objectClass=user)(objectClass=organizationalPerson))"
                };
                searcher.PropertiesToLoad.Add("cn");
                searcher.PropertyNamesOnly = true;
                searcher.SearchScope = SearchScope.Subtree;
                searcher.Sort.Direction = SortDirection.Ascending;
                searcher.Sort.PropertyName = "cn";
                searcher.ReferralChasing = ReferralChasingOption.All;

                var results = searcher.FindAll().Cast<SearchResult>().Select(sr => sr.GetDirectoryEntry())
                    .ToList();

                return results.Select(result => new DirectoryServicesUser {
                    WindowsAccountName = (string) result.Properties["sAMAccountName"]?.Value,
                    FirstName = (string) result.Properties["givenName"]?.Value,
                    LastName = (string) result.Properties["sn"]?.Value,
                    EmailAddress = ((string) result.Properties["mail"]?.Value),
                    WindowsPrimarySid =
                        (new SecurityIdentifier(((byte[]) result.Properties["objectSid"].Value), 0)).ToString(),
                    ParentGroupName = result.Parent.Name,
                });
            }).SelectMany(x => x);
        });

        public async Task<DirectoryServicesUser> GetUserByObjectSid(string objectSid) {
            foreach (var ldapConnectionString in _directoryServicesOptions.LdapConnectionStrings) {
                using var searcher = new DirectorySearcher(new DirectoryEntry(ldapConnectionString)) {
                    Filter =
                        $"(&(objectClass=person)(objectClass=user)(objectClass=organizationalPerson)(objectSid={objectSid}))"
                };
                searcher.PropertiesToLoad.Add("cn");
                searcher.PropertyNamesOnly = true;
                searcher.SearchScope = SearchScope.Subtree;
                searcher.Sort.Direction = SortDirection.Ascending;
                searcher.Sort.PropertyName = "cn";
                searcher.ReferralChasing = ReferralChasingOption.All;

                var results = searcher.FindAll().Cast<SearchResult>().Select(sr => sr.GetDirectoryEntry())
                    .ToList();

                if (!results.Any()) {
                    continue;
                }

                var result = results.First();

                var directoryServicesUser = new DirectoryServicesUser {
                    WindowsAccountName = (string) result.Properties["sAMAccountName"]?.Value,
                    FirstName = (string) result.Properties["givenName"]?.Value,
                    LastName = (string) result.Properties["sn"]?.Value,
                    EmailAddress = ((string) result.Properties["mail"]?.Value),
                    WindowsPrimarySid =
                        (new SecurityIdentifier(((byte[]) result.Properties["objectSid"].Value), 0))
                        .ToString(),
                    ParentGroupName = result.Parent.Name,
                };

                return await Task.FromResult(directoryServicesUser);
            }

            return await Task.FromResult<DirectoryServicesUser>(null);
        }

    }

}