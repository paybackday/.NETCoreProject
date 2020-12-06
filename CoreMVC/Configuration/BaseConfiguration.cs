using CoreMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Configuration
{
    public abstract class BaseConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<T> builder) // Burdaki ayarlamalara ek olarak .NET Core da Property(x=> ) olmadigi icin IEntityTypeConfiguration dan gelen Configure metodunu virtual yapariz ki miras alindigi yerde hem base calissin hemde istedigimiz gibi kullanabilecegimiz kendi kod bloklarimizi yazalim.
        {
            builder.Property(x => x.CreatedDate).HasColumnName("Yaratilma Tarihi");
        }
    }
}
