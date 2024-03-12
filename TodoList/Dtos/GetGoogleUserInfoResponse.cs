namespace TodoList.Dtos;

public sealed record GetGoogleUserInfoResponse
{
    public string? Sub { get; init; }
    public string? Name { get; init; }
    public string? GivenName { get; init; }
    public string? FamilyName { get; init; }
    public string? Picture { get; init; }
    public string? Email { get; init; }
    public string? EmailVerified { get; init; }
    public string? Locale { get; init; }
    public string? Hd { get; init; }
}