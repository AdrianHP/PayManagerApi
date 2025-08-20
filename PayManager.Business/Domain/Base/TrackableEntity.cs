using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PayManager.Business.Domain
{
    public abstract class TrackableEntity<T> : Entity<T>, ITrackableEntity
    {
        public long CreatedDateTicks { get; set; } = -1;

        public long ModifiedDateTicks { get; set; } = -1;

        [NotMapped]
        public DateTime? CreatedDate
        {
            get
            {
                if (IsDefaultTick(CreatedDateTicks))
                    return null;
                return new DateTime(CreatedDateTicks).ToLocalTime();
            }
            set
            {
                if (value != null) CreatedDateTicks = value.Value.Ticks;
            }
        }

        [NotMapped]
        public DateTime? ModifiedDate
        {
            get
            {
                if (IsDefaultTick(ModifiedDateTicks))
                    return null;
                return new DateTime(ModifiedDateTicks).ToLocalTime();
            }
            set
            {
                if (value != null) ModifiedDateTicks = value.Value.Ticks;
            }
        }

        private bool IsDefaultTick(long ticks)
        {
            return ticks == -1;
        }
    }

    public class TrackableEntity : TrackableEntity<Guid>
    {
        public TrackableEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
            ModifiedDate = DateTime.UtcNow;
        }
    }
}
