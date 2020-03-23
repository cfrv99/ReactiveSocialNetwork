using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime Date { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext context;

            public Handler(DataContext context)
            {
                this.context = context;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = new Activity
                {
                    Id=request.Id,
                    City=request.City,
                    Venue=request.Venue,
                    Title=request.Title,
                    Date=request.Date,
                    Description=request.Description,
                    Category=request.Category
                };

                context.Activities.Add(activity);
                var success = await context.SaveChangesAsync()>0;
                if(success)
                    return Unit.Value;

                throw new Exception("Problem saving changes");    
            }
        }
    }
}