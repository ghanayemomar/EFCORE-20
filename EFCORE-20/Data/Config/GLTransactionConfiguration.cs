using EFCORE_20.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCORE_20.Data.Config
{
    public class GLTransactionConfiguration : IEntityTypeConfiguration<GLTransaction>
    {
        public void Configure(EntityTypeBuilder<GLTransaction> builder)
        {
            builder.HasKey(x => x.Id);
            //
            builder.Property(x => x.Notes).HasColumnType("VARCHAR").HasMaxLength(255).IsRequired();
            //
            builder.Property(x => x.Amount).HasColumnType("decimal(18,2)").IsRequired();
            //
            builder.Property(x => x.CreatedAt).IsRequired();
            //
            builder.ToTable("GLTransactions");
        }
    }
}
