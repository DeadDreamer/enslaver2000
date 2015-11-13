using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EnslaverCore.Logic
{
    public delegate T Func<T>();
    public delegate void Action();

    public class ParameterizedLock
    {
        private object lockObject = new object();
        //MAX_PATH=260 в ASCII , чтобы не преобразовывать сразу ограничим для UNICODE
        private static int MaxLength = 260 / 4;
        public static int MillisecondsTimeout = 1000;

        private Mutex GetLock(string key)
        {
            lock (lockObject)
            {
                string _key = CheckKey(key);
                return new Mutex(false, _key);
            }
        }

        private string CheckKey(string key)
        {
            if (string.IsNullOrEmpty(key))
            {
                return Guid.NewGuid().ToString();
            }

            if (key.Length > MaxLength)
            {
                return key.Substring(key.Length - 260, 260);
            }
            return key;
        }

        public TResult RunWithLock<TResult>(string key, Func<TResult> body)
        {
            Mutex mutex = GetLock(key);
            TResult result = default(TResult);
            Exception someEx = null;
            bool waitResult = mutex.WaitOne(MillisecondsTimeout);
            try
            {
                result = body();
            }
            catch (Exception ex)
            {
                someEx = ex;
            }
            finally
            {
                if (waitResult)
                {
                    mutex.ReleaseMutex();
                }
            }
            if (someEx != null)
            {
                throw someEx;
            }
            return result;
        }

        public void RunWithLock(string key, Action body)
        {
            Mutex mutex = GetLock(key);
            bool waitResult = mutex.WaitOne(MillisecondsTimeout);
            try
            {
                body();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (waitResult)
                {
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}

