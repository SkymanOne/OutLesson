using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer.Repositories
{
	public class PostRepository : IRepostory<Post>
	{
		private readonly ApplicationContext _db;

		public PostRepository(ApplicationContext context)
		{
			_db = context;
		}

		public void Create(Post item)
		{
			lock (_db)
			{
				_db.Entry(item).State = EntityState.Added;
			}
		}

		public void Delete(int id)
		{
			var post = _db.Posts.Find(id);
		    if (post != null)
		        _db.Posts.Remove(post);
		}

		public Post Get(int id)
		{
			return _db.Posts.Find(id);
		}

		public IEnumerable<Post> GetAllByDescending()
		{
		    try
		    {
		        return from u in _db.Posts orderby u.Id descending select u;
		    }
		    catch (Exception e)
		    {
                Console.WriteLine(e.StackTrace);
		        return null;
		    }
		}

	    public IEnumerable<Post> GetAll()
	    {
	        return _db.Posts;
	    }

		public void Update(Post item)
		{
			_db.Entry(item).State = EntityState.Modified;
		}
	}
}