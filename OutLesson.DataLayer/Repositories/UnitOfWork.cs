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


		//public UnitOfWork(ApplicationContext context)
		//{
		//	db = context;
		//}

		public ApplicationContext DataContext
		{
			get { return _db; }
		}

		public TagRepository Tags => _tagRepository ?? (_tagRepository = new TagRepository(_db));

		public PostRepository Posts
		{
			get
			{
				if (_postRepository == null)
					_postRepository = new PostRepository(_db);
				return _postRepository;
			}
		}

		public OfferPostRepository OfferPosts
		{
			get
			{
				if (_offerPostRepository == null)
					_offerPostRepository = new OfferPostRepository(_db);
				return _offerPostRepository;

			}
		}

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