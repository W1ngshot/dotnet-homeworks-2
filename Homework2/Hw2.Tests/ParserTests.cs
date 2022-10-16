using Hw2;
using Xunit;

namespace Hw2Tests
{
    public class ParserTests
    {
        [Theory]
        [InlineData("+", CalculatorOperation.Plus)]
        [InlineData("-", CalculatorOperation.Minus)]
        [InlineData("*", CalculatorOperation.Multiply)]
        [InlineData("/", CalculatorOperation.Divide)]
        public void TestCorrectOperations(string operation, CalculatorOperation operationExpected)
        {
            var args = new[] {"1", operation, "2"};
            
            Parser.ParseCalcArguments(args, out _, out var actualOperation, out _);
            
            Assert.Equal(operationExpected, actualOperation);
        }
        
        [Fact]
        public void TestCorrectParseFirstValue()
        {
            var args = new[] {"1", "+", "2"};
            
            Parser.ParseCalcArguments(args, out var actualVal1, out _, out _);
            
            Assert.Equal(1, actualVal1);
        }
        
        [Fact]
        public void TestCorrectParseSecondValue()
        {
            var args = new[] {"1", "+", "2"};
            
            Parser.ParseCalcArguments(args, out _, out _, out var actualVal2);
            
            Assert.Equal(2, actualVal2);
        }
        
        [Theory]
        [InlineData("f", "+", "3")]
        [InlineData("3", "+", "f")]
        [InlineData("a", "+", "f")]
        public void TestParserWrongValues(string val1, string operation, string val2)
        {
            var args = new[] {val1, operation, val2};

            Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }
        
        [Fact]
        public void TestParserWrongOperation()
        {
            var args = new[] { "1", "a", "2" };
            
            Assert.Throws<InvalidOperationException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }

        [Fact]
        public void TestParserWrongLength()
        {
            var args = new[] {"1", "*", "2", "3"};

            Assert.Throws<ArgumentException>(() => Parser.ParseCalcArguments(args, out _, out _, out _));
        }
    }
}