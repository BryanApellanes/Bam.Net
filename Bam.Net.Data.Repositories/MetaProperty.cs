/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Bam.Net.Data.Repositories
{
	public class MetaProperty<T> : MetaProperty
	{
		public MetaProperty(Meta meta, PropertyInfo property)
			: base(meta, property)
		{
		}

		public T TypedValue
		{
			get
			{
				return this.Meta.ReadProperty<T>(PropertyInfo);
			}
		}
	}
	public class MetaProperty
	{
		public MetaProperty(Meta meta, PropertyInfo property)
		{
			this.Meta = meta;
			this.PropertyInfo = property;
		}

		public Meta Meta { get; private set; }
		public PropertyInfo PropertyInfo { get; private set; }

		public object CurrentValue
		{
			get
			{
				return this.Meta.ReadProperty<object>(PropertyInfo);
			}
		}

		public int HighestVersion
		{
			get
			{
				return this.Meta.GetHighestVersionNumber(PropertyInfo, Meta.Hash);
			}
		}

		public MetaPropertyVersionInfo[] GetVersionInfos()
		{
			return Versions.Select(ver => GetVersionInfo(ver)).ToArray();
		}

		public MetaPropertyVersionInfo GetVersionInfo()
		{
			return GetVersionInfo(HighestVersion);
		}

		public MetaPropertyVersionInfo GetVersionInfo(int version)
		{
			MetaPropertyVersionInfo info = new MetaPropertyVersionInfo 
			{ 
				Hash = Meta.Hash,
				PropertyInfo = PropertyInfo,
				Name = PropertyInfo.Name, 
				Version = version, 
				Value = GetVersionValue(version), 
				LastWrite = VersionDates[version] 
			};
			return info;
		}

		public object GetVersionValue(int version)
		{
			return Meta.ReadPropertyVersion<object>(PropertyInfo, version);
		}

		public int[] Versions
		{
			get
			{
				return Meta.GetVersions(PropertyInfo, Meta.Hash);
			}
		}

		Dictionary<int, DateTime> _versionDates;
		object _versionDatesLock = new object();
		public Dictionary<int, DateTime> VersionDates
		{
			get
			{
				return _versionDatesLock.DoubleCheckLock(ref _versionDates, () => Meta.GetVersionDates(PropertyInfo, Meta.Hash));
			}
		}

		public void RollValueBack(DateTime to)
		{
			int version = 1;
			DateTime selected = to;
			foreach(int curVer in VersionDates.Keys)
			{
				DateTime currentDate = VersionDates[version];
				if(currentDate < to)
				{
					if (selected >= currentDate)
					{
						selected = currentDate;
						version = curVer;
					}
				}
			}

			SetValueToVersion(version);
		}

		public void SetValue(object propertyValue)
		{
			lock(_versionDatesLock)
			{
				Meta.WriteProperty(PropertyInfo, propertyValue);
				_versionDates = null;
			}
		}

		public void SetValueToVersion(int version)
		{
			lock(_versionDatesLock)
			{
				object value = Meta.ReadPropertyVersion<object>(PropertyInfo, version);
				Meta.WriteProperty(PropertyInfo, value);
				_versionDates = null;
			}
		}
	}
}
