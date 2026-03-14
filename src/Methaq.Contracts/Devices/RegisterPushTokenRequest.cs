namespace Methaq.Contracts.Devices;

public record RegisterPushTokenRequest(
    string Token,
    string Platform);