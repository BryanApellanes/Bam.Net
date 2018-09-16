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
