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
        public void ReservationPostDTO_DefaultValues_AreNullOrFalse()
        {
            // Arrange & Act
            var reservationPostDTO = new ReservationPostDTO();

            // Assert
            Assert.Equal(default(DateTime), reservationPostDTO.ReservationDate);
            Assert.Equal(default(DateTime), reservationPostDTO.UseDate);
        }
    }
}
