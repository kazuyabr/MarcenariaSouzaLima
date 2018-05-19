using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Luis.Models;

namespace Marcenaria.Bot.Dialogs
{
    [Serializable]
    public class RootDialog : LuisDialog<object>
    {
        public RootDialog(ILuisService service) : base(service) { }

        [LuisIntent("None")]
        public async Task NoneAsync(IDialogContext context, LuisResult luisResult)
        {
            await context.PostAsync("Desculpe, eu não entendi...\n");
            context.Done<string>(null);
        }

        [LuisIntent("About")]
        public async Task AboutAsync(IDialogContext context, LuisResult luisResult)
        {
            //await context.PostAsync(AboutConstant.About);
            context.Done<string>(null);
        }

        [LuisIntent("Projects")]
        public async Task ProjectsAsync(IDialogContext context, LuisResult luisResult)
        {
            //var Carousel = new CreateCardService().CreateCarrouselImages();
            //var Message = context.MakeMessage();

            //Message.AttachmentLayout = AttachmentLayoutTypes.Carousel;
            //Message.Attachments.Add(Carousel);

            await context.PostAsync("");
            context.Done<string>(null);
        }

        [LuisIntent("ProjectDetail")]
        public async Task ProjectDetailAsync(IDialogContext context, LuisResult luisResult)
        {
            var Projects = luisResult.Entities?.Select(j => j.Entity);
            await context.PostAsync($"Aguarde um momento enquanto eu obtenho os projetos!");


            context.Done<string>(null);
        }
    }
}