using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreV3Bugs.Models
{
    public class TestEntity
    {
        public int Id { get; set; }
        public ValueObject1 VO1 { get; set; }
        public ValueObject2 VO2 { get; set; }
    }
}
