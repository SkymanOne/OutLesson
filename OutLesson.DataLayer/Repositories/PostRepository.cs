using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer.Repositories
{
	public class PostRepository : IRepostory<Post>
	{
		private ApplicationContext db;

		public PostRepository (ApplicationContext context)
		{
			this.db = context;
		}

		public void Create(Post item)
		{
			db.Posts.Add(item);
		}

		public void Delete(int id)
		{
			Post post = db.Posts.Find(id);
			if (post != null)
			{
				db.Posts.Remove(post);
			}
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
			db.Entry(item).State = System.Data.Entity.EntityState.Modified;
		}
	}
}
