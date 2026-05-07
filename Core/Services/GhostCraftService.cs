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


        public async Task<GhostCraftResultDto> CreateAsync(int userId, CreateGhostCraftDto dto)
        {
            var order = mapper.Map<GhostCraftOrder>(dto);

            order.UserId = userId;

            await unitOfWork.GetRepository<GhostCraftOrder, int>().AddAsync(order);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<GhostCraftResultDto>(order);
        }

        public async Task<IEnumerable<GhostCraftResultDto>> GetAllAsync()
        {
            var repo = unitOfWork.GetRepository<GhostCraftOrder, int>();

            var orders = await repo.GetAllAsync();

            return mapper.Map<IEnumerable<GhostCraftResultDto>>(orders);
        }
       
        public async Task<GhostCraftResultDto> UpdateAsync(int id ,UpdateGhostCraftDto dto)
        {
            var repo = unitOfWork.GetRepository<GhostCraftOrder, int>();

            var order = await repo.GetAsync(id);

            if (order == null)
                throw new NotFoundException("GhostCraft order not found");

            mapper.Map(dto, order);

            repo.Update(order);

            await unitOfWork.SaveChangesAsync();

            return mapper.Map<GhostCraftResultDto>(order);
        }

    }
}
