using Dinner.Domain.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dinner.Domain.Host.ValueObjects
{
    public sealed class HostId : ValueObject
    {
        public Guid Value { get; }

        public HostId(Guid value)
        {
            Value = value;
        }

        public static HostId Create(string id)
        {
            return new HostId(new Guid(id));
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
