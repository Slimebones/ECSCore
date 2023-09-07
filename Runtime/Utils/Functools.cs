using System;
using System.Linq;

namespace Slimebones.ECSCore.Utils {
    public class Functools {
        /// <summary>
        /// Binds arguments to a function.
        /// </summary>
        /// <remarks>
        /// See <see href="https://stackoverflow.com/a/27251027"/>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static Func<TResult> BindFunc<T, TResult>(Func<T, TResult> func, T arg) {
            return () => func(arg);
        }

        public static Func<T2, TResult> BindFunc<T1, T2, TResult>(
            Func<T1, T2, TResult> func,
            T1 arg
        ) {
            return t2 => func(arg, t2);
        }

        public static Func<T2, T3, TResult> BindFunc<T1, T2, T3, TResult>(
            Func<T1, T2, T3, TResult> func,
            T1 arg
        ) {
            return (t2, t3) => func(arg, t2, t3);
        }

        public static Action BindAction<T>(Action<T> func, T arg) {
            return () => func(arg);
        }

        public static Action<T2> BindAction<T1, T2>(
            Action<T1, T2> func,
            T1 arg
        ) {
            return t2 => func(arg, t2);
        }

        public static Action<T2, T3> BindAction<T1, T2, T3>(
            Action<T1, T2, T3> func,
            T1 arg
        ) {
            return (t2, t3) => func(arg, t2, t3);
        }
    }
}
