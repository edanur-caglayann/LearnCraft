using LearnCraftt.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LearnCraftt.Application.Repositories;

public interface IRepository<T> where T: BaseEntity
{
    DbSet<T> Table { get; }
}