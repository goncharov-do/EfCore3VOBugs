using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCoreV3Bugs.Models
{
    public class ValueObject1: ValueObject
    {
        public ValueObject1(bool someNonNullableProperty)
        {
            SomeNonNullableProperty = someNonNullableProperty;
        }

        public bool SomeNonNullableProperty { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return SomeNonNullableProperty;
        }
    }
}
