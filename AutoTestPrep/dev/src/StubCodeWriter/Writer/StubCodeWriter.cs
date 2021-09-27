using CodeWriter;
using CodeWriter.Data;
using CodeWriter.Template.Stub;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWriter.Stub
{
	public abstract class StubCodeWriter : ICodeWriter
	{
		/// <summary>
		/// Write stub code into a file specified by <para>path</para>.
		/// </summary>
		/// <param name="path">Path of file to output </param>
		/// <param name="data">Data about stub.</param>
		public virtual void Write(string path, WriteData data)
		{
			using (Stream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
			{
				this.Write(stream, data);
			}
		}

		/// <summary>
		/// Write stub code into a stream specified by <para>stream</para>.
		/// </summary>
		/// <param name="stream">Stream to output data.</param>
		/// <param name="data">Data about stub.</param>
		public virtual void Write(Stream stream, WriteData data)
		{
			var template = this.CreateTemplate();
			var content = template.TransformText();
			using (StreamWriter writer = new StreamWriter(stream))
			{
				writer.Write(content);
			}
		}

		/// <summary>
		/// Abstract method to create template.
		/// </summary>
		/// <returns>StubTemplate class.</returns>
		protected abstract StubTemplate CreateTemplate(WriteData writeData);
	}
}
