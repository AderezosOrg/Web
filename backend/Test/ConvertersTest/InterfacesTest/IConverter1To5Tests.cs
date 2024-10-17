using Xunit;
using Moq;
using IConverters;

namespace backend.Test.ConvertersTest.InterfacesTest
{
    public class IConverter1To5Tests
    {
        public class ExampleInput1 { public string Data1 { get; set; } }
        public class ExampleInput2 { public string Data2 { get; set; } }
        public class ExampleInput3 { public string Data3 { get; set; } }
        public class ExampleInput4 { public string Data4 { get; set; } }
        public class ExampleInput5 { public string Data5 { get; set; } }
        public class ExampleOutput { public string Result { get; set; } }

        private readonly Mock<IConverter1To5<ExampleInput1, ExampleInput2, ExampleInput3, ExampleInput4, ExampleInput5, ExampleOutput>> _mockConverter;
        private readonly ExampleInput1 _validInput1;
        private readonly ExampleInput2 _validInput2;
        private readonly ExampleInput3 _validInput3;
        private readonly ExampleInput4 _validInput4;
        private readonly ExampleInput5 _validInput5;
        private readonly ExampleOutput _expectedOutput;

        public IConverter1To5Tests()
        {
            _mockConverter = new Mock<IConverter1To5<ExampleInput1, ExampleInput2, ExampleInput3, ExampleInput4, ExampleInput5, ExampleOutput>>();

            _validInput1 = new ExampleInput1 { Data1 = "Test data 1" };
            _validInput2 = new ExampleInput2 { Data2 = "Test data 2" };
            _validInput3 = new ExampleInput3 { Data3 = "Test data 3" };
            _validInput4 = new ExampleInput4 { Data4 = "Test data 4" };
            _validInput5 = new ExampleInput5 { Data5 = "Test data 5" };
            _expectedOutput = new ExampleOutput { Result = "Converted data" };

            _mockConverter.Setup(c => c.Convert(It.IsAny<ExampleInput1>(), It.IsAny<ExampleInput2>(),
                                                 It.IsAny<ExampleInput3>(), It.IsAny<ExampleInput4>(),
                                                 It.IsAny<ExampleInput5>()))
                          .Returns(_expectedOutput);
        }

        [Fact]
        public void Convert_ValidInputs_ReturnsCorrectOutput()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput1, _validInput2, _validInput3, _validInput4, _validInput5);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(_expectedOutput.Result, result.Result);
        }

        [Fact]
        public void Convert_ValidInputs_ReturnsNonNull()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput1, _validInput2, _validInput3, _validInput4, _validInput5);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Convert_CorrectOutputType_ReturnsOutputType()
        {
            // Act
            var result = _mockConverter.Object.Convert(_validInput1, _validInput2, _validInput3, _validInput4, _validInput5);

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
            var emptyInput4 = new ExampleInput4 { Data4 = string.Empty };
            var emptyInput5 = new ExampleInput5 { Data5 = string.Empty };
            var expectedEmptyOutput = new ExampleOutput { Result = string.Empty };

            _mockConverter.Setup(c => c.Convert(emptyInput1, emptyInput2, emptyInput3, emptyInput4, emptyInput5))
                          .Returns(expectedEmptyOutput);

            // Act
            var result = _mockConverter.Object.Convert(emptyInput1, emptyInput2, emptyInput3, emptyInput4, emptyInput5);

            // Assert
            Assert.Equal(expectedEmptyOutput.Result, result.Result);
        }
    }
}
