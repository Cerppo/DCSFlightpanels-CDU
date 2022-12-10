﻿using NonVisuals.Radios;
using System;
using Xunit;

namespace Tests.NonVisuals
{
    public class RadioPanelPZ69UH1HTests
    {

        [Theory]
        [InlineData(1, true, 0)]
        [InlineData(12, true, 15)]
        [InlineData(15, true, 17)]
        [InlineData(17, true, 20)]
        [InlineData(97, true, 100)]
        
        [InlineData(1, false, 0)]
        [InlineData(12, false, 10)]
        [InlineData(15, false, 12)]
        [InlineData(17, false, 15)]
        [InlineData(97, false, 95)]
        
        //Funky values, funky results ?
        [InlineData(0, true, 2)]
        [InlineData(2, true, 5)]
        [InlineData(3, true, 0)]
        [InlineData(4, true, 0)]
        [InlineData(9, true, 0)]
        [InlineData(10, true, 12)]
        [InlineData(51, true, 0)]
        [InlineData(50, true, 52)]
        [InlineData(90, true, 92)]
        [InlineData(98, true, 0)]
        [InlineData(100, true, 0)]
        [InlineData(666, true, 0)]

        [InlineData(0, false, 97)]
        [InlineData(2, false, 0)]
        [InlineData(3, false, 0)]
        [InlineData(4, false, 0)]
        [InlineData(9, false, 0)]
        [InlineData(10, false, 7)]
        [InlineData(51, false, 0)]
        [InlineData(50, false, 47)]
        [InlineData(90, false, 87)]
        [InlineData(98, false, 0)]
        [InlineData(100, false, 0)]
        [InlineData(666, false, 0)]

        public void QuarterFrequencyStandbyAdjust_ShouldReturn_ExpectedValue(uint frequency, bool increase, uint expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69UH1H.QuarterFrequencyStandbyAdjust(frequency, increase));
        }

        [Theory]
        [InlineData(0, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 15, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 16, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 17, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 149, RadioPanelPZ69Base.Increase)]

        [InlineData(100, 117, RadioPanelPZ69Base.Increase)]

        [InlineData(116, 117, RadioPanelPZ69Base.Decrease)]
        [InlineData(116, 131, RadioPanelPZ69Base.Decrease)]
        [InlineData(116, 132, RadioPanelPZ69Base.Increase)]
        [InlineData(116, 133, RadioPanelPZ69Base.Increase)]
        [InlineData(116, 134, RadioPanelPZ69Base.Increase)]
        [InlineData(116, 147, RadioPanelPZ69Base.Increase)]
        [InlineData(116, 148, RadioPanelPZ69Base.Increase)]
        [InlineData(116, 149, RadioPanelPZ69Base.Increase)]

        [InlineData(125, 117, RadioPanelPZ69Base.Increase)]
        [InlineData(125, 131, RadioPanelPZ69Base.Decrease)]
        [InlineData(125, 132, RadioPanelPZ69Base.Decrease)]
        [InlineData(125, 133, RadioPanelPZ69Base.Decrease)]
        [InlineData(125, 134, RadioPanelPZ69Base.Decrease)]
        [InlineData(125, 147, RadioPanelPZ69Base.Increase)]
        [InlineData(125, 148, RadioPanelPZ69Base.Increase)]
        [InlineData(125, 149, RadioPanelPZ69Base.Increase)]

        [InlineData(132, 116, RadioPanelPZ69Base.Decrease)]
        [InlineData(132, 130, RadioPanelPZ69Base.Increase)]
        [InlineData(132, 131, RadioPanelPZ69Base.Increase)]
        [InlineData(132, 133, RadioPanelPZ69Base.Decrease)]
        [InlineData(132, 136, RadioPanelPZ69Base.Decrease)]
        [InlineData(132, 141, RadioPanelPZ69Base.Decrease)]
        [InlineData(132, 148, RadioPanelPZ69Base.Increase)]
        [InlineData(132, 149, RadioPanelPZ69Base.Increase)]

        [InlineData(149, 117, RadioPanelPZ69Base.Decrease)]
        [InlineData(149, 131, RadioPanelPZ69Base.Decrease)]
        [InlineData(149, 132, RadioPanelPZ69Base.Decrease)]
        [InlineData(149, 133, RadioPanelPZ69Base.Decrease)]
        [InlineData(149, 134, RadioPanelPZ69Base.Increase)]
        [InlineData(149, 147, RadioPanelPZ69Base.Increase)]
        [InlineData(149, 148, RadioPanelPZ69Base.Increase)]
        
        [InlineData(150, 148, RadioPanelPZ69Base.Increase)]
        
        [InlineData(666, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 666, RadioPanelPZ69Base.Increase)]
        [InlineData(666, 555, RadioPanelPZ69Base.Decrease)]
        [InlineData(555, 666, RadioPanelPZ69Base.Increase)]

