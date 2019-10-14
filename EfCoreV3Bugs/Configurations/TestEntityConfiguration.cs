using System;
using System.Collections.Generic;
using System.Text;
using EfCoreV3Bugs.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EfCoreV3Bugs.Configurations
{
    class TestEntityConfiguration: IEntityTypeConfiguration<TestEntity>
    {
        public void Configure(EntityTypeBuilder<TestEntity> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).ValueGeneratedOnAdd();

            builder.OwnsOne(c => c.VO1, t => { t.Property(c => c.SomeNonNullableProperty); });
            builder.OwnsOne(c => c.VO2, t => { t.Property(a => a.SomeNullableProperty).IsRequired(false); });
        }
    }
}
