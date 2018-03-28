using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using MailEnable.CustomCounters.Program.Core;

namespace MailEnable.CustomCounters.Program.Workers
{
    public abstract class WorkerBase : IWorker
    {
        private int _poolingInterval;
        protected Timer _poolingTimer;

        public virtual string Name { get; set; }
        public virtual string Description { get; set; }

        public virtual int PoolingInterval
        {
            get
            {
                return _poolingInterval;
            }
            set
            {
                if (value == 0)
                    throw new ArgumentException("PoolingInvertal cannot be 0 (zero) !!!");

                if (value <= 1000)
                    value *= 1000;

                this._poolingInterval = value;
            }
        }

        /// <summary>
        /// Default constructor
        /// </summary>
        public WorkerBase(int poolingInterval)
        {
            this.PoolingInterval = poolingInterval;
        }

        /// <summary>
        /// Used to start worker
        /// </summary>
        public virtual void Start()
        {
        }

        /// <summary>
        /// Used to stop worker
        /// </summary>
        public virtual void Stop()
        {
        }

        /// <summary>
        /// Enable timer when processing of an item is executing
        /// </summary>
        protected virtual void EnablePooling()
        {
            this._poolingTimer.Enabled = true;
        }

        /// <summary>
        /// Disable timer when processing of an item is executing
        /// </summary>
        protected virtual void DisablePooling()
        {
            this._poolingTimer.Enabled = false;
        }
    }
}
