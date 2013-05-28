using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RomanNumerals;

namespace Tests
{
    [TestClass]
    public class RomanNumberTest
    {
        //Since can repeat only three times the max number is 3999 = MMMCMXCIX
        private readonly Dictionary<RomanNumber, int> validCornerCases = new Dictionary<RomanNumber, int> {
                                                  { new RomanNumber("I"), 1}, { new RomanNumber("II"), 2}, { new RomanNumber("III"), 3}, { new RomanNumber("IV"), 4},                                                  { new RomanNumber("V"), 5},
                                                  { new RomanNumber("VI"), 6}, { new RomanNumber("VII"), 7}, { new RomanNumber("VIII"), 8}, { new RomanNumber("IX"), 9},
                                                  { new RomanNumber("X"), 10}, { new RomanNumber("XI"), 11}, { new RomanNumber("XII"), 12}, { new RomanNumber("XIII"), 13},
                                                  { new RomanNumber("XIV"), 14}, { new RomanNumber("XV"), 15}, { new RomanNumber("XVI"), 16}, { new RomanNumber("XVII"), 17},
                                                  { new RomanNumber("XVIII"), 18}, { new RomanNumber("XIX"), 19}, { new RomanNumber("XX"), 20}, { new RomanNumber("XXI"), 21},
                                                  { new RomanNumber("XXV"), 25}, { new RomanNumber("XXX"), 30}, { new RomanNumber("XXXV"), 35}, { new RomanNumber("XL"), 40},
                                                  { new RomanNumber("XLV"), 45}, { new RomanNumber("XLIX"), 49}, { new RomanNumber("L"), 50}, { new RomanNumber("LX"), 60},
                                                  { new RomanNumber("LXIX"), 69}, { new RomanNumber("LXX"), 70}, { new RomanNumber("LXXVI"), 76}, { new RomanNumber("LXXX"), 80},
                                                  { new RomanNumber("XC"), 90}, { new RomanNumber("XCIX"), 99}, { new RomanNumber("C"), 100}, { new RomanNumber("CL"), 150},
                                                  { new RomanNumber("CC"), 200}, { new RomanNumber("CCC"), 300}, { new RomanNumber("CD"), 400}, { new RomanNumber("CDXCIX"), 499},
                                                  { new RomanNumber("D"), 500}, { new RomanNumber("DC"), 600}, { new RomanNumber("DCLXVI"), 666}, { new RomanNumber("DCC"), 700},
                                                  { new RomanNumber("DCCC"), 800}, { new RomanNumber("CM"), 900}, { new RomanNumber("CMXCIX"), 999}, { new RomanNumber("M"), 1000},
                                                  { new RomanNumber("MCDXLIV"), 1444}, { new RomanNumber("MDCLXVI"), 1666}, { new RomanNumber("MCMXC"), 1990}, { new RomanNumber("MCMXCIX"), 1999},
                                                  { new RomanNumber("MM"), 2000}, { new RomanNumber("MMI"), 2001}, { new RomanNumber("MMX"), 2010}, { new RomanNumber("MMD"), 2500},
                                                  { new RomanNumber("MMM"), 3000}, { new RomanNumber("MMMDCCCLXXXVIII"), 3888}, { new RomanNumber("MMMCMXCIX"), 3999}
                                             };
        #region Constructor Tests
        [TestMethod]
        public void Array_constructor_should_assign_array_to_field()
        {
            //Arrange
            var expected = new [] {new RomanSymbol("I"), new RomanSymbol("D")};
            //Act
            var target = new RomanNumber(expected);
            //Assert
            target.Symbols.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void String_constructor_should_assign_new_array_of_symbols_to_field()
        {
            //Arrange
            var expected = new[] { new RomanSymbol("I"), new RomanSymbol("D") };
            //Act
            var target = new RomanNumber("ID");
            //Assert
            target.Symbols.Should().ContainInOrder(expected);
        }

        [TestMethod]
        public void If_empty_string_on_constructor_should_throw_exceptions()
        {
            //Act
            Action action = () => new RomanNumber("");
            //Assert
            action.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        public void If_empty_array_on_constructor_should_throw_exception()
        {
            //Act
            Action action = () => new RomanNumber(new RomanSymbol[0]);
            //Assert
            action.ShouldThrow<ArgumentNullException>();
        }

        #endregion

        [TestMethod]
        public void Test_Convert_to_decimal()
        {
            //Arrange
            
            //Act and Assert
            foreach (var romanNumber in validCornerCases.Keys)
            {
                romanNumber.ToDecimal().Should().Be(validCornerCases[romanNumber]);
            }
        }

        [TestMethod]
        public void I_before_something_but_IVX_should_be_invalid()
        {
            //Arrange
            var targets = new []
                {
                    new RomanNumber("ID"), new RomanNumber("IC"), new RomanNumber("IM"), new RomanNumber("IL")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }
        
        [TestMethod]
        public void X_before_D_or_M_should_be_invalid()
        {
            //Arrange
            var targets = new[]
                {
                   new RomanNumber("XD"), new RomanNumber("XM")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void If_Contains_more_than_one_D_or_L_or_V_should_be_invalid()
        {
            //Arrange
            var targets = new[]
                {
                   new RomanNumber("DD"), new RomanNumber("LL"), new RomanNumber("VV")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void If_Contains_more_than_Three_I_should_be_invalid()
        {
            //Arrange
            var targets = new[]
                {
                   new RomanNumber("IIII")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void If_Contains_more_than_Four_X_or_C_or_M_should_be_invalid()
        {
            //Arrange
            var targets = new[]
                {
                   new RomanNumber("XXXIXX"), new RomanNumber("CCCXCC"), new RomanNumber("MMMCMM")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void If_Contains_Four_Consecutive_X_or_C_or_M_should_be_invalid()
        {
            //Arrange
            var targets = new[]
                {
                    new RomanNumber("XXXX"), new RomanNumber("CCCC"), new RomanNumber("MMMM")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void If_Contains_invalid_symbol_should_be_invalid()
        {
            //Arrange
            var targets = new[]
                {
                    new RomanNumber("A")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void V_D_L_should_never_subtract_anything()
        {
            //Arrange
            var targets = new[]
                {
                    new RomanNumber("VX"), new RomanNumber("DM"), new RomanNumber("LC")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void If_a_symbol_was_subtracted_it_should_not_be_added_later()
        {
            //Arrange
            var targets = new[]
                {
                    new RomanNumber("IXII"), new RomanNumber("XCXX"), new RomanNumber("CDCC"), new RomanNumber("CMCC")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }

        [TestMethod]
        public void Only_one_symbol_should_subtract()
        {
            //Arrange
            var targets = new[]
                {
                    new RomanNumber("IIX"),new RomanNumber("IIIX"), new RomanNumber("XXC"),new RomanNumber("XXXC"), new RomanNumber("CCD"),new RomanNumber("CCCD"), 
                    new RomanNumber("CCM"),new RomanNumber("CCCM")
                };
            //Assert
            foreach (var romanNumber in targets)
            {
                romanNumber.IsValid().Should().BeFalse();
            }
        }
    }
}
