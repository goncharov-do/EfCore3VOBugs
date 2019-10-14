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

            builder.OwnsMany(c => c.VoCollection, t => { t.Property(c => c.SomeNonNullableProperty); });
        }
    }
}
