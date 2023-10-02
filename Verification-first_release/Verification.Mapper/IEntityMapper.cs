using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Verification.Mapper
{
    public interface IEntityMapper<Entity, Models> where Entity : class where Models : class
    {
        Models mapFromEntity(Entity entity);
        Entity mapToEntity(Models models);
        bool mapFromEntity(Entity entity, Models models);
        bool mapToEntity(Models models, Entity entity);
    }
}
