using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nemo_v2_Data.Entities;
using Nemo_v2_Repo.Abstraction;
using Nemo_v2_Service.Services;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReturnsExtensions;
using Xunit;

namespace ApiTest.ServiceTests
{
    public class BuyerServiceTest
    {
        private readonly BuyerService _buyerService;
        private readonly IUnitOfWork _unitOfWork = Substitute.For<IUnitOfWork>();

        public BuyerServiceTest()
        {
            _buyerService = new BuyerService(_unitOfWork);
        }

        [Fact]
        public async Task GetBuyers_ShouldReturnBuyers_WhenBuyersExists()
        {
            //Arrange
            var buyers = new List<Buyer>
            {
                new Buyer
                {
                    Id = 1,
                    Name = "TestBuyer1"
                },
                new Buyer
                {
                    Id = 2,
                    Name = "TestBuyer2"
                }
            };
            _unitOfWork.BuyerRepository.Get().Returns(buyers);

            //Act
            var result = _buyerService.GetBuyers();

            //Assert
            Assert.Equal(buyers,result);
        }

        [Fact]
        public async Task GetBuyers_ShouldThrowNullReferenceException_WhenBuyerNotExists()
        {
            //Arrange
            _unitOfWork.BuyerRepository.Get().Throws(new NullReferenceException());

            //Act

            //Assert
            Assert.Throws<NullReferenceException>(() => { _buyerService.GetBuyers();});
        }
    }
}