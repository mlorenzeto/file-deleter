using FileDeleterService.Core.Command;
using FileDeleterService.Core.Scheduling;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FileDeleterService.Core.Tasks
{
    public class FileDeleterTask : IScheduledTask
    {
        public string Schedule => Configuration.Instance().Get("TaskCron:DeleterTime");

        public async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            await Task.Run(() =>
            {
                new FileDeleterCommand().Execute();
            });
        }
    }
}
