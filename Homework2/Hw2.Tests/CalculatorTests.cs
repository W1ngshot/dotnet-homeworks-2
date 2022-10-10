//using Hw2;

using Hw2;
using Xunit;

namespace Hw2Tests
{
    public class CalculatorTests
    {
        [Theory]
        [InlineData(15, 5, CalculatorOperation.Plus, 20)]
        [InlineData(15, 5, CalculatorOperation.Minus, 10)]
        [InlineData(15, 5, CalculatorOperation.Multiply, 75)]
        [InlineData(15, 5, CalculatorOperation.Divide, 3)]
        public void TestAllOperations(int value1, int value2, CalculatorOperation operation, int expectedValue)
        {
            var actualValue = Calculator.Calculate(value1, operation, value2);
            
            Assert.Equal(expectedValue, actualValue);
        }
        
        [Fact]
        public void TestInvalidOperation()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                Calculator.Calculate(1, CalculatorOperation.Undefined, 2));
        }

        [Fact]
        public void TestDividingNonZeroByZero()
        {
            var actualResult = Calculator.Calculate(5, CalculatorOperation.Divide, 0);
            
            Assert.Equal(double.PositiveInfinity, actualResult);
        }

        [Fact]
        public void TestDividingZeroByNonZero()
        {
            var actualResult = Calculator.Calculate(0, CalculatorOperation.Divide, 5);
            
            Assert.Equal(0, actualResult);
        }
        
        [Fact]
        public void TestDividingZeroByZero()
        {
            var actualResult = Calculator.Calculate(0, CalculatorOperation.Divide, 0);
            
            Assert.Equal(double.NaN, actualResult);
        }
    }
}