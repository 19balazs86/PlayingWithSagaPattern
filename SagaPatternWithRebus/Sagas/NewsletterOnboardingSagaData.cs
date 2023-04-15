using Rebus.Sagas;

namespace SagaPatternWithRebus.Sagas;

public sealed class NewsletterOnboardingSagaData : ISagaData
{
    public Guid Id { get; set; }
    public int Revision { get; set; }

    public string Email { get; set; } = string.Empty;

    public bool WelcomeEmailSent { get; set; }

    public bool FollowUpEmailSent { get; set; }
}