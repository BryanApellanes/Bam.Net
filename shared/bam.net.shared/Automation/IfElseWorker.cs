/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Automation
{
    public class IfElseWorker : Worker
    {
        public IfElseWorker() : base() { }
        public IfElseWorker(string name) : base(name) { }
        public IfElseWorker(string name, Worker workIfTrue, Worker elseWork)
            : base(name)
        {
            this.IfTrueWorker = workIfTrue;
            this.ElseWorker = elseWork;
        }

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name" }; }
        }
        public Func<WorkState, bool> EvaluateWorkState { get; set; }
        public Worker IfTrueWorker { get; set; }
        public Worker ElseWorker { get; set; }
        public string UserPredicate { get; set; }
        protected override WorkState Do(WorkState currentWorkState)
        {
            if(Job.CurrentWorkState != currentWorkState)
            {
                Job.CurrentWorkState = currentWorkState;
            }
            return EvaluateWorkState(Job.CurrentWorkState) ? IfTrueWorker.Do(Job) : ElseWorker.Do(Job);
        }
    }
}
