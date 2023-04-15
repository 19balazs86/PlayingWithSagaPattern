using Rebus.Bus;
using Rebus.Handlers;

namespace SagaPatternWithRebus.Handlers;

public sealed class SendWelcomeEmailHandler : IHandleMessages<WelcomeEmail_Send>
{
    private readonly IEmailService _emailService;
    private readonly IBus _bus;

    public SendWelcomeEmailHandler(IEmailService emailService, IBus bus)
    {
        _emailService = emailService;
        _bus = bus;
    }

    public async Task Handle(WelcomeEmail_Send message)
    {
        await _emailService.SendWelcomeEmailAsync(message.Email);

        await _bus.Send(new WelcomeEmail_Sent(message.Email));
    }
}