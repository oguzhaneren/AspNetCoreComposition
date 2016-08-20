# ASP.Net Core Composition Example 

```csharp
app.CreateBranch(new PathString("/context1"), env, loggerFactory, _services, new Context1.Startup(Configuration));
app.CreateBranch(new PathString("/context2"), env, loggerFactory, _services, new Context2.Startup(Configuration));
```
