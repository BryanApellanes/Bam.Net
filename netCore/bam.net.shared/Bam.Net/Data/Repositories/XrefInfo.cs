/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Data.Repositories
{
	[Serializable]
	public class XrefInfo
	{
		public XrefInfo(object left, object right, string leftHash, string rightHash)
		{
			Args.ThrowIfNull(left, "left");
			Args.ThrowIfNull(right, "right");

			this.LeftType = left.GetType().AssemblyQualifiedName;
			this.RightType = right.GetType().AssemblyQualifiedName;
			this.LeftHash = leftHash;
			this.RightHash = rightHash;
		}

		public ulong Id { get; set; }
		public string Uuid { get; set; }

		string _leftTypeName;
		public string LeftType
		{
			get
			{
				return _leftTypeName;
			}
			set
			{
				_leftTypeName = value;
			}
		}
		
		string _rightTypeName;
		public string RightType
		{
			get
			{
				return _rightTypeName;
			}
			set
			{
				_rightTypeName = value;
			}
		}

		public string LeftHash
		{
			get;
			set;
		}


		public string RightHash
		{
			get;
			set;
		}

		public Xref Load(IObjectPersister objectLoader)
		{
			object left = objectLoader.ReadByHash(Type.GetType(LeftType), LeftHash);
			object right = objectLoader.ReadByHash(Type.GetType(RightType), RightHash);
			return new Xref(left, right);
		}
	}
}
