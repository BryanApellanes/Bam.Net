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
    public class IfElseWork: Worker
    {
        public IfElseWork() : base() { }
        public IfElseWork(string name) : base(name) { }
        public IfElseWork(string name, Worker workIfTrue, Worker elseWork)
            : base(name)
        {
            this.IfTrueWorker = workIfTrue;
            this.ElseWorker = elseWork;
        }

        public override string[] RequiredProperties
        {
            get { return new string[] { "Name" }; }
        }
        public bool Condition { get; set; }
        public Worker IfTrueWorker { get; set; }
        public Worker ElseWorker { get; set; }

        protected override WorkState Do()
        {
            return Condition ? IfTrueWorker.Do(this.Job) : ElseWorker.Do(this.Job);
        }
    }
}
