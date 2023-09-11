# Plugin API

The C# plugin API for [NucleusBot Companion](https://www.nucleus.bot/interface) allows for customization and integration.

# Example Plugins

- Discord Call Overlay
- BTTV Emotes

# Getting Started

## Main Class

You can start your plugin by creating a `class` that implements the `IPlugin` interface, or the abstract `PluginBase` class.

- `IPlugin` contains a `RegisterAsync` method that allows for asynchronous Plugin initialization
- `PluginBase` provides a `Register` method that wraps `RegisterAsync` in a Task for you if you do not need to Initialize asynchronously

```csharp
public class MyPlugin : PluginBase {
    protected override void Register(IPluginContext context) {
        // ... Write your code here
    }
}
```

## Web Plugins

The Companion app provides a local HTTP server to allow providing embeddable HTML/JavaScript pages (eg; for adding as OBS Browser Sources) using Blazor server/Razor pages.

To make sure that your Plugin has access to the tools necessary for creating razor pages, make sure that you change your `Console Application`s `.csproj` to use the Web SDK.

```xml
<!-- Before -->
<Project Sdk="Microsoft.NET.Sdk">
</Project>

<!-- After -->
<Project Sdk="Microsoft.NET.Sdk.Web">
</Project>
```

# Commands

One core functionality present in many chat bots is **Commands**. The Companion app supports Auto-Complete for command in the Input box.

## Slash commands

The Companion app supports converting chat commands as slash commands as long as a two conditions have been met:

- A handler has been created to receive the commands to a web API
- The Plugin has permission to make HTTP Requests

```csharp
// TODO: Write something here
throw new NotImplementedException();
```

# Emotes

The Companion app is capable of supporting custom emotes. Emotes only require a `name` and a `url`. `url`s must be loaded over HTTPS.

```csharp
void Register(IPluginContext context) {
    Uri uri = new Uri("https://test.dev/emotes");
    
    // Register Emotes with the NucleusBot format
    context.RegisterEmotes(uri);
    
    // Register Emotes with the NucleusBot format and add an Authorization header
    // (If your API requires authorization to access files)
    context.RegisterEmotes(uri, headers => {
        headers.Add("Authorization", "Bearer xxxxxxxxxxx");
    });
    
    // Register Emotes with a custom format
    context.RegisterEmotes(uri, converter: (content) => {
        IList<Emote> emotes = new List<Emote>();
        
        // If your 'content' is a JSONArray
        if (content.TryParseJSON(out JsonArray? json)) {
            // ... Parse your Emotes here
            string name = "...";
            string name = new Uri("https://...");
            emotes.add(new Emote(name, uri));
        }
        
        return emotes;
    });
    
    // ... Or a mix of both
    context.RegisterEmotes(uri,
        headers => {
            // ... Add headers
        },
        content => {
            // ... Parse the response body
        }
    );
}
```

# Scopes

`Emotes` and `Commands` can be registered in two different scopes:

- Globally
- Per Channel

Registration can be done on the `IScopedRegistryContext` interface, which is implemented by both `IPluginContext` (For Global) and `IChannelContext` (For Channels).

### Global Example:

Registering globally is done when Registering your plugin, or can be done later if the `IPluginContext` is stored.

```csharp
void Register(IPluginContext context) {
    // Register your global emotes here
    context.RegisterEmotes(new Uri("https://test.dev/emotes"));
}
```

### Channel Example:

Registering per channel is done after Joining to a channel. Emotes are stored on the channel for as long as it is Joined, and are automatically discarded when leaving the channel. Per-channel `Emotes` and `Commands` are only shown as long as that Channel is currently selected.

```csharp
void Register(IPluginContext context) {
    // Listen for Channel joins
    context.ChannelJoinEvent(channel => {
        // Register channel emotes with the channel
        channel.RegisterCommands(new Uri($"https://test.dev/emotes/{channel.Id}"));
    });
}
```

# Messages

Plugins are capable of updating, modifying, or completely cancelling chat messages. For example, if your plugin introduced new emotes, you'll need to replace the text contents with an `Image`

# Plugin Registration

In order for your DLL to be loaded you need to create a `plugin.json` file in your plugins folder.

Below is the minimum required configuration

```json
{
    "schema": 1,
    
    "id": "example-plugin",
    "version": "1.0.0",
    "name": "Example Plugin",
    
    "entrypoints": [
        "MyAssembly.dll"
    ]
}
```

The `plugin.json` file can contain the following types:

| Property       | Type              | Description                                           |
|----------------|-------------------|-------------------------------------------------------|
| `schema`       | Number            | Schema Version (Currently only supports `1`)          |
| `id`           | String            | Unique identifier for your Plugin                     |
| `version`      | SemVer            | Version of your plugin to determine updates           |
| `name`         | String            | Name of your plugin that shows on the plugins page    |
| `description`  | String            | Description of what your plugin does                  |
| `icon`         | String            | A local path to the icon used for your plugin         |
| `entrypoints`  | String / String[] | DLLs that should be loaded from your plugin folder    |
| `contributors` | String / String[] | Contributors credited for the plugin                  |
| `contact`      | Object            | Contact details for support with your plugin          |
| `license`      | String            | The license for your plugin (eg; `MIT`/`GPL`/`other`) |