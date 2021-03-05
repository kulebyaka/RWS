﻿using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace RWS.Data.Serializers
{
	public class XmlSerializer : IDataSerialize
	{
		public string Serialize(object data)
		{
			var xsSubmit = new System.Xml.Serialization.XmlSerializer(data.GetType(), new XmlRootAttribute(""));
			using var sww = new StringWriter();
			using var writer = XmlWriter.Create(sww);
			xsSubmit.Serialize(writer, data);
			return sww.ToString();
		}

		public T Deserialize<T>(string serializedData) where T : class, new()
		{
			if (string.IsNullOrWhiteSpace(serializedData))
				return null;

			var rdr = new StringReader(serializedData);
			var xsSubmit = new System.Xml.Serialization.XmlSerializer(typeof(T));

			var ttt = xsSubmit.Deserialize(rdr);
			return (T)ttt;
		}
	}
}