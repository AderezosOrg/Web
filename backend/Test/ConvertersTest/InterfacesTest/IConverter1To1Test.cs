using Xunit;
using Moq;
using IConverters;

namespace backend.Test.ConvertersTest.InterfacesTest
{
    public class IConverter1To1Tests
    {
        public class ExampleInput { public string Data { get; set; } }
        public class ExampleOutput { public string Result { get; set; } }

        private readonly Mock<IConverter1To1<ExampleInput, ExampleOutput>> _mockConverter;
        private readonly ExampleInput _validInput;
        private readonly ExampleOutput _expectedOutput;

        public IConverter1To1Tests()
        {
            _mockConverter = new Mock<IConverter1To1<ExampleInput, ExampleOutput>>();

            _validInput = new ExampleInput { Data = "Test data" };
            _expectedOutput = new ExampleOutput { Result = "Converted data" };

            _mockConverter.Setup(c => c.Convert(It.IsAny<ExampleInput>()))
                          .Returns(_expectedOutput);
        }

        [Fact]
        public void Convert_ValidInput_ReturnsCorrectOutput()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_expectedOutput.Result, result.Result);
        }

        [Fact]
        public void Convert_ValidInput_ReturnsNonNull()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput);

            // Assert
            Assert.NotNull(result);
        }
        

        [Fact]
        public void Convert_EmptyInput_ReturnsDefaultOutput()
        {
            // Arrange
            var emptyInput = new ExampleInput { Data = string.Empty };
            var expectedEmptyOutput = new ExampleOutput { Result = string.Empty };

            _mockConverter.Setup(c => c.Convert(emptyInput))
                          .Returns(expectedEmptyOutput);

            // Act
            var result = _mockConverter.Object.Convert(emptyInput);

            // Assert
            Assert.Equal(expectedEmptyOutput.Result, result.Result);
        }

        [Fact]
        public void Convert_CorrectOutputType_ReturnsOutputType()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput);

            // Assert
            Assert.IsType<ExampleOutput>(result);
        }
    }
}
