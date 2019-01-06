using System;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions.Xml;
using Moq;
using WebWallet.Models.Entities;
using WebWallet.Models.Enumerations;
using WebWallet.Services.InvestmentServices;
using WebWallet.Services.UserServices;
using WebWallet.Tests.Mocks;
using Xunit;
using WebWallet.ViewModels.Investment;

namespace WebWallet.Tests.Services
{
    public class InvestmentServiceTests
    {
        private InvestmentRepositoryMock _investmentRepository;
        private IInvestmentService _investmentService;
        private IList<Investment> _context;
        private AutoMapperMock _mapperMock;
        private UserServiceMock _userService;

        public InvestmentServiceTests()
        {
            _mapperMock = new AutoMapperMock();
            _userService = new UserServiceMock();

            _context = SeedData(5);
            _investmentRepository = new InvestmentRepositoryMock(_context);

            _investmentService = new InvestmentService(_investmentRepository, _userService.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Create_Adds_New_Investment_In_Context()
        {
            // Arrange
            var username = _userService.RegistrationVM.UserName;
            var investmentVmMock = new Mock<InvestmentVM>().Object;
            var initialContextCount = _context.Count;

            // Act
            await _investmentService.Create(investmentVmMock, username);

            // Assert
            Assert.True(initialContextCount < _context.Count);
        }

        [Fact]
        public async Task Delete_Removes_Correct_Investment_From_Context()
        {
            // Arrange
            var investmentId = _context[0].Id;
            var initialContextCount = _context.Count;

            // Act
            await _investmentService.Delete(investmentId);

            // Assert
            Assert.True(initialContextCount > _context.Count);
            Assert.Null(_context.FirstOrDefault(x => x.Id == investmentId));
        }

        [Fact]
        public async Task GetAll_Returns_IEnumerable()
        {
            // Arrange
            var username = _userService.User.UserName;

            // Act
            var result = await _investmentService.GetAll(username);

            // Assert
            Assert.Equal(result.ToList().Count, _context.Count);
        }

        [Fact]
        public async Task GetById_Returns_InvestmentVM()
        {
            // Arrange
            var id = _context[0].Id;

            // Act
            var result = await _investmentService.GetById(id);

            // Assert
            Assert.IsType<InvestmentVM>(result);
        }

        //[Fact]
        //public async Task Update_Saves_Updated_Investment()
        //{
        //    // Arrange
        //    var investment = _context[0];
        //    var investmentVm = new InvestmentVM
        //    {
        //        Id = investment.Id,
        //        UserId = investment.UserId,
        //        Amount = investment.Amount - 10,
        //        Name = investment.Name,
        //        Type = investment.Type
        //    };

        //    var mappedInvestment = investment;
        //    mappedInvestment.Amount -= 10;
        //    var mapper = new Mock<IMapper>()
        //        .Setup(x => x.Map<Investment>(investmentVm))
        //        .Returns(mappedInvestment);

        //    // Act
        //    var result = await _investmentService.Update(investmentVm);

        //    // Assert
        //    investment = _context[0];
        //    Assert.True(result);
        //    //Assert.Equal(investmentVm.Id, investment.Id);
        //    //Assert.Equal(investmentVm.Amount, investment.Amount - 10);
        //    //Assert.Equal(investmentVm.Name, investment.Name);
        //    //Assert.Equal(investmentVm.Type, investment.Type);
        //    //Assert.Equal(investmentVm.UserId, investment.UserId);
        //}

        private IList<Investment> SeedData(int count)
        {
            var investmentTypes = new InvestmentType[]
            {
                InvestmentType.Crypto,
                InvestmentType.Currency,
                InvestmentType.RealEstate,
                InvestmentType.Stock
            };

            var data = new List<Investment>();
            for (int i = 0; i < count; i++)
            {
                var investment = new Investment
                {
                    Id = i.ToString(),
                    UserId = _userService.User.Id,
                    Abbreviation = $"CODE{i}",
                    Amount = i + 100,
                    CreatedOn = DateTime.UtcNow,
                    ModifiedOn = DateTime.UtcNow,
                    Name = $"Name{i}",
                    Type = investmentTypes[i % investmentTypes.Length]
                };

                data.Add(investment);
            }
            return data;
        }
    }
}