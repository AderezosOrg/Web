using Xunit;
using DTOs.WithoutId;

namespace backend.Test.DTOsTest.WithoutIdTest
{
    public class ReservationPostDTOTest
    {
        [Fact]
        public void ReservationPostDTO_CanSet_ReservationDate()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            var reservationDate = new DateTime(2024, 10, 15);

            // Act
            reservationPostDTO.ReservationDate = reservationDate;

            // Assert
            Assert.Equal(reservationDate, reservationPostDTO.ReservationDate);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_ReservationDate()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { ReservationDate = new DateTime(2024, 10, 15) };

            // Act & Assert
            Assert.Equal(new DateTime(2024, 10, 15), reservationPostDTO.ReservationDate);
        }

        [Fact]
        public void ReservationPostDTO_CanSet_UseDate()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            var useDate = new DateTime(2024, 10, 20);

            // Act
            reservationPostDTO.UseDate = useDate;

            // Assert
            Assert.Equal(useDate, reservationPostDTO.UseDate);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_UseDate()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { UseDate = new DateTime(2024, 10, 20) };

            // Act & Assert
            Assert.Equal(new DateTime(2024, 10, 20), reservationPostDTO.UseDate);
        }

        [Fact]
        public void ReservationPostDTO_CanSet_Cancelled()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            bool cancelled = true;

            // Act
            reservationPostDTO.Cancelled = cancelled;

            // Assert
            Assert.Equal(cancelled, reservationPostDTO.Cancelled);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_Cancelled()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { Cancelled = false };

            // Act & Assert
            Assert.False(reservationPostDTO.Cancelled);
        }

        [Fact]
        public void ReservationPostDTO_CanSet_UserPhoneNumber()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            string userPhoneNumber = "123-456-7890";

            // Act
            reservationPostDTO.UserPhoneNumber = userPhoneNumber;

            // Assert
            Assert.Equal(userPhoneNumber, reservationPostDTO.UserPhoneNumber);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_UserPhoneNumber()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { UserPhoneNumber = "098-765-4321" };

            // Act & Assert
            Assert.Equal("098-765-4321", reservationPostDTO.UserPhoneNumber);
        }

        [Fact]
        public void ReservationPostDTO_CanSet_UserEmail()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            string userEmail = "user@example.com";

            // Act
            reservationPostDTO.UserEmail = userEmail;

            // Assert
            Assert.Equal(userEmail, reservationPostDTO.UserEmail);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_UserEmail()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { UserEmail = "test@example.com" };

            // Act & Assert
            Assert.Equal("test@example.com", reservationPostDTO.UserEmail);
        }

        [Fact]
        public void ReservationPostDTO_CanSet_RoomCode()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            string roomCode = "Room101";

            // Act
            reservationPostDTO.RoomCode = roomCode;

            // Assert
            Assert.Equal(roomCode, reservationPostDTO.RoomCode);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_RoomCode()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { RoomCode = "Room202" };

            // Act & Assert
            Assert.Equal("Room202", reservationPostDTO.RoomCode);
        }

        [Fact]
        public void ReservationPostDTO_CanSet_RoomFloorNumber()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            int roomFloorNumber = 2;

            // Act
            reservationPostDTO.RoomFloorNumber = roomFloorNumber;

            // Assert
            Assert.Equal(roomFloorNumber, reservationPostDTO.RoomFloorNumber);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_RoomFloorNumber()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { RoomFloorNumber = 3 };

            // Act & Assert
            Assert.Equal(3, reservationPostDTO.RoomFloorNumber);
        }

        [Fact]
        public void ReservationPostDTO_CanSet_PricePerNight()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO();
            decimal pricePerNight = 99.99m;

            // Act
            reservationPostDTO.PricePerNight = pricePerNight;

            // Assert
            Assert.Equal(pricePerNight, reservationPostDTO.PricePerNight);
        }

        [Fact]
        public void ReservationPostDTO_CanGet_PricePerNight()
        {
            // Arrange
            var reservationPostDTO = new ReservationPostDTO { PricePerNight = 89.99m };

            // Act & Assert
            Assert.Equal(89.99m, reservationPostDTO.PricePerNight);
        }

        [Fact]
        public void ReservationPostDTO_DefaultValues_AreNullOrFalse()
        {
            // Arrange & Act
            var reservationPostDTO = new ReservationPostDTO();

            // Assert
            Assert.Equal(default(DateTime), reservationPostDTO.ReservationDate);
            Assert.Equal(default(DateTime), reservationPostDTO.UseDate);
            Assert.False(reservationPostDTO.Cancelled);
            Assert.Null(reservationPostDTO.UserPhoneNumber);
            Assert.Null(reservationPostDTO.UserEmail);
            Assert.Null(reservationPostDTO.RoomCode);
            Assert.Equal(0, reservationPostDTO.RoomFloorNumber);
            Assert.Equal(0m, reservationPostDTO.PricePerNight);
        }
    }
}
