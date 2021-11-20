using BookStore.Shop.Api.RemoteModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Shop.Api.RemoteServices.IServices
{
    public interface IBookServices
    {
        Task<(bool result, BookRM book, string error)> GetBookAsync(Guid BookId);
    }
}
