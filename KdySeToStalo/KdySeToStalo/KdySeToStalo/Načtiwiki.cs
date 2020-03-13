using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.IO;
using System.Text;

using Xamarin.Forms;

namespace KdySeToStalo
{
    public class Načtiwiki : ContentPage
    {
        public Načtiwiki()
        {
			#region 
			var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Načtiwiki)).Assembly;
			Stream streamWiki = assembly.GetManifestResourceStream("KdySeToStalo.wiki.txt");

			string text = "";
			using (var reader = new StreamReader(streamWiki))
			{
				text = reader.ReadToEnd();
			}
			#endregion

		}
	}
}