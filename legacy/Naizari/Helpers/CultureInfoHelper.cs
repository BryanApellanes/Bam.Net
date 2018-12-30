/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using Naizari.Extensions;
using System.Web;
using System.Threading;

namespace Naizari.Helpers
{
    public struct Languages
    {
        public static readonly string Oromo = "om";
        public static readonly string Abkhazian= "ab";
        public static readonly string Afar	="aa";
        public static readonly string Afrikaans	="af";
        public static readonly string Albanian	="sq";
        public static readonly string Amharic	="am";
        public static readonly string Arabic	="ar";
        public static readonly string Armenian	="hy";
        public static readonly string Assamese	="as";
        public static readonly string Aymara	="ay";
        public static readonly string Azerbaijani	="az";
        public static readonly string Bashkir	="ba";
        public static readonly string Basque	="eu";
        public static readonly string Bengali	="bn";
        public static readonly string Bhutani	="dz";
        public static readonly string Bihari	="bh";
        public static readonly string Bislama	="bi";
        public static readonly string Breton	="br";
        public static readonly string Bulgarian	="bg";
        public static readonly string Burmese	="my";
        public static readonly string Byelorussian	="be";
        public static readonly string Cambodian	="km";
        public static readonly string Catalan	="ca";
        public static readonly string Chinese	="zh";
        public static readonly string Corsican	="co";
        public static readonly string Croatian	="hr";
        public static readonly string Czech	="cs";
        public static readonly string Danish	="da";
        public static readonly string Dutch	="nl";
        public static readonly string English	="en";
        public static readonly string Esperanto	="eo";
        public static readonly string Estonian	="et";
        public static readonly string Faeroese	="fo";
        public static readonly string Fiji	="fj";
        public static readonly string Finnish	="fi";
        public static readonly string French	="fr";
        public static readonly string Frisian	="fy";
        public static readonly string Galician	="gl";
        public static readonly string Georgian	="ka";
        public static readonly string German	="de";
        public static readonly string Greek	="el";
        public static readonly string Greenlandic	="kl";
        public static readonly string Guarani	="gn";
        public static readonly string Gujarati	="gu";
        public static readonly string Hausa	="ha";
        public static readonly string Hebrew ="he";
        public static readonly string Hindi	="hi";
        public static readonly string Hungarian	="hu";
        public static readonly string Icelandic	="is";
        public static readonly string Indonesian ="id";
        public static readonly string Interlingua	="ia";
        public static readonly string Interlingue	="ie";
        public static readonly string Inupiak	="ik";
        public static readonly string Inuktitut_Eskimo	="iu";
        public static readonly string Irish	="ga";
        public static readonly string Italian	="it";
        public static readonly string Japanese	="ja";
        public static readonly string Javanese	="jw";
        public static readonly string Kannada	="kn";
        public static readonly string Kashmiri	="ks";
        public static readonly string Kazakh	="kk";
        public static readonly string Kinyarwanda	="rw";
        public static readonly string Kirghiz	="ky";
        public static readonly string Kirundi	="rn";
        public static readonly string Korean	="ko";
        public static readonly string Kurdish	="ku";
        public static readonly string Laothian	="lo";
        public static readonly string Latin	="la";
        public static readonly string Latvian_Lettish	="lv";
        public static readonly string Lingala	="ln";
        public static readonly string Lithuanian	="lt";
        public static readonly string Macedonian	="mk";
        public static readonly string Malagasy	="mg";
        public static readonly string Malay	="ms";
        public static readonly string Malayalam	="ml";
        public static readonly string Maltese	="mt";
        public static readonly string Maori	="mi";
        public static readonly string Marathi	="mr";
        public static readonly string Moldavian	="mo";
        public static readonly string Mongolian	="mn";
        public static readonly string Nauru	="na";
        public static readonly string Nepali	="ne";
        public static readonly string Norwegian	="no";
        public static readonly string Occitan	="oc";
        public static readonly string Oriya	="or";
        public static readonly string Pashto="ps";
        public static readonly string Persian	="fa";
        public static readonly string Polish	="pl";
        public static readonly string Portuguese	="pt";
        public static readonly string Punjabi	="pa";
        public static readonly string Quechua	="qu";
        public static readonly string Rhaeto_Romance	="rm";
        public static readonly string Romanian	="ro";
        public static readonly string Russian	="ru";
        public static readonly string Samoan	="sm";
        public static readonly string Sangro	="sg";
        public static readonly string Sanskrit	="sa";
        public static readonly string Scots_Gaelic	="gd";
        public static readonly string Serbian	="sr";
        public static readonly string Serbo_Croatian	="sh";
        public static readonly string Sesotho	="st";
        public static readonly string Setswana	="tn";
        public static readonly string Shona	="sn";
        public static readonly string Sindhi	="sd";
        public static readonly string Singhalese	="si";
        public static readonly string Siswati	="ss";
        public static readonly string Slovak	="sk";
        public static readonly string Slovenian	="sl";
        public static readonly string Somali	="so";
        public static readonly string Spanish	="es";
        public static readonly string Sudanese	="su";
        public static readonly string Swahili	="sw";
        public static readonly string Swedish	="sv";
        public static readonly string Tagalog	="tl";
        public static readonly string Tajik	="tg";
        public static readonly string Tamil	="ta";
        public static readonly string Tatar	="tt";
        public static readonly string Tegulu	="te";
        public static readonly string Thai	="th";
        public static readonly string Tibetan	="bo";
        public static readonly string Tigrinya	=            "ti";
        public static readonly string Tonga	="to";
        public static readonly string Tsonga	="ts";
        public static readonly string Turkish	="tr";
        public static readonly string Turkmen	="tk";
        public static readonly string Twi	="tw";
        public static readonly string Uigur	="ug";
        public static readonly string Ukrainian	="uk";
        public static readonly string Urdu	="ur";
        public static readonly string Uzbek	="uz";
        public static readonly string Vietnamese	="vi";
        public static readonly string Volapuk	="vo";
        public static readonly string Welch	="cy";
        public static readonly string Wolof	="wo";
        public static readonly string Xhosa	="xh";
        public static readonly string Yiddish = "yi";
        public static readonly string Yoruba	="yo";
        public static readonly string Zhuang	="za";
        public static readonly string Zulu	="zu";
    }

    public class CultureInfoHelper
    {
        static Dictionary<string, CultureInfo> culturesByIsoName;
        static CultureInfoHelper()
        {
          
        }





    }
}
