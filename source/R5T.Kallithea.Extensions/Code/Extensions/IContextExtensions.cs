using System;


namespace R5T.Kallithea.Extensions
{
    public static class IContextExtensions
    {
        public static T Get<T>(this IContext context, string key)
        {
            var value = context.Get(key);

            if (value is T valueAsT)
            {
                return valueAsT;
            }
            else
            {
                throw new Exception($"Value for key '{key}' was not a {typeof(T)}. Found: {value.GetType()}.");
            }
        }

        public static Type GetValueType(this IContext context, string key)
        {
            var value = context.Get(key);

            var valueType = value.GetType();
            return valueType;
        }

        public static bool IsTypeStrict<T>(this IContext context, string key)
        {
            var expectedType = typeof(T);

            var output = context.IsTypeStrict(key, expectedType);
            return output;
        }

        public static bool IsTypeStrict(this IContext context, string key, Type type)
        {
            var value = context.Get(key);

            var actualType = value.GetType();

            var isType = type == actualType;
            return isType;
        }

        public static bool IsType<T>(this IContext context, string key)
        {
            var value = context.Get(key);

            var isValueT = value is T;
            return isValueT;
        }

        //// Is there a language built-in mechanism for performing type lookup?
        //public static bool IsType(this IContext context, string key, Type type)
        //{

        //}

        public static bool Exists<T>(this IContext context, string key)
        {
            var exists = context.Exists(key);
            if (!exists)
            {
                return false;
            }

            var rightType = context.IsType<T>(key);
            return rightType;
        }

        public static void SetStrict<T>(this IContext context, string key, T value)
        {
            var existsAlready = context.Exists(key);
            if (existsAlready)
            {
                var isType = context.IsTypeStrict<T>(key);
                if (!isType)
                {
                    throw new Exception($"Unable to set value of key '{key}' to type {typeof(T)}");
                }
            }
        }
    }
}
