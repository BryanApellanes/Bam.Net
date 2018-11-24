using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Logging.Counters
{
    public class Timer: Stats
    {
        public override object Value
        {
            get
            {
                return Duration;
            }
            set
            {
                Duration = (int)Value;
            }
        }

        public Instant StartTime { get; set; }
        public Instant EndTime { get; set; }
        int _duration;
        public int Duration
        {
            get
            {
                if(StartTime != null && EndTime != null)
                {
                    _duration = StartTime.DiffInMilliseconds(EndTime);
                }
                return _duration;
            }
            set
            {
                _duration = value;
            }
        }
        
        public bool Ended
        {
            get
            {
                return EndTime != null;
            }
        }

        public static int Time(Action action)
        {
            return Time(8.RandomLetters(), action);
        }

        public static int Time(string name, Action action)
        {
            Timer timer = Start(name);
            action();
            return timer.End();
        }

        public Timer Start()
        {
            StartTime = new Instant();
            return this;
        }

        /// <summary>
        /// Ends this instance by setting the EndTime and Duration.
        /// </summary>
        /// <returns></returns>
        public int End()
        {
            if(EndTime == null)
            {
                EndTime = new Instant();
                Duration = StartTime.DiffInMilliseconds(EndTime);
                Value = Duration;
            }
            return Duration;
        }

        /// <summary>
        /// Gets the number of milliseconds that have passed since this timer was started.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.InvalidOperationException">
        /// Timer already ended
        /// or
        /// Timer not started
        /// </exception>
        public int SoFar()
        {
            if(Ended)
            {
                throw new InvalidOperationException("Timer already ended");
            }

            if(StartTime == null)
            {
                throw new InvalidOperationException("Timer not started");
            }

            return StartTime.DiffInMilliseconds(new Instant());
        }

        public override string ToString()
        {
            return $"{Name}: {Duration}ms";
        }

        public static int End(Timer timer)
        {
            timer.Duration = timer.End();
            return timer.Duration;
        }

        public static Timer Start(string name)
        {
            return new Timer
            {
                Name = name,
                StartTime = new Instant()
            };
        }
    }
}
