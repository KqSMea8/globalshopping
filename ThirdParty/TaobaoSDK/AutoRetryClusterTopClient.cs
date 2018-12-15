using Baichuan.Api.Cluster;

namespace Baichuan.Api
{
    public class AutoRetryClusterTopClient : AutoRetryTopClient
    {
        public AutoRetryClusterTopClient(string serverUrl, string appKey, string appSecret)
            : base(serverUrl, appKey, appSecret)
        {
            ITopClient internalClient = new AutoRetryTopClient(serverUrl, appKey, appSecret);
            ClusterManager.InitRefreshThread(internalClient);
        }

        public AutoRetryClusterTopClient(string serverUrl, string appKey, string appSecret, string format)
            : base(serverUrl, appKey, appSecret, format)
        {
            ITopClient internalClient = new AutoRetryTopClient(serverUrl, appKey, appSecret);
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
