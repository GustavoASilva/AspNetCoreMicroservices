using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrders
{
    public class GetOrdersQuery : IRequest<List<GetOrdersDto>>
    {
        public string UserName { get; set; }

        public GetOrdersQuery(string userName)
        {
            UserName = userName;
        }

    }
}
