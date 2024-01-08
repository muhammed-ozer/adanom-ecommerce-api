using System.Globalization;

namespace Adanom.Ecommerce.API
{
    public static class AsyncHelper
    {
        #region Fields

        private static readonly TaskFactory _taskFactory;

        #endregion

        #region Ctor

        static AsyncHelper()
        {
            _taskFactory =
                new TaskFactory(
                    CancellationToken.None,
                    TaskCreationOptions.None,
                    TaskContinuationOptions.None,
                    TaskScheduler.Default);
        }

        #endregion

        #region RunSync

        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;

            return _taskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;

                return func();
            })
            .Unwrap()
            .GetAwaiter()
            .GetResult();
        }

        public static void RunSync(Func<Task> func)
        {
            var cultureUi = CultureInfo.CurrentUICulture;
            var culture = CultureInfo.CurrentCulture;

            _taskFactory.StartNew(() =>
            {
                Thread.CurrentThread.CurrentCulture = culture;
                Thread.CurrentThread.CurrentUICulture = cultureUi;

                return func();
            })
            .Unwrap()
            .GetAwaiter()
            .GetResult();
        } 

        #endregion
    }
}
