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

    }
}