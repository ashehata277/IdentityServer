
namespace SharedWeb.Helpers
{
    public static class TaskExtensions
    {
        public static async void Await(this Task task)
        {
            try
            {
                await task;
            }
            catch (Exception)
            {
                // ignored
            }
        }
        public static async Task<T?> Await<T>(this Task<T> task)
        {
            try
            {
                T? res;
                res = await task;
                return res;
            }
            catch (Exception)
            {
                return default(T);

            }
        }
        public static async void Await<T>(this Task<T> task,Action<T> successCallBack, Action failureCallBack)
        {
            try
            {
                T? res;
                res = await task;
               successCallBack(res);
            }
            catch (Exception)
            {
                failureCallBack();

            }
        }
    }
}