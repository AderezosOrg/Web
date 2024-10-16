CREATE DATABASE IF NOT EXISTS dbaderezosweb;

USE dbaderezosweb;

CREATE TABLE IF NOT EXISTS Bed(
  Id        VARCHAR(36) PRIMARY KEY NOT NULL,
  Size      VARCHAR(80),
  Capacity  VARCHAR(80)
);

CREATE TABLE IF NOT EXISTS Bathroom(
  Id            VARCHAR(36) PRIMARY KEY NOT NULL,
  Shower        BIT,
  Toilet        BIT,
  DressingTable BIT
);

CREATE TABLE IF NOT EXISTS Service(
  Id    VARCHAR(36) PRIMARY KEY NOT NULL,
  Type  VARCHAR(80)
);

CREATE TABLE IF NOT EXISTS Contact(
  Id          VARCHAR(36) PRIMARY KEY NOT NULL,
  PhoneNumber VARCHAR(80),
  Email       VARCHAR(80)
);

CREATE TABLE IF NOT EXISTS RoomTemplate(
  Id      VARCHAR(36) PRIMARY KEY NOT NULL,
  Side    VARCHAR(80),
  Windows TINYINT
);

CREATE TABLE IF NOT EXISTS BedInformation(
  RoomTemplateId  VARCHAR(36) NOT NULL,
  BedId           VARCHAR(36) NOT NULL,
  Quantity        TINYINT,
  FOREIGN KEY (RoomTemplateId)  REFERENCES  RoomTemplate(Id),
  FOREIGN KEY (BedId)           REFERENCES  Bed(Id)
); 

CREATE TABLE IF NOT EXISTS RoomBathInformation(
  RoomTemplateId  VARCHAR(36) NOT NULL,
  BathroomId      VARCHAR(36) NOT NULL,
  Quantity        TINYINT,
  FOREIGN KEY (RoomTemplateId)  REFERENCES  RoomTemplate(Id),
  FOREIGN KEY (BathroomId)      REFERENCES  Bathroom(Id) 
);

CREATE TABLE IF NOT EXISTS User(
  Id          VARCHAR(36) PRIMARY KEY NOT NULL,
  Name        VARCHAR(80),
  CINumber    VARCHAR(80),
  ContactId   VARCHAR(36) NOT NULL,
  FOREIGN KEY (ContactId) REFERENCES Contact(Id)
);

CREATE TABLE IF NOT EXISTS Hotel(
  Id          VARCHAR(36) PRIMARY KEY NOT NULL,
  Stars       TINYINT,
  Name        VARCHAR(80),
  AllowsPets  BIT,
  Address     VARCHAR(100),
  Tax         TINYINT,
  UserId      VARCHAR(36) NOT NULL,
  ContactId   VARCHAR(36) NOT NULL,
  BathroomId  VARCHAR(36) NOT NULL,
  FOREIGN KEY (UserId) REFERENCES User(Id),
  FOREIGN KEY (ContactId) REFERENCES Contact(Id),
  FOREIGN KEY (BathroomId) REFERENCES Bathroom(Id)
);

CREATE TABLE IF NOT EXISTS Room(
  Id              VARCHAR(36) PRIMARY KEY NOT NULL,
  Code            VARCHAR(80),
  FloorNumber     TINYINT,
  PricePerNight   DECIMAL(7,2),
  RoomTemplateId  VARCHAR(36) NOT NULL,
  HotelId         VARCHAR(36) NOT NULL,
  FOREIGN KEY (RoomTemplateId) REFERENCES RoomTemplate(Id),
  FOREIGN KEY (HotelId) REFERENCES Hotel(Id)
);

CREATE TABLE IF NOT EXISTS Reservation(
  ContactId         VARCHAR(36) NOT NULL,
  RoomId            VARCHAR(36) NOT NULL,
  ReservationDate   DATETIME,
  UseDate           DATETIME,
  Cancelled         BIT,
  FOREIGN KEY (ContactId) REFERENCES Contact(Id),
  FOREIGN KEY (RoomId) REFERENCES Room(Id)
);

CREATE TABLE IF NOT EXISTS RoomServices(
  RoomId    VARCHAR(36) NOT NULL,
  ServiceId VARCHAR(36) NOT NULL,
  FOREIGN KEY (RoomId) REFERENCES Room(Id),
  FOREIGN KEY (ServiceId) REFERENCES Service(Id)
);

