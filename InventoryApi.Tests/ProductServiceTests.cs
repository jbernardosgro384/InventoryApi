using FluentAssertions;
using InventoryApi.Application.Services;
using InventoryApi.Domain.Entities;
using InventoryApi.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Tests;

public class ProductServiceTests
{
    [Fact]
    public async Task CreateAsync_Should_Add_Product()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new AppDbContext(options);

        var service = new ProductService(context);

        var product = new Product
        {
            Name = "Keyboard",
            Price = 150,
            Stock = 10
        };

        var result = await service.CreateAsync(product);

        result.Should().NotBeNull();

        context.Products.Count().Should().Be(1);
    }
}