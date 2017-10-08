using System.Collections.Generic;
using System.Data.Entity;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer.Repositories
{
	public class OfferPostRepository : IRepostory<OfferPost>
	{
		private readonly ApplicationContext _db;

		public OfferPostRepository(ApplicationContext context)
		{
			_db = context;
		}

		public IEnumerable<OfferPost> GetAll()
		{
			return _db.OfferPosts;
		}

		public OfferPost Get(int id)
		{
			return _db.OfferPosts.Find(id);
		}

		public void Create(OfferPost item)
		{
			lock (_db)
			{
				_db.Entry(item).State = EntityState.Added;
			}
		}

		public void Update(OfferPost item)
		{
			lock (_db)
			{
				_db.Entry(item).State = EntityState.Modified;
			}
		}

		public void Delete(int id)
		{
			var offerPost = _db.OfferPosts.Find(id);
			if (offerPost != null)
				_db.OfferPosts.Remove(offerPost);
		}
	}
}