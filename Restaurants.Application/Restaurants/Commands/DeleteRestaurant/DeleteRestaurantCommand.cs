﻿using MediatR;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    public class DeleteRestaurantCommand : IRequest<bool>
    {
        public DeleteRestaurantCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
