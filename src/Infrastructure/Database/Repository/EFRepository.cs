﻿using Core.Entities;
using Core.Interfaces.Repository;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.Repository
{
    public class EFRepository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EFRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task Atualizar(T entidade)
        {
            var entidadeOriginal = await ObterPorId(entidade.Id);
            if (entidadeOriginal is null) throw new Exception("Contato não existe");
            entidade.DataCriacao = entidadeOriginal.DataCriacao;
            _context.Entry(entidadeOriginal).State = EntityState.Detached;
            _dbSet.Update(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task Cadastrar(T entidade)
        {
            entidade.DataCriacao = DateTime.Now;
            await _dbSet.AddAsync(entidade);
            await _context.SaveChangesAsync();
        }

        public async Task Excluir(int id)
        {
            var entidade = await ObterPorId(id);
            if (entidade is not null)
            {
                _dbSet.Remove(entidade);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<T> ObterPorId(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public async Task<IList<T>> ObterTodos()
        {
            return await _dbSet.ToListAsync();
        }
    }
}
