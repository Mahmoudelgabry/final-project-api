using AutoMapper;
using Domain.Contracts;
using Domain.Models;
using Services.Abstractions;
using Shared;
using Shareds.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class GhostCraftService(IUnitOfWork unitOfWork, IMapper mapper) : IGhostCraftService
    {


        public async Task CreateAsync(int userId, CreateGhostCraftDto dto)
        {
            var order = mapper.Map<GhostCraftOrder>(dto);

            order.UserId = userId;

            await unitOfWork.GetRepository<GhostCraftOrder, int>().AddAsync(order);

            await unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<GhostCraftResultDto>> GetAllAsync()
        {
            var repo = unitOfWork.GetRepository<GhostCraftOrder, int>();

            var orders = await repo.GetAllAsync();

            return mapper.Map<IEnumerable<GhostCraftResultDto>>(orders);
        }
        public async Task UpdateStatusAsync(int orderId, string status)
        {
            var repo = unitOfWork.GetRepository<GhostCraftOrder, int>();

            var order = await repo.GetAsync(orderId);

            if (order == null)
                throw new NotFoundException("Order not found");

            if (status != "Approved" && status != "Rejected")
                throw new BadRequestException("Invalid status");

            order.Status = status;

            repo.Update(order);

            await unitOfWork.SaveChangesAsync();
        }
    }
}
