using System;
using TMS.Domain.Entities.Bases;
using TMS.Domain.Entities.People;

namespace TMS.Domain.Entities.Users
{
    public class User : BaseEntity
    {
        // we will not usee the auditable entity and i will add them here in the user entity because we don't have other classes that need to be audited for now, if we had other classes that needed to be audited we would create a separate class for the auditable entity and inherit from it in the user entity and the other entities that need to be audited, so i delete it and i will add the properties here in the user entity, if we need to add more properties later we can just add them here without affecting other entities that don't need to be audited
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;   
        public int? CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int PersonId { get; set; }
        public virtual Person Person { get; set; } = null!;
    }
}