        public void GetCommandDirectionForVhfCommDial1_ShouldReturn_ExpectedValue(uint desiredFreq, uint actualFreq, string expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69UH1H.GetCommandDirectionForVhfCommDial1(desiredFreq, actualFreq));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(116, 116)]
        [InlineData(149, 149)]
        [InlineData(666, 666)]
        public void GetCommandDirectionForVhfCommDial1_ThrowsException_For_UnexpectedValues(uint desiredFreq, uint actualFreq)
        {
            Assert.Throws<Exception>(() => RadioPanelPZ69UH1H.GetCommandDirectionForVhfCommDial1(desiredFreq, actualFreq));
        }

        [Theory]
        [InlineData(5, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(25, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(26, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(49, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(51, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(75, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(100, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(450, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(475, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(950, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(975, 0, RadioPanelPZ69Base.Decrease)]

        [InlineData(5, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(25, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(26, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(49, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(51, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(75, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(100, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(450, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(475, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(555, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(950, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(975, 1, RadioPanelPZ69Base.Decrease)]

        [InlineData(5, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(25, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(26, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(49, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(51, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(75, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(100, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(450, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(475, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(555, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(950, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(975, 2, RadioPanelPZ69Base.Decrease)]

        [InlineData(5, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(26, 25, RadioPanelPZ69Base.Increase)]
        [InlineData(49, 25, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 25, RadioPanelPZ69Base.Increase)]
        [InlineData(51, 25, RadioPanelPZ69Base.Increase)]
        [InlineData(75, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(100, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(450, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(475, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(555, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(950, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(975, 25, RadioPanelPZ69Base.Decrease)]

        [InlineData(5, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(26, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(49, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(51, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(75, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(100, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(450, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(475, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(555, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(950, 50, RadioPanelPZ69Base.Decrease)]
        [InlineData(975, 50, RadioPanelPZ69Base.Decrease)]

        [InlineData(5, 13, RadioPanelPZ69Base.Decrease)]
        [InlineData(50, 26, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 49, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 51, RadioPanelPZ69Base.Decrease)]
        [InlineData(50, 75, RadioPanelPZ69Base.Decrease)]
        [InlineData(50, 100, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 450, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 475, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 500, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 555, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 950, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 975, RadioPanelPZ69Base.Increase)]
        
        [InlineData(500, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 49, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 51, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 75, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 100, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 450, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 475, RadioPanelPZ69Base.Increase)]
        [InlineData(500, 555, RadioPanelPZ69Base.Increase)]
        [InlineData(500, 950, RadioPanelPZ69Base.Increase)]
        [InlineData(500, 975, RadioPanelPZ69Base.Increase)]

        [InlineData(0, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 49, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 51, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 75, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 100, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 450, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 475, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 500, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 950, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 975, RadioPanelPZ69Base.Increase)]

        [InlineData(65, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(65, 25, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 26, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 49, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 51, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 75, RadioPanelPZ69Base.Decrease)]
        [InlineData(65, 85, RadioPanelPZ69Base.Decrease)]
        [InlineData(65, 95, RadioPanelPZ69Base.Decrease)]

        [InlineData(95, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(95, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(95, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(95, 49, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 51, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 75, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 85, RadioPanelPZ69Base.Increase)]

        public void GetCommandDirectionForVhfCommDial2_ShouldReturn_ExpectedValue(uint desiredFreq, uint actualFreq, string expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69UH1H.GetCommandDirectionForVhfCommDial2(desiredFreq, actualFreq));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(25, 25)]
        [InlineData(95, 95)]
        [InlineData(116, 116)]
        [InlineData(149, 149)]
        [InlineData(666, 666)]
        [InlineData(975, 975)]
        [InlineData(6666, 6666)]
        public void GetCommandDirectionForVhfCommDial2_ThrowsException_For_UnexpectedValues(uint desiredFreq, uint actualFreq)
        {
            Assert.Throws<Exception>(() => RadioPanelPZ69UH1H.GetCommandDirectionForVhfCommDial2(desiredFreq, actualFreq));
        }

        [Theory]
        [InlineData(20, 21, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 22, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 23, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 24, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 27, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 28, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 29, RadioPanelPZ69Base.Decrease)]
        [InlineData(20, 30, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 31, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 32, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 33, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 34, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 35, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 36, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 37, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 38, RadioPanelPZ69Base.Increase)]
        [InlineData(20, 39, RadioPanelPZ69Base.Increase)]

        [InlineData(21, 20, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 22, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 23, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 24, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 27, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 28, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 29, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 30, RadioPanelPZ69Base.Decrease)]
        [InlineData(21, 31, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 32, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 33, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 34, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 35, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 36, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 37, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 38, RadioPanelPZ69Base.Increase)]
        [InlineData(21, 39, RadioPanelPZ69Base.Increase)]

        [InlineData(22, 20, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 21, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 23, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 24, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 27, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 28, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 29, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 30, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 31, RadioPanelPZ69Base.Decrease)]
        [InlineData(22, 32, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 33, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 34, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 35, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 36, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 37, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 38, RadioPanelPZ69Base.Increase)]
        [InlineData(22, 39, RadioPanelPZ69Base.Increase)]

        [InlineData(33, 20, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 21, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 22, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 23, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 24, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 25, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 26, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 27, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 28, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 29, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 30, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 31, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 32, RadioPanelPZ69Base.Increase)]
        [InlineData(33, 34, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 35, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 36, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 37, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 38, RadioPanelPZ69Base.Decrease)]
        [InlineData(33, 39, RadioPanelPZ69Base.Decrease)]
        
        [InlineData(39, 20, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 21, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 22, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 23, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 24, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 27, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 28, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 29, RadioPanelPZ69Base.Decrease)]
        [InlineData(39, 30, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 31, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 32, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 33, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 34, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 35, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 36, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 37, RadioPanelPZ69Base.Increase)]
        [InlineData(39, 38, RadioPanelPZ69Base.Increase)]
        public void GetCommandDirectionForUhfDial1_ShouldReturn_ExpectedValue(uint desiredFreq, uint actualFreq, string expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69UH1H.GetCommandDirectionForUhfDial1(desiredFreq, actualFreq));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(20, 20)]
        [InlineData(27, 27)]
        [InlineData(39, 39)]
        [InlineData(666, 666)]
        public void GetCommandDirectionForUhfDial1_ThrowsException_For_UnexpectedValues(uint desiredFreq, uint actualFreq)
        {
            Assert.Throws<Exception>(() => RadioPanelPZ69UH1H.GetCommandDirectionForUhfDial1(desiredFreq, actualFreq));
        }


        [Theory]
        [InlineData(0, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 5, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 6, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 7, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(1, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(1, 2, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(1, 6, RadioPanelPZ69Base.Increase)]
        [InlineData(1, 7, RadioPanelPZ69Base.Increase)]
        [InlineData(1, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(1, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(2, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(2, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(2, 3, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(2, 7, RadioPanelPZ69Base.Increase)]
        [InlineData(2, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(2, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(3, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(3, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(3, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(3, 4, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(3, 8, RadioPanelPZ69Base.Increase)]
        [InlineData(3, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(4, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 3, RadioPanelPZ69Base.Increase)]
        [InlineData(4, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 6, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 7, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 8, RadioPanelPZ69Base.Decrease)]
        [InlineData(4, 9, RadioPanelPZ69Base.Increase)]

        [InlineData(5, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(5, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(5, 2, RadioPanelPZ69Base.Increase)]
        [InlineData(5, 3, RadioPanelPZ69Base.Increase)]
        [InlineData(5, 4, RadioPanelPZ69Base.Increase)]
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
        public void GetCommandDirectionForUhfDial2_ShouldReturn_ExpectedValue(uint desiredFreq, uint actualFreq, string expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69UH1H.GetCommandDirectionForUhfDial2(desiredFreq, actualFreq));
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(5, 5)]
        [InlineData(9, 9)]
        public void GetCommandDirectionForUhfDial2_ThrowsException_For_UnexpectedValues(uint desiredFreq, uint actualFreq)
        {
            Assert.Throws<Exception>(() => RadioPanelPZ69UH1H.GetCommandDirectionForUhfDial2(desiredFreq, actualFreq));
        }


        [Theory] // same values as VhfCommDial2
        [InlineData(5, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(25, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(26, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(49, 0, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(51, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(75, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(100, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(450, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(475, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(950, 0, RadioPanelPZ69Base.Decrease)]
        [InlineData(975, 0, RadioPanelPZ69Base.Decrease)]

        [InlineData(5, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(25, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(26, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(49, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(50, 1, RadioPanelPZ69Base.Increase)]
        [InlineData(51, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(75, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(100, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(450, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(475, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(500, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(555, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(950, 1, RadioPanelPZ69Base.Decrease)]
        [InlineData(975, 1, RadioPanelPZ69Base.Decrease)]

        [InlineData(0, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 49, RadioPanelPZ69Base.Decrease)]
        [InlineData(0, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 51, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 75, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 100, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 450, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 475, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 500, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 950, RadioPanelPZ69Base.Increase)]
        [InlineData(0, 975, RadioPanelPZ69Base.Increase)]

        [InlineData(65, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(65, 25, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 26, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 49, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 51, RadioPanelPZ69Base.Increase)]
        [InlineData(65, 75, RadioPanelPZ69Base.Decrease)]
        [InlineData(65, 85, RadioPanelPZ69Base.Decrease)]
        [InlineData(65, 95, RadioPanelPZ69Base.Decrease)]

        [InlineData(95, 5, RadioPanelPZ69Base.Decrease)]
        [InlineData(95, 25, RadioPanelPZ69Base.Decrease)]
        [InlineData(95, 26, RadioPanelPZ69Base.Decrease)]
        [InlineData(95, 49, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 50, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 51, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 75, RadioPanelPZ69Base.Increase)]
        [InlineData(95, 85, RadioPanelPZ69Base.Increase)]
        public void GetCommandDirectionForUhfDial3_ShouldReturn_ExpectedValue(uint desiredFreq, uint actualFreq, string expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69UH1H.GetCommandDirectionForUhfDial3(desiredFreq, actualFreq));
        }

        [Theory] // same values as VhfCommDial2
        [InlineData(0, 0)]
        [InlineData(25, 25)]
        [InlineData(95, 95)]
        [InlineData(116, 116)]
        [InlineData(149, 149)]
        [InlineData(666, 666)]
        [InlineData(975, 975)]
        [InlineData(6666, 6666)]
        public void GetCommandDirectionForUhfDial3_ThrowsException_For_UnexpectedValues(uint desiredFreq, uint actualFreq)
        {
            Assert.Throws<Exception>(() => RadioPanelPZ69UH1H.GetCommandDirectionForUhfDial3(desiredFreq, actualFreq));
        }

        [Theory] 
        [InlineData(107, 126, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 108, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 109, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 110, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 111, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 112, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 113, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 114, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 115, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 116, RadioPanelPZ69Base.Decrease)]
        [InlineData(107, 117, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 118, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 119, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 120, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 121, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 122, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 123, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 124, RadioPanelPZ69Base.Increase)]
        [InlineData(107, 125, RadioPanelPZ69Base.Increase)]
        
        [InlineData(108, 126, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 107, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 109, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 110, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 111, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 112, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 113, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 114, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 115, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 116, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 117, RadioPanelPZ69Base.Decrease)]
        [InlineData(108, 118, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 119, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 120, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 121, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 122, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 123, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 124, RadioPanelPZ69Base.Increase)]
        [InlineData(108, 125, RadioPanelPZ69Base.Increase)]

        [InlineData(109, 126, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 107, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 108, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 110, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 111, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 112, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 113, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 114, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 115, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 116, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 117, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 118, RadioPanelPZ69Base.Decrease)]
        [InlineData(109, 119, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 120, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 121, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 122, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 123, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 124, RadioPanelPZ69Base.Increase)]
        [InlineData(109, 125, RadioPanelPZ69Base.Increase)]

        [InlineData(117, 126, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 107, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 108, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 109, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 110, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 111, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 112, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 113, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 114, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 115, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 116, RadioPanelPZ69Base.Increase)]
        [InlineData(117, 118, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 119, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 120, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 121, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 122, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 123, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 124, RadioPanelPZ69Base.Decrease)]
        [InlineData(117, 125, RadioPanelPZ69Base.Decrease)]

        [InlineData(119, 126, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 107, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 108, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 109, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 110, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 111, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 112, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 113, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 114, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 115, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 116, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 117, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 118, RadioPanelPZ69Base.Increase)]
        [InlineData(119, 120, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 121, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 122, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 123, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 124, RadioPanelPZ69Base.Decrease)]
        [InlineData(119, 125, RadioPanelPZ69Base.Decrease)]

        [InlineData(126, 107, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 108, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 109, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 110, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 111, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 112, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 113, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 114, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 115, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 116, RadioPanelPZ69Base.Decrease)]
        [InlineData(126, 117, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 118, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 119, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 120, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 121, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 122, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 123, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 124, RadioPanelPZ69Base.Increase)]
        [InlineData(126, 125, RadioPanelPZ69Base.Increase)]
        public void GetCommandDirectionForVhfNavDial1_ShouldReturn_ExpectedValue(uint desiredFreq, uint actualFreq, string expectedValue)
        {
            Assert.Equal(expectedValue, RadioPanelPZ69UH1H.GetCommandDirectionForVhfNavDial1(desiredFreq, actualFreq));
        }

        [Theory] 
        [InlineData(0, 0)]
        [InlineData(25, 25)]
        [InlineData(95, 95)]
        [InlineData(116, 116)]
        [InlineData(149, 149)]
        [InlineData(666, 666)]
        [InlineData(975, 975)]
        [InlineData(6666, 6666)]
        public void GetCommandDirectionForVhfNavDial1_ThrowsException_For_UnexpectedValues(uint desiredFreq, uint actualFreq)
        {
            Assert.Throws<Exception>(() => RadioPanelPZ69UH1H.GetCommandDirectionForVhfNavDial1(desiredFreq, actualFreq));
        }

        
    }
}