DELIMITER //
CREATE PROCEDURE TruncateAllTables()
BEGIN
    SET FOREIGN_KEY_CHECKS = 0;

    TRUNCATE TABLE Bathroom;
    TRUNCATE TABLE Bed;
    TRUNCATE TABLE BedInformation;
    TRUNCATE TABLE Contact;
    TRUNCATE TABLE Hotel;
    TRUNCATE TABLE Reservation;
    TRUNCATE TABLE Room;
    TRUNCATE TABLE RoomBathInformation;
    TRUNCATE TABLE RoomServices;
    TRUNCATE TABLE RoomTemplate;
    TRUNCATE TABLE Service;
    TRUNCATE TABLE User;
    SET FOREIGN_KEY_CHECKS = 1;
END //
DELIMITER ;


DELIMITER //

CREATE PROCEDURE GetHotelByUserId(IN userId CHAR(36))
BEGIN
  SELECT
    h.Id,
    h.Stars,
    h.Name,
    h.AllowsPets,
    h.Address,
    h.Tax
  FROM
    Hotel h
  WHERE
    h.UserId = userId;
END //

DELIMITER ;


DELIMITER //

CREATE PROCEDURE GetBathroomInformationByBathroomId(IN bathroomId CHAR(36))
BEGIN
  SELECT
    i.RoomTemplateId,
    i.Quantity
  FROM
    RoomBathInformation i
  WHERE
    i.BathroomId = bathroomId;
END //

DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetBathroomInformationByRoomTemplateId(IN roomTemplateId CHAR(36))
BEGIN
  SELECT
    i.BathroomId,
    i.Quantity
  FROM
    RoomBathInformation i
  WHERE
    i.RoomTemplateId = roomTemplateId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteBathroomInformationByRoomTemplateId(IN roomTemplateId CHAR(36))
BEGIN
  DELETE
  FROM
    RoomBathInformation
  WHERE
    RoomTemplateId = roomTemplateId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteBathroomInformationByBathroomId(IN bathroomId CHAR(36))
BEGIN
  DELETE
  FROM
    RoomBathInformation
  WHERE
    BathroomId = bathroomId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetBedInformationByBedId(IN bedId CHAR(36))
BEGIN
  SELECT
    i.RoomTemplateId,
    i.Quantity
  FROM
    BedInformation i
  WHERE
    i.BedId = bedId;
END //
DELIMITER ;


DELIMITER //
CREATE PROCEDURE GetBedInformationByRoomTemplateId(IN roomTemplateId CHAR(36))
BEGIN
  SELECT
    i.BedId,
    i.Quantity
  FROM
    BedInformation i
  WHERE
    i.RoomTemplateId = roomTemplateId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteBedInformationByRoomTemplateId(IN roomTemplateId CHAR(36))
BEGIN
  DELETE
  FROM
    BedInformation
  WHERE
    RoomTemplateId = roomTemplateId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE DeleteBedInformationByBedId(IN bedId CHAR(36))
BEGIN
  DELETE
  FROM
    BedInformation
  WHERE
    BedId = bedId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetRoomServicesByRoomId(IN roomId CHAR(36))
BEGIN
  SELECT
    rs.ServiceId
  FROM
    RoomServices rs
  WHERE
    rs.RoomId = roomId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetRoomServicesByServiceId(IN serviceId CHAR(36))
BEGIN
  SELECT
    rs.RoomId
  FROM
    RoomServices rs
  WHERE
    rs.ServiceId = serviceId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetReservationByContactId(IN contactId CHAR(36))
BEGIN
  SELECT
    r.RoomId,
    r.ReservationDate,
    r.UseDate,
    r.Cancelled
  FROM
    Reservation r
  WHERE
    r.ContactId = contactId;
END //
DELIMITER ;

DELIMITER //
CREATE PROCEDURE GetReservationByRoomId(IN roomId CHAR(36))
BEGIN
  SELECT
    r.ContactId,
    r.ReservationDate,
    r.UseDate,
    r.Cancelled
  FROM
    Reservation r
  WHERE
    r.RoomId = roomId;
END //
DELIMITER ;