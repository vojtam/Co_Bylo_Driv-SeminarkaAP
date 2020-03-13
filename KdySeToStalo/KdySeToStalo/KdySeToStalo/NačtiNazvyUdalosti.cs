using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

using Xamarin.Forms;

namespace KdySeToStalo
{
    public class NačtiNazvyUdalosti : ContentPage
    {
        public NačtiNazvyUdalosti()
        {
			
				

				#region 
				var assemblyText = IntrospectionExtensions.GetTypeInfo(typeof(NačtiNazvyUdalosti)).Assembly;
				Stream streamText = assemblyText.GetManifestResourceStream("KdySeToStalo.udalosti_nazev.txt");

				string text = "";
				using (var reader = new StreamReader(streamText))
				{
					text = reader.ReadToEnd();
				}
				#endregion

				

				

				
			}
		}
    }
