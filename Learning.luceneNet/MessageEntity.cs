using Lucene.Net.Documents;
using static Lucene.Net.Documents.Field;

namespace Learning.luceneNet
{
	public class MessageEntity
	{
		[Field(Index.NO, Store.YES)]
		public string Title { get; set; }

		[Field(Index.ANALYZED, Store.YES)]
		public string Content { get; set; }
	}
}
