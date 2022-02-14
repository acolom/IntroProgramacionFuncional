using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lib
{
    public struct Either<TError, TSuccess>
    {
        private readonly TError? error;
        private readonly TSuccess? value;
        private readonly bool success;

        private bool Success { get { return success; } }

        internal TSuccess Value
        {
            get
            {
                if (!this.success)
                    throw new InvalidOperationException("Cannot access Value because is in failure state");

                return value!;
            }
        }

        internal TError Error
        {
            get
            {
                if (this.success)
                    throw new InvalidOperationException("Cannot access Value because is in valid state");

                return error!;
            }
        }

        private Either(TError? error, TSuccess? value, bool success)
        {
            this.error = error;
            this.value = value;
            this.success = success;
        }

        public static Either<TError, TSuccess> Ok(TSuccess success) => new Either<TError, TSuccess>(default, success, true);

        public static Either<TError, TSuccess> Failure(TError error) => new Either<TError, TSuccess>(error, default, false);

        public Either<TError, TResult> Map<TResult>(Func<TSuccess, TResult> transform)
        {
            return this.Success
                ? Either<TError, TResult>.Ok(transform(this.Value))
                : Either<TError, TResult>.Failure(this.Error);

        }

        public Either<TError, TResult> Bind<TResult>(Func<TSuccess, Either<TError, TResult>> transform)
        {
            return this.Success
              ? transform(this.Value)
              : Either<TError, TResult>.Failure(this.Error);

        }

        public void Match(Action<TSuccess> success, Action<TError> error = null)
        {
            if (this.Success)
                success(this.Value);
            else
                error?.Invoke(this.Error);
        }

        public TResult Match<TResult>(Func<TSuccess, TResult> success, Func<TError, TResult> none)
        {
            return this.Success
                ? success(this.Value)
                : none(this.Error);
        }
    }
}
