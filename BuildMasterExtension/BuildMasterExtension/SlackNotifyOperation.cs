using Inedo.BuildMaster.Extensibility.Operations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Inedo.Documentation;
using Inedo.BuildMaster.Extensibility;
using System.ComponentModel;
using System.IO;
using System.Net.Http;

namespace BuildMasterExtension
{
    [DisplayName("Notify via Slack")]
    [Description("Post a notification to Slack. See https://my.slack.com/services/new/incoming-webhook for setup.")]
    [ScriptAlias("Slack-Notify")]
    [ScriptNamespace("HDARS")]
    [Tag("general")]
    public class SlackNotifyOperation : ExecuteOperation
    {
        [Required]
        [ScriptAlias("WebhookURL")]
        [DisplayName("Webhook URL which was set up in Slack.")]
        public string WebhookURL { get; set; }

        [Required]
        [ScriptAlias("Message")]
        [DisplayName("Message to post to Slack")]
        public string Message { get; set; }

        [Required]
        [ScriptAlias("Channel")]
        [DisplayName("Channel to post message to")]
        public string Channel { get; set; }

        public override async Task ExecuteAsync(IOperationExecutionContext context)
        {
            //this.LogDebug($"Sending notification to Slack,  {context.ReleaseNumber}...");

            var client = new HttpClient();

            var requestContent = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("payload", "{\"channel\": \""+ Channel+"\", \"text\": \"" + Message+ "\"}"),
            });

            HttpResponseMessage response = await client.PostAsync(WebhookURL, requestContent);

            HttpContent responseContent = response.Content;

            using (var reader = new StreamReader(await responseContent.ReadAsStreamAsync()))
            {
                string responseMsg = await reader.ReadToEndAsync();
                
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    throw new Exception($"Failed to post message.  Status code {response.StatusCode}, message: {responseMsg}");
                }
            }
        }

        protected override ExtendedRichDescription GetDescription(IOperationConfiguration config)
        {
            return new ExtendedRichDescription(
                    new RichDescription(
                        "Notify operation will to the Channel ",
                         new Hilite(config[nameof(this.Channel)])
                         )
                    );
        }
    }
}
