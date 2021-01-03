using System.Collections.Generic;

namespace Graphql.Business.Common
{
    public abstract class Payload
    {
        protected Payload(IReadOnlyList<BadRequestError>? errors = null)
        {
            Errors = errors;
        }

        public IReadOnlyList<BadRequestError>? Errors { get; }
    }
}
