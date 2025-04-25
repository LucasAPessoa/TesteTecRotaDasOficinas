using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace RO.DevTest.Application.Features.Products.Commands.DeleteProductCommand
{
    public class DeleteProductCommand : IRequest<DeleteProductResult>
    {
        public Guid Id { get; set; }

        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }
    }
}
