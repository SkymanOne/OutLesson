using System;

namespace OutLesson.DataLayer.Repositories
{
	public class UnitOfWork : IDisposable
	{
		private readonly ApplicationContext _db = new ApplicationContext();

		private bool _disposed;
		private PostRepository _postRepository;
		private TagRepository _tagRepository;
		private OfferPostRepository _offerPostRepository;

		//get the repositories

		public ApplicationContext DataContext => _db;

		public TagRepository Tags => _tagRepository ?? (_tagRepository = new TagRepository(_db));

		public PostRepository Posts => _postRepository ?? (_postRepository = new PostRepository(_db));

		public OfferPostRepository OfferPosts => _offerPostRepository ?? (_offerPostRepository = new OfferPostRepository(_db));


		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Save()
		{
			_db.SaveChanges();
		}

		public virtual void Dispose(bool disposing)
		{
			if (!_disposed)
			{
				if (disposing)
					_db.Dispose();
				_disposed = true;
			}
		}
	}
}