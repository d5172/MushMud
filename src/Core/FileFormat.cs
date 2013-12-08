using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Reflection;

namespace MusicCompany.Core
{
	public enum FileFormat
	{
		[Extension(".flac")]
		[MimeType("application/octect-stream|application/flac")]
		Flac,
		
		[Extension(".jpg|.jpeg")]
		[MimeType("image/jpeg|application/jpeg|application/pjpeg")]
		Jpeg,

		[Extension(".mp3")]
		[MimeType("application/octect-stream|application/mp3")]
		MP3,

		[Extension(".png")]
		[MimeType("image/png|application/png")]
		Png,
		
		[Extension(".ogg")]
		[MimeType("application/octect-stream|application/ogg")]
		Ogg,
		
		[Extension(".torrent")]
		[MimeType("application/x-bittorrent|application/octect-stream")]
		Torrent,

		[Extension(".wav")]
		[MimeType("application/octect-stream|application/wav")]
		Wave,

		[Extension(".zip")]
		[MimeType("application/zip|application/octect-stream")]
		Zip,

		Unknown
	}

	public class ExtensionAttribute : Attribute
	{
		public String[] Extensions
		{
			get;
			private set;
		}

		public ExtensionAttribute(string extensions)
		{
			this.Extensions = extensions.Split('|');
		}
	}

	public class MimeTypeAttribute : Attribute
	{
		public String[] MimeTypes
		{
			get;
			private set;
		}

		public MimeTypeAttribute(string mimeTypes)
		{
			this.MimeTypes = mimeTypes.Split('|');
		}
	}

	public static class StringExtensions
	{
		public static FileFormat ToFileFormat(this String extension)
		{
			extension = extension.ToLowerInvariant();
			FileFormat output = FileFormat.Unknown;
			string[] matchingExtensions = null;

			//Look for our string value associated with fields in this enum
			foreach (FieldInfo fi in typeof(FileFormat).GetFields())
			{
				//Check for our custom attribute
				ExtensionAttribute[] attrs = fi.GetCustomAttributes(typeof (ExtensionAttribute), false) as ExtensionAttribute[];
				if (attrs.Length > 0)
				{
					matchingExtensions = attrs[0].Extensions;

					//Check for equality then select actual enum value.
					if (matchingExtensions.Contains(extension))
					{
						output = (FileFormat)Enum.Parse(typeof(FileFormat), fi.Name);
						break;
					}
				}
			}
			return output;
		}

		public static bool IsKnownFileFormat(this String extension)
		{
			return ToFileFormat(extension) != FileFormat.Unknown;
		}
	}

	public static class FileFormatExtensions
	{
		private readonly static FileFormat[] AudioTypes = new FileFormat[]{ FileFormat.Flac, FileFormat.MP3, FileFormat.Ogg, FileFormat.Wave };
		private readonly static FileFormat[] ImageTypes = new FileFormat[]{ FileFormat.Jpeg};

		public static bool IsAudioType(this FileFormat value)
		{
			return AudioTypes.Contains(value);
		}

		public static bool IsImageType(this FileFormat value)
		{
			return ImageTypes.Contains(value);
		}

		public static bool IsHighQualityAudio(this FileFormat value)
		{
			return value == FileFormat.Flac;
		}

		public static string ToMimeType(this FileFormat value)
		{
			FieldInfo fieldInfo = typeof(FileFormat).GetField(Enum.GetName(typeof(FileFormat), value));
			MimeTypeAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(MimeTypeAttribute), false) as MimeTypeAttribute[];
			if (attributes != null && attributes.Length > 0)
			{
				return attributes[0].MimeTypes[0];
			}
			else
			{
				throw new InvalidOperationException(string.Format("{0} does not have any MimeTypeAttributes", value));
			}
		}

		public static string GetExtension(this FileFormat value)
		{
			FieldInfo fieldInfo = typeof(FileFormat).GetField(Enum.GetName(typeof(FileFormat), value));
			ExtensionAttribute[] attributes = fieldInfo.GetCustomAttributes(typeof(ExtensionAttribute), false) as ExtensionAttribute[];
			if (attributes != null && attributes.Length > 0)
			{
				return attributes[0].Extensions[0];
			}
			else
			{
				throw new InvalidOperationException(string.Format("{0} does not have any ExtensionAttributes", value));
			}
		}
	}
}
