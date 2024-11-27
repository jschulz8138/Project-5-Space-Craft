using LinkServer.Filters;
using Uplink_Downlink;
using Payload_Ops;

var builder = WebApplication.CreateBuilder(args);
var spaceship = new Spaceship();

builder.Services.AddSingleton<Spaceship>(spaceship);
builder.Services.AddSingleton<AppLogger>(new ServerLogger("server_logs.txt"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddScoped<AuthenticateFilter>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseSession();
app.MapControllers();

app.Run("https://localhost:5001");