using AutoMapper;
using MassTransit;
using OrderMe.Cart.BusinessLogic.Cart.Dtos;
using OrderMe.Cart.BusinessLogic.Cart.Services;
using OrderMe.Integration.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderMe.Cart.BusinessLogic.Cart.Consumers
{
    public class ItemConsumer : IConsumer<ItemMessageRequestDto>
    {
        private readonly IMapper _mapper;
        private readonly ICartService _cartService;
        public ItemConsumer(IMapper mapper, ICartService cartService)
        {
            _mapper = mapper;
            _cartService = cartService;
        }

        public async Task Consume(ConsumeContext<ItemMessageRequestDto> itemRequest)
        {
            var updatedCartItemDto = _mapper.Map<CartItemDto>(itemRequest.Message);
            await _cartService.UpdateItemInAllCarts(updatedCartItemDto);
        }
    }
}
