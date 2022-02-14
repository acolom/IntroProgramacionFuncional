namespace lib
{
    public struct Maybe<T>
    {
        private Maybe(T value, bool success)
        {
            this.value = value;
            Success = success;
        }

        private readonly T value;

        public bool Success { get; }

        public T Value
        {
            get
            {
                if (!Success)
                    throw new InvalidOperationException("No se puede acceder a success porque la operacion no es satisfactoria");

                return this.value;
            }
        }


        public static implicit operator Maybe<T>(T value)
        {
            return new Maybe<T>(value, true);
        }

        public static Maybe<T> None() => new Maybe<T>(default(T)!, false);

        public static Maybe<T> Ok(T value) => value;

        public Maybe<TResult> Map<TResult>(Func<T, TResult> transform)
        {
            return this.Success
                ? Maybe<TResult>.Ok(transform(this.Value))
                : Maybe<TResult>.None();


            /* if (!this.Success)
                 return Maybe<TResult>.None();

             return transform(this.Value);*/

        }

        public Maybe<TResult> Bind<TResult>(Func<T, Maybe<TResult>> transform)
        {
            return this.Success
                ? transform(this.Value)
                : Maybe<TResult>.None();


            /* if (!this.Success)
                 return Maybe<TResult>.None();

             return transform(this.Value);*/

        }

        public void Match(Action<T> success, Action? none = null)
        {
            if (this.Success)
                success(this.Value);
            else
                none?.Invoke();
        }

        public TResult Match<TResult>(Func<T, TResult> success, Func<TResult> none)
        {
            return this.Success
                ? success(this.value)
                : none();
        }


    }

    public static partial class MaybeExtensions
    {
        /// <summary>
        ///     This method should be used in linq queries. We recommend using Bind method.
        /// </summary>
        public static Maybe<TR> SelectMany<T, TK, TR>(
            this Maybe<T> result,
            Func<T, Maybe<TK>> func,
            Func<T, TK, TR> project)
        {
            return result
                .Bind(func)
                .Map(x => project(result.Value, x));
        }
    }
}