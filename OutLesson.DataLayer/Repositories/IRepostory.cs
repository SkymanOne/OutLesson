using System.Collections.Generic;

namespace OutLesson.DataLayer.Repositories
{
	internal interface IRepostory<T> where T : class
	{
		IEnumerable<T> GetAll();
		T Get(int id);
		void Create(T item);
		void Update(T item);
		void Delete(int id);
	}
}