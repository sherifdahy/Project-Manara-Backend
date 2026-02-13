using App.Core.Entities.Interfaces;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;

namespace App.Infrastructure.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }

    // ======================== GetById ========================
    public async Task<T?> GetByIdAsync(object id, CancellationToken cancellationToken = default)
        => await _context.Set<T>().FindAsync(new[] { id }, cancellationToken);

    // ======================== GetAll ========================
    public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default)
        => await _context.Set<T>().ToListAsync(cancellationToken);

    // ======================== Distinct ========================
    public async Task<List<string>> GetDistinctAsync(
        Expression<Func<T, string>> column,
        CancellationToken cancellationToken = default)
        => await _context.Set<T>()
            .Select(column)
            .Distinct()
            .ToListAsync(cancellationToken);

    // ======================== Find ========================
    public async Task<T?> FindAsync(
        Expression<Func<T, bool>> criteria,
        Func<IQueryable<T>, IQueryable<T>>? includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            query = includes(query);

        return await query.SingleOrDefaultAsync(criteria, cancellationToken);
    }

    // ======================== FindAll ========================
    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
            .Where(criteria)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        Func<IQueryable<T>, IQueryable<T>>? includes = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            query = includes(query);

        return await query.Where(criteria).ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        int? skip = null,
        int? take = null,
        string? orderBy = null,
        string? direction = null,
        CancellationToken cancellationToken = default)
    {
        return await FindAllAsync(
            criteria,
            includes: null,
            skip, take, orderBy, direction,
            cancellationToken);
    }

    public async Task<IEnumerable<T>> FindAllAsync(
        Expression<Func<T, bool>> criteria,
        Func<IQueryable<T>, IQueryable<T>>? includes = null,
        int? skip = null,
        int? take = null,
        string? orderBy = null,
        string? direction = null,
        CancellationToken cancellationToken = default)
    {
        IQueryable<T> query = _context.Set<T>();

        if (includes != null)
            query = includes(query);

        query = query.Where(criteria);

        if (!string.IsNullOrWhiteSpace(orderBy))
        {
            var dir = string.IsNullOrWhiteSpace(direction) ? "asc" : direction;
            query = query.OrderBy($"{orderBy} {dir}");
        }

        if (skip.HasValue)
            query = query.Skip(skip.Value);

        if (take.HasValue)
            query = query.Take(take.Value);

        return await query.ToListAsync(cancellationToken);
    }

    // ======================== Add ========================
    public async Task<T> AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddAsync(entity, cancellationToken);
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(
        IEnumerable<T> entities,
        CancellationToken cancellationToken = default)
    {
        await _context.Set<T>().AddRangeAsync(entities, cancellationToken);
        return entities;
    }

    // ======================== Update ========================
    public T Update(T entity)
    {
        _context.Update(entity);
        return entity;
    }

    public bool UpdateRange(IEnumerable<T> entities)
    {
        _context.UpdateRange(entities);
        return true;
    }

    // ======================== Delete ========================
    public void Delete(T entity)
        => _context.Set<T>().Remove(entity);

    public void DeleteRange(IEnumerable<T> entities)
        => _context.Set<T>().RemoveRange(entities);

    // ======================== Count ========================
    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
        => await _context.Set<T>().CountAsync(cancellationToken);

    public async Task<int> CountAsync(
        Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken = default)
        => await _context.Set<T>().CountAsync(criteria, cancellationToken);

    // ======================== Max ========================
    public async Task<long> MaxAsync(
        Expression<Func<T, long>> column,
        CancellationToken cancellationToken = default)
    {
        if (!await _context.Set<T>().AnyAsync(cancellationToken))
            return 0;

        return await _context.Set<T>().MaxAsync(column, cancellationToken);
    }

    public async Task<long> MaxAsync(
        Expression<Func<T, bool>> criteria,
        Expression<Func<T, long>> column,
        CancellationToken cancellationToken = default)
    {
        var query = _context.Set<T>().Where(criteria);

        if (!await query.AnyAsync(cancellationToken))
            return 0;

        return await query.MaxAsync(column, cancellationToken);
    }

    // ======================== Exist ========================
    public async Task<bool> IsExistAsync(
        Expression<Func<T, bool>> criteria,
        CancellationToken cancellationToken = default)
        => await _context.Set<T>().AnyAsync(criteria, cancellationToken);

    // ======================== Last ========================
    public async Task<T?> LastAsync(
        Expression<Func<T, bool>> criteria,
        Expression<Func<T, object>> orderBy,
        CancellationToken cancellationToken = default)
    {
        return await _context.Set<T>()
            .Where(criteria)
            .OrderByDescending(orderBy)
            .FirstOrDefaultAsync(cancellationToken);
    }
}