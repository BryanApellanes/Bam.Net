/*
	Copyright © Bryan Apellanes 2015  
*/
using System;

namespace Bam.Net.Schema.Org
{
	///<summary>The act of an agent communicating (service provider, social media, etc) their arrival by registering/confirming for a previously reserved service (e.g. flight check in) or at a place (e.g. hotel), possibly resulting in a result (boarding pass, etc).Related actions:CheckOutAction: The antagonym of CheckInAction.ArriveAction: Unlike ArriveAction, CheckInAction implies that the agent is informing/confirming the start of a previously reserved service.ConfirmAction: Unlike ConfirmAction, CheckInAction implies that the agent is informing/confirming the *start* of a previously reserved service rather than its validity/existence.</summary>
	public class CheckInAction: CommunicateAction
	{
	}
}
