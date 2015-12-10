/*
	Copyright © Bryan Apellanes 2015  
*/
// model is SchemaDefinition
using System;
using System.Data;
using System.Data.Common;
using Bam.Net;
using Bam.Net.Data;
using Bam.Net.Data.Qi;

namespace Bam.Net.Instructions
{
	// schema = Instructions 
    public static class InstructionsContext
    {
		public static string ConnectionName
		{
			get
			{
				return "Instructions";
			}
		}

		public static Database Db
		{
			get
			{
				return Bam.Net.Data.Db.For(ConnectionName);
			}
		}

﻿
	public class InstructionSetQueryContext
	{
			public InstructionSetCollection Where(WhereDelegate<InstructionSetColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.InstructionSet.Where(where, db);
			}
		   
			public InstructionSetCollection Where(WhereDelegate<InstructionSetColumns> where, OrderBy<InstructionSetColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Instructions.InstructionSet.Where(where, orderBy, db);
			}

			public InstructionSet OneWhere(WhereDelegate<InstructionSetColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.InstructionSet.OneWhere(where, db);
			}
		
			public InstructionSet FirstOneWhere(WhereDelegate<InstructionSetColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.InstructionSet.FirstOneWhere(where, db);
			}

			public InstructionSetCollection Top(int count, WhereDelegate<InstructionSetColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.InstructionSet.Top(count, where, db);
			}

			public InstructionSetCollection Top(int count, WhereDelegate<InstructionSetColumns> where, OrderBy<InstructionSetColumns> orderBy, Database db = null)
			{
				return Bam.Net.Instructions.InstructionSet.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<InstructionSetColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.InstructionSet.Count(where, db);
			}
	}

	static InstructionSetQueryContext _instructionSets;
	static object _instructionSetsLock = new object();
	public static InstructionSetQueryContext InstructionSets
	{
		get
		{
			return _instructionSetsLock.DoubleCheckLock<InstructionSetQueryContext>(ref _instructionSets, () => new InstructionSetQueryContext());
		}
	}﻿
	public class SectionQueryContext
	{
			public SectionCollection Where(WhereDelegate<SectionColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Section.Where(where, db);
			}
		   
			public SectionCollection Where(WhereDelegate<SectionColumns> where, OrderBy<SectionColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Instructions.Section.Where(where, orderBy, db);
			}

			public Section OneWhere(WhereDelegate<SectionColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Section.OneWhere(where, db);
			}
		
			public Section FirstOneWhere(WhereDelegate<SectionColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Section.FirstOneWhere(where, db);
			}

			public SectionCollection Top(int count, WhereDelegate<SectionColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Section.Top(count, where, db);
			}

			public SectionCollection Top(int count, WhereDelegate<SectionColumns> where, OrderBy<SectionColumns> orderBy, Database db = null)
			{
				return Bam.Net.Instructions.Section.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<SectionColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Section.Count(where, db);
			}
	}

	static SectionQueryContext _sections;
	static object _sectionsLock = new object();
	public static SectionQueryContext Sections
	{
		get
		{
			return _sectionsLock.DoubleCheckLock<SectionQueryContext>(ref _sections, () => new SectionQueryContext());
		}
	}﻿
	public class StepQueryContext
	{
			public StepCollection Where(WhereDelegate<StepColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Step.Where(where, db);
			}
		   
			public StepCollection Where(WhereDelegate<StepColumns> where, OrderBy<StepColumns> orderBy = null, Database db = null)
			{
				return Bam.Net.Instructions.Step.Where(where, orderBy, db);
			}

			public Step OneWhere(WhereDelegate<StepColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Step.OneWhere(where, db);
			}
		
			public Step FirstOneWhere(WhereDelegate<StepColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Step.FirstOneWhere(where, db);
			}

			public StepCollection Top(int count, WhereDelegate<StepColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Step.Top(count, where, db);
			}

			public StepCollection Top(int count, WhereDelegate<StepColumns> where, OrderBy<StepColumns> orderBy, Database db = null)
			{
				return Bam.Net.Instructions.Step.Top(count, where, orderBy, db);
			}

			public long Count(WhereDelegate<StepColumns> where, Database db = null)
			{
				return Bam.Net.Instructions.Step.Count(where, db);
			}
	}

	static StepQueryContext _steps;
	static object _stepsLock = new object();
	public static StepQueryContext Steps
	{
		get
		{
			return _stepsLock.DoubleCheckLock<StepQueryContext>(ref _steps, () => new StepQueryContext());
		}
	}    }
}																								
