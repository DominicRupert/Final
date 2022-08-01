using System.Collections.Generic;

namespace Final.Interfaces
{
  public interface IRepository<TItem, TId>
  {
    List<TItem> GetAll();
    TItem GetById(TId id);
  }
}