using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using OutLesson.DataLayer.ObjectModels;

namespace OutLesson.DataLayer.Repositories
{
	public class TagRepository : IRepostory<Tag>
	{
		private ApplicationContext db;

		public TagRepository(ApplicationContext context)
		{
			this.db = context;
		}

		public void Create(Tag item)
		{
			db.Tags.Add(item);
		}

		public void Delete(int id)
		{
			Tag tag = db.Tags.Find(id);
			if (tag != null)
			{
				db.Tags.Remove(tag);
			}
		}

		public Tag Get(int id)
		{
			return db.Tags.Find(id);
		}

		public IEnumerable<Tag> GetAll()
		{
			return db.Tags;
		}

		public void Update(Tag item)
		{
			db.Entry(item).State = EntityState.Modified;
		}
	}
}
