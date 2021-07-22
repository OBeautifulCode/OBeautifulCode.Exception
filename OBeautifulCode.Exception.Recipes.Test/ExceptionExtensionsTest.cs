// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionExtensionsTest.cs" company="OBeautifulCode">
//   Copyright (c) OBeautifulCode 2018. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace OBeautifulCode.Exception.Recipes.Test
{
    using System;

    using FluentAssertions;

    using OBeautifulCode.Assertion.Recipes;

    using Xunit;

    public static class ExceptionExtensionsTest
    {
        [Fact]
        public static void Rethrow___Should_throw_ArgumentNullException___When_parameter_exception_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ExceptionExtensions.Rethrow(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("exception");
        }

        [Fact]
        public static void Rethrow___Should_rethrow_exception___When_called()
        {
            try
            {
                // Arrange, Act
                TestRethrowMethod1();
            }
            catch (Exception ex)
            {
                // Assert
                ex.AsTest().Must().BeOfType<InvalidOperationException>();
                ex.Message.AsTest().Must().BeEqualTo("something went wrong rethrow");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowMethod2");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowMethod3");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowMethod4");
            }
        }

        [Fact]
        public static void RethrowInnerExceptionOrElseRethrow___Should_throw_ArgumentNullException___When_parameter_exception_is_null()
        {
            // Arrange, Act
            var actual = Record.Exception(() => ExceptionExtensions.RethrowInnerExceptionOrElseRethrow(null));

            // Assert
            actual.Should().BeOfType<ArgumentNullException>();
            actual.Message.Should().Contain("exception");
        }

        [Fact]
        public static void RethrowInnerExceptionOrElseRethrow___Should_rethrow_exception___When_there_is_no_inner_exception()
        {
            try
            {
                // Arrange, Act
                TestRethrowInnerExceptionMissingMethod1();
            }
            catch (Exception ex)
            {
                // Assert
                ex.AsTest().Must().BeOfType<InvalidOperationException>();
                ex.Message.AsTest().Must().BeEqualTo("something went wrong rethrow inner exception missing");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowInnerExceptionMissingMethod2");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowInnerExceptionMissingMethod3");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowInnerExceptionMissingMethod4");
            }
        }

        [Fact]
        public static void RethrowInnerExceptionOrElseRethrow___Should_rethrow_inner_exception___When_there_is_an_inner_exception()
        {
            try
            {
                // Arrange, Act
                TestRethrowInnerExceptionMethod1();
            }
            catch (Exception ex)
            {
                // Assert
                ex.AsTest().Must().BeOfType<InvalidOperationException>();
                ex.Message.AsTest().Must().BeEqualTo("something went wrong rethrow inner exception exists");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowInnerExceptionMethod2");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowInnerExceptionMethod3");
                ex.StackTrace.AsTest().Must().ContainString("TestRethrowInnerExceptionMethod4");
            }
        }

        private static void TestRethrowMethod1()
        {
            try
            {
                TestRethrowMethod2();
            }
            catch (Exception ex)
            {
                ex.Rethrow();
            }
        }

        private static void TestRethrowMethod2()
        {
            TestRethrowMethod3();
        }

        private static void TestRethrowMethod3()
        {
            TestRethrowMethod4();
        }

        private static void TestRethrowMethod4()
        {
            throw new InvalidOperationException("something went wrong rethrow");
        }

        private static void TestRethrowInnerExceptionMissingMethod1()
        {
            try
            {
                TestRethrowInnerExceptionMissingMethod2();
            }
            catch (Exception ex)
            {
                ex.RethrowInnerExceptionOrElseRethrow();
            }
        }

        private static void TestRethrowInnerExceptionMissingMethod2()
        {
            TestRethrowInnerExceptionMissingMethod3();
        }

        private static void TestRethrowInnerExceptionMissingMethod3()
        {
            TestRethrowInnerExceptionMissingMethod4();
        }

        private static void TestRethrowInnerExceptionMissingMethod4()
        {
            throw new InvalidOperationException("something went wrong rethrow inner exception missing");
        }

        private static void TestRethrowInnerExceptionMethod1()
        {
            try
            {
                TestRethrowInnerExceptionMethod2();
            }
            catch (Exception ex)
            {
                ex.RethrowInnerExceptionOrElseRethrow();
            }
        }

        private static void TestRethrowInnerExceptionMethod2()
        {
            try
            {
                TestRethrowInnerExceptionMethod3();
            }
            catch (Exception ex)
            {
                throw new NotSupportedException("will be ignored", ex);
            }
        }

        private static void TestRethrowInnerExceptionMethod3()
        {
            TestRethrowInnerExceptionMethod4();
        }

        private static void TestRethrowInnerExceptionMethod4()
        {
            throw new InvalidOperationException("something went wrong rethrow inner exception exists");
        }
    }
}
