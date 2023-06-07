using System;

namespace Lax.Mvc.AdminLte.Forms {

    public interface ISelectListItem {

        Guid Id { get; }

        string Name { get; }

    }

}