# Intro

Minimal example of using Serilog in a .NET 6 Minimal API.

# Good to know

1. Log.Logger has to be set for Log.ForContext<> to work.
2. Only the Serilog.AspNetCore nuget package is needed. The Serilog / Serilog.Sinks.Console packages (among others) are
   implicitly
   added).
3. Log.ForContext<Program> does not work when overriding the minimum log level in combinations with using top-level
   statements:

```
"MinimumLevel": {
    "Default": "Information",
    "Override": {
        "SerilogWebapi": "Debug"
    }
}
```

The override works based on namespaces and when you check the typeof(Program).namespace at runtime you will see it's
null.

So if you want to use Log.ForContext<Program> you have to explicitly define a Program class with a Main method as is
done in this example.
