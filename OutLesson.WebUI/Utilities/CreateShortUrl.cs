using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutLesson.WebUI.Utilities
{
	public class CreateShortUrl
	{
		private Dictionary<string, string> dictionaryChar = new Dictionary<string, string>()
		{
			{"а","a"},
			{"б","b"},
			{"в","v"},
			{"г","g"},
			{"д","d"},
			{"е","e"},
			{"ё","yo"},
			{"ж","zh"},
			{"з","z"},
			{"и","i"},
			{"й","y"},
			{"к","k"},
			{"л","l"},
			{"м","m"},
			{"н","n"},
			{"о","o"},
			{"п","p"},
			{"р","r"},
			{"с","s"},
			{"т","t"},
			{"у","u"},
			{"ф","f"},
			{"х","h"},
			{"ц","ts"},
			{"ч","ch"},
			{"ш","sh"},
			{"щ","sch"},
			{"ъ",""},
			{"ы","yi"},
			{"ь",""},
			{"э","e"},
			{"ю","yu"},
			{"я","ya"},
			{" ", "-" },
			{"!", "" },
			{"?", "" },
			{",", "" }
		};


		public string ReplaceString(string input)
		{
			string output = "";

			input = input.ToLower();

			foreach (char ch in input)
			{
				var ss = "";
				if (dictionaryChar.TryGetValue(ch.ToString(), out ss))
				{
					output += ss;
				}
				else
				{
					output += ch;
				}
			}
			

			return output;
		}
	}
}