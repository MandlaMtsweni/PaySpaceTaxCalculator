using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace TaxCalculator.Tests.Integration_Tests
{
    public class TaxBracket : TestBase
    {
        [Fact]
        public async void get_list_of_Tax_brackets()
        {
            // Act
            int expected = 6;

            //Arrange
            var result = await taxRepository.GeTaxBrackets();

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expected, result.Count);
        }
    }
}
