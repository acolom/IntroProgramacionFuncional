using System;
using Xunit;

namespace lib.tests
{
    public class MaybeTests
    {
        [Fact]

        public void If_Access_Value_On_Fail_Expect_Exception()
        {
            var failed = Maybe<int>.None();
            Assert.Throws<InvalidOperationException>(() => failed.Value);
        }

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

    }
}