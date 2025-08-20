using System;

namespace Insurella.Business.Domain
{
    public interface ITrackableEntity
    {
        DateTime? CreatedDate
        {
            get;
            set;
        }

        DateTime? ModifiedDate
        {
            get;
            set;
        }
    }
}