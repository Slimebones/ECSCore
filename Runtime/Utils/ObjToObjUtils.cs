using System.Collections.Generic;

namespace Slimebones.ECSCore.Utils
{
    public static class ObjToObjUtils
    {
        /// <summary>
        /// Creates a dictionary out of ObjToObj items array.
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="objToObj"></param>
        /// <returns></returns>
        public static Dictionary<TKey, TValue> GetDictionary<TKey, TValue>(
            IObjToObj[] objToObj
        )
        {
            Dictionary<TKey, TValue> result = new Dictionary<TKey, TValue>();

            foreach (var item in objToObj)
            {
                result[(TKey) item.Key] = (TValue) item.Value;
            }

            return result;
        }
    }
}