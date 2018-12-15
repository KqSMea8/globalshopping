using Baichuan.Api.Cluster;

namespace Baichuan.Api
{
    /// <summary>
    /// 异地多活自动分配集群客户端。
    /// </summary>
    public class ClusterTopClient : DefaultTopClient
    {
        public ClusterTopClient(string serverUrl, string appKey, string appSecret)
            : base(serverUrl, appKey, appSecret)
        {
            ITopClient internalClient = new AutoRetryTopClient(serverUrl, appKey, appSecret);
            ClusterManager.InitRefreshThread(internalClient);
        }

        public ClusterTopClient(string serverUrl, string appKey, string appSecret, string format)
            : base(serverUrl, appKey, appSecret, format)
        {
            ITopClient internalClient = new AutoRetryTopClient(serverUrl, appKey, appSecret, format);
            ClusterManager.InitRefreshThread(internalClient);
        }

        internal override string GetServerUrl(string serverUrl, string apiName, string session)
        {
            DnsConfig dnsConfig = ClusterManager.GetDnsConfigFromCache();
            if (dnsConfig == null)
            {
                return serverUrl;
            }
            else
            {
                return dnsConfig.GetBestVipUrl(serverUrl, apiName, session);
            }
        }

        internal override string GetSdkVersion()
        {
            return SDK_VERSION_CLUSTER;
        }
    }
}
