using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace authAPI
{
    public class RefreshTokenProvider : AuthenticationTokenProvider
    {
        private static ConcurrentDictionary<string, AuthenticationTicket> _refreshTokens = new ConcurrentDictionary<string, AuthenticationTicket>();

        public override Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            context.Ticket.Properties.AllowRefresh = true;

            var guid = Guid.NewGuid().ToString();
            context.Ticket.Properties.ExpiresUtc= DateTimeOffset.Now.AddHours(8);

            _refreshTokens.TryAdd(guid, context.Ticket);
            
            context.SetToken(guid);
            return Task.FromResult<object>(null);
        }

        public override Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            if (_refreshTokens.TryRemove(context.Token, out AuthenticationTicket ticket))
            {
                context.SetTicket(ticket);
            }
            return Task.FromResult<object>(null);
        }

    }
}