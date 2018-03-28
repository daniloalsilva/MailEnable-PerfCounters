using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailEnable.CustomCounters.Program.Core;
using MailEnable.CustomCounters.Program.Workers;

namespace MailEnable.CustomCounters.Program
{
    public class MailEnableManager
    {
        private List<IWorker> _workersList;
        private ConcurrentBag<Task> _tasksList;

        /// <summary>
        /// Default constructor
        /// </summary>
        public MailEnableManager()
        {
            this._workersList = new List<IWorker>();
            this._tasksList = new ConcurrentBag<Task>();
        }

        /// <summary>
        /// Add a new worker to be processed
        /// </summary>
        public void AddWorker(WorkerBase worker)
        {
            if (worker != null)
            {
                this._workersList.Add(worker);
            }
        }

        /// <summary>
        /// Execute all workers
        /// </summary>
        public void Run()
        {
            foreach (var worker in this._workersList)
            {
                Task task = new Task(() => worker.Start());
                task.Start();
                _tasksList.Add(task);
            }

            Task.WaitAll(_tasksList.ToArray());
        }

        /// <summary>
        /// Stop all workers
        /// </summary>
        public void Stop()
        {
            foreach (var worker in this._workersList)
            {
                worker.Stop();
            }
        }
    }
}
