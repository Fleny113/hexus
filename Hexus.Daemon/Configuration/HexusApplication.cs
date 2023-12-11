﻿using System.ComponentModel;
using System.Diagnostics;
using YamlDotNet.Serialization;

namespace Hexus.Daemon.Configuration;

public sealed record HexusApplication
{
    public required string Name { get; set; }
    public required string Executable { get; set; }

    [DefaultValue("")] public string Arguments { get; set; } = "";
    public string WorkingDirectory { get; set; } = "";
    public HexusApplicationStatus Status { get; set; } = HexusApplicationStatus.Exited;
    [DefaultValue("")] public string Note { get; set; } = "";

    public Dictionary<string, string> EnvironmentVariables { get; set; } = [];

    #region Internal proprieties
    [YamlIgnore] internal Process? Process { get; set; }

    // Logs
    [YamlIgnore] internal SemaphoreSlim LogSemaphore { get; } = new(initialCount: 0, maxCount: 1);
    [YamlIgnore] internal CircularBuffer<string> LogBuffer { get; } = new(30);

    // Performance tracking
    [YamlIgnore] internal Dictionary<int, CpuStats> CpuStatsMap { get; } = [];
    [YamlIgnore] internal Timer? CpuUsageRefreshTimer { get; set; }
    [YamlIgnore] internal double LastCpuUsage { get; set; }

    internal record CpuStats
    {
        public TimeSpan LastTotalProcessorTime { get; set; }
        public DateTimeOffset LastGetProcessCpuUsageInvocation { get; set; }
    }

    #endregion
}
