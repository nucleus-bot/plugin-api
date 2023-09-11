![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Nucleus.API?logo=nuget&label=NuGet) ![Nuget](https://img.shields.io/nuget/dt/Nucleus.API?label=Downloads) ![Discord](https://img.shields.io/discord/349012716100386827?label=Discord&logo=discord&logoColor=ffffff&color=7389D8&link=https%3A%2F%2Fdiscord.gg%2FU7gKrmw)

# Plugin API

The C# plugin API for [NucleusBot Companion](https://www.nucleus.bot/companion) allows for customization and integration.

# Example Plugins

- Discord Call Overlay
- BTTV Emotes

# Getting Started

## Registration

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

## Main Class

You can start your plugin by creating a `class` that implements the `IPlugin` interface. `Type`s that meet the following rules will automatically be called when your plugin DLL is loaded:

- Has to implement `IPlugin`
- Cannot be static
- Cannot be an interface
- Cannot be abstract

If you create a constructor that accepts an `IPluginContext`, it will be passed to your Plugins constructor

```csharp
class MyPlugin : IPlugin {
    private readonly IPluginContext Context;
    
    public MyPlugin(IPluginContext context) {
        this.Context = context;
    }
}
```

### Async Registration

`IPlugin` contains a `Register` method that allows for asynchronous Plugin initialization after the constructor has been called.

```csharp
class MyPlugin : IPlugin {
    protected async ValueTask Register() {
        // ... Write your async code here
    }
}
```

### Disposing

When your Plugin is unloaded or disabled by the user, you can clean up any resources being used by your plugin by implementing `IDisposable` or `IAsyncDisposable`. Any Disposable objects returned by the `IPluginContext` will be disposed of automatically ***after*** the Plugin class is disposed.

## Web Plugins

The Companion app provides a local HTTP server to allow providing embeddable HTML/JavaScript pages (eg; for adding as OBS Browser Sources) using Blazor server/Razor pages.

To make sure that your Plugin has access to the tools necessary for creating razor pages, make sure that you change your `Console Application`s `.csproj` to use the Web SDK:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
    <!-- Your project details -->
</Project>
```

# Commands

One core functionality present in many chat bots is **Commands**. The Companion app supports Auto-Complete for command in the Input box.

## Slash commands

The Companion app supports handling chat commands as slash commands as long a handler has been created to receive the commands to a web API

```csharp
// TODO: Write something here
throw new NotImplementedException();
```

# Emotes

The Companion app is capable of supporting custom emotes. Emotes only require a `name` and a `url`. `url`s must be loaded over HTTPS.

```csharp
ValueTask Register() {
    Uri uri = new Uri("https://test.dev/emotes");
    
    // Register Emotes with the NucleusBot format
    this.Context.RegisterEmotes(uri);
    
    // Register Emotes with the NucleusBot format and add an Authorization header
    // (If your API requires authorization to access files)
    this.Context.RegisterEmotes(uri, headers => {
        headers.Add("Authorization", "Bearer xxxxxxxxxxx");
    });
    
    // Register Emotes with a custom format
    this.Context.RegisterEmotes(uri, converter: (content) => {
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
    this.Context.RegisterEmotes(uri,
        headers => {
            // ... Add headers
        },
        content => {
            // ... Parse the response body
        }
    );
    
    return ValueTask.CompletedTask;
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
ValueTask Register() {
    // Register your global emotes here
    this.Context.RegisterEmotes(new Uri("https://test.dev/emotes"));
}
```

### Channel Example:

Registering per channel is done after Joining to a channel. Emotes are stored on the channel for as long as it is Joined, and are automatically discarded when leaving the channel. Per-channel `Emotes` and `Commands` are only shown as long as that Channel is currently selected.

```csharp
ValueTask Register() {
    // Listen for Channel joins
    this.Context.ChannelJoinEvent(channel => {
        // Register channel emotes with the channel
        channel.RegisterCommands(new Uri($"https://test.dev/emotes/{channel.Id}"));
    });
}
```

# Messages

Plugins are capable of updating, modifying, or completely cancelling chat messages. For example, if your plugin introduced new emotes, you'll need to replace the text contents with an `Image`
