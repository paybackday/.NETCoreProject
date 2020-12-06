using CoreMVC.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMVC.Configuration
{
    public class OrderDetailConfiguration:BaseConfiguration<OrderDetail>
    {
        //Ben burada ayarlama yapmak istiyorum.
        public override void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            base.Configure(builder);//base i birakiyorum ki o ozelligide alsin. Ben onun uzerine ozellikler tanimlayayim,
            builder.Ignore(x => x.ID);
            builder.HasKey(x => new { x.OrderID, x.ProductID });
            builder.ToTable("Satislar");
        }
    }
}
