/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Net.Translation.Yandex
{
    public class YandexTranslationResponse
    {
        public YandexTranslationResponse()
        {
            this.text = new string[] { string.Empty };
        }
        public YandexResponseCode code { get; set; }
        public string lang { get; set; }
        public string[] text { get; set; }
    }
}
