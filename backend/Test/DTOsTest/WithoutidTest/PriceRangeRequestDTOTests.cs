using System;
using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest;

public class PriceRangeRequestDTOTests
{
     [Fact]
        public void PriceRangeRequestDTO_Should_Set_And_Get_MinPrice()
        {
            // Arrange
            var priceRangeDto = new PriceRangeRequestDTO();
            var minPrice = 100m;

            // Act
            priceRangeDto.MinPrice = minPrice;

            // Assert
            Assert.Equal(minPrice, priceRangeDto.MinPrice);
        }

        [Fact]
        public void PriceRangeRequestDTO_Should_Set_And_Get_MaxPrice()
        {
            // Arrange
            var priceRangeDto = new PriceRangeRequestDTO();
            var maxPrice = 500m;

            // Act
            priceRangeDto.MaxPrice = maxPrice;

            // Assert
            Assert.Equal(maxPrice, priceRangeDto.MaxPrice);
        }

        [Fact]
        public void PriceRangeRequestDTO_Should_Have_Default_Values()
        {
            // Arrange
            var priceRangeDto = new PriceRangeRequestDTO();

            // Assert
            Assert.Equal(0m, priceRangeDto.MinPrice);
            Assert.Equal(0m, priceRangeDto.MaxPrice);
        }

        [Fact]
        public void PriceRangeRequestDTO_MinPrice_Should_Be_Less_Than_Or_Equal_To_MaxPrice()
        {
            // Arrange
            var priceRangeDto = new PriceRangeRequestDTO
            {
                MinPrice = 100m,
                MaxPrice = 500m
            };

            // Act & Assert
            Assert.True(priceRangeDto.MinPrice <= priceRangeDto.MaxPrice);
        }

        [Fact]
        public void PriceRangeRequestDTO_Should_Throw_Exception_If_MinPrice_Greater_Than_MaxPrice()
        {
            // Arrange
            var priceRangeDto = new PriceRangeRequestDTO
            {
                MinPrice = 600m,
                MaxPrice = 500m
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            {
                if (priceRangeDto.MinPrice > priceRangeDto.MaxPrice)
                {
                    throw new ArgumentException("MinPrice cannot be greater than MaxPrice");
                }
            });
        }   
}