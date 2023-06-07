namespace Lax.Data.SharePoint.Rest {

    public class SharePointUserInformation {

        public int Id { get; }
        public string LoginName { get; }
        public string Title { get; }
        public string Email { get; }

        public SharePointUserInformation(
            int id,
            string loginName,
            string title,
            string email) {
            Id = id;
            LoginName = loginName;
            Title = title;
            Email = email;
        }

    }

}