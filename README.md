[![Nuget (with prereleases)](https://img.shields.io/nuget/vpre/Nucleus.API?logo=nuget&label=NuGet)](https://www.nuget.org/packages/Nucleus.API/)
[![Nuget](https://img.shields.io/nuget/dt/Nucleus.API?label=Downloads)](https://www.nuget.org/packages/Nucleus.API/)
[![Discord](https://img.shields.io/discord/349012716100386827?label=Discord&logo=discord&logoColor=ffffff&color=7389D8)](https://discord.gg/U7gKrmw)

# Plugin API

The C# plugin API for [NucleusBot Companion](https://www.nucleus.bot/companion) allows for customization and integration.

# Example Plugins

- Discord Call Overlay **(TBA)**
- BTTV Emotes **(TBA)**

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

If your class has a constructor that accepts an `IPluginContext`, it will be passed in during initialization

```csharp
class MyPlugin : IPlugin {
    private readonly IPluginContext Context;
    
    public MyPlugin(IPluginContext context) {
        this.Context = context;
    }
}
```

### Registration

`IPlugin` contains a `Register` method that allows for asynchronous Plugin initialization after the constructor has been called.

```csharp
class MyPlugin : IPlugin {
    public async ValueTask Register() {
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

The Companion app is capable of supporting custom emotes. Emotes only require a `name` and a `url` **(`url`s must be loaded over HTTPS)**.

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

## Middleware

To intercept messages you'll need to create a MessageHandler middleware in your `Register` method.

```csharp
ValueTask Register() {
    this.Context.MessageHandlerMiddleware(async (context, next) => {
        // Do what you need to do here
        
        // Call the next middleware
        await next();
    });
}
```

### Priority

Your Middleware can be assigned one of three priorities, `FIRST`, `INSERT`, or `LAST` (The default is `INSERT`).

- *After* all priority stages are completed (At the end of `LAST`) is when the Message is populated to the UI. After this point it cannot be modified or cancelled.

```csharp
private async ValueTask MyMethod(IMessageContext context, Func<ValueTask> next)
    => await next();

ValueTask Register() {
    // This will run last
    this.Context.MessageHandlerMiddleware(MyMethod, Ordering.LAST);

    // This will run first
    this.Context.MessageHandlerMiddleware(MyMethod, Ordering.FIRST);

    // This will run in the middle
    this.Context.MessageHandlerMiddleware(MyMethod, Ordering.INSERT);
}
```

| Priority | Description                                                                                                                                                                     |
|----------|---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------|
| `FIRST`  | Will run with highest priority. Anything that modifies the message should run here so that Middleware that reads the final message will do so after these modifications are run |
| `INSERT` | Runs based off `Insert-Order`, plugins that are loaded first will be called first.                                                                                              |
| `LAST`   | Runs after all other processing is completed, if your plugin *reads* messages in their final form, it should be done here                                                       |

## Circuits

Message middleware runs in a circuit. When the first Middleware is run it is passed a delegate `next` which will begin the next middleware.

- If you *do not* invoke the `next` middleware, short-circuiting (see below) will occur.
- Calling `next` multiple times may cause unintended side effects.
  - If the next middleware short-circuits, invoking `next` will fix the short-circuit
  - If short-circuiting has not occurred, invoking `next` will return a `ValueTask.CompletedTask`

Where you invoke `next` may change how your middleware runs.

If you invoke `next` at the start of your method, all other middleware will run (Including populating the message to the UI):

```csharp
this.Context.MessageHandlerMiddleware(async (context, next) => {
    await next();
    
    // Do read-only operations here
});
```

### Short-circuiting

It is possible to "short-circuit" the middleware by simply not calling the `next` delegate. Doing so can prevent the message from populating in the UI, which may be the desired effect.


## Content

A number of indexers and methods exist for updating the contents of a Message.

### Components

Messages consist of `Components` that are implemented by the main Companion application. Some examples of Components are `Text` or `Image`. Since components are only available as interfaces in the API, they must be constructed and accessed through the `IPluginContext` (`IPluginContext` provides an `IComponentsHelper` interface)

```csharp
IPluginContext context = ...;
IComponentsHelper components = context.Components;

// Text is created using a String
ITextComponent text = components.Text("This is text");

// Images require a "text" and an Image URI
IImageComponent image = components.Image("Kappa", new Uri("https://static-cdn.jtvnw.net/emoticons/v2/25/default/dark/2.0"));

// Text and Image are both `IComponent`s
IComponent component = text;
```

### Ranges

Once we have an `IComponent`, sections of messages can easily be replaced using a `Range`.

```csharp
async (context, next) => {
    // This will replace the start of every message with "Neat!"
    context.Contents[..4] = components.Text("Neat!");
};
```