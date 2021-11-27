using Discord;
using Discord.Interactions;
using Discord.Rest;
using Microsoft.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var discord = new Discord.Rest.DiscordRestClient();

await discord.LoginAsync(Discord.TokenType.Bot, builder.Configuration["token"]);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddRouting();
builder.Services.AddSingleton(discord);
builder.Services.AddInteractionService(config => config.UseCompiledLambda = true);

var app = builder.Build();

var commands = app.Services.GetRequiredService<InteractionService>();

await commands.AddModulesAsync(Assembly.GetExecutingAssembly(), app.Services);
await commands.RegisterCommandsToGuildAsync(app.Configuration.GetValue<ulong>("test_guild"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseInteractionService("/interactions", builder.Configuration["pbk"]);

app.Run();