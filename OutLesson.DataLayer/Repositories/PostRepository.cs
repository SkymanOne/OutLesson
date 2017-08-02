using System.Collections.Generic;
using System.Data.Entity;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer.Repositories
{
	public class PostRepository : IRepostory<Post>
	{
		private readonly ApplicationContext db;

		public PostRepository(ApplicationContext context)
		{
			db = context;
		}

		public void Create(Post item)
		{
			db.Posts.Add(item);
		}

		public void Delete(int id)
		{
			var post = db.Posts.Find(id);
			if (post != null)
				db.Posts.Remove(post);
		}

		public Post Get(int id)
		{
			return db.Posts.Find(id);
		}

		public IEnumerable<Post> GetAll()
		{
			return db.Posts;
		}

		public void Update(Post item)
		{
			db.Entry(item).State = EntityState.Modified;
		}
	}
}