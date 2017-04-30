using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using PullReqDashboard.API.Models.Response;
using System.Collections.Generic;
using Microsoft.AspNetCore.Cors;

namespace PullReqDashboard.API.Utilities
{
    public class SignalRHub : Hub
    {

        //public override Task OnConnected()
        //{
            
        //    if (Clients != null)
        //    {
        //        var count = Clients.All().Count();
        //    }
        //    return Task.CompletedTask;
        //}

        //public override Task OnDisconnected(bool stopCalled)
        //{
        //    return Task.CompletedTask;
        //}

        //public async Task Send(IEnumerable<GetPullRequest> pullRequests)
        //{
        //    await Clients.All.InvokeAsync("Send", pullRequests);
        //}
    }
}