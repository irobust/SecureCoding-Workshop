# Enable CORS
### Allow Specific Origins
```
public void ConfigureServices(IServiceCollection services){
...
services.AddCors(options => options.AddPolicy("AllowSpecificOrigin",
                builder => builder.WithOrigins("http://localhost:8080")
            ));
...
}
```
```
public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
    ....
    app.useCors("AllowSpecificOrigin");
    ....
}
```


### Retrieved Allow Origin from settings
#### Move AllowedOrigin to AppSettings
```
{
  "Logging": {
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  },
  "AllowOrigin": "http://localhost:5000"
}
```

#### Retrieved allowOrigin
```
var allowedOrigin = Configuration.GetValue<string>("AllowOrigin") ?? "";
services.AddCors(options => options.AddPolicy("AllowAnyOrigin",
    builder => builder.WithOrigins(allowedOrigin)
));
services.AddControllers();
```

### Set preflight max age
```
options.AddPolicy("AllowAnyOrigin", builder => builder.WithOrigins(allowedOrigin).SetPreflightMaxAge(TimeSpan.FromMinutes(10)));
```

### Set allow with method and allow with header
```
options.AddPolicy("PublicApi", builder => builder.AllowAnyOrigin().WithMethods("Get").WithHeaders("Content-Type"));
```

### Add CORS to public and private api
1. Add policy for public and private api
```
services.AddCors(options => {
                    options.AddPolicy("Private", builder => builder.WithOrigins(allowedOrigin).SetPreflightMaxAge(TimeSpan.FromMinutes(10)));
                    options.AddPolicy("Public", builder => builder.AllowAnyOrigin());
                }
);
```

2. Apply default policy
```
app.UseRouting();
app.UseCors("AllowAnyOrigin");
```

3. Add EnableCors annotation to controller or action
```
[HttpGet]
[EnableCors("PublicApi")]
public IEnumerable<ProductModel> Get()
{
    ...
}
```


### Allow wildcard sub domain
```
options.AddPolicy("AllowSubDomains", builder => builder.WithOrigins("https://*.example.com").SetIsOriginAllowedToAllowWildcardSubdomains());
```

### Allow multiple sub domain
1. create a custom function for validate domain name
```
public static bool checkWhitelistingDomain(string host)
{
    var corsOriginAllowed = new[] { "globalmantics", "example" };
    return corsOriginAllowed.Any(origin => host.Contains(origin));
}
```

2. add method name to SetIsOriginAllowed
```
options.AddPolicy("AllowMultipleDomains", builder => builder.SetIsOriginAllowed(checkWhitelistingDomain));
```
