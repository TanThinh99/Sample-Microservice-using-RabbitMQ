using AutoMapper;
using Order.Core.Models;
using Order.Logic.Persistence.Dtos;

namespace ordering_server
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<ProductTransactionDto, ProductTransaction>();

            CreateMap<ProductTransaction, TransactionResponseDto>();
        }
    }
}
