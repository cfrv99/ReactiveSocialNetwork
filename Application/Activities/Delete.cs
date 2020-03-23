using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
 
    public class Delete
    {
        public class Command:IRequest
        {
            public Guid Id { get; set; }
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
                
                var activity = await context.Activities.FindAsync(request.Id);
                if(activity==null)
                {
                    throw new Exception("Could not found activity");
                }
                context.Activities.Remove(activity);
                var success = await context.SaveChangesAsync()>0;
                if(success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }
        }
    }
}