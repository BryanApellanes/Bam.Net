/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bam.Net.Data
{
    public abstract class FormatPart: IHasParameterInfos
    {
        public FormatPart()
        {
            this.parameters = new List<IParameterInfo>();
            this.StartNumber = 1;
        }
        
        public int? StartNumber { get; set; }
        public int? NextNumber
        {
            get
            {
                return StartNumber + Parameters.Count();
            }
        }
		Func<string, string> _columnNameProvider;
		public Func<string, string> ColumnNameFormatter
		{
			get
			{
				if (_columnNameProvider == null)
				{
					_columnNameProvider = (c) =>
					{
						return string.Format("[{0}]", c);
					};
				}

				return _columnNameProvider;
			}
			set
			{
				_columnNameProvider = value;
			}
		}
        /// <summary>
        /// Adds the specified IParameterInfo
        /// </summary>
        /// <param name="parameter"></param>
        public void AddParameter(IParameterInfo parameter)
        {
            this.parameters.Add(parameter);
        }

        public abstract string Parse();

        List<IParameterInfo> parameters;
        #region IHasParameterInfos Members

        public IParameterInfo[] Parameters
        {
            get
            {
                return parameters.ToArray();
            }
            set
            {
                this.parameters = new List<IParameterInfo>();
                this.parameters.AddRange(value);
            }
        }

        #endregion
    }
}
