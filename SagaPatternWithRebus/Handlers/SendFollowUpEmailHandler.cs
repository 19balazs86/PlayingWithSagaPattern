using Rebus.Bus;
using Rebus.Handlers;

namespace SagaPatternWithRebus.Handlers;

public sealed class SendFollowUpEmailHandler : IHandleMessages<FollowUpEmail_Send>
{
    private readonly IEmailService _emailService;
    private readonly IBus _bus;

    public SendFollowUpEmailHandler(IEmailService emailService, IBus bus)
    {
        _emailService = emailService;
        _bus = bus;
    }

    public async Task Handle(FollowUpEmail_Send message)
    {
        await _emailService.SendFollowUpEmailAsync(message.Email);

        await _bus.Send(new FollowUpEmail_Sent(message.Email));
    }
}