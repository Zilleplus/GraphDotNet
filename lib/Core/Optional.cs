using System;

namespace Core
{
    public class Optional<T>
    {
        public class EmptyOptionalException : Exception { }

        T val_ = default(T);
        bool hasValue = false;

        public bool HasValue
        {
            get { return hasValue; }
        }

        public T Value
        {
            get
            {
                if (hasValue) { return val_; }
                throw new EmptyOptionalException();
            }
        }

        public Optional() { }

        public Optional(T val)
        {
            val_ = val;
            hasValue = true;
        }

        public Optional<TOut> Map<TOut>(Func<T, TOut> func)
            => hasValue ? new Optional<TOut>(func(val_))
            : new Optional<TOut>();

        public Optional<TOut> Bind<TOut>(Func<T, Optional<TOut>> func)
            => hasValue ? func(val_)
            : new Optional<TOut>();
    }
}
