using System;
using System.Collections.Generic;
using System.Text;

namespace EfCoreV3Bugs.Models
{
    public class TestEntity
    {
        public int Id { get; set; }
        public List<ValueObject1> VoCollection { get; set; }
    }
}
