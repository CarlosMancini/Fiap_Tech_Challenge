﻿using Core.Entities;

namespace Core.Interfaces.Services
{
    public interface IService<T> where T : EntityBase
    {
        Task<IList<T>> ObterTodos();
        Task<T> ObterPorId(int id);
    }
}
