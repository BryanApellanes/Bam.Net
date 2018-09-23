/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
namespace Bam.Net.Automation
{
    public interface IWorker
    {
        Job Job { get; set; }
        string Name { get; set; }
        int StepNumber { get; set; }
        bool Busy { get; set; }
        WorkState<T> State<T>(WorkState<T> state);
        WorkState Do(Job job);        
    }
}
