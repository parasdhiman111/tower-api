using System;
namespace credit_work_app.Clients.Interfaces
{
	public interface IWebClient
	{
        Task<TResult> GetAsync<TResult>(string url);
    }
}

