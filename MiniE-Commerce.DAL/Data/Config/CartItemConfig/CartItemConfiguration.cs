using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MiniE_Commerce.DAL.Entities.CartItemEntity;

namespace MiniE_Commerce.DAL.Data.Config.CartItemConfig
{
    public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {

            builder.HasOne(ci => ci.User)
                   .WithMany()
                   .HasForeignKey(ci => ci.UserId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ci => ci.Product)
                     .WithMany()
                     .HasForeignKey(ci => ci.ProductId)
                     .OnDelete(DeleteBehavior.Cascade);


            builder.Property(ci => ci.Quantity)
                   .IsRequired();
        }
    }
}
