using ServiceStack.Redis;
using System;

namespace Learning.Redis
{
	public interface IUnitOfWork : IDisposable
	{
		void TranCommand(Action<IRedisClient> command);

		void TranCommand(Action<IRedisClient> command, Action onSuccessCallback);

		void Commit();

		void Roolback();
	}
}
