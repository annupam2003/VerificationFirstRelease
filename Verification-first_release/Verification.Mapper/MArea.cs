using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verification.EntityModel;
using Verification.DomainModel;

namespace Verification.Mapper
{
    public class MArea : IEntityMapper<Area, AreaModel>
    {
        public List<AreaZone> areaZones { get; set; }

        public AreaModel mapFromEntity(Area entity)
        {
            return new AreaModel
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                DateIs = entity.DateIs,
                Name = entity.Name,
                Pincode = entity.Pincode,
                PinSno = entity.PinSno ?? 0,
                VirPin = entity.VirPin,
                AreaZone = entity.AreaZone.Name
            };
        }

        public bool mapFromEntity(Area entity, AreaModel models)
        {
            models.Id = entity.Id;
            models.IsActive = entity.IsActive;
            models.DateIs = entity.DateIs;
            models.Name = entity.Name;
            models.Pincode = entity.Pincode;
            models.PinSno = entity.PinSno ?? 0;
            models.VirPin = entity.VirPin;
            models.AreaZone = entity.AreaZone.Name;
            return true;
        }

        public Area mapToEntity(AreaModel models)
        {
            var areaZone =areaZones.Where(x => x.Name.ToUpper().Trim().Equals(models.AreaZone.ToUpper().Trim())).Select(x => x).FirstOrDefault();
            if (!(areaZone == null))
            {
                return new Area
                {
                    Id = models.Id,
                    IsActive = models.IsActive,
                    DateIs = models.DateIs,
                    Name = models.Name,
                    Pincode= models.Pincode,
                    PinSno=models.PinSno,
                    VirPin=models.VirPin,                  
                    AreaZone=areaZone,
                    AreaZoneId=areaZone.Id
                };
            }
            return null;
        }

        

        public bool mapToEntity(AreaModel models, Area entity)
        {
            var areaZone = areaZones.Where(x => x.Name.ToUpper().Trim().Equals(models.AreaZone.ToUpper().Trim())).Select(x => x).FirstOrDefault();
            if (!(areaZone == null))
            {

                entity.Id = models.Id;
                entity.IsActive = models.IsActive;
                entity.DateIs = models.DateIs;
                entity.Name = models.Name;
                entity.Pincode = models.Pincode;
                entity.PinSno = models.PinSno;
                entity.VirPin = models.VirPin;
                entity.AreaZone = areaZone;
                entity.AreaZoneId = areaZone.Id;
            }
            return false;
        }



    }
}
