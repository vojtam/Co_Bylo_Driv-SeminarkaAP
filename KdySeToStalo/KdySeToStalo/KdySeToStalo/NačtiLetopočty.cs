using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using Xamarin.Forms;

namespace KdySeToStalo
{
    public class NačtiLetopočty : ContentPage
    {
		public NačtiLetopočty()
		{

			

			#region 
			var assembly = IntrospectionExtensions.GetTypeInfo(typeof(NačtiLetopočty)).Assembly;
			Stream streamRoky = assembly.GetManifestResourceStream("KdySeToStalo.letopocty.txt");

			string text = "";
			using (var reader = new StreamReader(streamRoky))
			{
				text = reader.ReadToEnd();
			}
			#endregion

			


			
		}
	}
}
