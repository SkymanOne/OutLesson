using System;

namespace OutLesson.DataLayer.Repositories
{
	public class UnitOfWork : IDisposable
	{
		private readonly ApplicationContext db = new ApplicationContext();

		private bool disposed;
		private PostRepository postRepository;
		private TagRepository tagRepository;

		public TagRepository Tags
		{
			get
			{
				if (tagRepository == null)
					tagRepository = new TagRepository(db);
				return tagRepository;
			}
		}

		public PostRepository Posts
		{
			get
			{
				if (postRepository == null)
					postRepository = new PostRepository(db);
				return postRepository;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Save()
		{
			db.SaveChanges();
		}

		public virtual void Dispose(bool disposing)
		{
			if (!disposed)
			{
				if (disposing)
					db.Dispose();
				disposed = true;
			}
		}
	}
}