namespace Lax.Data.SharePoint.Rest {

    public interface ISharePointEntityContentTypeProvider {

        EntityContentTypeMap<TEntity> ContentTypeMap<TEntity>() where TEntity : Entity;

    }

}