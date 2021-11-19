using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebService.Core.Shared
{
    public enum Status
    {
        Created,
        Updated,
        Deleted,
        NotFound,
        BadRequest,
        Conflict
    }
}
