using Xunit;
using backend.DTOs.WithId;
using System;
namespace backend.Test.DTOsTest.WithIdTest;

public class CancelReservationDTOTests
{
    [Fact]
    public void CancelReservationDTO_CanSetProperties()
    {
        // Arrange
        var cancelReservationDTO = new CancelReservationDTO();
        var contactId = Guid.NewGuid();
        var roomId = Guid.NewGuid();

        // Act
        cancelReservationDTO.ContactID = contactId;
        cancelReservationDTO.RoomID = roomId;

        // Assert
        Assert.Equal(contactId, cancelReservationDTO.ContactID);
        Assert.Equal(roomId, cancelReservationDTO.RoomID);
    }

    [Fact]
    public void CancelReservationDTO_DefaultConstructor_SetsPropertiesToDefault()
    {
        // Arrange & Act
        var cancelReservationDTO = new CancelReservationDTO();

        // Assert
        Assert.Equal(Guid.Empty, cancelReservationDTO.ContactID);
        Assert.Equal(Guid.Empty, cancelReservationDTO.RoomID);
    }

    [Fact]
    public void CancelReservationDTO_CanHandleEmptyGuid()
    {
        // Arrange
        var cancelReservationDTO = new CancelReservationDTO();
        Guid emptyGuid = Guid.Empty;

        // Act
        cancelReservationDTO.ContactID = emptyGuid;
        cancelReservationDTO.RoomID = emptyGuid;

        // Assert
        Assert.Equal(Guid.Empty, cancelReservationDTO.ContactID);
        Assert.Equal(Guid.Empty, cancelReservationDTO.RoomID);
    }
}