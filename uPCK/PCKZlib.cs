﻿using System;
using System.IO;
using System.Windows.Forms;
using zlib;

namespace uPCK
{
	public static class PCKZlib
	{
		public static byte[] Decompress(byte[] bytes, int size)
		{
			byte[] output = new byte[size];
			ZOutputStream zos = new ZOutputStream(new MemoryStream(output));
			try
			{
				CopyStream(new MemoryStream(bytes), zos, size);
			}
			catch
			{
				MessageBox.Show("Bad zlib data");
                //return Decompress(bytes, size+=2);
			}
			return output;
		}

		public static byte[] Compress(byte[] bytes, int CompressionLevel)
		{
			MemoryStream ms = new MemoryStream();
			ZOutputStream zos = new ZOutputStream(ms, CompressionLevel);
			CopyStream(new MemoryStream(bytes), zos, bytes.Length);
			zos.finish();
			return ms.ToArray().Length < bytes.Length ? ms.ToArray() : bytes;
		}

		public static void CopyStream(Stream input, Stream output, int Size)
		{
			byte[] buffer = new byte[Size];
			int len;
			while ((len = input.Read(buffer, 0, Size)) > 0)
			{
				output.Write(buffer, 0, len);
			}
			output.Flush();
		}
	}
}
