/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Net;
using Bam.Net.CoreServices.Distributed;
using Bam.Net.Testing;

namespace Bam.Net.Distributed.Tests
{
    [Serializable]
	public class DistributedUnitTests: CommandLineTestInterface
	{
		class TestRing : Ring
		{
			public bool SetSlotsWasCalled { get; set; }
			protected override void InitializeSlots(int count)
			{
				SetSlotsWasCalled = true;
			}

			protected internal override Slot CreateSlot()
			{
				throw new NotImplementedException();
			}

			public override string GetHashString(object value)
			{
				throw new NotImplementedException();
			}

			public override int GetRepositoryKey(object value)
			{
				throw new NotImplementedException();
			}

			protected override Slot FindSlotByKey(int key)
			{
				throw new NotImplementedException();
			}
		}

		[UnitTest]
		public void SettingSlotCountShouldCallSetSlots()
		{
			TestRing ring = new TestRing();
			Expect.IsFalse(ring.SetSlotsWasCalled);
			ring.SetSlotCount(360);
			Expect.IsTrue(ring.SetSlotsWasCalled);
		}

		[UnitTest]
		public void SetSlotCountShouldSetSlotLength()
		{
			ComputeRing ring = new ComputeRing();
			ring.SetSlotCount(2);
			Expect.AreEqual(2, ring.Slots.Length);
		}

		[UnitTest]
		public void SlotShouldMakeFullCircleAfterInit()
		{
			ComputeRing ring = new ComputeRing();
			int slotCount = RandomNumber.Between(8, 16);
			ring.SetSlotCount(slotCount);
			PrintSlots(ring);

			Expect.AreEqual(slotCount, ring.Slots.Length);
			double fullCircle = 360;
			double endAngle = ring.Slots[ring.Slots.Length - 1].EndAngle;
			Expect.AreEqual(fullCircle, endAngle);
		}

		[UnitTest]
		public void SlotShouldMakeFullCircleAfterInit13()
		{
			ComputeRing ring = new ComputeRing();
			int slotCount = 13;//RandomNumber.Between(8, 16);
			ring.SetSlotCount(slotCount);
			PrintSlots(ring);

			Expect.AreEqual(slotCount, ring.Slots.Length);
			double fullCircle = 360;
			double endAngle = ring.Slots[ring.Slots.Length - 1].EndAngle;
			Expect.AreEqual(fullCircle, endAngle);
		}

		private static void PrintSlots(ComputeRing ring)
		{
			ring.Slots.Each((s, i) =>
			{
				OutLineFormat("Slot {0}", ConsoleColor.Blue, i);
				OutLineFormat("\tstart angle: {0}", ConsoleColor.White, s.StartAngle);
				OutLineFormat("\t  end angle: {0}", ConsoleColor.Yellow, s.EndAngle);
				OutLineFormat("\tstart key: {0}", ConsoleColor.Cyan, s.Keyspace.Start);
				OutLineFormat("\t  end key: {0}", ConsoleColor.Cyan, s.Keyspace.End);
			});
		}

		[UnitTest]
		public void AddComputeNodeShouldAddSlot()
		{
			int slotCount = RandomNumber.Between(8, 16);
			ComputeRing ring = new ComputeRing(slotCount);
			ring.AddComputeNode(new ComputeNode());
			Expect.AreEqual(slotCount + 1, ring.Slots.Length);

			PrintSlots(ring);
		}

		[UnitTest]
		public void ComputeNodeFromCurrentHostShouldBeSelf()
		{
			ComputeNode current = ComputeNode.FromCurrentHost();
			string hostName = Dns.GetHostName();
			Expect.AreEqual(hostName, current.HostName);
			object info = current.GetInfo();
			Expect.AreEqual(hostName, info.Property<string>("HostName"));
			OutLine(info.PropertiesToString());
		}

		[UnitTest]
		public void ComputeNodeGetInfoStringsTest()
		{
			ComputeNode current = ComputeNode.FromCurrentHost();
			Dictionary<string, string> info = current.GetInfoDictionary();
			info.Keys.Each((k) =>
			{
				OutLineFormat("Key: {0}, Val: {1}", k, info[k]);
			});
		}

		public void Before()
		{
			OutLine("BEFORE RAN", ConsoleColor.Cyan);
		}

		public void After()
		{
			OutLine("AFTER RAN", ConsoleColor.Cyan);
		}

		[UnitTest]
		public void ComputeNodeFromCurrentShouldHaveHostname()
		{
			ComputeNode current = ComputeNode.FromCurrentHost();
			Expect.AreEqual(current.HostName, Dns.GetHostName());
		}

		[UnitTest("Set Slot count shouldn't overwrite existing slots")]
		public void SetSlotCountShouldKeepExistingSlots()
		{
			Before();
			ComputeRing ring = new ComputeRing();

			ComputeNode node = new ComputeNode();
			node.HostName = "HostName_".RandomLetters(4);
			ring.AddComputeNode(node);
			Expect.AreEqual(1, ring.Slots.Length);

			ComputeNode check = ring.Slots[0].GetProvider<ComputeNode>();
			Expect.IsNotNull(check);

			ring.SetSlotCount(3);

			Expect.AreEqual(3, ring.Slots.Length);
			check = ring.Slots[0].GetProvider<ComputeNode>();

			Expect.AreEqual(node.HostName, check.HostName);

			PrintSlots(ring);
			After();
		}
	}
}
