using System;

namespace PayManager.Business.Domain
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