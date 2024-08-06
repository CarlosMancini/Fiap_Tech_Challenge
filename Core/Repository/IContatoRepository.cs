﻿using Core.Entities;

namespace Core.Repository
{
    public interface IContatoRepository : IRepository<Contato>
    {
        IList<Contato> ObterPorRegião(int RegiaoId);
    }
}
