using System;
using Xunit;
using DTOs.WithId;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest;

public class RoomTemplatePostDTOTests
{
    [Fact]
        public void RoomTemplatePostDTO_Should_Set_And_Get_Side()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();
            var side = "North";

            // Act
            roomTemplate.Side = side;

            // Assert
            Assert.Equal(side, roomTemplate.Side);
        }

        [Fact]
        public void RoomTemplatePostDTO_Should_Set_And_Get_Windows()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();
            var windows = 4;

            // Act
            roomTemplate.Windows = windows;

            // Assert
            Assert.Equal(windows, roomTemplate.Windows);
        }

        [Fact]
        public void RoomTemplatePostDTO_Should_Set_And_Get_Beds()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();
            var beds = new List<BedAddToTemplateDTO>
            {
                new BedAddToTemplateDTO { BedID = Guid.NewGuid(), BedQuantity = 2 }
            };

            // Act
            roomTemplate.Beds = beds;

            // Assert
            Assert.Equal(beds, roomTemplate.Beds);
        }

        [Fact]
        public void RoomTemplatePostDTO_Should_Set_And_Get_Bathrooms()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();
            var bathrooms = new List<BathroomAddToTemplateDTO>
            {
                new BathroomAddToTemplateDTO { BathRoomID = Guid.NewGuid(), BathroomQuantity = 1 }
            };

            // Act
            roomTemplate.Bathrooms = bathrooms;

            // Assert
            Assert.Equal(bathrooms, roomTemplate.Bathrooms);
        }

        [Fact]
        public void RoomTemplatePostDTO_Should_Allow_Empty_Beds_List()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();
            var beds = new List<BedAddToTemplateDTO>();

            // Act
            roomTemplate.Beds = beds;

            // Assert
            Assert.NotNull(roomTemplate.Beds);
            Assert.Empty(roomTemplate.Beds);
        }

        [Fact]
        public void RoomTemplatePostDTO_Should_Allow_Empty_Bathrooms_List()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();
            var bathrooms = new List<BathroomAddToTemplateDTO>();

            // Act
            roomTemplate.Bathrooms = bathrooms;

            // Assert
            Assert.NotNull(roomTemplate.Bathrooms);
            Assert.Empty(roomTemplate.Bathrooms);
        }

        [Fact]
        public void RoomTemplatePostDTO_Beds_Should_Be_Null_By_Default()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();

            // Assert
            Assert.Null(roomTemplate.Beds);
        }

        [Fact]
        public void RoomTemplatePostDTO_Bathrooms_Should_Be_Null_By_Default()
        {
            // Arrange
            var roomTemplate = new RoomTemplatePostDTO();

            // Assert
            Assert.Null(roomTemplate.Bathrooms);
        }
}
