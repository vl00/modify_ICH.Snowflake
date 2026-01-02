using System.Threading.Tasks;

namespace ICH.Snowflake
{
    public interface IDistributedSupport
    {
        /// <summary>
        /// 获取下一个可用的机器id
        /// </summary>
        Task<int> GetNextWorkId();

        /// <summary>
        /// 刷新机器id的存活状态
        /// </summary>
        Task RefreshAlive();
    }
}