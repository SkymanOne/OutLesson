using System.Collections.Generic;
using System.Data.Entity;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer.Repositories
{
	public class OfferPostRepository : IRepostory<OfferPost>
	{
		private ApplicationContext db;

		public OfferPostRepository(ApplicationContext context)
		{
			db = context;
		}

		public IEnumerable<OfferPost> GetAll()
		{
			return db.OfferPosts;
		}

		public OfferPost Get(int id)
		{
			return db.OfferPosts.Find(id);
		}

		public void Create(OfferPost item)
		{
			db.OfferPosts.Add(item);
		}

		public void Update(OfferPost item)
		{
			db.Entry(item).State = EntityState.Modified;
			
		}

		public void Delete(int id)
		{
			var offerPost = db.OfferPosts.Find(id);
			if (offerPost != null)
				db.OfferPosts.Remove(offerPost);
		}
	}
}