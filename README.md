# bmx-slack
BuildMaster extension providing a Slack notifier operation

The operation takes 3 argument:
1. WebhookURL - this is the URL slack provides once you've configured a Webhook.
2. Message - The message to post.  i.e. "Package ${PackageNumber} deployed to ${ApplicationName} ${EnvironmentName} for release ${ReleaseNumber}"
3. Channel - The channel to post message in.

Setup:
In order to post messages you need to set up a Webhook URL in Slack.  Visit https://api.slack.com/incoming-webhooks to set up a webhook for your team.