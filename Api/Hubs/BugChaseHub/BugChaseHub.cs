using System.Collections.Concurrent;
using Domain.Entities.BugChaseEntities;
using Microsoft.AspNetCore.SignalR;

namespace Api.Hubs.BugChaseHub;

// BugChaseHub.cs (SignalR)
public class BugChaseHub : Hub
{
    private static readonly ConcurrentDictionary<string, BugChasePlayer> ConnectedPlayers = new();

    public override Task OnConnectedAsync()
    {
        var player = new BugChasePlayer
        {
            ConnectionId = Context.ConnectionId,
            Username = Context.GetHttpContext()?.Request.Query["username"].ToString() ?? Context.ConnectionId
        };
        ConnectedPlayers[player.ConnectionId] = player;
        Clients.All.SendAsync("PlayerJoined", player.Username);
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        if (ConnectedPlayers.TryRemove(Context.ConnectionId, out var player))
        {
            Clients.All.SendAsync("PlayerLeft", player.Username);
        }
        return base.OnDisconnectedAsync(exception);
    }

    public async Task SendScore(string username, int score)
    {
        if (ConnectedPlayers.TryGetValue(Context.ConnectionId, out var player))
        {
            player.Score = score;
            await Clients.All.SendAsync("ScoreUpdated", username, score);
        }
    }

    public async Task GetLeaderboard()
    {
        var leaderboard = ConnectedPlayers.Values
            .OrderByDescending(p => p.Score)
            .Take(10)
            .Select(p => new LeaderboardEntry { Username = p.Username, Score = p.Score })
            .ToList();

        await Clients.Caller.SendAsync("ReceiveLeaderboard", leaderboard);
    }
}
