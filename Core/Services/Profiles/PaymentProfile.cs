using AutoMapper;
using Domain.Models;

namespace Shared.Mapping
{
    public class PaymentProfile : Profile
    {
        public PaymentProfile()
        {
            CreateMap<SavedPaymentMethod, SavedPaymentDto>();
        }
    }
}
