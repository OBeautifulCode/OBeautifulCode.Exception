// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Error.Recipes.Test
{
    using System;
    using System.Collections;
    using System.Diagnostics.CodeAnalysis;

    using FakeItEasy;

    using FluentAssertions;

    using Xunit;

    public static class ExceptionExtensionsTest
    {
        [Fact]
        public static void AddErrorCode__Should_throw_ArgumentNullException_with_errorCode_added_to_Data___When_parameter_exception_is_null()
        {
            // Arrange
            var errorCode = A.Dummy<string>();

            // Act
            var actual = Record.Exception(() => ExceptionExtensions.AddErrorCode(null, errorCode));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("exception");
            actual.Data.Keys.Should().Contain(Constants.ExceptionDataKeyForErrorCode);
            actual.Data[Constants.ExceptionDataKeyForErrorCode].Should().Be(errorCode);
        }

        [Fact]
        public static void AddErrorCode__Should_throw_ArgumentNullException_with_errorCode_added_to_Data___When_parameter_exception_Data_is_null()
        {
            // Arrange
            var errorCode = A.Dummy<string>();
            var exception = new ExceptionWithNoData();

            // Act
            var actual = Record.Exception(() => exception.AddErrorCode(errorCode));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("exception.Data");
            actual.Data.Keys.Should().Contain(Constants.ExceptionDataKeyForErrorCode);
            actual.Data[Constants.ExceptionDataKeyForErrorCode].Should().Be(errorCode);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void AddErrorCode__Should_throw_ArgumentNullException_without_errorCode_added_to_Data___When_parameter_errorCode_is_null()
        {
            // Arrange
            var exception = new ArgumentException();

            // Act
            var actual = Record.Exception(() => exception.AddErrorCode(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("errorCode");
            actual.Data.Keys.Should().NotContain(Constants.ExceptionDataKeyForErrorCode);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void AddErrorCode__Should_throw_ArgumentNullException_without_errorCode_added_to_Data___When_parameter_errorCode_is_white_space()
        {
            // Arrange
            var errorCode = "  \r\n  ";
            var exception = new ArgumentException();

            // Act
            var actual = Record.Exception(() => exception.AddErrorCode(errorCode));

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("errorCode");
            actual.Message.Should().Contain("white space");
            actual.Data.Keys.Should().NotContain(Constants.ExceptionDataKeyForErrorCode);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void AddErrorCode__Should_throw_ArgumentException_with_errorCode_added_to_Data___When_parameter_exception_Data_already_contains_ExceptionDataKeyForErrorCode()
        {
            // Arrange
            var errorCode = " a good code ";
            var exception = new ArgumentException();
            exception.Data[Constants.ExceptionDataKeyForErrorCode] = A.Dummy<object>();

            // Act
            var actual = Record.Exception(() => exception.AddErrorCode(errorCode));

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("exception.Data.Keys");
            actual.Message.Should().Contain(Constants.ExceptionDataKeyForErrorCode);
            actual.Data.Keys.Should().Contain(Constants.ExceptionDataKeyForErrorCode);
            actual.Data[Constants.ExceptionDataKeyForErrorCode].Should().Be(errorCode);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void AddErrorCode__Should_throw_ArgumentException_with_errorCode_added_to_Data___When_parameter_exception_Data_already_contains_ExceptionDataKeyForErrorCodesVector()
        {
            // Arrange
            var errorCode = " a good code";
            var exception = new ArgumentException();
            exception.Data[Constants.ExceptionDataKeyForErrorCodesVector] = A.Dummy<object>();

            // Act
            var actual = Record.Exception(() => exception.AddErrorCode(errorCode));

            // Assert
            actual.Should().BeOfType<ArgumentException>();
            actual.Message.Should().Contain("exception.Data.Keys");
            actual.Message.Should().Contain(Constants.ExceptionDataKeyForErrorCodesVector);
            actual.Data.Keys.Should().Contain(Constants.ExceptionDataKeyForErrorCode);
            actual.Data[Constants.ExceptionDataKeyForErrorCode].Should().Be(errorCode);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void AddErrorCode__Should_not_throw___When_all_parameters_are_valid()
        {
            // Arrange
            var errorCode = " a good code ";
            var exception = new ArgumentException();

            // Act
            var actual = Record.Exception(() => exception.AddErrorCode(errorCode));

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        public static void GetErrorCode___Should_throw_ArgumentNullException___When_parameter_exception_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ExceptionExtensions.GetErrorCode(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("exception");
        }

        [Fact]
        public static void GetErrorCode___Should_return_null___When_parameter_exception_Data_is_null()
        {
            // Arrange,
            var exception = new ExceptionWithNoData();

            // Act
            var actual = exception.GetErrorCode();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCode___Should_return_null___When_error_code_was_not_added_to_exception()
        {
            // Arrange,
            var exception = new ArgumentException();

            // Act
            var actual = exception.GetErrorCode();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCode___Should_return_error_code___When_error_code_was_added_to_exception()
        {
            // Arrange,
            var exception = new ArgumentException();
            var expected = "this-is-an-ERROR_CODE";
            exception.AddErrorCode(expected);

            // Act
            var actual = exception.GetErrorCode();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        public static void GetErrorCodesVector___Should_throw_ArgumentNullException___When_parameter_exception_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ExceptionExtensions.GetErrorCodesVector(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("exception");
        }

        [Fact]
        public static void GetErrorCodesVector___Should_return_null___When_parameter_exception_Data_is_null()
        {
            // Arrange,
            var exception = new ExceptionWithNoData();

            // Act
            var actual = exception.GetErrorCodesVector();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCodesVector___Should_return_null___When_error_code_was_not_added_to_exception_and_no_InnerException_nor_InnerExceptions()
        {
            // Arrange,
            var exception = new ArgumentException();

            // Act
            var actual = exception.GetErrorCodesVector();

            // Assert
            actual.Should().BeNull();
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCodesVector___Should_return_error_code_of_exception___When_error_code_was_added_to_exception_and_no_InnerException_nor_InnerExceptions()
        {
            // Arrange,
            var exception = new ArgumentException();
            var errorCode = A.Dummy<string>();
            exception.AddErrorCode(errorCode);

            // Act
            var actual = exception.GetErrorCodesVector();

            // Assert
            actual.Should().Be(errorCode);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCodesVector___Should_return_vector___When_there_are_InnerException_and_InnerExceptions()
        {
            // Arrange
            var exceptionF = new ArgumentException().AddErrorCode("ErrorF");
            var exceptionE = new AggregateException(exceptionF).AddErrorCode("ErrorE");
            var exceptionD = new ArgumentException().AddErrorCode("ErrorD");
            var exceptionC = new ArgumentException(A.Dummy<string>(), exceptionD).AddErrorCode("ErrorC");
            var exceptionG = new ArgumentException().AddErrorCode("ErrorG");
            var exceptionB = new AggregateException(exceptionC, exceptionE, exceptionG).AddErrorCode("ErrorB");
            var exceptionA = new ArgumentException(A.Dummy<string>(), exceptionB).AddErrorCode("ErrorA");

            var expected = "ErrorA -> ErrorB -> [ErrorC -> ErrorD, ErrorE -> ErrorF, ErrorG]";

            // Act
            var actual = exceptionA.GetErrorCodesVector();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCodesVector___Should_return_vector___When_there_are_InnerException_and_InnerExceptions_but_not_all_have_error_codes_scenario_1()
        {
            // Arrange
            var exceptionF = new ArgumentException().AddErrorCode("ErrorF");
            var exceptionE = new AggregateException(exceptionF).AddErrorCode("ErrorE");
            var exceptionD = new ArgumentException().AddErrorCode("ErrorD");
            var exceptionC = new ArgumentException(A.Dummy<string>(), exceptionD).AddErrorCode("ErrorC");
            var exceptionG = new ArgumentException().AddErrorCode("ErrorG");
            var exceptionB = new AggregateException(exceptionC, exceptionE, exceptionG);
            var exceptionA = new ArgumentException(A.Dummy<string>(), exceptionB).AddErrorCode("ErrorA");

            var expected = "ErrorA -> [ErrorC -> ErrorD, ErrorE -> ErrorF, ErrorG]";

            // Act
            var actual = exceptionA.GetErrorCodesVector();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCodesVector___Should_return_vector___When_there_are_InnerException_and_InnerExceptions_but_not_all_have_error_codes_scenario_2()
        {
            // Arrange
            var exceptionF = new ArgumentException();
            var exceptionE = new AggregateException(exceptionF).AddErrorCode("ErrorE");
            var exceptionD = new ArgumentException().AddErrorCode("ErrorD");
            var exceptionC = new ArgumentException(A.Dummy<string>(), exceptionD);
            var exceptionG = new ArgumentException();
            var exceptionB = new AggregateException(exceptionC, exceptionE, exceptionG).AddErrorCode("ErrorB");
            var exceptionA = new ArgumentException(A.Dummy<string>(), exceptionB).AddErrorCode("ErrorA");

            var expected = "ErrorA -> ErrorB -> [ErrorD, ErrorE]";

            // Act
            var actual = exceptionA.GetErrorCodesVector();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCodesVector___Should_return_vector___When_there_are_InnerException_and_InnerExceptions_but_not_all_have_error_codes_scenario_3()
        {
            // Arrange
            var exceptionF = new ArgumentException();
            var exceptionE = new AggregateException(exceptionF).AddErrorCode("ErrorE");
            var exceptionD = new ArgumentException();
            var exceptionC = new ArgumentException(A.Dummy<string>(), exceptionD);
            var exceptionG = new ArgumentException();
            var exceptionB = new AggregateException(exceptionC, exceptionE, exceptionG).AddErrorCode("ErrorB");
            var exceptionA = new ArgumentException(A.Dummy<string>(), exceptionB).AddErrorCode("ErrorA");

            var expected = "ErrorA -> ErrorB -> ErrorE";

            // Act
            var actual = exceptionA.GetErrorCodesVector();

            // Assert
            actual.Should().Be(expected);
        }

        [Fact]
        [SuppressMessage("Microsoft.Usage", "CA2208:InstantiateArgumentExceptionsCorrectly", Justification = "For testing purposes only.")]
        public static void GetErrorCodesVector___Should_return_vector___When_there_are_InnerException_and_InnerExceptions_but_not_all_have_error_codes_scenario_4()
        {
            // Arrange
            var exceptionF = new ArgumentException();
            var exceptionE = new AggregateException(exceptionF);
            var exceptionD = new ArgumentException().AddErrorCode("ErrorD");
            var exceptionC = new ArgumentException(A.Dummy<string>(), exceptionD);
            var exceptionG = new ArgumentException();
            var exceptionB = new AggregateException(exceptionC, exceptionE, exceptionG).AddErrorCode("ErrorB");
            var exceptionA = new ArgumentException(A.Dummy<string>(), exceptionB).AddErrorCode("ErrorA");

            var expected = "ErrorA -> ErrorB -> ErrorD";

            // Act
            var actual = exceptionA.GetErrorCodesVector();

            // Assert
            actual.Should().Be(expected);
        }

        [SuppressMessage("Microsoft.Usage", "CA2237:MarkISerializableTypesWithSerializable", Justification = "For testing purposes only.")]
        [SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "For testing purposes only.")]
        [SuppressMessage("Microsoft.Design", "CA1064:ExceptionsShouldBePublic", Justification = "For testing purposes only.")]
        private class ExceptionWithNoData : Exception
        {
            public override IDictionary Data => null;
        }
    }
}
