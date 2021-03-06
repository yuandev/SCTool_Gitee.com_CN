using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace NSW.StarCitizen.Tools.Update
{
    public interface IUpdateRepository : IDisposable
    {
        string Name { get; }
        string Repository { get; }
        UpdateRepositoryType Type { get; }
        string? CurrentVersion { get; }
        IEnumerable<UpdateInfo>? UpdateReleases { get; }
        UpdateInfo? LatestUpdateInfo { get; }

        Task<IEnumerable<UpdateInfo>> GetAllAsync(CancellationToken cancellationToken);
        Task<IEnumerable<UpdateInfo>> RefreshUpdatesAsync(CancellationToken cancellationToken);
        Task<UpdateInfo?> GetLatestAsync(CancellationToken cancellationToken);
        Task<string> DownloadAsync(UpdateInfo updateInfo, string? downloadPath, CancellationToken cancellationToken, IDownloadProgress downloadProgress);
        Task<bool> CheckAsync(CancellationToken cancellationToken);
        void SetCurrentVersion(string version);
        UpdateInfo? UpdateCurrentVersion(string? fallbackVersionName);

        bool IsMonitorStarted { get; }
        int MonitorRefreshTime { get; }
        void MonitorStart(int refreshTime);
        void MonitorStop();

        event EventHandler<string> MonitorStarted;
        event EventHandler<string> MonitorStopped;
        event EventHandler<string> MonitorNewVersion;
    }
}
