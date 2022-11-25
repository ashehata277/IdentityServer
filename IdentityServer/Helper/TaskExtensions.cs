
namespace IdentityServer.Helper.TaskExtension
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


            }
        }
        public static async Task<T> Await<T>(this Task<T> task)
        {
            try
            {
                T? res = default(T);
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
                T? res = default(T);
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