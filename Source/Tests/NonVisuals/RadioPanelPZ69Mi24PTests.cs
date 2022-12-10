﻿using NonVisuals.Radios;
using System;
using Xunit;

namespace Tests.NonVisuals
{
    public class RadioPanelPZ69Mi24PTests
    {

        [Theory]
        [InlineData(0, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 6, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 7, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(1, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(1, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 7, RadioPanelPZ69Base.Increase)]
        [InlineData(1, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(1, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(2, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(2, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(2, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(2, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(3, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(3, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(3, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(3, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 8, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(4, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 3, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 8, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 9, RadioPanelPZ69Base.Decrease)]

        [InlineData(5, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(5, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(5, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(5, 3, RadioPanelPZ69Base.Increase)]
        [InlineData(5, 4, RadioPanelPZ69Base.Increase)]
        [InlineData(5, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(5, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(5, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(5, 8, RadioPanelPZ69Base.Decrease)]
        [InlineData(5, 9, RadioPanelPZ69Base.Decrease)]

        [InlineData(6, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(6, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(6, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(6, 3, RadioPanelPZ69Base.Increase)]
        [InlineData(6, 4, RadioPanelPZ69Base.Increase)]
        [InlineData(6, 5, RadioPanelPZ69Base.Increase)]
        [InlineData(6, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(6, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(6, 8, RadioPanelPZ69Base.Decrease)]
        [InlineData(6, 9, RadioPanelPZ69Base.Decrease)]

        [InlineData(7, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(7, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(7, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(7, 3, RadioPanelPZ69Base.Increase)]
        [InlineData(7, 4, RadioPanelPZ69Base.Increase)]
        [InlineData(7, 5, RadioPanelPZ69Base.Increase)]
        [InlineData(7, 6, RadioPanelPZ69Base.Increase)]
        [InlineData(7, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(7, 8, RadioPanelPZ69Base.Decrease)]
        [InlineData(7, 9, RadioPanelPZ69Base.Decrease)]

        [InlineData(8, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(8, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(8, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(8, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(8, 4, RadioPanelPZ69Base.Increase)]
        [InlineData(8, 5, RadioPanelPZ69Base.Increase)]
        [InlineData(8, 6, RadioPanelPZ69Base.Increase)]
        [InlineData(8, 7, RadioPanelPZ69Base.Increase)]
        [InlineData(8, 8, RadioPanelPZ69Base.Decrease)]
        [InlineData(8, 9, RadioPanelPZ69Base.Decrease)]

        [InlineData(9, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(9, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(9, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(9, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(9, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(9, 5, RadioPanelPZ69Base.Increase)]
        [InlineData(9, 6, RadioPanelPZ69Base.Increase)]
        [InlineData(9, 7, RadioPanelPZ69Base.Increase)]
        [InlineData(9, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(9, 9, RadioPanelPZ69Base.Decrease)]

        public void GetCommandDirectionFor0To9Dials_ShouldReturn_ExpectedValues(int desiredDialPosition, uint actualDialPosition, string expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69Mi24P.GetCommandDirectionFor0To9Dials(desiredDialPosition, actualDialPosition));
        }
    }
}
