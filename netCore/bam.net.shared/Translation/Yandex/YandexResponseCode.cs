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
    public enum YandexResponseCode
    {
        Invalid = -1,
        Ok = 200,
        ApiKeyInvalid = 401,
        ApiKeyBlocked = 402,
        DailyRequestLimitExceeded = 403,
        DailyCharacterLimitExceeded = 404
    }
}
