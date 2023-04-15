using Rebus.Bus;
using Rebus.Handlers;
using Rebus.Sagas;

namespace SagaPatternWithRebus.Sagas;

public sealed class NewsletterOnboardingSaga :
    Saga<NewsletterOnboardingSagaData>,
    IAmInitiatedBy<Initiate_SubscribeToNewsletter>,
    IHandleMessages<WelcomeEmail_Sent>,
    IHandleMessages<FollowUpEmail_Sent>
{
    private readonly IBus _bus;

    public NewsletterOnboardingSaga(IBus bus)
    {
        _bus = bus;
    }

    protected override void CorrelateMessages(ICorrelationConfig<NewsletterOnboardingSagaData> config)
    {
        config.Correlate<Initiate_SubscribeToNewsletter>(m => m.Email, sagaData => sagaData.Email);
        config.Correlate<WelcomeEmail_Sent>(             m => m.Email, sagaData => sagaData.Email);
        config.Correlate<FollowUpEmail_Sent>(            m => m.Email, sagaData => sagaData.Email);
    }

    public async Task Handle(Initiate_SubscribeToNewsletter message)
    {
        if (!IsNew)
        {
            return;
        }

        await _bus.Send(new WelcomeEmail_Send(message.Email));
    }

    public async Task Handle(WelcomeEmail_Sent message)
    {
        Data.WelcomeEmailSent = true;

        await _bus.Defer(TimeSpan.FromSeconds(30), new FollowUpEmail_Send(message.Email));
    }

    public Task Handle(FollowUpEmail_Sent message)
    {
        Data.FollowUpEmailSent = true;

        MarkAsComplete();

        return Task.CompletedTask;
    }
}
