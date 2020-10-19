using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace FileSystemDemo.UnitTests
{
    public class BasicMathsTests
    {
        [Fact]
        public void Add_returnsPositive()
        {
            var basicMaths = new BasicMaths();
            var expected = 4;

            var result = basicMaths.Add(1, 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Substract_returnsNegative()
        {
            var basicMaths = new BasicMaths();
            var expected = -2;

            var result = basicMaths.Substract(1, 3);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void Mult_returnsPositive()
        {
            var basicMaths = new BasicMaths();
            var expected = 6;

            var result = basicMaths.Mult(2, 3);
            Assert.Equal(expected, result);
        }
    }
}
