using System.Collections.Generic;

namespace OutLesson.DataLayer.ObjectModels
{
	public class Tag
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public virtual ICollection<ApplicationUser> Autors { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
	}
}