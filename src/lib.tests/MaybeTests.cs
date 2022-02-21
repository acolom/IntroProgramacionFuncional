using System;
using Xunit;

namespace lib.tests
{
    public class MaybeTests
    {
        [Fact]
        public void CheckIdentity()
        {
            Maybe<int> maybeInt = 1;

            Assert.True(maybeInt.Success);
            Assert.True(maybeInt.Value == 1);
        }

        [Fact]
        public void CheckIdentity_Ok()
        {
            Maybe<int> maybeInt = Maybe<int>.Ok(1);

            Assert.True(maybeInt.Success);
            Assert.True(maybeInt.Value == 1);
        }

        [Fact]
        public void If_Access_Value_On_Fail_Expect_Exception()
        {
            var failed = Maybe<int>.None();
            Assert.Throws<InvalidOperationException>(() => failed.Value);
        }

        [Fact]
        public void Bind_Test_Execution()
        {
            Maybe<int> maybeInt = Maybe<int>.Ok(1);
            var returnSuccess = (int a) => Maybe<int>.Ok(a);

            var result = maybeInt
                .Bind((a) => returnSuccess(a))
                .Bind(returnSuccess);

            Assert.True(result.Success);
        }

        [Fact]
        public void Match_Bind_Execution_Success()
        {
            Maybe<int> maybeInt = Maybe<int>.Ok(1);
            Func<int, Maybe<int>> returnFailure = (int a) => Maybe<int>.None();
            var returnSuccess = (int a) => Maybe<int>.Ok(a);
            
            var executed = false;
            var markExecuted = (int a) =>
            {
                executed = true;
                return Maybe<int>.Ok(a);
            };

            maybeInt
                .Bind((a) => returnSuccess(a))
                .Bind(returnSuccess)
                .Bind(markExecuted);

            Assert.True(executed);
        }

        [Fact]
        public void Match_Bind_Execution_Failure()
        {
            Maybe<int> maybeInt = Maybe<int>.Ok(1);
            Func<int, Maybe<int>> returnFailure = (int a) => Maybe<int>.None();
            var returnSuccess = (int a) => Maybe<int>.Ok(a);
            var executed = false;
            var markExecuted = (int a) =>
            {
                executed = true;
                return Maybe<int>.Ok(a);
            };

            maybeInt
                .Bind((a) => returnSuccess(a))
                .Bind(returnFailure)
                .Bind(markExecuted);

            Assert.False(executed);
        }


        [Fact]
        public void Match_Test_Execution_Success()
        {
            Maybe<int> maybeInt = Maybe<int>.Ok(1);
            var returnSuccess = (int a) => Maybe<int>.Ok(a);
            var executed = false;
            var markExecuted = (int a) =>
            {
                executed = true;
            };

            maybeInt
                .Bind((a) => returnSuccess(a))
                .Bind(returnSuccess)
                .Match(markExecuted);

            Assert.True(executed);
        }

        [Fact]
        public void Match_Test_Execution_Failure()
        {
            Maybe<int> maybeInt = Maybe<int>.Ok(1);
            var returnSuccess = (int a) => Maybe<int>.Ok(a);
            var returnFailure = (int a) => Maybe<int>.None();
            var executed = false;
            var markExecuted = (int a) =>
            {
                executed = true;
            };

            maybeInt
                .Bind((a) => returnSuccess(a))
                .Bind(returnFailure)
                .Match(markExecuted);

            Assert.False(executed);
        }
    }
}