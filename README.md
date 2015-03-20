# Webhook for .NET

### NuGet intallation

`PM> Install-Package Webhook`


### Configuration

Add into the configuration file the following lines:
 
```
<configSections>
	...
	<section name="webhook" type="Webhook.Helpers.ConfigSection, Webhook" />
	...
</configSections>
...
<webhook enable="true">
	<hooks>
		<add name="notes" url="http://service.com/notes/hook" method="GET" />
		<add name="shows" url="http://service.com/shows/hook" method="POST" />
		...
	</hooks>
</webhook>
```

### Usage

Create an `IHook` instance, passing as *optional* argument an `Action<Exception>` to handle errors within the execution of the `hook` (most commonly to log the error).

```
IHook hook = new Hook(onError: ex => log.Error(ex.Message));
```

Call the `Notify[Async]` method of the `IHook` interface. It expects the following parameters:

* `key`: `string`. The key to access the hook configuration
* `queryString`: `object`. *Optional*
* `body`: `object`. *Optional*

```
hook.NotifyAsync("notes-es", queryString: new { ids = new string[] { "qwe", "rty" } });
```

This will make an `asynchronous` call to the specified `url`.

`IHook` supports both `synchronous` and `asynchronous` calls.
