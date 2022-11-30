using MediatR;

namespace BK.Application.Commands.BookEntries
{
    public class DeleteBookEntryCommand : IRequest<bool>
    {
        public int BookEntryId { get; set; }
    }
}
