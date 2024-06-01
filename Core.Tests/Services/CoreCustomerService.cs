using DotNetApi.Core.Services;
using DotNetApi.Core.Data;
using DotNetApi.Core.Data.Models;
using DotNetApi.Domain.Services;
using DomainDTOs = DotNetApi.Domain.DTOs;
using Moq;
using AutoMapper;
using Moq.EntityFrameworkCore;

namespace Core.Tests.Services;

public class CoreCustomerServiceTests
{
    [Fact]
    public async void CustomerSerrvice_Should_Fail_When_Request_Is_Null()
    {
        var mapper = new Mock<IMapper>();
        var dbContext = new Mock<DataContext>();

        var customerService = new CoreCustomerService(mapper.Object, dbContext.Object);

        await Assert.ThrowsAsync<ArgumentNullException>(async () => await customerService.CreateCustomer(null));
    }

    [Fact]
    public async void CustomerService_Should_Fail_When_Name_Is_Empty()
    {
        var mapper = new Mock<IMapper>();
        var dbContext = new Mock<DataContext>();

        var customerService = new CoreCustomerService(mapper.Object, dbContext.Object);

        var request = new CreateCustomerRequest
        {
            Name = string.Empty
        };

        await Assert.ThrowsAsync<ArgumentException>(async () => await customerService.CreateCustomer(request));
    }

    [Fact]
    public async void CustomerService_Should_Succeed_When_Request_Is_Valid()
    {
        var mapper = new Mock<IMapper>();
        var dbContext = new Mock<DataContext>();

        var customerService = new CoreCustomerService(mapper.Object, dbContext.Object);

        var request = new CreateCustomerRequest
        {
            Name = "Test Customer"
        };

        mapper.Setup(x => x.Map<DomainDTOs.Customer>(It.IsAny<Customer>())).Returns(new DomainDTOs.Customer() { Name = request.Name });
        dbContext.SetupGet(x => x.Customers).ReturnsDbSet(new List<Customer>());

        var result = await customerService.CreateCustomer(request);

        Assert.NotNull(result);
        Assert.Equal(request.Name, result.Name);
    }
}
