using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCoreV3Bugs.Models
{
    public class ValueObject2: ValueObject
    {
        public ValueObject2(string someNullableProperty)
        {
            SomeNullableProperty = someNullableProperty;
        }

        public string SomeNullableProperty { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SomeNullableProperty;
        }
    }
}
