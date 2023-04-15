namespace SagaPatternWithRebus;

public record Initiate_SubscribeToNewsletter(string Email);

public record WelcomeEmail_Send(string Email);

public record WelcomeEmail_Sent(string Email);

public record FollowUpEmail_Send(string Email);

public record FollowUpEmail_Sent(string Email);