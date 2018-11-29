﻿using System.Threading;
using System.Threading.Tasks;
using NLog.Extensions.Logging;
using Wikiled.Console.Arguments;
using Wikiled.ConsoleApp.Commands;
using Wikiled.ConsoleApp.Commands.Config;

namespace Wikiled.ConsoleApp
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            NLog.LogManager.LoadConfiguration("nlog.config");
            AutoStarter starter = new AutoStarter("Twitter Bot");
            starter.Factory.AddNLog();
            starter.RegisterCommand<DiscoveryCommand, DiscoveryConfig>("Discovery");
            starter.RegisterCommand<EnrichCommand, EnrichConfig>("Enrich");
            starter.RegisterCommand<DownloadMessagesCommand, DownloadMessagesConfig>("DownloadMessages");
            starter.RegisterCommand<TwitterLoadCommand, TwitterLoadConfig>("load");
            starter.RegisterCommand<TwitterMonitorCommand, TwitterMonitorConfig>("monitor");
            await starter.Start(args, CancellationToken.None).ConfigureAwait(false);
        }
    }
}
