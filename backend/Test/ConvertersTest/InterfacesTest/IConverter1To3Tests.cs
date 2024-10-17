using Xunit;
using Moq;
using IConverters;

namespace backend.Test.ConvertersTest.InterfacesTest
{
    public class IConverter1To3Tests
    {
        public class ExampleInput1 { public string Data1 { get; set; } }
        public class ExampleInput2 { public string Data2 { get; set; } }
        public class ExampleInput3 { public string Data3 { get; set; } }
        public class ExampleOutput { public string Result { get; set; } }

        private readonly Mock<IConverter1To3<ExampleInput1, ExampleInput2, ExampleInput3, ExampleOutput>> _mockConverter;
        private readonly ExampleInput1 _validInput1;
        private readonly ExampleInput2 _validInput2;
        private readonly ExampleInput3 _validInput3;
        private readonly ExampleOutput _expectedOutput;

        public IConverter1To3Tests()
        {
            _mockConverter = new Mock<IConverter1To3<ExampleInput1, ExampleInput2, ExampleInput3, ExampleOutput>>();

            _validInput1 = new ExampleInput1 { Data1 = "Test data 1" };
            _validInput2 = new ExampleInput2 { Data2 = "Test data 2" };
            _validInput3 = new ExampleInput3 { Data3 = "Test data 3" };
            _expectedOutput = new ExampleOutput { Result = "Converted data" };

            _mockConverter.Setup(c => c.Convert(It.IsAny<ExampleInput1>(), It.IsAny<ExampleInput2>(), It.IsAny<ExampleInput3>()))
                          .Returns(_expectedOutput);
        }

        [Fact]
        public void Convert_ValidInputs_ReturnsCorrectOutput()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput1, _validInput2, _validInput3);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_expectedOutput.Result, result.Result);
        }

        [Fact]
        public void Convert_ValidInputs_ReturnsNonNull()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput1, _validInput2, _validInput3);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Convert_CorrectOutputType_ReturnsOutputType()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput1, _validInput2, _validInput3);

            // Assert
            Assert.IsType<ExampleOutput>(result);
        }

        [Fact]
        public void Convert_EmptyInputs_ReturnsDefaultOutput()
        {
            // Arrange
            var emptyInput1 = new ExampleInput1 { Data1 = string.Empty };
            var emptyInput2 = new ExampleInput2 { Data2 = string.Empty };
            var emptyInput3 = new ExampleInput3 { Data3 = string.Empty };
            var expectedEmptyOutput = new ExampleOutput { Result = string.Empty };

            _mockConverter.Setup(c => c.Convert(emptyInput1, emptyInput2, emptyInput3))
                          .Returns(expectedEmptyOutput);

            // Act
            var result = _mockConverter.Object.Convert(emptyInput1, emptyInput2, emptyInput3);

            // Assert
            Assert.Equal(expectedEmptyOutput.Result, result.Result);
        }
    }
}
