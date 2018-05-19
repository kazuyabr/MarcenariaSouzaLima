using System;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Net.Http;
using System.Configuration;
using Marcenaria.Bot.Dialogs;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;

namespace Marcenaria.Bot
{
    [BotAuthentication]
    public class MessagesController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]Activity activity)
        {
            var Connector = new ConnectorClient(new Uri(activity.ServiceUrl));
            var Attributes = new LuisModelAttribute(ConfigurationManager.AppSettings["LuisId"], ConfigurationManager.AppSettings["LuisSubscriptionKey"]);
            var Service = new LuisService(Attributes);

            switch (activity.Type)
            {
                case ActivityTypes.Message:
                    await Conversation.SendAsync(activity, () => new RootDialog(Service));
                    break;
                case ActivityTypes.ConversationUpdate:
                    if (activity.MembersAdded.Any(o => o.Id == activity.Recipient.Id))
                    {
                        var reply = activity.CreateReply();
                        reply.Text = Welcome();

                        await Connector.Conversations.ReplyToActivityAsync(reply);
                    }
                    break;
            }

            return Request.CreateResponse(HttpStatusCode.OK);
        }

        private string Welcome()
        {
            var Now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time")).TimeOfDay;
            string Welcome;

            if (Now < TimeSpan.FromHours(12))
                Welcome = "Bom dia";
            else if (Now < TimeSpan.FromHours(18))
                Welcome = "Boa tarde";
            else
                Welcome = "Boa noite";

            return $"{Welcome}! Eu sou o Botinho Souza Lima, Em que posso ajudar?";
        }
    }
}