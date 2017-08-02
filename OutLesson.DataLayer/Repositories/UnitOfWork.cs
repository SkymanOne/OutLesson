using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutLesson.DataLayer.Repositories
{
	public class UnitOfWork : IDisposable
	{
		private ApplicationContext db = new ApplicationContext();
		private TagRepository tagRepository;
		private PostRepository postRepository;

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

		public void Save()
		{
			db.SaveChanges();
		}

		private bool disposed = false;

		public virtual void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					db.Dispose();
				}
				this.disposed = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}
	}
}
