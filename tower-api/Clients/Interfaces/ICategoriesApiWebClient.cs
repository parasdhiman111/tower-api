using System;
using credit_work_app.Clients.Models;

namespace credit_work_app.Clients.Interfaces
{
	public interface ICategoriesApiWebClient
    { 
        Task<ApiResponse<TSuccess>> GetAsync<TSuccess>(string url);
    }
}

