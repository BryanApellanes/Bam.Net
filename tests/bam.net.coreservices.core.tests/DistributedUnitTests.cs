/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using Bam.Net.CommandLine;
using Bam.Net.Services.DataReplication;
using Bam.Net.Testing;
using Bam.Net.Testing.Unit;

namespace Bam.Net.Distributed.Tests
{
    [Serializable]
	public class DistributedUnitTests: CommandLineTool
	{
		class TestRing : Ring
		{
			public bool SetSlotsWasCalled { get; set; }
			protected override void InitializeArcs(int count)
			{
				SetSlotsWasCalled = true;
			}

			protected internal override Arc CreateArc()
			{
				throw new NotImplementedException();
			}

			public override string GetHashString(object value)
			{
				throw new NotImplementedException();
			}

			public override int GetObjectKey(object value)
			{
				throw new NotImplementedException();
			}

			protected override Arc FindArcByObjectKey(int key)
			{
				throw new NotImplementedException();
			}
		}

		[UnitTest]
		public void SettingSlotCountShouldCallSetSlots()
		{
			TestRing ring = new TestRing();
			Expect.IsFalse(ring.SetSlotsWasCalled);
			ring.SetArcCount(360);
			Expect.IsTrue(ring.SetSlotsWasCalled);
		}

		[UnitTest]
		public void SetSlotCountShouldSetSlotLength()
		{
			RepositoryRing ring = new RepositoryRing();
			ring.SetArcCount(2);
			Expect.AreEqual(2, ring.Arcs.Length);
		}

		[UnitTest]
		public void SlotShouldMakeFullCircleAfterInit()
		{
			RepositoryRing ring = new RepositoryRing();
			int slotCount = RandomNumber.Between(8, 16);
			ring.SetArcCount(slotCount);
			PrintSlots(ring);

			Expect.AreEqual(slotCount, ring.Arcs.Length);
			double fullCircle = 360;
			double endAngle = ring.Arcs[ring.Arcs.Length - 1].EndAngle;
			Expect.AreEqual(fullCircle, endAngle);
		}

		[UnitTest]
		public void SlotShouldMakeFullCircleAfterInit13()
		{
			RepositoryRing ring = new RepositoryRing();
			int slotCount = 13;//RandomNumber.Between(8, 16);
			ring.SetArcCount(slotCount);
			PrintSlots(ring);

			Expect.AreEqual(slotCount, ring.Arcs.Length);
			double fullCircle = 360;
			double endAngle = ring.Arcs[ring.Arcs.Length - 1].EndAngle;
			Expect.AreEqual(fullCircle, endAngle);
		}

		private static void PrintSlots(RepositoryRing ring)
		{
			ring.Arcs.Each((s, i) =>
			{
				Message.PrintLine("Slot {0}", ConsoleColor.Blue, i);
				Message.PrintLine("\tstart angle: {0}", ConsoleColor.White, s.StartAngle);
				Message.PrintLine("\t  end angle: {0}", ConsoleColor.Yellow, s.EndAngle);
				Message.PrintLine("\tstart key: {0}", ConsoleColor.Cyan, s.Keyspace.Start);
				Message.PrintLine("\t  end key: {0}", ConsoleColor.Cyan, s.Keyspace.End);
			});
		}

		[UnitTest]
		public void AddComputeNodeShouldAddSlot()
		{
			int slotCount = RandomNumber.Between(8, 16);
			RepositoryRing ring = new RepositoryRing(slotCount);
			ring.AddArc(new RepositoryService());
			Expect.AreEqual(slotCount + 1, ring.Arcs.Length);

			PrintSlots(ring);
		}

		public void Before()
		{
			OutLine("BEFORE RAN", ConsoleColor.Cyan);
		}

		public void After()
		{
			OutLine("AFTER RAN", ConsoleColor.Cyan);
		}


		[UnitTest("Set Slot count shouldn't overwrite existing slots")]
		public void SetSlotCountShouldKeepExistingSlots()
		{
			Before();
			RepositoryRing ring = new RepositoryRing();

            RepositoryService node = new RepositoryService();
			ring.AddArc(node);
			Expect.AreEqual(1, ring.Arcs.Length);

            RepositoryService check = (RepositoryService)ring.Arcs[0].GetServiceProvider();
			Expect.IsNotNull(check);

			ring.SetArcCount(3);

			Expect.AreEqual(3, ring.Arcs.Length);
			check = (RepositoryService)ring.Arcs[0].GetServiceProvider();
            
			PrintSlots(ring);
			After();
		}
	}
}
