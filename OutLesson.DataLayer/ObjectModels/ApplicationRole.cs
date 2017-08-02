using Microsoft.AspNet.Identity.EntityFramework;

namespace OutLesson.DataLayer.ObjectModels
{
	public class ApplicationRole : IdentityRole
	{
		public string Description { get; set; }
	}
}