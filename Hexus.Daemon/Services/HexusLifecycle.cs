﻿using Hexus.Daemon.Configuration;

namespace Hexus.Daemon.Services;

public sealed class HexusLifecycle(HexusConfigurationManager configManager, ProcessManagerService processManager) : IHostedLifecycleService
{
    public Task StartedAsync(CancellationToken cancellationToken)
    {
        foreach (var application in configManager.Configuration.Applications.Values)
        {
            if (application is { Status: HexusApplicationStatus.Operating })
                processManager.StartApplication(application);
        }

        return Task.CompletedTask;
    }

    public Task StoppedAsync(CancellationToken cancellationToken)
    {
        foreach (var application in processManager.Application.Values)
        {
            processManager.StopApplication(application.Name);
        }

        File.Delete(configManager.Configuration.UnixSocket);

        return Task.CompletedTask;
    }

    public Task StartingAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    public Task StoppingAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    public Task StartAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
