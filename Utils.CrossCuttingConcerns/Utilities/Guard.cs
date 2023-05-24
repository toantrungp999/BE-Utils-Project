namespace Utils.CrossCuttingConcerns.Utilities
{
    public static class Guard
    {
        /// <summary>
        /// Throw an exception with a message if the object is null.
        /// </summary>
        public static void ThrowIfNull<TExceptionType>(object obj, string message) where TExceptionType : Exception
        {
            if (obj != null)
            {
                return;
            }

            if (!(Activator.CreateInstance(typeof(TExceptionType), message) is Exception exceptionObj))
            {
                throw new ArgumentNullException(message);
            }

            throw exceptionObj;
        }

        /// <summary>
        /// Throw an exception with a message if the condition is true.
        /// </summary>
        public static void ThrowByCondition<TExceptionType>(bool condition, string message) where TExceptionType : Exception
        {
            if (!condition)
            {
                return;
            }

            if (!(Activator.CreateInstance(typeof(TExceptionType), message) is Exception exceptionObj))
            {
                throw new ArgumentException(message);
            }

            throw exceptionObj;
        }

        /// <summary>
        /// Execute an action if the object is null.
        /// </summary>
        public static void DoIfNull(object obj, Action action)
        {
            if (obj != null)
            {
                return;
            }

            action();
        }

        /// <summary>
        /// Execute an action if the condition is true.
        /// </summary>
        public static void DoByCondition(bool condition, Action action)
        {
            if (!condition)
            {
                return;
            }

            action();
        }
    }
}
